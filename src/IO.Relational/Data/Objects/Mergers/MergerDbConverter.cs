using BindOpen.Data.Helpers;
using System.Collections.Generic;

namespace BindOpen.Data;

/// <summary>
/// This class represents a Db converter of mergers.
/// </summary>
public static class MergerDbConverter
{
    /// <summary>
    /// Converts a merger poco into a DTO one.
    /// </summary>
    /// <param key="poco">The poco to consider.</param>
    /// <returns>The DTO object.</returns>
    public static MergerDb ToDb(
        this IBdoMerger poco)
    {
        if (poco == null) return null;

        MergerDb dbItem = new();
        dbItem.UpdateFromPoco(poco);

        return dbItem;
    }

    public static MergerDb UpdateFromPoco(
        this MergerDb dbItem,
        IBdoMerger poco)
    {
        if (dbItem == null) return null;

        if (poco == null) return dbItem;

        poco.Identifier ??= StringHelper.NewGuid();

        dbItem.Identifier = poco.Identifier;
        dbItem.AddedValues = poco.AddedValues == null ? null : new List<string>(poco.AddedValues);
        dbItem.RemovedValues = poco.RemovedValues == null ? null : new List<string>(poco.RemovedValues);

        return dbItem;
    }

    /// <summary>
    /// Converts a merger DTO to a poco one.
    /// </summary>
    /// <param key="dbItem">The DTO to consider.</param>
    /// <returns>The poco object.</returns>
    public static IBdoMerger ToPoco(this MergerDb dbItem)
    {
        if (dbItem == null) return null;

        BdoMerger poco = new()
        {
            Identifier = dbItem.Identifier,
            AddedValues = dbItem.AddedValues == null ? null : new List<string>(dbItem.AddedValues),
            RemovedValues = dbItem.RemovedValues == null ? null : new List<string>(dbItem.RemovedValues)
        };

        return poco;
    }
}
