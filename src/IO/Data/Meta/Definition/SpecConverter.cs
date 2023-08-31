using AutoMapper;
using BindOpen.System.Data;
using BindOpen.System.Data.Assemblies;
using BindOpen.System.Data.Conditions;
using BindOpen.System.Data.Helpers;
using BindOpen.System.Data.Meta;
using BindOpen.System.Data.Meta.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class SpecConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static SpecDto ToDto(this IBdoSpec poco)
        {
            if (poco == null) return null;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<BdoSpec, SpecDto>()
                    .ForMember(q => q.Aliases, opt => opt.Ignore())
                    .ForMember(q => q.AvailableDataModes, opt => opt.Ignore())
                    .ForMember(q => q.ItemSpecLevels, opt => opt.Ignore())
                    .ForMember(q => q.SpecLevels, opt => opt.Ignore())

                    .ForMember(q => q.Children, opt => opt.Ignore())
                    .ForMember(q => q.ClassReference, opt => opt.Ignore())
                    .ForMember(q => q.Condition, opt => opt.MapFrom(q => q.Condition.ToDto()))
                    .ForMember(q => q.ConstraintStatement, opt => opt.MapFrom(q => q.ConstraintStatement.ToDto<string>()))
                    .ForMember(q => q.Reference, opt => opt.MapFrom(q => q.Reference.ToDto()))
                    .ForMember(q => q.DefaultItems, opt => opt.Ignore())
                    .ForMember(q => q.Description, opt => opt.MapFrom(q => q.Description.ToDto()))
                    .ForMember(q => q.Detail, opt => opt.MapFrom(q => q.Detail.ToDto()))
                    .ForMember(q => q.RequirementStatement, opt => opt.MapFrom(q => q.RequirementStatement.ToDto()))
                    .ForMember(q => q.ItemRequirementStatement, opt => opt.MapFrom(q => q.ItemRequirementStatement.ToDto()))
                    .ForMember(q => q.Title, opt => opt.MapFrom(q => q.Title.ToDto()))
            );

            var mapper = new Mapper(config);
            var dto = mapper.Map<SpecDto>(poco);

            dto.Aliases = poco?.Aliases == null ? null : new List<string>(poco.Aliases);
            dto.AvailableDataModes = poco?.AvailableDataModes == null ? null : new List<DataMode>(poco.AvailableDataModes);

            dto.Children = poco?._Children?.Select(q => q.ToDto()).ToList();

            dto.ClassReference = poco.DataType.IsSpecified() ? poco?.DataType.ToDto() : null;
            dto.DefinitionUniqueName = poco?.DataType?.DefinitionUniqueName;

            dto.ItemSpecLevels = poco?.ItemSpecLevels == null ? null : new List<SpecificationLevels>(poco.ItemSpecLevels);
            dto.IsAllocatable = poco?.IsAllocatable == false ? null : poco?.IsAllocatable;
            dto.IsStatic = poco?.IsStatic == false ? null : poco?.IsStatic;
            dto.MaxDataItemNumber = (int?)(poco?.MaxDataItemNumber == -1 ? null : poco?.MaxDataItemNumber);
            dto.MinDataItemNumber = (int?)(poco?.MinDataItemNumber == 0 ? null : poco?.MinDataItemNumber);
            dto.SpecLevels = poco?.SpecLevels == null ? null : new List<SpecificationLevels>(poco.SpecLevels);

            dto.ValueType = poco?.DataType?.ValueType ?? DataValueTypes.Any;

            var dataList = poco.DefaultData?.ToObjectList().Select(q => q?.ToMeta().ToDto()).ToList();
            dto.DefaultItems = dataList;

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoSpec ToPoco(
            this SpecDto dto)
        {
            if (dto == null) return null;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<SpecDto, BdoSpec>()
                    .ForMember(q => q.Aliases, opt => opt.Ignore())
                    .ForMember(q => q.AvailableDataModes, opt => opt.Ignore())
                    .ForMember(q => q.ItemSpecLevels, opt => opt.Ignore())
                    .ForMember(q => q.SpecLevels, opt => opt.Ignore())

                    .ForMember(q => q._Children, opt => opt.Ignore())
                    .ForMember(q => q.Condition, opt => opt.MapFrom(q => q.Condition.ToPoco()))
                    .ForMember(q => q.ConstraintStatement, opt => opt.MapFrom(q => q.ConstraintStatement.ToPoco()))
                    .ForMember(q => q.Reference, opt => opt.MapFrom(q => q.Reference.ToPoco()))
                    .ForMember(q => q.DataType, opt => opt.Ignore())
                    .ForMember(q => q.Description, opt => opt.Ignore())
                    .ForMember(q => q.DefaultData, opt => opt.Ignore())
                    .ForMember(q => q.Detail, opt => opt.Ignore())
                    .ForMember(q => q.ItemRequirementStatement, opt => opt.MapFrom(q => q.ItemRequirementStatement.ToPoco()))
                    .ForMember(q => q.RequirementStatement, opt => opt.MapFrom(q => q.RequirementStatement.ToPoco()))
                    .ForMember(q => q.Title, opt => opt.Ignore())
                );

            var mapper = new Mapper(config);
            var poco = mapper.Map<BdoSpec>(dto);

            poco.Aliases = dto?.Aliases == null ? null : new List<string>(dto.Aliases);
            poco.AvailableDataModes = dto?.AvailableDataModes == null ? null : new List<DataMode>(dto.AvailableDataModes);

            poco._Children = BdoData.NewSet(dto?.Children?.Select(q => q.ToPoco()).ToArray());

            poco.DataType = new BdoDataType(dto?.ClassReference?.ToPoco());
            poco.DataType.DefinitionUniqueName = dto.DefinitionUniqueName;
            poco.DataType.ValueType = dto.ValueType;

            poco.ItemSpecLevels = dto?.ItemSpecLevels == null ? null : new List<SpecificationLevels>(dto.ItemSpecLevels);
            poco.SpecLevels = dto?.SpecLevels == null ? null : new List<SpecificationLevels>(dto.SpecLevels);

            poco
                .WithTitle(dto.Title.ToPoco<string>())
                .WithDescription(dto.Description.ToPoco<string>())
                .WithDetail(dto.Detail.ToPoco());

            return poco;
        }
    }
}
