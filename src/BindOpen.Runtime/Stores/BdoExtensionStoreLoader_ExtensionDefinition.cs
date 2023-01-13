using BindOpen.Logging;
using BindOpen.Runtime.Definition;
using System;
using System.Reflection;
using System.Xml.Serialization;

namespace BindOpen.Runtime.Stores
{
    /// <summary>
    /// This class represents a extension scope loader.
    /// </summary>
    internal partial class BdoExtensionStoreLoader : IBdoExtensionStoreLoader
    {
        /// <summary>
        /// The default resource file name for library definition.
        /// </summary>
        private const string __DefaultResourceFileName = "BindOpen.Extension";

        /// <summary>
        /// Extract extension definition from the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly to consider.</param>
        /// <param name="resourceFullName">The full name of the resouce to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The created library.</returns>
        private IBdoExtensionDefinition ExtractExtensionDefinition(
            Assembly assembly,
            string resourceFullName = null,
            IBdoLog log = null)
        {
            IBdoExtensionDefinition definition = null;

            if (assembly != null)
            {
                if (resourceFullName == null)
                {
                    resourceFullName = Array.Find(
                       assembly.GetManifestResourceNames(), p => p.EndsWith(__DefaultResourceFileName, StringComparison.OrdinalIgnoreCase));
                }

                if (resourceFullName == null)
                {
                    log?.AddWarning("Could not find any library definition in assembly (default named '" + __DefaultResourceFileName.ToLower() + "')");
                }
                else
                {
                    try
                    {
                        using var stream = assembly.GetManifestResourceStream(resourceFullName);
                        {
                            XmlSerializer serializer = new(typeof(BdoExtensionDefinition));
                            definition = (BdoExtensionDefinition)serializer.Deserialize(stream);
                            definition?.Initialize();
                        }
                    }
                    catch (Exception ex)
                    {
                        log?.AddException(ex);
                    }
                }
            }

            return definition;
        }
    }
}