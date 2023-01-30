using System;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public static partial class BdoMetaDataExtensions
    {
        /// <summary>
        /// Creates a meta data of the specified object.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static IBdoMetaData ToMeta(
            this object obj,
            string name = null)
        {
            IBdoMetaData meta = null;
            if (obj?.GetType().IsList() == true)
            {
                var objList = obj.AsObjectList();
                meta = BdoMeta.New(name, objList.ToArray());
            }
            else
            {
                meta = BdoMeta.New(name, obj);
            }

            if (meta is IBdoMetaObject metaObj)
            {
                metaObj.UpdateTree();
            }

            return meta;
        }

        /// <summary>
        /// Creates a data element set from a dynamic object.
        /// </summary>
        /// <param name="obj">The objet to consider.</param>
        public static IBdoMetaData[] ToMetaArray(
            this object obj,
            Type type = null,
            bool onlyMetaAttributes = true)
            => obj.ToMetaSet(type, onlyMetaAttributes)?.ToArray();
    }
}
