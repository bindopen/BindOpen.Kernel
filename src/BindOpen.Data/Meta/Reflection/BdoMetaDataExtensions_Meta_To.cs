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
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static IBdoMetaData ToMetaData(
            this object obj,
            string name)
            => obj.ToMetaData(false, name);

        /// <summary>
        /// Creates a meta data of the specified object.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static IBdoMetaData ToMetaData(
            this object obj,
            bool onlyMetaAttributes = false,
            string name = null)
            => BdoMeta.New(name, obj)
                .UpdateTree(onlyMetaAttributes);

        /// <summary>
        /// Creates a meta data of the specified object.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static T ToMetaData<T>(
            this object obj,
            string name)
            where T : class, IBdoMetaData
            => obj.ToMetaData<T>(false, name);

        /// <summary>
        /// Creates a meta data of the specified object.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static T ToMetaData<T>(
            this object obj,
            bool onlyMetaAttributes = false,
            string name = null)
            where T : class, IBdoMetaData
            => obj.ToMetaData(onlyMetaAttributes, name) as T;

        /// <summary>
        /// Creates a meta data of the specified object.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static IBdoMetaList AsMetaList(
            this IBdoMetaData meta)
            => meta as IBdoMetaList;

        /// <summary>
        /// Creates a data element list from a dynamic object.
        /// </summary>
        /// <param name="obj">The objet to consider.</param>
        public static IBdoMetaData[] ToArray(
            this IBdoMetaData meta)
            => meta.ToList()?.ToArray();

        /// <summary>
        /// Creates a data element list from a dynamic object.
        /// </summary>
        /// <param name="obj">The objet to consider.</param>
        public static List<IBdoMetaData> ToList(
            this IBdoMetaData meta)
        {
            if (meta is IBdoMetaList metaList)
            {
                return metaList.ToList();
            }

            return null;
        }
    }
}
