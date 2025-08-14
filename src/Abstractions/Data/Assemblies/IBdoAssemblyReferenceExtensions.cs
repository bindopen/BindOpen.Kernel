using BindOpen.Data.Assemblies;
using System;

namespace BindOpen.Data;

/// <summary>
/// This class provides extensions of IBdoAssemblyReference class.
/// </summary>
public static partial class IBdoAssemblyReferenceExtensions
{
    /// <summary>
    /// Sets the file name of the specified assembly reference.
    /// </summary>
    /// <typeparam name="T">The class of the specified reference.</typeparam>
    /// <param name="reference">The reference to consider.</param>
    /// <param name="alias">The alias to consider.</param>
    /// <returns>Returns the reference.</returns>
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
    /// Sets the assembly version of the specified assembly reference.
    /// </summary>
    /// <typeparam name="T">The class of the specified reference.</typeparam>
    /// <param name="reference">The reference to consider.</param>
    /// <param name="assemblyVersion">The assembly version to consider.</param>
    /// <returns>Returns the reference.</returns>
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
    /// Sets the assembly file name of the specified assembly reference.
    /// </summary>
    /// <typeparam name="T">The class of the specified reference.</typeparam>
    /// <param name="reference">The reference to consider.</param>
    /// <param name="assemblyFileName">The assembly file name to consider.</param>
    /// <returns>Returns the reference.</returns>
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
