using BindOpen.Data.Meta;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BindOpen.Data;

public partial class DataDbContext : DbContext
{
    public ConfigurationDb GetConfiguration(string identifier)
    {
        return Configurations
            .Include(q => q.Children)
            .Include(q => q.Description)
            .FirstOrDefault(q => q.Identifier == identifier);
    }

    public ConfigurationDb Upsert(IBdoConfiguration poco)
    {
        if (poco == null) return default;

        var dbItem = GetConfiguration(poco.Identifier);

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

    public IBdoConfiguration Delete(IBdoConfiguration poco)
    {
        if (poco == null) return null;

        var dbItem = GetConfiguration(poco.Identifier);

        if (dbItem != null)
        {
            Remove(dbItem);
        }

        return poco;
    }
}
