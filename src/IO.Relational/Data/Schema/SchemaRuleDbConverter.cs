using BindOpen.Data.Conditions;
using BindOpen.Data.Meta;

namespace BindOpen.Data.Schema;

/// <summary>
/// This class represents a rule converter.
/// </summary>
public static class SchemaRuleDbConverter
{
    /// <summary>
    /// Converts a requirement level conditional statement poco into a DTO one.
    /// </summary>
    /// <param key="poco">The poco to consider.</param>
    /// <returns>The DTO object.</returns>
    public static SchemaRuleDb ToDb(
        this IBdoSchemaRule poco,
        DataDbContext context)
    {
        if (poco == null) return null;

        var dbItem = new SchemaRuleDb()
        {
            Condition = poco.Condition.ToDb(context),
            GroupId = poco.GroupId,
            Identifier = poco.Identifier,
            Kind = poco.Kind,
            Reference = poco.Reference.ToDb(context),
            ResultCode = poco.ResultCode,
            ResultDescription = poco.ResultDescription,
            ResultEventKind = poco.ResultEventKind,
            ResultTitle = poco.ResultTitle,
            Value = BdoData.NewScalar(poco.Value).ToDb(context)
        };

        return dbItem;
    }

    /// <summary>
    /// Converts a string conditional statement DTO into a poco one.
    /// </summary>
    /// <param key="dbItem">The DTO to consider.</param>
    /// <returns>The poco object.</returns>
    public static IBdoSchemaRule ToPoco(this SchemaRuleDb dbItem)
    {
        if (dbItem == null) return null;

        var poco = new BdoSchemaRule()
        {
            Condition = dbItem.Condition.ToPoco(),
            GroupId = dbItem.GroupId,
            Kind = dbItem.Kind,
            Identifier = dbItem.Identifier,
            Reference = dbItem.Reference.ToPoco(),
            ResultCode = dbItem.ResultCode,
            ResultDescription = dbItem.ResultDescription,
            ResultEventKind = dbItem.ResultEventKind,
            ResultTitle = dbItem.ResultTitle,
            Value = dbItem.Value
        };

        return poco;
    }
}
