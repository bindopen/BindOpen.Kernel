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
            IBdoMetaSet detail)
            where T : IDetailed
        {
            if (obj != null)
            {
                obj.Detail = detail;
            }
            return obj;
        }
    }
}
