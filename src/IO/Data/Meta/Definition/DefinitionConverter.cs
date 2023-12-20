using AutoMapper;
using BindOpen.Data.Helpers;
using System.Linq;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a IO converter of definitions.
    /// </summary>
    public static class DefinitionConverter
    {
        /// <summary>
        /// Converts a definition poco into a DTO one.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static DefinitionDto ToDto(this IBdoDefinition poco) => poco.ToDto<DefinitionDto>();

        /// <summary>
        /// Converts a definition poco of the specified class into a DTO one.
        /// </summary>
        /// <typeparam name="T">The type of configuration to consider.</typeparam>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static T ToDto<T>(this IBdoDefinition poco)
            where T : DefinitionDto
        {
            if (poco == null) return null;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<IBdoDefinition, T>()
                    .ForMember(q => q.CreationDate, opt => opt.MapFrom(q => StringHelper.ToString(q.CreationDate)))
                    .ForMember(q => q.Description, opt => opt.MapFrom(q => q.Description.ToDto()))
                    .ForMember(q => q.Items, opt => opt.MapFrom(q => q.Items == null ? null : q.Items.Select(q => q.ToDto()).ToList()))
                    .ForMember(q => q.LastModificationDate, opt => opt.MapFrom(q => StringHelper.ToString(q.CreationDate)))
                    .ForMember(q => q.Title, opt => opt.MapFrom(q => q.Title.ToDto()))
                    .ForMember(q => q.UsedItemIds, opt => opt.MapFrom(q => q.UsedItemIds == null ? null : q.UsedItemIds.Select(q => q).ToList()))
            );

            var mapper = new Mapper(config);
            var dto = mapper.Map<T>(poco);

            return dto;
        }

        /// <summary>
        /// Converts a definition DTO to a poco one.
        /// </summary>
        /// <param key="dto">The DTO to consider.</param>
        /// <returns>The poco object.</returns>
        public static IBdoDefinition ToPoco(this DefinitionDto dto) => dto.ToPoco<BdoDefinition>();

        /// <summary>
        /// Converts a definition DTO of the specified class to a poco one.
        /// </summary>
        /// <typeparam name="T">The type of configuration to consider.</typeparam>
        /// <param key="dto">The DTO to consider.</param>
        /// <returns>The poco object.</returns>
        public static T ToPoco<T>(this DefinitionDto dto)
            where T : IBdoDefinition
        {
            if (dto == null) return default;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<DefinitionDto, T>()
                    .ForMember(q => q.CreationDate, opt => opt.MapFrom(q => q.CreationDate.ToDateTime(null)))
                    .ForMember(q => q.Description, opt => opt.Ignore())
                    .ForMember(q => q.Items, opt => opt.Ignore())
                    .ForMember(q => q.LastModificationDate, opt => opt.MapFrom(q => q.CreationDate.ToDateTime(null)))
                    .ForMember(q => q.Title, opt => opt.Ignore())
                    .ForMember(q => q.UsedItemIds, opt => opt.MapFrom(q => q.UsedItemIds == null ? null : q.UsedItemIds.Select(q => q).ToList()))
            );

            var mapper = new Mapper(config);
            var poco = mapper.Map<T>(dto);
            poco
                .WithTitle(dto.Title.ToPoco<string>())
                .WithDescription(dto.Description.ToPoco<string>())
                .With(dto.Items.Select(q => q.ToPoco()).ToArray());

            return poco;
        }
    }
}
