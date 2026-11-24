using BindOpen.Data.Assemblies;
using BindOpen.Data.Meta;
using System.Linq;

namespace BindOpen.Data.Conditions;

/// <summary>
/// This class represents a Db converter of basic conditions.
/// </summary>
public static class ConditionDbConverter
{
    /// <summary>
    /// Converts a basic condition poco into a DTO one.
    /// </summary>
    /// <param key="poco">The poco to consider.</param>
    /// <returns>The DTO object.</returns>
    public static ConditionDb ToDb(
        this IBdoCondition poco,
        DataDbContext context)
    {
        if (poco == null) return null;

        ConditionDb dbItem = new();
        dbItem.UpdateFromPoco(poco, context);

        return dbItem;
    }

    public static ConditionDb UpdateFromPoco(
        this ConditionDb dbItem,
        IBdoCondition poco,
        DataDbContext context)
    {
        if (dbItem == null) return null;

        if (poco == null) return dbItem;

        switch (dbItem.Kind)
        {
            case BdoConditionKind.Basic:
                if (poco is IBdoBasicCondition basicCondition)
                {
                    dbItem.ArgumentMetaData1 = basicCondition.Argument1.ToDb(context);
                    dbItem.ArgumentMetaData2 = basicCondition.Argument2.ToDb(context);
                    dbItem.Identifier = basicCondition.Identifier;
                    dbItem.Name = basicCondition.Name;
                    dbItem.Operator = basicCondition.Operator;
                    dbItem.ParentId = basicCondition.Parent?.Identifier;
                }
                break;
            case BdoConditionKind.Composite:
                if (poco is IBdoCompositeCondition compositeCondition)
                {
                    dbItem.CompositionKind = compositeCondition.CompositionKind;
                    dbItem.Conditions ??= [];
                    dbItem.Conditions.RemoveAll(q => compositeCondition._Children?.Any(p => p?.Identifier == q?.Identifier) != true);
                    dbItem.Identifier = compositeCondition.Identifier;
                    dbItem.Name = compositeCondition.Name;
                    dbItem.Operator = DataOperators.None;
                    dbItem.ParentId = compositeCondition.Parent?.Identifier;

                    if (compositeCondition?._Children?.Count > 0)
                    {
                        foreach (var subCondition in compositeCondition._Children)
                        {
                            var dbSubCondition = context.Upsert(subCondition);

                            if (dbItem.Conditions.Any(p => p?.Identifier == dbSubCondition?.Identifier) != true)
                            {
                                dbItem.Conditions.Add(dbSubCondition);
                            }
                        }
                    }

                    dbItem.ParentId = compositeCondition.Parent?.Identifier;
                }
                break;
            case BdoConditionKind.Expression:
                if (poco is IBdoExpressionCondition expressionCondition)
                {
                    dbItem.ExpressionItem = expressionCondition.Expression.ToDb(context);
                    dbItem.Identifier = poco.Identifier;
                    dbItem.Name = poco.Name;
                    dbItem.Operator = DataOperators.None;
                    dbItem.ParentId = poco.Parent?.Identifier;
                }
                break;
        }

        return dbItem;
    }

    /// <summary>
    /// Converts a basic condition DTO into a poco one.
    /// </summary>
    /// <param key="dbItem">The DTO to consider.</param>
    /// <returns>The poco object.</returns>
    public static IBdoCondition ToPoco(
        this ConditionDb dbItem)
    {
        if (dbItem == null) return null;

        switch (dbItem.Kind)
        {
            case BdoConditionKind.Basic:
                return new BdoBasicCondition()
                {
                    Argument1 = dbItem.ArgumentMetaData1?.ToPoco(),
                    Argument2 = dbItem.ArgumentMetaData2?.ToPoco(),
                    Identifier = dbItem.Identifier,
                    Operator = dbItem.Operator,
                    Name = dbItem.Name,
                    Parent = null
                };
            case BdoConditionKind.Composite:
                return new BdoCompositeCondition()
                {
                    CompositionKind = dbItem.CompositionKind,
                    _Children = new TBdoSet<IBdoCondition>(dbItem.Conditions?.Select(q => q.ToPoco())),
                    Identifier = dbItem.Identifier,
                    Name = dbItem.Name,
                    Parent = null
                };
            case BdoConditionKind.Expression:
                return new BdoExpressionCondition()
                {
                    Expression = dbItem.ExpressionItem.ToPoco(),
                    Identifier = dbItem.Identifier,
                    Name = dbItem.Name,
                    Parent = null
                };
        }

        return null;
    }
}
