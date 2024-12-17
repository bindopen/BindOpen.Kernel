using BindOpen.Data.Helpers;
using BindOpen.Data.Meta;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BindOpen.Data;

public partial class DataDbContext : DbContext
{
    public MetaSetDto GetMetaSet(string identifier)
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

    public MetaSetDto Upsert(IBdoMetaSet poco)
    {
        if (poco == null) return default;

        Repair(poco);

        var dbItem = GetMetaSet(poco.Identifier);

        if (dbItem == null)
        {
            var dto = poco.ToDto();
            Add(dto);
        }
        else
        {
            dbItem.UpdateFromPoco(poco);

            dbItem.Items ??= [];

            if (poco?.Items.Count > 0)
            {
                dbItem.Items.RemoveAll(q => poco.Items.Any(p => p?.Identifier == q?.Identifier) != true);

                foreach (var subItem in poco.Items)
                {
                    Repair(subItem);

                    var dbSubItem = GetMetaData(subItem.Identifier);

                    if (dbSubItem == null)
                    {
                        var dtoSubItem = subItem.ToDto();
                        Add(dtoSubItem);
                    }
                    else
                    {
                        dbSubItem.UpdateFromPoco(subItem);
                    }
                }
            }
        }

        return dbItem;
    }

    public IBdoMetaSet Delete(IBdoMetaSet poco, bool removeItems = true)
    {
        if (poco == null) return null;

        var dto = GetMetaSet(poco.Identifier);

        if (dto != null)
        {
            if (removeItems)
            {
                if (dto.Items != null)
                {
                    foreach (var item in poco.Items)
                    {
                        Delete(item);
                    }
                }
            }

            Remove(dto);
        }

        return poco;
    }
}
