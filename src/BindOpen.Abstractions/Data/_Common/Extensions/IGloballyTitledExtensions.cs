using BindOpen.Data.Items;

namespace BindOpen.Data
{
    /// <summary>
    /// This interface represents an described data item.
    /// </summary>
    public static class IGloballyTitledExtensions
    {
        public static T WithTitle<T>(
            this T obj,
            IBdoDictionary dictionary)
            where T : IGloballyTitled
        {
            if (obj != null)
            {
                obj.Title = dictionary;
            }

            return obj;
        }
    }
}
