using BindOpen.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BindOpen.Data;

public partial class DataDbContext : DbContext
{
    public MergerDto GetMerger(string identifier)
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

    public MergerDto Upsert(IBdoMerger poco)
    {
        if (poco == null) return default;

        Repair(poco);

        var dbItem = GetMerger(poco.Identifier);

        if (dbItem == null)
        {
            var dto = poco.ToDto();
            Add(dto);
        }
        else
        {
            dbItem.UpdateFromPoco(poco);
        }

        return dbItem;
    }
}
