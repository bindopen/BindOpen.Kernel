using BindOpen.Data.Helpers;
using System.Linq;

namespace BindOpen.Data.Assemblies;

/// <summary>
/// This static class provides methods to create extension items.
/// </summary>
public static class ClassReferenceDbExtensions
{
    public static ClassReferenceDto GetClassReference(
        this DataDbContext context,
        string identifier)
    {
        return context.ClassReferences
            .FirstOrDefault(q => q.Identifier == identifier);
    }

    public static ClassReferenceDto Upsert(
        this DataDbContext context,
        IBdoClassReference poco)
    {
        if (context == null || poco == null) return default;

        poco.Identifier ??= StringHelper.NewGuid();

        var dbItem = context.GetClassReference(poco.Identifier);

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
