using AutoMapper;
using BindOpen.Data.Meta;
using BindOpen.Scoping.Script;

namespace BindOpen.Data;

/// <summary>
/// This class represents a IO converter of expressions.
/// </summary>
public static class ExpressionIOConverter
{
    /// <summary>
    /// Converts an expression poco into a DTO one.
    /// </summary>
    /// <param key="poco">The poco to consider.</param>
    /// <returns>The DTO object.</returns>
    public static ExpressionDto ToDto(this IBdoExpression poco)
    {
        ExpressionDto dto = new();
        dto.UpdateFromPoco(poco);

        return dto;
    }

    public static ExpressionDto UpdateFromPoco(
        this ExpressionDto dto,
        IBdoExpression poco)
    {
        if (dto == null) return null;

        if (poco == null) return dto;

        MapperConfiguration config;

        config = new MapperConfiguration(
            cfg => cfg.CreateMap<IBdoExpression, ExpressionDto>()
                .ForMember(q => q.Scriptword, opt => opt.Ignore())
        );

        var mapper = new Mapper(config);
        mapper.Map(poco, dto);

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
    /// Converts an expression DTO to a poco one.
    /// </summary>
    /// <param key="dto">The DTO to consider.</param>
    /// <returns>The poco object.</returns>
    public static IBdoExpression ToPoco(
        this ExpressionDto dto)
    {
        if (dto == null) return null;

        IBdoExpression poco;

        if (dto.ExpressionKind == BdoExpressionKind.Word)
        {
            poco = dto.Scriptword.ToPoco();
        }
        else
        {
            poco = new BdoExpression()
            {
                Identifier = dto.Identifier,
                ExpressionKind = dto.ExpressionKind,
                Text = dto.Text
            };
        }

        return poco;
    }
}
