using System.Linq;

namespace BindOpen.Data;

/// <summary>
/// This class represents a Db converter of dictionaries.
/// </summary>
public static class StringDictionaryDbConverter
{
    /// <summary>
    /// Converts a dictionary poco into a DTO one.
    /// </summary>
    /// <param key="poco">The poco to consider.</param>
    /// <returns>The DTO object.</returns>
    public static StringDictionaryDb ToDb<TItem>(this ITBdoDictionary<TItem> poco)
    {
        StringDictionaryDb dbItem = new();
        dbItem.UpdateFromPoco(poco);

        return dbItem;
    }

    public static StringDictionaryDb UpdateFromPoco<TItem>(
        this StringDictionaryDb dbItem,
        ITBdoDictionary<TItem> poco)
    {
        if (dbItem == null) return null;

        if (poco == null) return dbItem;

        dbItem.Identifier = poco.Identifier;
        dbItem.Values = poco?.Select(value =>
        {
            var valueDb = value.ToDb();
            valueDb.StringDictionary = dbItem;
            valueDb.StringDictionaryId = dbItem?.Identifier;
            return valueDb;
        }).ToList();

        return dbItem;
    }

    /// <summary>
    /// Converts a dictionary DTO to a poco one.
    /// </summary>
    /// <param key="dbItem">The DTO to consider.</param>
    /// <returns>The poco object.</returns>
    public static ITBdoDictionary<TItem> ToPoco<TItem>(this StringDictionaryDb dbItem)
    {
        if (dbItem == null) return null;

        TBdoDictionary<TItem> poco = BdoData.NewDictionary(dbItem.Values?.Select(q => q.ToPoco<TItem>()).ToArray());
        poco.WithId(dbItem.Identifier);

        return poco;
    }
}
