using System.Linq;

namespace BindOpen.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class IBdoUsingExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="detail"></param>
        public static T Using<T>(
            this T obj,
            params string[] usedItemIds)
            where T : IBdoUsing
        {
            if (obj != null)
            {
                obj.UsedItemIds = usedItemIds?.ToList();
            }
            return obj;
        }
    }
}