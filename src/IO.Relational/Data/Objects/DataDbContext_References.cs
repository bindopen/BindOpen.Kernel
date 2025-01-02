using BindOpen.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BindOpen.Data;

public partial class DataDbContext : DbContext
{
    public ReferenceDb GetReference(string identifier)
    {
        return References
            .Include(q => q.Expression)
            .Include(q => q.MetaData)
            .FirstOrDefault(q => q.Identifier == identifier);
    }

    private IBdoReference Repair(IBdoReference poco)
    {
        if (poco != null)
        {
            poco.Identifier ??= StringHelper.NewGuid();
            if (poco.Expression != null) poco.Expression.Identifier ??= poco.Identifier;
        }

        return poco;
    }

    public ReferenceDb Upsert(IBdoReference poco)
    {
        if (poco == null) return default;

        Repair(poco);

        var dbItemItem = GetReference(poco.Identifier);

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

    public IBdoReference Delete(IBdoReference poco)
    {
        if (poco == null) return null;

        var dbItem = GetReference(poco.Identifier);

        if (dbItem != null)
        {
            Remove(dbItem);
        }

        return poco;
    }
}
