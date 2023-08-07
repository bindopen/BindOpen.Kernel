using BindOpen.System.Data.Helpers;

namespace BindOpen.System.Data
{
    /// <summary>
    /// This interface represents an identified data item.
    /// </summary>
    public static class IIdentifiedExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="id"></param>
        public static T WithId<T>(
            this T obj,
            string id = null)
            where T : IIdentified
        {
            if (obj != null)
            {
                obj.Id = id ?? StringHelper.NewGuid();
            }

            return obj;
        }
    }
}