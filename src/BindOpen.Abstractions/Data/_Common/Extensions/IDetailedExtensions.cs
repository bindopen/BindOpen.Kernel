using BindOpen.Data.Meta;

namespace BindOpen.Data
{
    /// <summary>
    /// This interface represents a named data.
    /// </summary>
    public static class IDetailedExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="detail"></param>
        public static T WithDetail<T>(
            this T obj,
            IBdoMetaList detail)
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
