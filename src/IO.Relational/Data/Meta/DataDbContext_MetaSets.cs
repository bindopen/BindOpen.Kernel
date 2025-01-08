using BindOpen.Data.Meta;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BindOpen.Data;

public partial class DataDbContext : DbContext
{
    public MetaSetDb GetMetaSet(string identifier)
    {
        return MetaSets
            .Include(q => q.Items)
            .FirstOrDefault(q => q.Identifier == identifier);
    }

    public MetaSetDb Upsert(IBdoMetaSet poco)
    {
        if (poco == null) return default;

        var dbItem = GetMetaSet(poco.Identifier);

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

    public IBdoMetaSet Delete(IBdoMetaSet poco, bool removeItems = true)
    {
        if (poco == null) return null;

        var dbItem = GetMetaSet(poco.Identifier);

        if (dbItem != null)
        {
            if (removeItems)
            {
                if (poco.Items != null)
                {
                    foreach (var item in poco.Items)
                    {
                        Delete(item);
                    }
                }
            }

            Remove(dbItem);
        }

        return poco;
    }
}
