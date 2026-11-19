using BindOpen.Data.Assemblies;
using System;

namespace BindOpen.Data;

/// <summary>
/// Provides extension methods for IBdoAssemblyReference to facilitate fluent configuration 
/// of assembly metadata.
/// </summary>
public static partial class IBdoAssemblyReferenceExtensions
{
    /// <summary>
    /// Sets the alias for the specified assembly reference.
    /// </summary>
    /// <typeparam name="T">The type implementing IBdoAssemblyReference.</typeparam>
    /// <param name="reference">The assembly reference instance.</param>
    /// <param name="alias">The simplified name or alias to assign.</param>
    /// <returns>Returns the reference instance for fluent chaining.</returns>
    public static T WithAlias<T>(
        this T reference,
        string alias)
        where T : IBdoAssemblyReference
    {
        if (reference != null)
        {
            reference.Alias = alias;
        }
        return reference;
    }

    /// <summary>
    /// Sets the version for the specified assembly reference.
    /// </summary>
    /// <typeparam name="T">The type implementing IBdoAssemblyReference.</typeparam>
    /// <param name="reference">The assembly reference instance.</param>
    /// <param name="assemblyVersion">The specific version to target.</param>
    /// <returns>Returns the reference instance for fluent chaining.</returns>
    public static T WithAssemblyVersion<T>(
        this T reference,
        Version assemblyVersion)
        where T : IBdoAssemblyReference
    {
        if (reference != null)
        {
            reference.AssemblyVersion = assemblyVersion;
        }
        return reference;
    }

    /// <summary>
    /// Sets the physical file name or path for the specified assembly reference.
    /// </summary>
    /// <typeparam name="T">The type implementing IBdoAssemblyReference.</typeparam>
    /// <param name="reference">The assembly reference instance.</param>
    /// <param name="assemblyFileName">The file path or name (e.g., "MyPlugin.dll").</param>
    /// <returns>Returns the reference instance for fluent chaining.</returns>
    public static T WithAssemblyFileName<T>(
        this T reference,
        string assemblyFileName)
        where T : IBdoAssemblyReference
    {
        if (reference != null)
        {
            reference.AssemblyFileName = assemblyFileName;
        }
        return reference;
    }
}