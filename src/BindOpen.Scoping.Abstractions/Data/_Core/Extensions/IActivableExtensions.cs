namespace BindOpen.Scoping.Data
{
    /// <summary>
    /// This class represents an accessibility level extension.
    /// </summary>
    public static class IActivableExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="index"></param>
        public static T IsActive<T>(
            this T obj,
            bool isActive)
            where T : IActivable
        {
            if (obj != null)
            {
                obj.IsActive = isActive;
            }

            return obj;
        }
    }
}
