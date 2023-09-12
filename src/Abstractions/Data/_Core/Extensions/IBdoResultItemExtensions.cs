namespace BindOpen.Kernel.Data
{
    /// <summary>
    /// This static class provides methods to handle conditions.
    /// </summary>
    public static class IBdoResultItemExtensions
    {
        public static T WithKey<T>(
            this T obj,
            string key)
            where T : IBdoResultItem
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
            where T : IBdoResultItem
        {
            if (obj != null)
            {
                obj.Status = status;
            }

            return obj;
        }
    }
}