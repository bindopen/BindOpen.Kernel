namespace BindOpen.Kernel.Data
{
    /// <summary>
    /// This static class extends IDefaultable interface.
    /// </summary>
    public static class IDefaultableExtensions
    {
        /// <summary>
        /// Indicates whether the specified defautable object.
        /// </summary>
        /// <param key="isDefault">Indicates whether the specified object is default.</param>
        /// <returns>Returns the specified defautable object.</returns>
        public static T AsDefault<T>(
            this T source,
            bool isDefault = true)
            where T : IDefaultable
        {
            if (source != null)
            {
                source.IsDefault = isDefault;
            }

            return source;
        }
    }
}
