using AutoMapper;
using BindOpen.Kernel.Scoping.Script;

namespace BindOpen.Kernel.Data
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

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<BdoExpression, ExpressionDto>()
                    .ForMember(q => q.Word, opt => opt.MapFrom(q => q.Word.ToDto(true)))
            );

            var mapper = new Mapper(config);
            var dto = mapper.Map<ExpressionDto>(poco);

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

            var poco = new BdoExpression()
            {
                Kind = dto.Kind,
                Text = dto.Text,
                Word = dto.Word.ToPoco()
            };

            return poco;
        }
    }
}
