using BindOpen.Data.Assemblies;
using BindOpen.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BindOpen.Data;

public partial class DataDbContext : DbContext
{
    public ClassReferenceDto GetClassReference(string identifier)
    {
        return ClassReferences
            .FirstOrDefault(q => q.Identifier == identifier);
    }

    public ClassReferenceDto Upsert(IBdoClassReference poco)
    {
        if (poco == null) return default;

        poco.Identifier ??= StringHelper.NewGuid();

        var dbItem = GetClassReference(poco.Identifier);

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
}
