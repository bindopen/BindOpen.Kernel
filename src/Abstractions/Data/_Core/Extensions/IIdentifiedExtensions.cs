using BindOpen.Data.Helpers;

namespace BindOpen.Data
{
    /// <summary>
    /// This interface defines an identified data item.
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
                obj.Identifier = id ?? StringHelper.NewGuid();
            }

            return obj;
        }
    }
}