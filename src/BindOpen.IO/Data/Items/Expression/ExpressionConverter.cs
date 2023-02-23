using AutoMapper;
using BindOpen.Extensions.Scripting;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class ExpressionConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static ExpressionDto ToDto(this IBdoExpression poco)
        {
            if (poco == null) return null;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<BdoExpression, ExpressionDto>());

            var mapper = new Mapper(config);
            var dto = mapper.Map<ExpressionDto>(poco);

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoExpression ToPoco(
            this ExpressionDto dto)
        {
            if (dto == null) return null;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<ExpressionDto, BdoExpression>()
                    .ForMember(q => q.Word, opt => opt.Ignore()));

            var mapper = new Mapper(config);
            var poco = mapper.Map<BdoExpression>(dto);

            if (poco.Kind == BdoExpressionKind.Word)
            {
                poco.Word = BdoScript.NewWordFromScript(poco.Text);
                poco.Text = "";
            }

            return poco;
        }
    }
}
