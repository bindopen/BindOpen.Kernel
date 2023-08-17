﻿using AutoMapper;
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
    public static class ScriptwordConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static ScriptwordDto ToDto(this IBdoScriptword poco, bool root = true)
        {
            if (poco == null) return null;

            if (root) poco = poco.Root() as IBdoScriptword;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<IBdoScriptword, ScriptwordDto>()
                    .ForMember(q => q.Child, opt => opt.MapFrom(q => q.Child.ToDto(false)))
                    .ForMember(q => q.ClassReference, opt => opt.Ignore())
                    .ForMember(q => q.DataReference, opt => opt.MapFrom(q => q.DataReference.ToDto()))
                    .ForMember(q => q.Item, opt => opt.Ignore())
                    .ForMember(q => q.MetaItems, opt => opt.Ignore())
                    .ForMember(q => q.Spec, opt => opt.MapFrom(q => q.Spec.ToDto()))
                );

            var mapper = new Mapper(config);
            var dto = mapper.Map<ScriptwordDto>(poco);

            dto.ClassReference = poco.DataType.IsSpecified() ? poco?.DataType.ToDto() : null;
            dto.DefinitionUniqueName = poco?.DataType?.DefinitionUniqueName;

            dto.MetaItems = poco.Items?.Select(q => q.ToDto()).ToList();
            dto.ValueType = poco?.DataType.ValueType ?? DataValueTypes.Any;
            if (poco.Spec?.DataType.ValueType == poco.DataType?.ValueType
                || (poco.DataType.ValueType == DataValueTypes.Scriptword))
            {
                dto.ValueType = DataValueTypes.Any;
            }

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
                    .ForMember(q => q.DataReference, opt => opt.MapFrom(q => q.DataReference.ToPoco()))
                    .ForMember(q => q.DataType, opt => opt.Ignore())
                    .ForMember(q => q.Items, opt => opt.Ignore())
                    .ForMember(q => q.Parent, opt => opt.Ignore())
                    .ForMember(q => q.DataReference, opt => opt.Ignore())
                    .ForMember(q => q.Spec, opt => opt.MapFrom(q => q.Spec.ToPoco()))
                );

            var mapper = new Mapper(config);
            var poco = mapper.Map<BdoScriptword>(dto);

            poco.Child = dto.Child.ToPoco();

            poco.DataType = new BdoDataType(dto?.ClassReference?.ToPoco());
            poco.DataType.DefinitionUniqueName = dto.DefinitionUniqueName;
            poco.DataType.ValueType = dto.ValueType;

            poco.With(dto.MetaItems?.Select(q => q.ToPoco()).ToArray());

            return poco;
        }
    }
}