using AutoMapper;
using BindOpen.Scoping.Script;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a IO converter of expressions.
    /// </summary>
    public static class ExpressionConverter
    {
        /// <summary>
        /// Converts an expression poco into a DTO one.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static ExpressionDto ToDto(this IBdoExpression poco)
        {
            if (poco == null) return null;

            ExpressionDto dto;

            if (poco is IBdoScriptword wordPoco)
            {
                dto = new ExpressionDto()
                {
                    ExpressionKind = BdoExpressionKind.Word,
                    Word = ScriptwordConverter.ToDto(wordPoco)
                };
            }
            else
            {
                var config = new MapperConfiguration(
                    cfg => cfg.CreateMap<BdoExpression, ExpressionDto>()
                );

                var mapper = new Mapper(config);
                dto = mapper.Map<ExpressionDto>(poco);
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
                poco = dto.Word.ToPoco();
            }
            else
            {
                poco = new BdoExpression()
                {
                    ExpressionKind = dto.ExpressionKind,
                    Text = dto.Text
                };
            }

            return poco;
        }
    }
}
