namespace BindOpen.System.Data
{
    /// <summary>
    /// This interface represents a named data.
    /// </summary>
    public static class IDefaultableExtensions
    {
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
