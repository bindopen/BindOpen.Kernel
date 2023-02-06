using BindOpen.Data;
using BindOpen.Data.Items;
using BindOpen.Extensions;
using BindOpen.Logging;
using BindOpen.Runtime.Definition;
using System;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace BindOpen.Runtime.Stores
{
    /// <summary>
    /// This class represents a extension scope loader.
    /// </summary>
    internal partial class BdoExtensionStoreLoader :
        BdoItem, IBdoExtensionStoreLoader
    {
        /// <summary>
        /// Loads the extension dico of the specified kind from the specified assembly.
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
        /// Loads the specified BindOpen extension dico.
        /// </summary>
        /// <param name="assembly">The assembly to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The created library.</returns>
        private static ITBdoExtensionDictionary<T> ExtractDictionaryFromAssembly<T>(
            Assembly assembly,
            IBdoLog log = null) where T : IBdoExtensionItemDefinition
        {
            ITBdoExtensionDictionary<T> dico = default;

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
                            XmlSerializer serializer = new(type);
                            dico = (TBdoExtensionDictionary<T>)serializer.Deserialize(stream);
                            //dico.UpdateRuntimeInfo(log: log);
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

            return dico;
        }

        /// <summary>
        /// Gets the dico resource name.
        /// </summary>
        /// <returns>Returns the class of the specified dico.</returns>
        private static string GetDictionaryResourceName<T>() where T : IBdoExtensionItemDefinition
        {
            BdoExtensionItemKind itemKind = typeof(T).GetExtensionItemKind();

            return itemKind switch
            {
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
        /// <returns>Returns the class of the specified dico.</returns>
        private static Type GetDictionaryType<T>() where T : IBdoExtensionItemDefinition
        {
            BdoExtensionItemKind itemKind = typeof(T).GetExtensionItemKind();

            return itemKind switch
            {
                BdoExtensionItemKind.Connector => typeof(BdoConnectorDictionary),
                BdoExtensionItemKind.Format => null,
                BdoExtensionItemKind.Entity => typeof(BdoEntityDictionary),
                BdoExtensionItemKind.Handler => typeof(BdoHandlerDictionary),
                BdoExtensionItemKind.Metrics => typeof(BdoMetricsDictionary),
                BdoExtensionItemKind.Routine => typeof(BdoRoutineDictionary),
                BdoExtensionItemKind.Scriptword => typeof(BdoScriptwordDictionary),
                BdoExtensionItemKind.Task => typeof(BdoTaskDictionary),
                _ => null,
            };
        }

        /// <summary>
        /// Updates this instance with the specified attribute information.
        /// </summary>
        /// <param name="definition">The definition to consider.</param>
        /// <param name="attribute">The attribute to consider.</param>
        public static void UpdateDictionary(
            IBdoExtensionItemDefinition definition,
            DescribedDataItemAttribute attribute)
        {
            definition.WithName(attribute.Name?.IndexOf("$") > 0 ?
                attribute.Name[(attribute.Name.IndexOf("$") + 1)..] : attribute.Name);

            definition.WithDescription(BdoData.NewDictionary(attribute.Description));
            definition.WithCreationDate(attribute.CreationDate.ToDateTime());
            definition.WithLastModificationDate(attribute.LastModificationDate.ToDateTime());
        }

        /// <summary>
        /// Updates this instance with the specified attribute information.
        /// </summary>
        /// <param name="definition">The definition to consider.</param>
        /// <param name="attribute">The attribute to consider.</param>
        public static void UpdateDictionary(
            IBdoExtensionItemDefinition definition,
            TitledDescribedDataItemAttribute attribute)
        {
            UpdateDictionary(definition, attribute as DescribedDataItemAttribute);
            definition.WithTitle(BdoData.NewDictionary(attribute.Title));
        }
    }
}