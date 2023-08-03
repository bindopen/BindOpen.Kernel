using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.System.Data.Meta.Reflection
{
    /// <summary>
    /// This class represents a meta data.
    /// </summary>
    public static partial class BdoMetaDataExtensions
    {
        /// <summary>
        /// Creates a meta data of the specified object.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static IBdoMetaData ToMetaData(
            this object obj,
            Type type,
            string name = null,
            bool onlyMetaAttributes = false)
        {
            IBdoMetaData meta = null;

            if (type != null && typeof(IBdoMetaData).IsAssignableFrom(type) == true)
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
        public static IBdoMetaData ToMetaData(
            this object obj,
            string name = null,
            bool onlyMetaAttributes = false)
        {
            return obj == null ? null :
                obj.ToMetaData(null, name, onlyMetaAttributes);
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
            => obj.ToMetaData(null, name, onlyMetaAttributes) as T;

        /// <summary>
        /// Creates a meta data of the specified object.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static IBdoMetaSet AsMetaSet(
            this IBdoMetaData meta)
            => meta as IBdoMetaSet;

        /// <summary>
        /// Creates a data element list from a dynamic object.
        /// </summary>
        /// <param key="obj">The objet to consider.</param>
        public static IBdoMetaData[] ToArray(
            this IBdoMetaData meta)
            => meta.ToList()?.ToArray();

        /// <summary>
        /// Creates a data element list from a dynamic object.
        /// </summary>
        /// <param key="obj">The objet to consider.</param>
        public static IList<IBdoMetaData> ToList(
            this IBdoMetaData meta)
        {
            if (meta is IBdoMetaSet metaSet)
            {
                return metaSet.ToList();
            }

            return null;
        }
    }
}
