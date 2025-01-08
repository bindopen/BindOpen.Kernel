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
    public MergerDb Upsert(IBdoMerger poco)
    {
        if (poco == null) return default;

        var dbItem = GetMerger(poco.Identifier);

        if (dbItem == null)
        {
            dbItem = poco.ToDb();
            Add(dbItem);
        }
        else
        {
            dbItem.UpdateFromPoco(poco);
        }

        return dbItem;
    }
}
