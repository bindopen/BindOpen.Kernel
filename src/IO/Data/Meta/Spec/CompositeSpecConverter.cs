using AutoMapper;
using BindOpen.System.Data.Assemblies;
using BindOpen.System.Data.Conditions;
using BindOpen.System.Data.Helpers;
using BindOpen.System.Data.Meta.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class CompositeSpecConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static CompositeSpecDto ToDto(this IBdoCompositeSpec poco)
        {
            if (poco == null) return null;

            if (poco == null) return null;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<BdoCompositeSpec, CompositeSpecDto>()
                    .ForMember(q => q.ClassReference, opt => opt.Ignore())
                    .ForMember(q => q.Children, opt => opt.Ignore())
                    .ForMember(q => q.Condition, opt => opt.MapFrom(q => q.Condition.ToDto()))
                    .ForMember(q => q.DefaultItems, opt => opt.Ignore())
                    .ForMember(q => q.Description, opt => opt.MapFrom(q => q.Description.ToDto()))
                    .ForMember(q => q.Detail, opt => opt.MapFrom(q => q.Detail.ToDto()))
                    .ForMember(q => q.Title, opt => opt.MapFrom(q => q.Title.ToDto()))
            );

            var mapper = new Mapper(config);
            var dto = mapper.Map<CompositeSpecDto>(poco);

            dto.Aliases = poco?.Aliases == null ? null : new List<string>(poco.Aliases);
            dto.AvailableDataModes = poco?.AvailableDataModes == null ? null : new List<DataMode>(poco.AvailableDataModes);

            dto.Children = poco?._Children?.Select(q => q.ToDto()).ToList();
            dto.ClassReference = poco?.DataType.ClassReference?.ToDto();

            dto.DataSpecLevels = poco?.DataSpecLevels == null ? null : new List<SpecificationLevels>(poco.DataSpecLevels);
            dto.IsAllocatable = poco?.IsAllocatable == false ? null : poco?.IsAllocatable;
            dto.IsStatic = poco?.IsStatic == false ? null : poco?.IsStatic;
            dto.MaxDataItemNumber = (int?)(poco?.MaxDataItemNumber == -1 ? null : poco?.MaxDataItemNumber);
            dto.MinDataItemNumber = (int?)(poco?.MinDataItemNumber == 0 ? null : poco?.MinDataItemNumber);
            dto.SpecLevels = poco?.SpecLevels == null ? null : new List<SpecificationLevels>(poco.SpecLevels);

            dto.ValueType = poco?.DataType.ValueType ?? DataValueTypes.Any;

            var dataList = poco.DefaultData?.ToObjectList().Select(q => q?.ToMeta().ToDto()).ToList();
            dto.DefaultItems = dataList;

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoCompositeSpec ToPoco(
            this CompositeSpecDto dto)
        {
            if (dto == null) return null;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<CompositeSpecDto, BdoCompositeSpec>()
                    .ForMember(q => q._Children, opt => opt.Ignore())
                    .ForMember(q => q.Condition, opt => opt.MapFrom(q => q.Condition.ToPoco()))
                    .ForMember(q => q.DataType, opt => opt.Ignore())
                    .ForMember(q => q.Description, opt => opt.Ignore())
                    .ForMember(q => q.DefaultData, opt => opt.Ignore())
                    .ForMember(q => q.Detail, opt => opt.Ignore())
                    .ForMember(q => q.Title, opt => opt.Ignore())
                );

            var mapper = new Mapper(config);
            var poco = mapper.Map<BdoCompositeSpec>(dto);

            poco._Children = dto?.Children?.Select(q => q.ToPoco()).ToList();

            poco.DataType = new BdoDataType()
            {
                ClassReference = dto.ClassReference.ToPoco(),
                ValueType = dto.ValueType
            };
            poco
                .WithTitle(dto.Title.ToPoco<string>())
                .WithDescription(dto.Description.ToPoco<string>())
                .WithDetail(dto.Detail.ToPoco());

            return poco;
        }
    }
}
