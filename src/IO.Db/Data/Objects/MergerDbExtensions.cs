using BindOpen.Data.Helpers;
using System.Linq;

namespace BindOpen.Data;

/// <summary>
/// This static class provides methods to create extension items.
/// </summary>
public static class MergerDbExtensions
{
    public static MergerDto GetMerger(
        this DataDbContext context,
        string identifier)
    {
        return context.Mergers
            .FirstOrDefault(q => q.Identifier == identifier);
    }

    private static IBdoMerger Repair(IBdoMerger poco)
    {
        if (poco != null)
        {
            poco.Identifier ??= StringHelper.NewGuid();
        }

        return poco;
    }

    public static MergerDto Upsert(
        this DataDbContext context,
        IBdoMerger poco)
    {
        if (context == null || poco == null) return default;

        Repair(poco);

        var dbItem = context.GetMerger(poco.Identifier);

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
