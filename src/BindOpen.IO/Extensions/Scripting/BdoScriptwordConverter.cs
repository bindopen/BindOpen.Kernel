using AutoMapper;
using BindOpen.Data.Meta.Reflection;
using System.Linq;

namespace BindOpen.Extensions.Scripting
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
                    .ForMember(q => q.it, opt => opt.MapFrom(q => q.Select(p => q.ToDto()).ToList()))
                    .ForMember(q => q.Child, opt => opt.MapFrom(q => q.Child.ToDto()))
                    .ForMember(q => q.Kind, opt => opt.MapFrom(q => q.Kind))
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

            //var config = new MapperConfiguration(
            //       cfg => cfg.CreateMap<ScriptwordDto, BdoScriptword>()
            //           .ForMember(q => q.ite, opt => opt.MapFrom(q => q.MetaItems == null ? null : q.MetaItems.Select(q => q.ToPoco()).ToList()))
            //           .ForMember(q => q.Child, opt => opt.MapFrom(q => q.Child.ToPoco()))
            //           .ForMember(q => q.Kind, opt => opt.MapFrom(q => q.Kind))
            //       );

            //var mapper = new Mapper(config);
            //var poco = mapper.Map<BdoScriptword>(dto);

            return poco;
        }
    }
}
