using BindOpen.Data;
using BindOpen.Data.Assemblies;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Scoping
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public static partial class ExtensionLoadOptionsExtensions
    {
        /// <summary>
        /// Sets the source kinds of this instance.
        /// </summary>
        /// <param key="sourceKinds">The source kinds to consider.</param>
        /// <returns>Returns this instance.</returns>
        public static T AddSource<T>(
            this T obj,
            DatasourceKind kind,
            string uri = null)
            where T : IExtensionLoadOptions
        {
            if (obj != null)
            {
                obj.Sources ??= new List<(DatasourceKind Kind, string Uri)>();
                obj.Sources.Add((kind, uri));
            }
            return obj;
        }

        /// <summary>
        /// Sets the source kinds of this instance.
        /// </summary>
        /// <param key="sourceKinds">The source kinds to consider.</param>
        /// <returns>Returns this instance.</returns>
        public static T WithExtensionKinds<T>(
            this T obj,
            params BdoExtensionKinds[] extensionKinds)
            where T : IExtensionLoadOptions
        {
            if (obj != null)
            {
                obj.ExtensionKinds = extensionKinds?.ToList();
            }
            return obj;
        }

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
