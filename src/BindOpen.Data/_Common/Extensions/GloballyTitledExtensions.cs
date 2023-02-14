using System.Collections.Generic;

namespace BindOpen.Data
{
    /// <summary>
    /// This interface represents an described data item.
    /// </summary>
    public static class GloballyTitledExtensions
    {
        public static T AddTitle<T>(
            this T obj,
            KeyValuePair<string, string> item)
            where T : IBdoTitled
        {
            if (obj != null)
            {
                obj.Title ??= BdoData.NewDictionary();
                obj.Title.Add(item);
            }

            return obj;
        }
    }
}
