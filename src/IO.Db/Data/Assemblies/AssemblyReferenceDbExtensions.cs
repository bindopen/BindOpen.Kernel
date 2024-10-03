using BindOpen.Data.Helpers;
using System.Linq;

namespace BindOpen.Data.Assemblies;

/// <summary>
/// This static class provides methods to create extension items.
/// </summary>
public static class AssemblyReferenceDbExtensions
{
    public static AssemblyReferenceDto GetAssemblyReference(
        this DataDbContext context,
        string identifier)
    {
        return context.AssemblyReferences
            .FirstOrDefault(q => q.Identifier == identifier);
    }

    public static AssemblyReferenceDto Upsert(
        this DataDbContext context,
        IBdoAssemblyReference poco)
    {
        if (context == null || poco == null) return default;

        poco.Identifier ??= StringHelper.NewGuid();

        var dbItem = context.GetAssemblyReference(poco.Identifier);

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
