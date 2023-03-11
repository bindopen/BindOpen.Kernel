using BindOpen.Data;
using BindOpen.Data.Assemblies;
using BindOpen.Extensions;
using BindOpen.Scopes.Scopes;
using System.Linq;

namespace BindOpen.Scopes
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
                obj.Sources ??= new();
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
            params BdoExtensionKind[] extensionKinds)
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
                obj.References ??= new();
                obj.References.AddRange(references?.ToList());
            }
            return obj;
        }

        public static IExtensionLoadOptions AddAssemblyFrom<Q>(
            this IExtensionLoadOptions obj)
        {
            if (obj != null)
            {
                obj.References ??= new();
                obj.References.Add(BdoData.AssemblyFrom<Q>());
            }
            return obj;
        }

        public static T AddAllAssemblies<T>(
            this T obj)
            where T : IExtensionLoadOptions
        {
            if (obj != null)
            {
                obj.References = new() { BdoData.AssemblyAsAll() };
            }
            return obj;
        }
    }
}
