using System.Linq;

namespace BindOpen.Data.Schema;

/// <summary>
/// This class represents a Db converter of definitions.
/// </summary>
public static class DefinitionDbConverter
{
    /// <summary>
    /// Converts a definition poco into a DTO one.
    /// </summary>
    /// <param key="poco">The poco to consider.</param>
    /// <returns>The DTO object.</returns>
    public static DefinitionDb ToDb(
        this IBdoDefinition poco,
        DataDbContext context) => poco.ToDb<DefinitionDb>(context);

    /// <summary>
    /// Converts a definition poco of the specified class into a DTO one.
    /// </summary>
    /// <typeparam name="T">The type of configuration to consider.</typeparam>
    /// <param key="poco">The poco to consider.</param>
    /// <returns>The DTO object.</returns>
    public static T ToDb<T>(
        this IBdoDefinition poco,
        DataDbContext context)
        where T : DefinitionDb, new()
    {
        if (poco == null) return null;

        T dbItem = new()
        {
            CreationDate = poco.CreationDate ?? new(),
            Description = poco.Description.ToDb(context),
            Items = poco.Items == null ? null : poco.Items.Select(q => q.ToDb(context)).ToList(),
            LastModificationDate = poco.LastModificationDate ?? new(),
            Title = poco.Title.ToDb(context),
            UsedItemIds = poco.UsedItemIds == null ? null : poco.UsedItemIds.Select(q => q).ToList()
        };

        return dbItem;
    }

    /// <summary>
    /// Converts a definition DTO to a poco one.
    /// </summary>
    /// <param key="dbItem">The DTO to consider.</param>
    /// <returns>The poco object.</returns>
    public static IBdoDefinition ToPoco(
        this DefinitionDb dbItem) => dbItem.ToPoco<BdoDefinition>();

    /// <summary>
    /// Converts a definition DTO of the specified class to a poco one.
    /// </summary>
    /// <typeparam name="T">The type of configuration to consider.</typeparam>
    /// <param key="dbItem">The DTO to consider.</param>
    /// <returns>The poco object.</returns>
    public static T ToPoco<T>(
        this DefinitionDb dbItem)
        where T : IBdoDefinition, new()
    {
        if (dbItem == null) return default;

        T poco = new()
        {
            CreationDate = dbItem.CreationDate,
            LastModificationDate = dbItem.LastModificationDate,
            UsedItemIds = dbItem.UsedItemIds == null ? null : dbItem.UsedItemIds.Select(q => q).ToList()
        };

        poco
            .WithTitle(dbItem.Title.ToPoco<string>())
            .WithDescription(dbItem.Description.ToPoco<string>())
            .With(dbItem.Items.Select(q => q.ToPoco()).ToArray());

        return poco;
    }
}
