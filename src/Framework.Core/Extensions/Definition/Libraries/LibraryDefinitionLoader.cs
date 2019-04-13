using System;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Extensions.Definition.Libraries
{
    /// <summary>
    /// This static class provides methods to load library definitions.
    /// </summary>
    public static class LibraryDefinitionLoader
    {
        // ------------------------------------------
        // CONSTANTS
        // ------------------------------------------

        #region Constants

        /// <summary>
        /// The default resource file name for library definition.
        /// </summary>
        public static readonly string __DefaultResourceFileName = "Library.Definition";

        #endregion

        /// <summary>
        /// Loads a definition from the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly to consider.</param>
        /// <param name="resourceFullName">The full name of the resouce to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The created library.</returns>
        public static LibraryDefinitionDto LoadFrom(
            Assembly assembly, string resourceFullName = null, ILog log = null)
        {
            LibraryDefinitionDto definition = null;

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
                            XmlSerializer serializer = new XmlSerializer(typeof(LibraryDefinitionDto));
                            definition = (LibraryDefinitionDto)serializer.Deserialize(stream);

                            definition?.Initialize();
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