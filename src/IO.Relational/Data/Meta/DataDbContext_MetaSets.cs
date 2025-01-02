using BindOpen.Data.Helpers;
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

    private IBdoMetaSet Repair(IBdoMetaSet poco)
    {
        if (poco != null)
        {
            poco.Identifier ??= StringHelper.NewGuid();

            if (poco.Items != null)
            {
                foreach (var item in poco.Items)
                {
                    Repair(item);
                }
            }
        }

        return poco;
    }

    public MetaSetDb Upsert(IBdoMetaSet poco)
    {
        if (poco == null) return default;

        Repair(poco);

        var dbItemItem = GetMetaSet(poco.Identifier);

        if (dbItemItem == null)
        {
            var dbItem = poco.ToDb();
            Add(dbItem);
        }
        else
        {
            dbItemItem.UpdateFromPoco(poco);

            dbItemItem.Items ??= [];

            if (poco?.Items.Count > 0)
            {
                dbItemItem.Items.RemoveAll(q => poco.Items.Any(p => p?.Identifier == q?.Identifier) != true);

                foreach (var subItem in poco.Items)
                {
                    Upsert(subItem);
                }
            }
        }

        return dbItemItem;
    }

    public IBdoMetaSet Delete(IBdoMetaSet poco, bool removeItems = true)
    {
        if (poco == null) return null;

        var dbItem = GetMetaSet(poco.Identifier);

        if (dbItem != null)
        {
            if (removeItems)
            {
                if (dbItem.Items != null)
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
