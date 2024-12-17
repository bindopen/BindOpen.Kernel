using BindOpen.Data.Assemblies;
using BindOpen.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BindOpen.Data;

public partial class DataDbContext : DbContext
{
    public AssemblyReferenceDto GetAssemblyReference(string identifier)
    {
        return AssemblyReferences
            .FirstOrDefault(q => q.Identifier == identifier);
    }

    public AssemblyReferenceDto Upsert(IBdoAssemblyReference poco)
    {
        if (poco == null) return default;

        poco.Identifier ??= StringHelper.NewGuid();

        var dbItem = GetAssemblyReference(poco.Identifier);

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
