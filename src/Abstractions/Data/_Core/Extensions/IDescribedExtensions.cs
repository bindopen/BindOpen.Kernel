namespace BindOpen.Kernel.Data
{
    /// <summary>
    /// This static class extends IDescribed interface.
    /// </summary>
    public static class IDescribedExtensions
    {
        /// <summary>
        /// Sets the description text to the specified described object.
        /// </summary>
        /// <param key="text">The descrpition text to consider.</param>
        /// <returns>Returns the specified described object.</returns>
        public static T WithDescription<T>(
            this T obj,
            string text)
            where T : IDescribed
        {
            if (obj != null)
            {
                obj.Description = text;
            }

            return obj;
        }
    }
}
