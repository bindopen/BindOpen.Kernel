using AutoMapper;
using BindOpen.Extensions.Scripting;
using BindOpen.Runtime.Scopes;

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
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static ExpressionDto ToDto(this IBdoExpression poco)
        {
            if (poco == null) return null;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<BdoExpression, ExpressionDto>()
                    .ForMember(q => q.Word, opt => opt.MapFrom(q => q.Word.ToDto())));

            var mapper = new Mapper(config);
            var dto = mapper.Map<ExpressionDto>(poco);

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoExpression ToPoco(
            this ExpressionDto dto,
            IBdoScope scope)
        {
            if (dto == null) return null;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<ExpressionDto, BdoExpression>()
                    .ForMember(q => q.Word, opt => opt.MapFrom(q => q.Word.ToPoco(scope))));

            var mapper = new Mapper(config);
            var poco = mapper.Map<BdoExpression>(dto);

            return poco;
        }
    }
}
