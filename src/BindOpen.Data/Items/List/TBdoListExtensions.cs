using BindOpen.Data.Items;
using BindOpen.Data.Meta;
using System;

namespace BindOpen.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class TBdoListExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T FromObject<T>(
            this T set,
            object obj,
            Type type = null,
            bool onlyMetaAttributes = false)
            where T : ITBdoList<IBdoMetaData>
        {
            set?.With(
                obj.ToMetaArray(type, onlyMetaAttributes));
            return set;
        }
    }
}