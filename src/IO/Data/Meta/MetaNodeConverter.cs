using AutoMapper;
using BindOpen.Data;
using BindOpen.Data.Assemblies;
using BindOpen.Data.Schema;
using BindOpen.Scoping.Script;
using System.Linq;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a IO converter of meta nodes.
    /// </summary>
    public static class MetaNodeConverter
    {
        /// <summary>
        /// Converts an expression poco into a DTO one.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static MetaNodeDto ToDto(this IBdoMetaNode poco)
        {
            MetaNodeDto dto = new();
            dto.UpdateFromPoco(poco);

            return dto;
        }

        public static MetaNodeDto UpdateFromPoco(
            this MetaNodeDto dto,
            IBdoMetaNode poco)
        {
            if (dto == null) return null;

            if (poco == null) return dto;

            MapperConfiguration config;

            config = new MapperConfiguration(
                cfg => cfg.CreateMap<IBdoMetaNode, MetaNodeDto>()
                    .ForMember(q => q.ClassReference, opt => opt.Ignore())
                    .ForMember(q => q.Reference, opt => opt.MapFrom(q => q.Reference.ToDto()))
                    .ForMember(q => q.MetaItems, opt => opt.Ignore())
                    .ForMember(q => q.Schema, opt => opt.MapFrom(q => q.Schema.ToDto()))
            );

            var mapper = new Mapper(config);
            mapper.Map(poco, dto);

            dto.ClassReference = poco.DataType.IsSpecified() ? poco?.DataType.ToDto() : null;
            dto.DefinitionUniqueName = poco?.DataType?.DefinitionUniqueName;

            dto.MetaItems = poco.Items?.Select(q => q.ToDto()).ToList();
            dto.ValueType = poco?.DataType.ValueType ?? DataValueTypes.Any;
            if (poco.Schema?.DataType.ValueType == poco.DataType?.ValueType)
            {
                dto.ValueType = DataValueTypes.Any;
            }

            return dto;
        }

        /// <summary>
        /// Converts a meta node DTO to a poco one.
        /// </summary>
        /// <param key="dto">The DTO to consider.</param>
        /// <returns>The poco object.</returns>
        public static IBdoMetaNode ToPoco(
            this MetaNodeDto dto)
        {
            if (dto == null) return null;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<MetaNodeDto, BdoMetaNode>()
                    .ForMember(q => q.Reference, opt => opt.MapFrom(q => q.Reference.ToPoco()))
                    .ForMember(q => q.DataType, opt => opt.Ignore())
                    .ForMember(q => q.Items, opt => opt.Ignore())
                    .ForMember(q => q.Parent, opt => opt.Ignore())
                    .ForMember(q => q.Schema, opt => opt.Ignore())
                );

            var mapper = new Mapper(config);
            var poco = mapper.Map<BdoMetaNode>(dto);

            poco.DataType = new BdoDataType(dto?.ClassReference?.ToPoco())
            {
                DefinitionUniqueName = dto.DefinitionUniqueName,
                Identifier = dto.Identifier,
                ValueType = dto.ValueType
            };
            poco.Schema = dto.Schema.ToPoco();

            poco.With(dto.MetaItems?.Select(q => q.ToPoco()).ToArray());

            return poco;
        }
    }
}
