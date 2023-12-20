using BindOpen.Data.Meta;

namespace BindOpen.Data
{
    /// <summary>
    /// This interface represents a named data.
    /// </summary>
    public static class IBdoDetailedExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="detail"></param>
        public static T WithDetail<T>(
            this T obj,
            IBdoMetaSet detail)
            where T : IBdoDetailed
        {
            if (obj != null)
            {
                obj.Detail = detail;
            }
            return obj;
        }
    }
}
