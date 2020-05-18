using BindOpen.Data.Items;
using BindOpen.Extensions.Definition;
using BindOpen.System.Diagnostics;
using System;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace BindOpen.Extensions.Runtime
{
    /// <summary>
    /// This class represents a extension scope loader.
    /// </summary>
    internal partial class BdoExtensionStoreLoader : DataItem, IBdoExtensionStoreLoader
    {
        /// <summary>
        /// Loads the extension dictionary of the specified kind from the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly to consider.</param>
        /// <param name="kind">The kind of item to consider.</param>
        /// <param name="extensionDefinition">The extension definition to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns></returns>
        private int LoadDictionary(
            Assembly assembly,
            BdoExtensionItemKind kind,
            IBdoExtensionDefinition extensionDefinition = null,
            IBdoLog log = null)
        {
            if (assembly == null)
            {
                return -1;
            }

            switch (kind)
            {
                case BdoExtensionItemKind.Carrier:
                    return LoadCarrierDictionaryFromAssembly(assembly, extensionDefinition, log);
                case BdoExtensionItemKind.Connector:
                    return LoadConnectorDictionaryFromAssembly(assembly, extensionDefinition, log);
                case BdoExtensionItemKind.Entity:
                    return LoadEntityDictionaryFromAssembly(assembly, extensionDefinition, log);
                case BdoExtensionItemKind.Format:
                    return LoadFormatDictionaryFromAssembly(assembly, extensionDefinition, log);
                case BdoExtensionItemKind.Handler:
                    return LoadHandlerDictionaryFromAssembly(assembly, extensionDefinition, log);
                case BdoExtensionItemKind.Metrics:
                    return LoadMetricsDictionaryFromAssembly(assembly, extensionDefinition, log);
                case BdoExtensionItemKind.Routine:
                    return LoadRoutineDictionaryFromAssembly(assembly, extensionDefinition, log);
                case BdoExtensionItemKind.Scriptword:
                    return LoadScripwordDictionaryFromAssembly(assembly, extensionDefinition, log);
                case BdoExtensionItemKind.Task:
                    return LoadTaskDictionaryFromAssembly(assembly, extensionDefinition, log);
                case BdoExtensionItemKind.Any:
                case BdoExtensionItemKind.None:
                    break;
            }
            return -1;
        }

        /// <summary>
        /// Loads the specified BindOpen extension dictionary.
        /// </summary>
        /// <param name="assembly">The assembly to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The created library.</returns>
        private ITBdoExtensionDictionaryDto<T> ExtractDictionaryFromAssembly<T>(
            Assembly assembly,
            IBdoLog log = null) where T : BdoExtensionItemDefinitionDto
        {
            TBdoExtensionDictionaryDto<T> dictionary = default;

            if (assembly != null)
            {
                string resourceFileName = GetDictionaryResourceName<T>();

                string resourceFullName = Array.Find(
                    assembly.GetManifestResourceNames(), p => p.EndsWith(resourceFileName, StringComparison.OrdinalIgnoreCase));

                Stream stream = null;
                if (resourceFullName == null)
                {
                    log?.AddWarning("No dictionary named '" + resourceFileName + "' found in assembly");
                }
                else
                {
                    try
                    {
                        stream = assembly.GetManifestResourceStream(resourceFullName);
                        if (stream == null)
                        {
                            log?.AddError("Could not open the item dictionary named '" + resourceFullName + "' in assembly");
                        }
                        else
                        {
                            Type type = GetDictionaryType<T>();
                            XmlSerializer serializer = new XmlSerializer(type);
                            dictionary = (TBdoExtensionDictionaryDto<T>)serializer.Deserialize(stream);
                        }
                    }
                    catch (Exception ex)
                    {
                        log?.AddException(ex);
                    }
                    finally
                    {
                        stream?.Close();
                    }
                }
            }

            return dictionary;
        }

        /// <summary>
        /// Gets the dictionary resource name.
        /// </summary>
        /// <returns>Returns the class of the specified dictionary.</returns>
        private string GetDictionaryResourceName<T>() where T : IBdoExtensionItemDefinitionDto
        {
            BdoExtensionItemKind itemKind = typeof(T).GetExtensionItemKind();

            return itemKind switch
            {
                BdoExtensionItemKind.Carrier => "BindOpen.Carriers",
                BdoExtensionItemKind.Connector => "BindOpen.Connectors",
                BdoExtensionItemKind.Entity => "BindOpen.Entities",
                BdoExtensionItemKind.Format => "BindOpen.Formats",
                BdoExtensionItemKind.Handler => "BindOpen.Handlers",
                BdoExtensionItemKind.Metrics => "BindOpen.Metrics",
                BdoExtensionItemKind.Routine => "BindOpen.Routines",
                BdoExtensionItemKind.Scriptword => "BindOpen.Scriptwords",
                BdoExtensionItemKind.Task => "BindOpen.Tasks",
                _ => null,
            };
        }

        /// <summary>
        /// Gets the item definition file name of the TO specified extension item definition class.
        /// </summary>
        /// <returns>Returns the class of the specified dictionary.</returns>
        private Type GetDictionaryType<T>() where T : IBdoExtensionItemDefinitionDto
        {
            BdoExtensionItemKind itemKind = typeof(T).GetExtensionItemKind();

            return itemKind switch
            {
                BdoExtensionItemKind.Carrier => typeof(BdoCarrierDictionaryDto),
                BdoExtensionItemKind.Connector => typeof(BdoConnectorDictionaryDto),
                BdoExtensionItemKind.Format => null,
                BdoExtensionItemKind.Entity => typeof(BdoEntityDictionaryDto),
                BdoExtensionItemKind.Handler => typeof(BdoHandlerDictionaryDto),
                BdoExtensionItemKind.Metrics => typeof(BdoMetricsDictionaryDto),
                BdoExtensionItemKind.Routine => typeof(BdoRoutineDictionaryDto),
                BdoExtensionItemKind.Scriptword => typeof(BdoScriptwordDictionaryDto),
                BdoExtensionItemKind.Task => typeof(BdoTaskDictionaryDto),
                _ => null,
            };
        }

        /// <summary>
        /// Updates this instance with the specified attribute information.
        /// </summary>
        /// <param name="definition">The definition to consider.</param>
        /// <param name="attribute">The attribute to consider.</param>
        public void UpdateDictionary(
            IBdoExtensionItemDefinitionDto definition,
            DescribedDataItemAttribute attribute)
        {
            definition.Name = attribute.Name?.IndexOf("$") > 0 ?
                attribute.Name.Substring(attribute.Name.IndexOf("$") + 1) : attribute.Name;

            definition.Title = ItemFactory.CreateDictionary(attribute.Title);
            definition.Description = ItemFactory.CreateDictionary(attribute.Description);
            definition.CreationDate = attribute.CreationDate;
            definition.LastModificationDate = attribute.LastModificationDate;
            definition.Index = attribute.Index;
        }
    }
}