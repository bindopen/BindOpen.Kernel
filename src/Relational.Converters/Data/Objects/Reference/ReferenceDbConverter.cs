using BindOpen.Data.Helpers;
using BindOpen.Data.Meta;

namespace BindOpen.Data;

/// <summary>
/// This class represents a Db converter of references.
/// </summary>
public static class ReferenceDbConverter
{
    /// <summary>
    /// Converts a reference poco into a DTO one.
    /// </summary>
    /// <param key="poco">The poco to consider.</param>
    /// <returns>The DTO object.</returns>
    public static ReferenceDb ToDb(
        this IBdoReference poco,
        DataDbContext context)
    {
        if (poco == null) return null;

        ReferenceDb dbItem = new();
        dbItem.UpdateFromPoco(poco, context);

        return dbItem;
    }

    public static ReferenceDb UpdateFromPoco(
        this ReferenceDb dbItem,
        IBdoReference poco,
        DataDbContext context)
    {
        if (dbItem == null) return null;

        if (poco == null) return dbItem;

        poco.Identifier ??= StringHelper.NewGuid();

        dbItem.Identifier = poco.Identifier;
        dbItem.Kind = poco.Kind;
        dbItem.MetaData = poco.MetaData.ToDb(context);

        // Expression

        dbItem.Expression = context?.Upsert(poco.Expression);
        if (dbItem.Expression != null)
        {
            dbItem.Expression.Reference = dbItem;
            dbItem.ExpressionId = dbItem.Expression.Identifier;
        }

        dbItem.MetaData = context?.Upsert(poco.MetaData);
        if (dbItem.MetaData != null)
        {
            dbItem.MetaData.Reference = dbItem;
            dbItem.MetaDataId = dbItem.MetaData.Identifier;
        }

        return dbItem;
    }

    /// <summary>
    /// Converts a reference DTO to a poco one.
    /// </summary>
    /// <param key="dbItem">The DTO to consider.</param>
    /// <returns>The poco object.</returns>
    public static IBdoReference ToPoco(
        this ReferenceDb dbItem)
    {
        if (dbItem == null) return null;

        BdoReference poco = new()
        {
            Identifier = dbItem.Identifier,
            Expression = dbItem.Expression.ToPoco(),
            Kind = dbItem.Kind,
            MetaData = dbItem.MetaData.ToPoco()
        };

        return poco;
    }
}
