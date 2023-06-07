using AutoMapper;
using BindOpen.Scoping.Data.Meta;
using System.Linq;

namespace BindOpen.Scoping.Script
{
    /// <summary>
    /// This class represents a task converter.
    /// </summary>
    public static class BdoScriptwordConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static ScriptwordDto ToDto(this IBdoScriptword poco)
        {
            if (poco == null) return null;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<IBdoScriptword, ScriptwordDto>()
                    .ForMember(q => q.MetaItems, opt => opt.MapFrom(q => q.Select(q => q.ToDto()).ToList()))
                    .ForMember(q => q.Item, opt => opt.Ignore())
                    .ForMember(q => q.Child, opt => opt.MapFrom(q => q.Child.ToDto()))
                    .ForMember(q => q.SubSet, opt => opt.Ignore())
                    .ForMember(q => q.DataReference, opt => opt.Ignore())
                    .ForMember(q => q.Specs, opt => opt.Ignore())
                );

            var mapper = new Mapper(config);
            var dto = mapper.Map<ScriptwordDto>(poco);

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoScriptword ToPoco(
            this ScriptwordDto dto)
        {
            if (dto == null) return null;

            var config = new MapperConfiguration(
                   cfg => cfg.CreateMap<ScriptwordDto, BdoScriptword>()
                       .ForMember(q => q.Child, opt => opt.Ignore())
                       .ForMember(q => q.Reference, opt => opt.Ignore())
                       .ForMember(q => q.Items, opt => opt.Ignore())
                       .ForMember(q => q.Parent, opt => opt.Ignore())
                       .ForMember(q => q.Specs, opt => opt.Ignore())
                   );

            var mapper = new Mapper(config);
            var poco = new BdoScriptword()
            {
                Child = dto.Child.ToPoco()
            };
            poco.With(dto.MetaItems.Select(q => q.ToPoco()).ToArray());

            return poco;
        }
    }
}
