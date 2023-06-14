using System.Collections.Generic;

namespace BindOpen.System.Data
{
    /// <summary>
    /// This interface represents an described data item.
    /// </summary>
    public static class GloballyDescribedExtensions
    {
        public static T AddDescription<T>(
            this T obj,
            KeyValuePair<string, string> item)
            where T : IBdoDescribed
        {
            if (obj != null)
            {
                obj.Description ??= BdoData.NewDictionary();
                obj.Description.Add(item);
            }

            return obj;
        }
    }
}
