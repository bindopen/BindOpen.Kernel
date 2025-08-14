using BindOpen.Data.Conditions;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BindOpen.Data;

public partial class DataDbContext : DbContext
{
    public ConditionDb GetCondition(string identifier)
    {
        return Conditions
            .Include(q => q.Conditions)
            .FirstOrDefault(q => q.Identifier == identifier);
    }

    public ConditionDb Upsert(IBdoCondition poco)
    {
        if (poco == null) return default;

        var dbItem = GetCondition(poco.Identifier);

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

    public IBdoCondition Delete(IBdoCondition poco)
    {
        if (poco == null) return null;

        var dbItem = GetCondition(poco.Identifier);

        if (dbItem != null)
        {
            Remove(dbItem);
        }

        return poco;
    }
}
