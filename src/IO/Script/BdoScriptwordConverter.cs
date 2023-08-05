using AutoMapper;
using BindOpen.System.Data;
using BindOpen.System.Data.Assemblies;
using BindOpen.System.Data.Meta;
using BindOpen.System.Scoping.Script;
using System.Linq;

namespace BindOpen.System.Scoping.Script
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
                    .ForMember(q => q.Child, opt => opt.MapFrom(q => q.Child.ToDto()))
                    .ForMember(q => q.ClassReference, opt => opt.Ignore())
                    .ForMember(q => q.Reference, opt => opt.Ignore())
                    .ForMember(q => q.Item, opt => opt.Ignore())
                    .ForMember(q => q.MetaItems, opt => opt.MapFrom(q => q.Select(q => q.ToDto()).ToList()))
                    .ForMember(q => q.Specs, opt => opt.Ignore())
                );

            var mapper = new Mapper(config);
            var dto = mapper.Map<ScriptwordDto>(poco);

            dto.ValueType = poco?.DataType.ValueType ?? DataValueTypes.Any;
            dto.ClassReference = poco?.DataType.ClassReference?.ToDto();

            dto.MetaItems = poco.Items?.Select(q => q.ToDto()).ToList();
            dto.Specs = poco.Specs?.Select(q => q.ToDto()).ToList();

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
                        .ForMember(q => q.DataType, opt => opt.Ignore())
                        .ForMember(q => q.Items, opt => opt.Ignore())
                        .ForMember(q => q.Parent, opt => opt.Ignore())
                        .ForMember(q => q.Reference, opt => opt.Ignore())
                        .ForMember(q => q.Specs, opt => opt.Ignore())
                   );

            var mapper = new Mapper(config);
            var poco = mapper.Map<BdoScriptword>(dto);

            poco.Child = dto.Child.ToPoco();
            poco.DataType = new()
            {
                ClassReference = dto.ClassReference.ToPoco(),
                ValueType = dto.ValueType
            };
            poco.Reference = dto.Reference.ToPoco();
            var specs = dto.Specs?.Select(q => q.ToPoco())?.ToArray();
            poco.Specs = specs?.Length > 0 ? BdoData.NewSet<IBdoSpec>(specs) : null;

            poco.With(dto.MetaItems?.Select(q => q.ToPoco()).ToArray());

            return poco;
        }
    }
}
