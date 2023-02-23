using System.Collections.Generic;

namespace BindOpen.Data.Meta.Reflection
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
            string name)
            => obj.ToMetaData(false, name);

        /// <summary>
        /// Creates a meta data of the specified object.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static IBdoMetaData ToMetaData(
            this object obj,
            bool onlyMetaAttributes = false,
            string name = null)
            => BdoMeta.New(name, obj)
                .UpdateTree(onlyMetaAttributes);

        /// <summary>
        /// Creates a meta data of the specified object.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static T ToMetaData<T>(
            this object obj,
            string name)
            where T : class, IBdoMetaData
            => obj.ToMetaData<T>(false, name);

        /// <summary>
        /// Creates a meta data of the specified object.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static T ToMetaData<T>(
            this object obj,
            bool onlyMetaAttributes = false,
            string name = null)
            where T : class, IBdoMetaData
            => obj.ToMetaData(onlyMetaAttributes, name) as T;

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
        public static List<IBdoMetaData> ToList(
            this IBdoMetaData meta)
        {
            if (meta is IBdoMetaSet metaList)
            {
                return metaList.ToList();
            }

            return null;
        }
    }
}
