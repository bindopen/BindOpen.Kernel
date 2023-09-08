using BindOpen.Kernel.Data;
using BindOpen.Kernel.Logging;
using System;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace BindOpen.Kernel.Scoping.Stores
{
    /// <summary>
    /// This class represents a extension scope loader.
    /// </summary>
    internal partial class BdoExtensionStoreLoader :
        BdoObject, IBdoExtensionStoreLoader
    {
        /// <summary>
        /// Loads the extension dico of the specified kind from the specified assembly.
        /// </summary>
        /// <param key="assembly">The assembly to consider.</param>
        /// <param key="kind">The kind of item to consider.</param>
        /// <param key="packageDefinition">The extension definition to consider.</param>
        /// <param key="log">The log to consider.</param>
        /// <returns></returns>
        private int LoadDictionary(
            Assembly assembly,
            BdoExtensionKinds kind,
            IBdoPackageDefinition packageDefinition = null,
            IBdoLog log = null)
        {
            if (assembly == null)
            {
                return -1;
            }

            switch (kind)
            {
                case BdoExtensionKinds.Connector:
                    return LoadConnectorDictionaryFromAssembly(assembly, packageDefinition, log);
                case BdoExtensionKinds.Entity:
                    return LoadEntityDictionaryFromAssembly(assembly, packageDefinition, log);
                case BdoExtensionKinds.Function:
                    return LoadFunctionDictionaryFromAssembly(assembly, packageDefinition, log);
                case BdoExtensionKinds.Task:
                    return LoadTaskDictionaryFromAssembly(assembly, packageDefinition, log);
                default:
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
                    log?.AddEvent(EventKinds.Warning, "No dictionary named '" + resourceFileName + "' found in assembly");
                }
                else
                {
                    try
                    {
                        stream = assembly.GetManifestResourceStream(resourceFullName);
                        if (stream == null)
                        {
                            log?.AddEvent(EventKinds.Error,
                                "Could not open the item dictionary named '" + resourceFullName + "' in assembly");
                        }
                        else
                        {
                            Type type = GetDictionaryType<T>();
                            XmlSerializer serializer = new(type);
                            dico = (TBdoExtensionDictionary<T>)serializer.Deserialize(stream);
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
            BdoExtensionKinds itemKind = typeof(T).GetExtensionKind();

            return itemKind switch
            {
                BdoExtensionKinds.Connector => "BindOpen.Connectors",
                BdoExtensionKinds.Entity => "BindOpen.Entities",
                BdoExtensionKinds.Function => "BindOpen.Functions",
                BdoExtensionKinds.Task => "BindOpen.Tasks",
                _ => null,
            };
        }

        /// <summary>
        /// Gets the item definition file name of the TO specified extension item definition class.
        /// </summary>
        /// <returns>Returns the class of the specified dico.</returns>
        private static Type GetDictionaryType<T>() where T : IBdoExtensionDefinition
        {
            BdoExtensionKinds itemKind = typeof(T).GetExtensionKind();

            return itemKind switch
            {
                BdoExtensionKinds.Connector => typeof(BdoConnectorDictionary),
                BdoExtensionKinds.Entity => typeof(BdoEntityDictionary),
                BdoExtensionKinds.Function => typeof(BdoFunctionDictionary),
                BdoExtensionKinds.Task => typeof(BdoTaskDictionary),
                _ => null,
            };
        }
    }
}