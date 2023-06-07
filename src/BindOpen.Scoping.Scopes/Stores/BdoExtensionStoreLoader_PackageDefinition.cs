using BindOpen.Scoping.Data;
using BindOpen.Scoping.Data.Assemblies;
using BindOpen.Scoping.Data.Helpers;
using BindOpen.Scoping.Extensions;
using BindOpen.Logging;
using System;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace BindOpen.Scoping.Scopes.Stores
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
        /// <param key="assembly">The assembly to consider.</param>
        /// <param key="resourceFullName">The full name of the resouce to consider.</param>
        /// <param key="log">The log to consider.</param>
        /// <returns>The created library.</returns>
        private IBdoPackageDefinition ExtractPackageDefinition(
            Assembly assembly,
            string resourceFullName = null,
            IBdoLog log = null)
        {
            IBdoPackageDefinition definition = null;

            if (assembly != null)
            {
                resourceFullName ??= Array.Find(
                       assembly.GetManifestResourceNames(), p => p.EndsWith(__DefaultResourceFileName, StringComparison.OrdinalIgnoreCase));

                if (resourceFullName == null)
                {
                    log?.AddEvent(EventKinds.Warning,
                        "Could not find any library definition in assembly (default named '" + __DefaultResourceFileName.ToLower() + "')");
                }
                else
                {
                    try
                    {
                        using var stream = assembly.GetManifestResourceStream(resourceFullName);
                        {
                            XmlSerializer serializer = new(typeof(BdoPackageDefinition));
                            definition = (BdoPackageDefinition)serializer.Deserialize(stream);
                            definition?.Initialize();
                        }
                    }
                    catch (Exception ex)
                    {
                        log?.AddException(ex);
                    }
                }

                definition ??= new BdoPackageDefinition();
                definition.AssemblyName ??= assembly.GetName().Name;

                if (string.IsNullOrEmpty(definition.Description?.Get()))
                {
                    var descriptionAttribute = assembly
                         .GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false)
                         .OfType<AssemblyDescriptionAttribute>()
                         .FirstOrDefault();
                    if (descriptionAttribute != null)
                    {
                        definition.Description ??= BdoData.NewDictionary();
                        definition.Description.Set(descriptionAttribute.Description);
                    }
                }

                definition.FileName ??= definition.AssemblyName + ".dll";
                definition.Id ??= StringHelper.NewGuid();
                definition.Name ??= resourceFullName ?? definition.AssemblyName;
                definition.RootNamespace ??= resourceFullName ?? definition.AssemblyName;

                if (string.IsNullOrEmpty(definition.Title?.Get()))
                {
                    definition.Title ??= BdoData.NewDictionary();
                    definition.Title.Set(
                        string.Format(
                            "Extension library 'BindOpen.Scoping.Scopes.Tests'",
                            definition.AssemblyName));
                }

                definition.UsingAssemblyReferences ??= assembly.GetReferencedAssemblies().ToReferences().ToArray();
            }

            return definition;
        }
    }
}