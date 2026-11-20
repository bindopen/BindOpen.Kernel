using BindOpen.Data;
using BindOpen.Data.Assemblies;

namespace BindOpen.Scoping;

/// <summary>
/// This class represents a data element set.
/// </summary>
public static partial class ExtensionLoadOptionsExtensions
{
    public static T AddAssemblies<T>(
        this T obj,
        params IBdoAssemblyReference[] references)
        where T : IExtensionLoadOptions
    {
        if (obj != null)
        {
            obj.References ??= [];
            foreach (var reference in references)
            {
                obj.References.Add(reference);
            }
        }
        return obj;
    }

    public static IExtensionLoadOptions AddAssemblyFrom<Q>(
        this IExtensionLoadOptions options,
        string referenceAlias = null)
    {
        var reference = BdoData.AssemblyFrom<Q>().WithAlias(referenceAlias);
        return options.AddAssemblies(reference);
    }

    public static T AddAllAssemblies<T>(
        this T obj)
        where T : IExtensionLoadOptions
    {
        var reference = BdoData.AssemblyAsAll();
        return obj.AddAssemblies(reference);
    }

    public static IExtensionLoadOptions AddClassFrom<Q>(
        this IExtensionLoadOptions options,
        string referenceAlias)
    {
        var reference = BdoData.Class<Q>().WithAlias(referenceAlias);
        return options.AddAssemblies(reference);
    }
}
