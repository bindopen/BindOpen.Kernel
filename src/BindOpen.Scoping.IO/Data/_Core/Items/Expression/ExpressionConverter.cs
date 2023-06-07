using AutoMapper;

namespace BindOpen.Scoping.Data
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
                cfg => cfg.CreateMap<BdoExpression, ExpressionDto>()
            );

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

            var poco = new BdoExpression()
            {
                Kind = dto.Kind,
                Text = dto.Text
            };

            return poco;
        }
    }
}
