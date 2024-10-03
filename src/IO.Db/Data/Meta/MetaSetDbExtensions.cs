using BindOpen.Data.Helpers;
using System.Linq;

namespace BindOpen.Data.Meta;

/// <summary>
/// This static class provides methods to create extension items.
/// </summary>
public static class MetaSetDbExtensions
{
    public static MetaSetDto GetMetaSet(
        this DataDbContext context,
        string identifier)
    {
        return context.MetaSets
            .FirstOrDefault(q => q.Identifier == identifier);
    }

    private static IBdoMetaSet Repair(IBdoMetaSet poco)
    {
        poco.Identifier ??= StringHelper.NewGuid();
        if (poco.Items != null)
        {
            foreach (var item in poco.Items)
            {
                item.Repair();
            }
        }

        return poco;
    }

    public static MetaSetDto Upsert(
        this DataDbContext context,
        IBdoMetaSet poco)
    {
        if (context == null || poco == null) return default;

        Repair(poco);

        var dbItem = context.GetMetaSet(poco.Identifier);

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
