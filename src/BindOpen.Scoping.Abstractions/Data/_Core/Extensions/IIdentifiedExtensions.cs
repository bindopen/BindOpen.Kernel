namespace BindOpen.Scoping.Data
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
            string id)
            where T : IIdentified
        {
            if (obj != null)
            {
                obj.Id = id;
            }

            return obj;
        }
    }
}