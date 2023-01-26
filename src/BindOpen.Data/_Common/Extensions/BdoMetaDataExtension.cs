namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public static partial class BdoMetaDataExtension
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
            if (obj?.GetType().IsList() == true)
            {
                var objList = obj.AsObjectList();
                return BdoMeta.New(name, objList.ToArray());
            }
            return BdoMeta.New(name, obj);
        }

        /// <summary>
        /// Creates a data element set from a dynamic object.
        /// </summary>
        /// <param name="obj">The objet to consider.</param>
        public static IBdoMetaData[] ToMetaArray(this object obj)
            => obj.ToMetaSet()?.ToArray();
    }
}
