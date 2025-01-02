using BindOpen.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BindOpen.Data;

public partial class DataDbContext : DbContext
{
    public MergerDb GetMerger(string identifier)
    {
        return Mergers
            .FirstOrDefault(q => q.Identifier == identifier);
    }

    public IBdoMerger Repair(IBdoMerger poco)
    {
        if (poco != null)
        {
            poco.Identifier ??= StringHelper.NewGuid();
        }

        return poco;
    }

    public MergerDb Upsert(IBdoMerger poco)
    {
        if (poco == null) return default;

        Repair(poco);

        var dbItemItem = GetMerger(poco.Identifier);

        if (dbItemItem == null)
        {
            var dbItem = poco.ToDb();
            Add(dbItem);
        }
        else
        {
            dbItemItem.UpdateFromPoco(poco);
        }

        return dbItemItem;
    }
}
