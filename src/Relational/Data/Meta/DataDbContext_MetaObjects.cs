using BindOpen.Data.Meta;
using Microsoft.EntityFrameworkCore;

namespace BindOpen.Data;

public partial class DataDbContext : DbContext
{
    public MetaDataDb Upsert(IBdoMetaNode poco)
    {
        if (poco == null) return default;

        var dbItem = GetMetaData(poco.Identifier);

        if (dbItem == null)
        {
            dbItem = (poco as IBdoMetaData).ToDb(this);
            Add(dbItem);
        }
        else
        {
            dbItem.UpdateFromPoco(poco, this);
        }

        return dbItem;
    }
}
