using BindOpen.Data.Meta;
using System.Reflection;

namespace BindOpen.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class BdoMetaSetExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T FromObject<T>(
            this T set,
            object obj)
            where T : IBdoMetaSet
        {
            set?.WithItems(obj.ToMetaArray());
            return set;
        }

        /// <summary>
        /// Creates a data element set from a dynamic object.
        /// </summary>
        /// <param name="obj">The objet to consider.</param>
        public static IBdoMetaSet ToMetaSet(this object obj)
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