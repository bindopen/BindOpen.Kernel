namespace BindOpen.Data
{
    /// <summary>
    /// This interface defines a storable data item.
    /// </summary>
    public static class IActivableExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="date"></param>
        /// <returns></returns>
        public static T AsActive<T>(
            this T obj,
            bool active = true)
            where T : IActivable
        {
            if (obj != null)
            {
                obj.IsActive = active;
            }

            return obj;
        }
    }
}
