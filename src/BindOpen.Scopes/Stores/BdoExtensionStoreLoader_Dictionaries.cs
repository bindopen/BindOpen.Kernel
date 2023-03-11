using BindOpen.Data;
using BindOpen.Extensions;
using BindOpen.Extensions.Connectors;
using BindOpen.Extensions.Entities;
using BindOpen.Extensions.Functions;
using BindOpen.Extensions.Tasks;
using BindOpen.Logging;
using System;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace BindOpen.Scopes.Stores
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
        /// <param key="assembly">The assembly to consider.</param>
        /// <param key="kind">The kind of item to consider.</param>
        /// <param key="extensionDefinition">The extension definition to consider.</param>
        /// <param key="log">The log to consider.</param>
        /// <returns></returns>
        private int LoadDictionary(
            Assembly assembly,
            BdoExtensionKind kind,
            IBdoPackageDefinition extensionDefinition = null,
            IBdoLog log = null)
        {
            if (assembly == null)
            {
                return -1;
            }

            switch (kind)
            {
                case BdoExtensionKind.Connector:
                    return LoadConnectorDictionaryFromAssembly(assembly, extensionDefinition, log);
                case BdoExtensionKind.Entity:
                    return LoadEntityDictionaryFromAssembly(assembly, extensionDefinition, log);
                case BdoExtensionKind.Function:
                    return LoadFunctionDictionaryFromAssembly(assembly, extensionDefinition, log);
                case BdoExtensionKind.Task:
                    return LoadTaskDictionaryFromAssembly(assembly, extensionDefinition, log);
                case BdoExtensionKind.Any:
                case BdoExtensionKind.None:
                    break;
            }
            return -1;
        }

        /// <summary>
        /// Loads the specified BindOpen extension dico.
        /// </summary>
        /// <param key="assembly">The assembly to consider.</param>
        /// <param key="log">The log to consider.</param>
        /// <returns>The created library.</returns>
        private static ITBdoExtensionDictionary<T> ExtractDictionaryFromAssembly<T>(
            Assembly assembly,
            IBdoLog log = null) where T : IBdoExtensionDefinition
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
        private static string GetDictionaryResourceName<T>() where T : IBdoExtensionDefinition
        {
            BdoExtensionKind itemKind = typeof(T).GetExtensionKind();

            return itemKind switch
            {
                BdoExtensionKind.Connector => "BindOpen.Connectors",
                BdoExtensionKind.Entity => "BindOpen.Entities",
                BdoExtensionKind.Function => "BindOpen.Functions",
                BdoExtensionKind.Task => "BindOpen.Tasks",
                _ => null,
            };
        }

        /// <summary>
        /// Gets the item definition file name of the TO specified extension item definition class.
        /// </summary>
        /// <returns>Returns the class of the specified dico.</returns>
        private static Type GetDictionaryType<T>() where T : IBdoExtensionDefinition
        {
            BdoExtensionKind itemKind = typeof(T).GetExtensionKind();

            return itemKind switch
            {
                BdoExtensionKind.Connector => typeof(BdoConnectorDictionary),
                BdoExtensionKind.Entity => typeof(BdoEntityDictionary),
                BdoExtensionKind.Function => typeof(BdoFunctionDictionary),
                BdoExtensionKind.Task => typeof(BdoTaskDictionary),
                _ => null,
            };
        }

        /// <summary>
        /// Updates this instance with the specified attribute information.
        /// </summary>
        /// <param key="definition">The definition to consider.</param>
        /// <param key="attribute">The attribute to consider.</param>
        public static void UpdateDictionary(
            IBdoExtensionDefinition definition,
            MetaExtensionAttribute attribute)
        {
            definition.WithName(attribute.Name?.IndexOf("$") > 0 ?
                attribute.Name[(attribute.Name.IndexOf("$") + 1)..] : attribute.Name);

            definition.WithDescription(BdoData.NewDictionary(attribute.Description));
            //definition.WithCreationDate(attribute.CreationDate.ToDateTime());
            //definition.WithLastModificationDate(attribute.LastModificationDate.ToDateTime());
        }

        ///// <summary>
        ///// Updates this instance with the specified attribute information.
        ///// </summary>
        ///// <param key="definition">The definition to consider.</param>
        ///// <param key="attribute">The attribute to consider.</param>
        //public static void UpdateDictionary(
        //    IBdoExtensionDefinition definition,
        //    MetaExtensionAttribute attribute)
        //{
        //    //UpdateDictionary(definition, attribute as MetaExtensionAttribute);
        //    //definition.WithTitle(BdoData.NewDictionary(attribute.Title));
        //}
    }
}