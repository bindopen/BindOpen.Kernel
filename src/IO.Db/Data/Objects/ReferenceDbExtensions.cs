using BindOpen.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BindOpen.Data;

public partial class DataDbContext : DbContext
{
    public ReferenceDto GetReference(string identifier)
    {
        return References
            .Include(q => q.Expression)
            .Include(q => q.MetaData)
            .FirstOrDefault(q => q.Identifier == identifier);
    }

    private IBdoReference Repair(IBdoReference poco)
    {
        poco.Identifier ??= StringHelper.NewGuid();
        if (poco.Expression != null) poco.Expression.Identifier ??= poco.Identifier;

        return poco;
    }

    public ReferenceDto Upsert(IBdoReference poco)
    {
        if (poco == null) return default;

        Repair(poco);

        var dbItem = GetReference(poco.Identifier);

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

    public IBdoReference Delete(IBdoReference poco)
    {
        if (poco == null) return null;

        var dto = GetReference(poco.Identifier);

        if (dto != null)
        {
            Remove(dto);
        }

        return poco;
    }
}
