using BindOpen.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data;

/// <summary>
/// This static class provides methods to create extension items.
/// </summary>
public static class StringDictionaryDbExtensions
{
    public static StringDictionaryDto GetStringDictionary(
        this DataDbContext context,
        string identifier)
    {
        return context.StringDictionaries
            .Include(q => q.Values)
            .FirstOrDefault(q => q.Identifier == identifier);
    }

    public static StringDictionaryDto Upsert<TItem>(
        this DataDbContext context,
        ITBdoDictionary<TItem> poco)
    {
        if (context == null || poco == null) return default;

        poco.Identifier ??= StringHelper.NewGuid();

        var dbItem = context.GetStringDictionary(poco.Identifier);

        if (dbItem == null)
        {
            var dto = poco.ToDto();
            context.Add(dto);
        }
        else
        {
            dbItem.UpdateFromPoco(poco);

            dbItem.Values ??= [];

            dbItem.Values.RemoveAll(q => poco?.Keys.Any(p => p == q.Key) != true);

            var newKeys = poco.Keys.Where(q => dbItem?.Values.Any(p => p.Key == q) != true).ToList();
            foreach (var key in newKeys)
            {
                var pairPoco = KeyValuePair.Create(key, poco[key]);

                var pair = new KeyValuePairDto();
                pair.UpdateFromPoco(pairPoco);

                dbItem.Values.Add(pair);
            }

            var existingPairs = dbItem.Values.Where(q => poco?.Keys.Any(p => q.Key == p) != true).ToList();
            foreach (var pair in existingPairs)
            {
                var key = pair.Key;
                var pairPoco = KeyValuePair.Create(key, poco[key]);

                pair.UpdateFromPoco(pairPoco);
            }
        }

        return dbItem;
    }
}
