using BindOpen.Data.Meta;
using System.Linq;

namespace BindOpen.Data.Schema;

/// <summary>
/// This class represents a Db converter of schema sets.
/// </summary>
public static class SchemaSetDbConverter
{
    /// <summary>
    /// Converts a schema set poco into a DTO one.
    /// </summary>
    /// <param key="poco">The poco to consider.</param>
    /// <returns>The DTO object.</returns>
    public static SchemaSetDb ToDb(
        this IBdoSchemaSet poco,
        DataDbContext context)
    {
        if (poco == null) return null;

        SchemaSetDb dbItem = new()
        {
            Name = poco.Name,
            Identifier = poco.Identifier,
            Items = poco.Items?.Select(q => q.ToDb(context)).ToList()
        };

        return dbItem;
    }

    /// <summary>
    /// Converts a schema set DTO to a poco one.
    /// </summary>
    /// <param key="dbItem">The DTO to consider.</param>
    /// <returns>The poco object.</returns>
    public static IBdoSchemaSet ToPoco(
        this SchemaSetDb dbItem)
    {
        if (dbItem == null) return null;

        BdoSchemaSet poco = new()
        {
            Name = dbItem.Name,
            Identifier = dbItem.Identifier
        };
        poco.With(dbItem.Items?.Select(q => q.ToPoco()).ToArray());

        return poco;
    }
}
