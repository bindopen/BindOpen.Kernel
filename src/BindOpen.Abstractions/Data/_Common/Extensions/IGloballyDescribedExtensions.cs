using BindOpen.Data.Items;

namespace BindOpen.Data
{
    /// <summary>
    /// This interface represents an described data item.
    /// </summary>
    public static class IGloballyDescribedExtensions
    {
        public static T WithDescription<T>(
            this T obj,
            IBdoDictionary dictionary)
            where T : IGloballyDescribed
        {
            if (obj != null)
            {
                obj.Description = dictionary;
            }

            return obj;
        }
    }
}
