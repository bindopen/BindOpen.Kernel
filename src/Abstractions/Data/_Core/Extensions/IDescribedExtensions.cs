namespace BindOpen.System.Data
{
    /// <summary>
    /// This interface represents an described data item.
    /// </summary>
    public static class IDescribedExtensions
    {
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
