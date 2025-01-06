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

    public ReferenceDb Upsert(IBdoReference poco)
    {
        if (poco == null) return default;

        var dbItem = GetReference(poco.Identifier);

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
