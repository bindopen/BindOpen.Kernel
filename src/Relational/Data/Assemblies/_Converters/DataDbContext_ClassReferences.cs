using BindOpen.Data.Assemblies;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BindOpen.Data;

public partial class DataDbContext : DbContext
{
    public ClassReferenceDb GetClassReference(string identifier)
    {
        return ClassReferences
            .FirstOrDefault(q => q.Identifier == identifier);
    }

    public ClassReferenceDb Upsert(IBdoClassReference poco)
    {
        if (poco == null) return default;

        var dbItem = GetClassReference(poco.Identifier);

        if (dbItem == null)
        {
            dbItem = poco.ToDb();
            Add(dbItem);
        }
        else
        {
            dbItem.UpdateFromPoco(poco);
        }

        return dbItem;
    }
}
