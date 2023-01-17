using AutoMapper;
using BindOpen.Extensions.Scripting;

namespace BindOpen.MetaData.Items
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class ExpressionConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static ExpressionDto ToDto(this IBdoExpression poco)
        {
            if (poco == null) return null;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<BdoExpression, ExpressionDto>());

            var mapper = new Mapper(config);
            var dto = mapper.Map<ExpressionDto>(poco);
            dto.Word = poco.Word?.ToDto();

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoExpression ToPoco(this ExpressionDto dto)
        {
            if (dto == null) return null;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<ExpressionDto, BdoExpression>());

            var mapper = new Mapper(config);
            var poco = mapper.Map<BdoExpression>(dto);
            poco.Word = dto.Word?.ToPoco<BdoScriptword>();

            return poco;
        }
    }
}
