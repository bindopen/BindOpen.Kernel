namespace BindOpen.Data
{
    /// <summary>
    /// This static class extends IActivable interface.
    /// </summary>
    public static class IActivableExtensions
    {
        /// <summary>
        /// Indicates whether the specified activable object.
        /// </summary>
        /// <param key="isActive">Indicates whether the specified object is active.</param>
        /// <returns>Returns the specified activable object.</returns>
        public static T AsActive<T>(
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
