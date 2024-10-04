using BindOpen.Data.Helpers;
using BindOpen.Data.Meta;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BindOpen.Data;

public partial class DataDbContext : DbContext
{
    public ConfigurationDto GetConfiguration(string identifier)
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

    public ConfigurationDto Upsert(IBdoConfiguration poco)
    {
        if (poco == null) return default;

        Repair(poco);

        var dbItem = GetConfiguration(poco.Identifier);

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

    public IBdoConfiguration Delete(IBdoConfiguration poco)
    {
        if (poco == null) return null;

        var dto = GetConfiguration(poco.Identifier);

        if (dto != null)
        {
            Remove(dto);
        }

        return poco;
    }
}
