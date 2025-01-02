using AutoMapper;
using BindOpen.Scoping.Script;

namespace BindOpen.Data;

/// <summary>
/// This class represents a Db converter of expressions.
/// </summary>
public static class ExpressionDbConverter
{
    /// <summary>
    /// Converts an expression poco into a DTO one.
    /// </summary>
    /// <param key="poco">The poco to consider.</param>
    /// <returns>The DTO object.</returns>
    public static ExpressionDb ToDb(this IBdoExpression poco)
    {
        ExpressionDb dbItem = new();
        dbItem.UpdateFromPoco(poco);

        return dbItem;
    }

    public static ExpressionDb UpdateFromPoco(
        this ExpressionDb dbItem,
        IBdoExpression poco)
    {
        if (dbItem == null) return null;

        if (poco == null) return dbItem;

        MapperConfiguration config;

        config = new MapperConfiguration(
            cfg => cfg.CreateMap<IBdoExpression, ExpressionDb>()
                .ForMember(q => q.Scriptword, opt => opt.Ignore())
        );

        var mapper = new Mapper(config);
        mapper.Map(poco, dbItem);

        if (poco is IBdoScriptword wordPoco)
        {
            if (dbItem.Scriptword?.Identifier != poco?.Identifier)
            {
                dbItem.Scriptword = ScriptwordDbConverter.ToDb(wordPoco);
            }
            else if (wordPoco != null)
            {
                dbItem.Scriptword ??= new();
                dbItem.Scriptword.UpdateFromPoco(wordPoco);
            }
        }

        return dbItem;
    }

    /// <summary>
    /// Converts an expression DTO to a poco one.
    /// </summary>
    /// <param key="dbItem">The DTO to consider.</param>
    /// <returns>The poco object.</returns>
    public static IBdoExpression ToPoco(
        this ExpressionDb dbItem)
    {
        if (dbItem == null) return null;

        IBdoExpression poco;

        if (dbItem.ExpressionKind == BdoExpressionKind.Word)
        {
            poco = dbItem.Scriptword.ToPoco();
        }
        else
        {
            poco = new BdoExpression()
            {
                Identifier = dbItem.Identifier,
                ExpressionKind = dbItem.ExpressionKind,
                Text = dbItem.Text
            };
        }

        return poco;
    }
}
