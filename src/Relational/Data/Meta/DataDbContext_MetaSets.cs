using BindOpen.Data.Meta;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BindOpen.Data;

public partial class DataDbContext : DbContext
{
    public MetaSetDb GetMetaSet(string identifier)
    {
        return MetaSets
            .Include(q => q.MetaItems)
            .FirstOrDefault(q => q.Identifier == identifier);
    }

    public IBdoMetaSet Delete(IBdoMetaSet poco, bool removeItems = true)
    {
        if (poco == null) return null;

        var dbItem = GetMetaSet(poco.Identifier);

        if (dbItem != null)
        {
            if (removeItems)
            {
                if (poco.Items != null)
                {
                    foreach (var item in poco.Items)
                    {
                        Delete(item);
                    }
                }
            }

            Remove(dbItem);
        }

        return poco;
    }
}
