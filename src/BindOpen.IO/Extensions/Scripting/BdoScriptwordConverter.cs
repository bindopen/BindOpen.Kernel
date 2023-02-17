using AutoMapper;
using BindOpen.Data;
using BindOpen.Data.Configuration;
using BindOpen.Data.Items;
using BindOpen.Data.Meta;
using BindOpen.Data.Meta.Reflection;
using BindOpen.Logging;
using BindOpen.Runtime.Scopes;
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
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static ScriptwordConfigurationDto ToDto(this IBdoScriptword poco)
        {
            if (poco == null) return null;

            var items = poco.Parameters
                .ToMetaData(true)
                .AsMetaList()?.Select(p => p.ToDto()).ToList();

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<IBdoScriptword, ScriptwordConfigurationDto>()
                    .ForMember(q => q.DataExpression, opt => opt.Ignore())
                    .ForMember(q => q.DefinitionUniqueName, opt => opt.MapFrom(q => q.Definition == null ? null : q.Definition.UniqueName))
                    .ForMember(q => q.Description, opt => opt.Ignore())
                    .ForMember(q => q.ExtensionKind, opt => opt.MapFrom(q => BdoExtensionItemKind.Scriptword))
                    .ForMember(q => q.Items, opt => opt.MapFrom(q => items))
                    .ForMember(q => q.SubScriptword, opt => opt.MapFrom(q => q.SubScriptword.ToDto()))
                    .ForMember(q => q.Title, opt => opt.Ignore())
                    .ForMember(q => q.UsedItemIds, opt => opt.Ignore())
                    .ForMember(q => q.WordKind, opt => opt.MapFrom(q => q.Kind))
                );

            var mapper = new Mapper(config);
            var dto = mapper.Map<ScriptwordConfigurationDto>(poco);

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoScriptword ConvertToPoco(
            this IBdoScope scope,
            ScriptwordConfigurationDto dto,
            IBdoLog log = null)
        {
            if (dto == null) return null;

            var metas = dto.Items.Select(p => scope.ConvertToPoco(p, log)).ToArray();
            var config = BdoConfig.New(metas);

            scope.CreateConnector()
            var poco = Bdo.NewScriptword(config);
            poco.DefinitionUniqueName = dto.DefinitionUniqueName;
            poco.SubScriptword = scope.ConvertToPoco(dto.SubScriptword, log);
            poco. = scope.ConvertToPoco(dto.SubScriptword, log);

                    .ForMember(q => q.DataExpression, opt => opt.Ignore())
                    .ForMember(q => q.DefinitionUniqueName, opt => opt.MapFrom(q => q.Definition == null ? null : q.Definition.UniqueName))
                    .ForMember(q => q.Description, opt => opt.Ignore())
                    .ForMember(q => q., opt => opt.MapFrom(q => BdoExtensionItemKind.Scriptword))
                    .ForMember(q => q.Items, opt => opt.MapFrom(q => items))
                    .ForMember(q => q.SubScriptword, opt => opt.MapFrom(q => q.SubScriptword.ToDto()))
                    .ForMember(q => q.Title, opt => opt.Ignore())
                    .ForMember(q => q.UsedItemIds, opt => opt.Ignore())
                    .ForMember(q => q.WordKind, opt => opt.MapFrom(q => q.Kind))

            return poco;
        }
    }
}
