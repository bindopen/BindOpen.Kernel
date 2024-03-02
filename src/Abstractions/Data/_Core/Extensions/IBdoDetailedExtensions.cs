using BindOpen.Data.Meta;

namespace BindOpen.Data
{
    /// <summary>
    /// This interface defines a named data.
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

        // Flag

        /// <summary>
        /// 
        /// </summary>
        /// <param key="isAllocatable"></param>
        /// <returns></returns>
        public static bool GetFlagValue(
            this IBdoDetailed detailed,
            string flagName)
        {
            return detailed?.Detail?.GetData<bool?>(flagName) ?? false;
        }
    }
}
