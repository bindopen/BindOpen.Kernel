using BindOpen.Data;
using System;
using System.Reflection;

namespace BindOpen.Scoping;

/// <summary>
/// This class provides routines to resolve assembly loading failures.
/// </summary>
/// <remarks>
/// It hooks into the <see cref="AppDomain.AssemblyResolve"/> event to manually 
/// search for missing assemblies in custom locations.
/// </remarks>
public class AssemblyResolver : BdoObject
{
    // ------------------------------------------
    // STATIC METHODS
    // ------------------------------------------

    #region Static Methods

    /// <summary>
    /// Attaches the assembly resolving routine to the specified application domain.
    /// </summary>
    /// <param name="appDomain">The application domain to monitor.</param>
    /// <returns>Returns the attached <see cref="ResolveEventHandler"/>.</returns>
    public static ResolveEventHandler Resolve(AppDomain appDomain)
    {
        if (appDomain == null) return null;

        var resolver = new AssemblyResolver();
        return resolver.AttachToDomain(appDomain);
    }

    #endregion

    // ------------------------------------------
    // PRIVATE METHODS
    // ------------------------------------------

    #region Private Methods

    /// <summary>
    /// Attaches the resolve event handler to the domain.
    /// </summary>
    private ResolveEventHandler AttachToDomain(AppDomain appDomain)
    {
        if (appDomain == null) return null;

        ResolveEventHandler handler = BuildResolveEventHandler();
        appDomain.AssemblyResolve += handler;

        return handler;
    }

    /// <summary>
    /// Builds the delegate responsible for resolving assemblies.
    /// </summary>
    private ResolveEventHandler BuildResolveEventHandler()
    {
        return new ResolveEventHandler((object sender, ResolveEventArgs args) =>
        {
            // Extract assembly short name
            var assemblyNameObject = new AssemblyName(args.Name);
            string shortName = assemblyNameObject.Name;

            // Skip resource and serialization assemblies to avoid infinite loops or overhead
            if (shortName.EndsWith(".resources") || shortName.EndsWith(".XmlSerializers"))
            {
                return null;
            }

            Assembly assembly = null;

            try
            {
                // Logic for custom probing (e.g., checking specific subfolders)
                // would go here. For now, we return null to let the standard 
                // loader continue its process if no custom logic is defined.
            }
            catch (Exception ex)
            {
                // In a resolver, we generally avoid throwing to prevent application crash
                // but we should log the diagnostic information.
                System.Diagnostics.Debug.WriteLine($"AssemblyResolver Error: {ex}");
            }

            return assembly;
        });
    }

    #endregion
}