using BindOpen.Scoping.Data.Meta;

namespace BindOpen.Scoping.Data
{
    /// <summary>
    /// This interface represents a named data.
    /// </summary>
    public static class DetailedExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="detail"></param>
        public static T WithDetail<T>(
            this T obj,
            params IBdoMetaData[] elms)
            where T : IBdoDetailed
        {
            if (obj != null)
            {
                obj.Detail = BdoMeta.NewSet(elms);
            }
            return obj;
        }
    }
}
