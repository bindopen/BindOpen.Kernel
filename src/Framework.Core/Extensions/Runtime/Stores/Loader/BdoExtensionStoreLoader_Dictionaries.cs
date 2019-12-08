using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Definition.Dictionaries;
using BindOpen.Framework.Core.Extensions.Definition.Extensions;
using BindOpen.Framework.Core.Extensions.Definition.Items;
using BindOpen.Framework.Core.System.Diagnostics;
using System;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Extensions.Runtime.Stores
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

            switch (itemKind)
            {
                case BdoExtensionItemKind.Carrier:
                    return "BindOpen.Carriers";
                case BdoExtensionItemKind.Connector:
                    return "BindOpen.Connectors";
                case BdoExtensionItemKind.ContextExtension:
                    return "BindOpen.Context";
                case BdoExtensionItemKind.Entity:
                    return "BindOpen.Entities";
                case BdoExtensionItemKind.Format:
                    return "BindOpen.Formats";
                case BdoExtensionItemKind.Handler:
                    return "BindOpen.Handlers";
                case BdoExtensionItemKind.Metrics:
                    return "BindOpen.Metrics";
                case BdoExtensionItemKind.Routine:
                    return "BindOpen.Routines";
                case BdoExtensionItemKind.Scriptword:
                    return "BindOpen.Scriptwords";
                case BdoExtensionItemKind.Task:
                    return "BindOpen.Tasks";
            }

            return null;
        }

        /// <summary>
        /// Gets the item definition file name of the TO specified extension item definition class.
        /// </summary>
        /// <returns>Returns the class of the specified dictionary.</returns>
        private Type GetDictionaryType<T>() where T : IBdoExtensionItemDefinitionDto
        {
            BdoExtensionItemKind itemKind = typeof(T).GetExtensionItemKind();

            switch (itemKind)
            {
                case BdoExtensionItemKind.Carrier:
                    return typeof(BdoCarrierDictionaryDto);
                case BdoExtensionItemKind.Connector:
                    return typeof(BdoConnectorDictionaryDto);
                case BdoExtensionItemKind.Entity:
                    return typeof(BdoEntityDictionaryDto);
                case BdoExtensionItemKind.Handler:
                    return typeof(BdoHandlerDictionaryDto);
                case BdoExtensionItemKind.Metrics:
                    return typeof(BdoMetricsDictionaryDto);
                case BdoExtensionItemKind.Routine:
                    return typeof(BdoRoutineDictionaryDto);
                case BdoExtensionItemKind.Scriptword:
                    return typeof(BdoScriptwordDictionaryDto);
                case BdoExtensionItemKind.Task:
                    return typeof(BdoTaskDictionaryDto);
            }

            return null;
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

            definition.Title = new DictionaryDataItem(attribute.Title);
            definition.Description = new DictionaryDataItem(attribute.Description);
            definition.CreationDate = attribute.CreationDate;
            definition.LastModificationDate = attribute.LastModificationDate;
            definition.Index = attribute.Index;
        }
    }
}