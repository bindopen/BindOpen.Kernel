using BindOpen.Data.Assemblies;
using BindOpen.Data.Helpers;
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

        poco.Identifier ??= StringHelper.NewGuid();

        var dbItemItem = GetClassReference(poco.Identifier);

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
}
