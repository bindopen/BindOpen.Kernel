namespace BindOpen.Kernel.Data
{
    /// <summary>
    /// This static class provides methods to handle conditions.
    /// </summary>
    public static class IResultItemExtensions
    {
        public static T WithKey<T>(
            this T obj,
            string key)
            where T : IResultItem
        {
            if (obj != null)
            {
                obj.Key = key;
            }

            return obj;
        }

        public static T WithStatus<T>(
            this T obj,
            ResourceStatus status)
            where T : IResultItem
        {
            if (obj != null)
            {
                obj.Status = status;
            }

            return obj;
        }
    }
}