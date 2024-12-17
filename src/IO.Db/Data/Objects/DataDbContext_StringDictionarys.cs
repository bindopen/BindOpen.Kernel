using BindOpen.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data;

public partial class DataDbContext : DbContext
{
    public StringDictionaryDto GetStringDictionary(string identifier)
    {
        return StringDictionaries
            .Include(q => q.Values)
            .FirstOrDefault(q => q.Identifier == identifier);
    }

    public StringDictionaryDto Upsert<TItem>(ITBdoDictionary<TItem> poco)
    {
        if (poco == null) return default;

        poco.Identifier ??= StringHelper.NewGuid();

        var dbItem = GetStringDictionary(poco.Identifier);

        if (dbItem == null)
        {
            var dto = poco.ToDto();
            Add(dto);
        }
        else
        {
            dbItem.UpdateFromPoco(poco);

            dbItem.Values ??= [];

            if (poco?.Keys.Count > 0)
            {
                dbItem.Values.RemoveAll(q => poco.Keys.Any(p => p == q.Key) != true);

                foreach (var key in poco.Keys)
                {
                    var keyExists = dbItem.Values.Any(q => q.Key == key);

                    if (!keyExists)
                    {
                        var pairPoco = KeyValuePair.Create(key, poco[key]);
                        var pairDto = pairPoco.ToDto();

                        dbItem.Values.Add(pairDto);
                    }
                    else
                    {
                        var pairDto = dbItem.Values.FirstOrDefault(q => q.Key == key);
                        var pairPoco = KeyValuePair.Create(key, poco[key]);
                        pairDto.UpdateFromPoco(pairPoco);
                    }
                }
            }
        }

        return dbItem;
    }
}
