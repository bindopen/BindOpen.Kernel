using BindOpen.Data;
using BindOpen.Data.Assemblies;
using System.Collections.Generic;

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
                obj.References ??= new List<IBdoAssemblyReference>();
                foreach (var reference in references)
                {
                    obj.References.Add(reference);
                }
            }
            return obj;
        }

        public static IExtensionLoadOptions AddAssemblyFrom<Q>(
            this IExtensionLoadOptions options)
        {
            if (options != null)
            {
                options.References ??= new List<IBdoAssemblyReference>();
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
                obj.References = new List<IBdoAssemblyReference>() { BdoData.AssemblyAsAll() };
            }
            return obj;
        }
    }
}
