using System.Reflection;

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
        public static BdoMetaData ToMeta(
            this object obj,
            string name = null)
        {
            if (obj?.GetType().IsList() == true)
            {
                var objList = obj.AsObjectList();
                return BdoData.NewMeta(name, objList.ToArray());
            }
            return BdoData.NewMeta(name, obj);
        }

        /// <summary>
        /// Creates a data element set from a dynamic object.
        /// </summary>
        /// <param name="obj">The objet to consider.</param>
        public static IBdoMetaData[] ToMetaArray(this object obj)
            => obj.ToMetaSet()?.ToArray();

        /// <summary>
        /// Creates a data element set from a dynamic object.
        /// </summary>
        /// <param name="obj">The objet to consider.</param>
        public static BdoMetaSet ToMetaSet(this object obj)
            => obj.ToMetaSet<BdoMetaSet>();

        /// <summary>
        /// Creates a data element set from a dynamic object.
        /// </summary>
        /// <param name="obj">The objet to consider.</param>
        public static T ToMetaSet<T>(this object obj)
            where T : class, IBdoMetaSet, new()
        {
            T metaSet = default;

            if (obj != null)
            {
                metaSet = new();
                foreach (var info in obj.GetType().GetProperties())
                {
                    string propertyName = info.Name;
                    object propertyValue = info.GetValue(obj);

                    if (info.GetCustomAttribute(typeof(BdoDataAttribute)) is BdoDataAttribute attribute)
                    {
                        propertyName = attribute.Name;
                    }

                    metaSet.Add(propertyValue.ToMeta(propertyName));
                }
            }

            return metaSet;
        }
    }
}
