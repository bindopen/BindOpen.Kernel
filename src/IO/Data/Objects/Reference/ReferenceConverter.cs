using AutoMapper;
using BindOpen.System.Data.Meta;
using BindOpen.System.Scoping.Script;

namespace BindOpen.System.Data
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class ReferenceConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static ReferenceDto ToDto(this IBdoReference poco)
        {
            if (poco == null) return null;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<BdoReference, ReferenceDto>()
                    .ForMember(q => q.MetaData, opt => opt.MapFrom(q => q.MetaData.ToDto()))
                    .ForMember(q => q.Word, opt => opt.MapFrom(q => q.Word.ToDto(true)))
            );

            var mapper = new Mapper(config);
            var dto = mapper.Map<ReferenceDto>(poco);

            dto.ExpressionKind = poco.Expression?.Kind ?? BdoExpressionKind.Literal;
            dto.Text = poco.Expression?.Text;

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoReference ToPoco(
            this ReferenceDto dto)
        {
            if (dto == null) return null;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<ReferenceDto, BdoReference>()
                    .ForMember(q => q.MetaData, opt => opt.MapFrom(q => q.MetaData.ToPoco()))
                    .ForMember(q => q.Word, opt => opt.Ignore())
            );

            var mapper = new Mapper(config);
            var poco = mapper.Map<BdoReference>(dto);

            poco.Word = dto.Word.ToPoco();

            if (poco.Kind == BdoReferenceKind.Expression)
            {
                poco.Expression = new BdoExpression()
                {
                    Kind = dto.ExpressionKind,
                    Text = dto.Text
                };
            }

            return poco;
        }
    }
}
