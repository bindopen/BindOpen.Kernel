using BindOpen.Data;
using BindOpen.Data.Assemblies;
using BindOpen.Data.Helpers;
using BindOpen.Data.Objects.Interfaces;
using BindOpen.Logging;
using System;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace BindOpen.Scoping;

/// <summary>
/// This partial class handles the extraction of package definitions from assembly manifest resources.
/// </summary>
internal partial class BdoExtensionStoreLoader : IBdoExtensionStoreLoader
{
    /// <summary>
    /// The default resource file name prefix for library definitions.
    /// </summary>
    private const string __DefaultResourceFileName = "BindOpen.Extension";

    /// <summary>
    /// Extracts the extension package definition from the specified assembly.
    /// </summary>
    /// <param name="assembly">The assembly to scan.</param>
    /// <param name="resourceFullName">The full name of the resource to consider. If null, searches for the default name.</param>
    /// <param name="log">The log to populate with diagnostic events.</param>
    /// <returns>Returns the extracted <see cref="IBdoPackageDefinition"/>.</returns>
    private IBdoPackageDefinition ExtractPackageDefinition(
        Assembly assembly,
        string resourceFullName = null,
        IBdoLog log = null)
    {
        if (assembly == null) return null;

        IBdoPackageDefinition definition = null;

        // 1. Try to find the manifest resource file (XML)
        resourceFullName ??= Array.Find(
            assembly.GetManifestResourceNames(),
            p => p.EndsWith(__DefaultResourceFileName, StringComparison.OrdinalIgnoreCase));

        if (resourceFullName == null)
        {
            log?.AddEvent(BdoEventKinds.Warning,
                $"Could not find any library definition resource in assembly (expected: '{__DefaultResourceFileName}')");
        }
        else
        {
            try
            {
                // 2. Deserialize the XML definition if found
                using var stream = assembly.GetManifestResourceStream(resourceFullName);
                if (stream != null)
                {
                    var serializer = new XmlSerializer(typeof(BdoPackageDefinition));
                    definition = (BdoPackageDefinition)serializer.Deserialize(stream);
                    definition?.Initialize();
                }
            }
            catch (Exception ex)
            {
                log?.AddException(ex);
            }
        }

        // 3. Fallback: Auto-generate definition from Assembly metadata if XML is missing or incomplete
        definition ??= new BdoPackageDefinition();

        definition.AssemblyName ??= assembly.GetName().Name;
        definition.FileName ??= definition.AssemblyName + ".dll";
        definition.Identifier ??= StringHelper.NewGuid();
        definition.Name ??= resourceFullName ?? definition.AssemblyName;
        definition.RootNamespace ??= resourceFullName ?? definition.AssemblyName;

        // Extract Description from Assembly Attributes
        if (string.IsNullOrEmpty(definition.Description?.Get()))
        {
            var descriptionAttr = assembly
                 .GetCustomAttributes<AssemblyDescriptionAttribute>()
                 .FirstOrDefault();

            if (descriptionAttr != null)
            {
                definition.Description ??= BdoData.NewDictionary<string>();
                definition.Description.With(descriptionAttr.Description);
            }
        }

        // Extract Title from Assembly metadata
        if (string.IsNullOrEmpty(definition.Title?.Get()))
        {
            definition.Title ??= BdoData.NewDictionary<string>();
            definition.Title.With($"Extension library '{definition.AssemblyName}'");
        }

        // Map referenced assemblies
        definition.UsingAssemblyReferences ??= assembly.GetReferencedAssemblies().ToReferences().ToArray();

        return definition;
    }
}