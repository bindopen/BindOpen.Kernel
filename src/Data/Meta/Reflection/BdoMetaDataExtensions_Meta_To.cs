using BindOpen.System.Data.Assemblies;
using System;

namespace BindOpen.System.Data.Meta.Reflection
{
    /// <summary>
    /// This class represents a meta data.
    /// </summary>
    public static partial class BdoMetaDataExtensions
    {
        // Metadata

        /// <summary>
        /// Creates a meta data of the specified object.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static IBdoMetaData ToMeta(
            this object obj,
            Type type,
            string name = null,
            bool onlyMetaAttributes = false,
            bool includeNullValues = true)
        {
            IBdoMetaData meta;

            if (obj == null && !includeNullValues) return null;

            if (type != null && typeof(IBdoMetaData).IsAssignableFrom(type) == true && obj != null)
            {
                meta = obj as IBdoMetaData;

                if (meta != null)
                {
                    meta.Name = name;
                }
            }
            else
            {
                meta = BdoData.NewMeta(name, type, obj);
            }

            meta?.UpdateTree(onlyMetaAttributes, includeNullValues);

            return meta;
        }

        /// <summary>
        /// Creates a meta data of the specified object.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static IBdoMetaData ToMeta(
            this object obj,
            string name = null,
            bool onlyMetaAttributes = false,
            bool includeNullValues = true)
        {
            return obj.ToMeta(null, name, onlyMetaAttributes, includeNullValues);
        }

        /// <summary>
        /// Creates a meta data of the specified object.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static T ToMeta<T>(
            this object obj,
            string name = null,
            bool onlyMetaAttributes = false,
            bool includeNullValues = true)
            where T : class, IBdoMetaData
            => obj.ToMeta(null, name, onlyMetaAttributes, includeNullValues) as T;

        // Specification

        /// <summary>
        /// Creates a meta data of the specified object.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static T ToSpec<T>(
            this Type type,
            string name = null,
            bool onlyMetaAttributes = true)
            where T : class, IBdoSpec, new()
        {
            var obj = AssemblyHelper.CreateInstance(type);

            var spec = obj.ToMeta(name, onlyMetaAttributes).ToSpec<T>();

            return spec;
        }

        /// <summary>
        /// Creates a meta data of the specified object.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static IBdoSpec ToSpec(
            this Type type,
            string name = null,
            bool onlyMetaAttributes = true)
            => type.ToSpec<BdoSpec>(name, onlyMetaAttributes);
    }
}
