using BindOpen.Data.Meta;
using BindOpen.Scoping.Script;

namespace BindOpen.Data;

/// <summary>
/// This class represents a IO converter of expressions.
/// </summary>
public static class ExpressionIOConverter
{
    /// <summary>
    /// Converts an assembly reference poco into a DTO one.
    /// </summary>
    /// <param key="poco">The poco to consider.</param>
    /// <returns>The DTO object.</returns>
    public static ExpressionDto ToDto(this IBdoExpression poco)
    {
        if (poco == null) return null;

        ExpressionDto dto = new();
        dto.UpdateFromPoco(poco);

        return dto;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="poco"></param>
    /// <returns></returns>
    public static ExpressionDto UpdateFromPoco(
        this ExpressionDto dto,
        IBdoExpression poco)
    {
        if (dto == null) return null;

        if (poco == null) return dto;

        dto.ExpressionKind = poco.ExpressionKind;
        dto.Identifier = poco.Identifier;
        dto.Text = poco.Text;

        if (poco is IBdoScriptword wordPoco)
        {
            if (dto.Scriptword?.Identifier != poco?.Identifier)
            {
                dto.Scriptword = ScriptwordIOConverter.ToDto(wordPoco);
            }
            else if (wordPoco != null)
            {
                dto.Scriptword ??= new();
                dto.Scriptword.UpdateFromPoco(wordPoco);
            }
        }

        return dto;
    }

    /// <summary>
    /// Converts an assembly reference DTO into a poco one.
    /// </summary>
    /// <param key="dto">The DTO to consider.</param>
    /// <returns>The poco object.</returns>
    public static IBdoExpression ToPoco(
        this ExpressionDto dto)
    {
        if (dto == null) return null;

        if (dto.ExpressionKind == BdoExpressionKind.Word)
        {
            var poco = dto.Scriptword.ToPoco();
            return poco;
        }
        else
        {
            BdoExpression poco = new();
            poco.UpdateFromDto(dto);

            return poco;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="poco"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    public static IBdoExpression UpdateFromDto(
        this IBdoExpression poco,
        ExpressionDto dto)
    {
        if (poco == null) return null;

        if (dto == null) return poco;

        if (dto.ExpressionKind == BdoExpressionKind.Word && dto is IBdoScriptword word)
        {
            word.UpdateFromDto(dto.Scriptword);
        }
        else
        {
            poco.Identifier = dto.Identifier;
            poco.ExpressionKind = dto.ExpressionKind;
            poco.Text = dto.Text;
        }

        return poco;
    }
}
