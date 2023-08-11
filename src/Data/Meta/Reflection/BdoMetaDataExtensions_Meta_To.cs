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
            bool onlyMetaAttributes = false)
        {
            IBdoMetaData meta = null;

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

            meta?.UpdateTree(onlyMetaAttributes);

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
            bool onlyMetaAttributes = false)
        {
            return obj?.ToMeta(null, name, onlyMetaAttributes);
        }

        /// <summary>
        /// Creates a meta data of the specified object.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static T ToMetaData<T>(
            this object obj,
            string name = null,
            bool onlyMetaAttributes = false)
            where T : class, IBdoMetaData
            => obj.ToMeta(null, name, onlyMetaAttributes) as T;

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
        {
            var spec = type.ToSpec<BdoAggregateSpec>(name, onlyMetaAttributes);

            return spec;
        }
    }
}
