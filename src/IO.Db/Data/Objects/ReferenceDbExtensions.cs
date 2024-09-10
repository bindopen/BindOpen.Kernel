using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BindOpen.Data;

/// <summary>
/// This static class provides methods to create extension items.
/// </summary>
public static class ReferenceDbExtensions
{
    public static ReferenceDto GetReference(
        this DataDbContext context,
        string identifier)
    {
        return context.References
            .Include(q => q.Expression)
            .Include(q => q.MetaData)
            .FirstOrDefault(q => q.Identifier == identifier);
    }

    public static ReferenceDto Upsert(
        this DataDbContext context,
        IBdoReference poco)
    {
        if (context == null || poco?.Identifier == null) return default;

        var dbItem = context.GetReference(poco.Identifier);

        if (dbItem == null)
        {
            var dto = poco.ToDto();
            context.Add(dto);
        }
        else
        {
            dbItem.UpdateFromPoco(poco);
        }

        return dbItem;
    }
}
