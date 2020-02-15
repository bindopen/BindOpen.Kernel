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
        /// The default resource file name for library definition.
        /// </summary>
        private static readonly string __DefaultResourceFileName = "BindOpen.Extension";

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

                Stream stream = null;
                if (resourceFullName == null)
                {
                    log?.AddError("Could not find any library definition in assembly (default named '" + __DefaultResourceFileName.ToLower() + "')");
                }
                else
                {
                    try
                    {
                        stream = assembly.GetManifestResourceStream(resourceFullName);
                        if (stream == null)
                        {
                            log?.AddError("Could not find the library definition named '" + resourceFullName + "' in assembly");
                        }
                        else
                        {
                            IBdoExtensionDefinitionDto definitionDto = null;
                            XmlSerializer serializer = new XmlSerializer(typeof(BdoExtensionDefinitionDto));
                            definitionDto = (BdoExtensionDefinitionDto)serializer.Deserialize(stream);
                            definitionDto?.Initialize();

                            definition = new BdoExtensionDefinition(definitionDto);
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

            return definition;
        }
    }
}