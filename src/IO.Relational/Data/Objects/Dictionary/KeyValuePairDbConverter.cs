using BindOpen.Data.Helpers;
using System.Collections.Generic;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a Db converter of key value pairs.
    /// </summary>
    public static class KeyValuePairDbConverter
    {
        /// <summary>
        /// Converts a key value pair poco into a DTO one.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static KeyValuePairDb ToDb<TItem>(this KeyValuePair<string, TItem> poco)
        {
            KeyValuePairDb dbItem = new();
            dbItem.UpdateFromPoco(poco);

            return dbItem;
        }

        public static KeyValuePairDb UpdateFromPoco<TItem>(
            this KeyValuePairDb dbItem,
            KeyValuePair<string, TItem> poco)
        {
            if (dbItem == null) return null;

            var valueType = typeof(TItem).GetValueType();

            dbItem.Value = poco.Value.ToString(valueType);
            dbItem.Key = poco.Key;

            return dbItem;
        }

        /// <summary>
        /// Converts a key value pair DTO to a poco one.
        /// </summary>
        /// <param key="dbItem">The DTO to consider.</param>
        /// <returns>The poco object.</returns>
        public static KeyValuePair<string, TItem> ToPoco<TItem>(this KeyValuePairDb dbItem)
        {
            var valueType = typeof(TItem).GetValueType();

            TItem item = dbItem == null ? default : dbItem.Value.ToObject(valueType).As<TItem>();

            KeyValuePair<string, TItem> poco = new(dbItem?.Key, item);

            return poco;
        }
    }
}
