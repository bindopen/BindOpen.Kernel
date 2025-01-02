using BindOpen.Data.Helpers;
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

    private IBdoConfiguration Repair(IBdoConfiguration poco)
    {
        if (poco != null)
        {
            poco.Identifier ??= StringHelper.NewGuid();
            //if (poco.Expression != null) poco.Expression.Identifier ??= poco.Identifier;
            //if (poco.MetaData != null) poco.MetaData.Identifier ??= poco.Identifier;
        }

        return poco;
    }

    public ConfigurationDb Upsert(IBdoConfiguration poco)
    {
        if (poco == null) return default;

        Repair(poco);

        var dbItemItem = GetConfiguration(poco.Identifier);

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
