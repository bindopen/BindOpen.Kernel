using Microsoft.EntityFrameworkCore;
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

        var dbItem = GetStringDictionary(poco.Identifier);

        if (dbItem == null)
        {
            dbItem = poco.ToDb(this);
            Add(dbItem);
        }
        else
        {
            dbItem.UpdateFromPoco(poco, this);
        }

        return dbItem;
    }

    public StringDictionaryDb Delete(ITBdoDictionary<string> poco)
    {
        if (poco == null) return null;

        var dbItem = GetStringDictionary(poco.Identifier);

        if (dbItem != null)
        {
            Remove(dbItem);
        }

        return dbItem;
    }
}
