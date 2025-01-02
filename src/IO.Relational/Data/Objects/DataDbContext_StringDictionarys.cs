using BindOpen.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data;

public partial class DataDbContext : DbContext
{
    public StringDictionaryDb GetStringDictionary(string identifier)
    {
        return StringDictionaries
            .Include(q => q.Values)
            .FirstOrDefault(q => q.Identifier == identifier);
    }

    public StringDictionaryDb Upsert<TItem>(ITBdoDictionary<TItem> poco)
    {
        if (poco == null) return default;

        poco.Identifier ??= StringHelper.NewGuid();

        var dbItemItem = GetStringDictionary(poco.Identifier);

        if (dbItemItem == null)
        {
            var dbItem = poco.ToDb();
            Add(dbItem);
        }
        else
        {
            dbItemItem.UpdateFromPoco(poco);

            dbItemItem.Values ??= [];

            if (poco?.Keys.Count > 0)
            {
                dbItemItem.Values.RemoveAll(q => poco.Keys.Any(p => p == q.Key) != true);

                foreach (var key in poco.Keys)
                {
                    var keyExists = dbItemItem.Values.Any(q => q.Key == key);

                    if (!keyExists)
                    {
                        var pairPoco = KeyValuePair.Create(key, poco[key]);
                        var pairDb = pairPoco.ToDb();

                        dbItemItem.Values.Add(pairDb);
                    }
                    else
                    {
                        var pairDb = dbItemItem.Values.FirstOrDefault(q => q.Key == key);
                        var pairPoco = KeyValuePair.Create(key, poco[key]);
                        pairDb.UpdateFromPoco(pairPoco);
                    }
                }
            }
        }

        return dbItemItem;
    }
}
