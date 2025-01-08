using BindOpen.Data;
using BindOpen.Data.Assemblies;

namespace BindOpen.Scoping
{
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

        public static IExtensionLoadOptions AddClassFrom<Q>(
            this IExtensionLoadOptions options)
        {
            if (options != null)
            {
                options.References ??= [];
                options.References.Add(BdoData.Class<Q>());
            }
            return options;
        }

        public static IExtensionLoadOptions AddAssemblyFrom<Q>(
            this IExtensionLoadOptions options)
        {
            if (options != null)
            {
                options.References ??= [];
                options.References.Add(BdoData.AssemblyFrom<Q>());
            }
            return options;
        }

        public static T AddAllAssemblies<T>(
            this T obj)
            where T : IExtensionLoadOptions
        {
            if (obj != null)
            {
                obj.References = [BdoData.AssemblyAsAll()];
            }
            return obj;
        }
    }
}
