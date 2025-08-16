using BindOpen.Data.Helpers;
using System.Collections.Generic;
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
    public static StringDictionaryDb ToDb<TItem>(
        this ITBdoDictionary<TItem> poco,
        DataDbContext context)
    {
        if (poco == null) return null;

        StringDictionaryDb dbItem = new();
        dbItem.UpdateFromPoco(poco, context);

        return dbItem;
    }

    public static StringDictionaryDb UpdateFromPoco<TItem>(
        this StringDictionaryDb dbItem,
        ITBdoDictionary<TItem> poco,
        DataDbContext context)
    {
        if (dbItem == null) return null;

        if (poco == null) return dbItem;

        poco.Identifier ??= StringHelper.NewGuid();

        dbItem.Identifier = poco.Identifier;

        if (context == null)
        {
            dbItem.Values = poco?.Select(value =>
            {
                var valueDb = value.ToDb();
                valueDb.StringDictionary = dbItem;
                valueDb.StringDictionaryId = dbItem?.Identifier;

                return valueDb;
            }).ToList();
        }
        else
        {
            dbItem.Values ??= [];
            dbItem.Values.RemoveAll(q => poco.Any(p => p.Key == q?.Key) != true);

            if (poco?.Values?.Count > 0)
            {
                foreach (var key in poco.Keys)
                {
                    var keyExists = dbItem.Values.Any(q => q.Key == key);

                    if (!keyExists)
                    {
                        var pairPoco = KeyValuePair.Create(key, poco[key]);
                        var pairDb = pairPoco.ToDb();

                        dbItem.Values.Add(pairDb);
                    }
                    else
                    {
                        var pairDb = dbItem.Values.FirstOrDefault(q => q.Key == key);
                        var pairPoco = KeyValuePair.Create(key, poco[key]);
                        pairDb.UpdateFromPoco(pairPoco);
                    }
                }
            }
        }

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
