using AutoMapper;
using BindOpen.Data;
using BindOpen.Data.Assemblies;
using BindOpen.Scoping.Script;
using System.Linq;

namespace BindOpen.Data.Meta;

/// <summary>
/// This class represents a IO converter of meta objects.
/// </summary>
public static class MetaObjectConverter
{
    /// <summary>
    /// Converts an expression poco into a DTO one.
    /// </summary>
    /// <param key="poco">The poco to consider.</param>
    /// <returns>The DTO object.</returns>
    public static MetaObjectDto ToDto(this IBdoMetaObject poco)
    {
        MetaObjectDto dto = new();
        dto.UpdateFromPoco(poco);

        return dto;
    }

    public static MetaObjectDto UpdateFromPoco(
        this MetaObjectDto dto,
        IBdoMetaObject poco)
    {
        if (dto == null) return null;

        if (poco == null) return dto;

        MapperConfiguration config;

        config = new MapperConfiguration(
            cfg => cfg.CreateMap<IBdoMetaObject, MetaObjectDto>()
                .ForMember(q => q.ClassReference, opt => opt.Ignore())
                .ForMember(q => q.Reference, opt => opt.MapFrom(q => q.Reference.ToDto()))
                //.ForMember(q => q.Item, opt => opt.Ignore())
                .ForMember(q => q.MetaItems, opt => opt.Ignore())
                .ForMember(q => q.Spec, opt => opt.MapFrom(q => q.Spec.ToDto()))
        );

        var mapper = new Mapper(config);
        mapper.Map(poco, dto);

        dto.ClassReference = poco.DataType.IsSpecified() ? poco?.DataType.ToDto() : null;
        dto.DefinitionUniqueName = poco?.DataType?.DefinitionUniqueName;

        dto.MetaItems = poco.Items?.Select(q => q.ToDto()).ToList();
        dto.ValueType = poco?.DataType.ValueType ?? DataValueTypes.Any;
        if (poco.Spec?.DataType.ValueType == poco.DataType?.ValueType
            || poco.DataType.ValueType == DataValueTypes.Object)
        {
            dto.ValueType = DataValueTypes.Any;
        }
        if (dto.Spec?.ValueType == DataValueTypes.Object)
        {
            dto.Spec.ValueType = DataValueTypes.Any;
        }

        return dto;
    }

    /// <summary>
    /// Converts a meta object DTO to a poco one.
    /// </summary>
    /// <param key="dto">The DTO to consider.</param>
    /// <returns>The poco object.</returns>
    public static IBdoMetaObject ToPoco(
        this MetaObjectDto dto)
    {
        if (dto == null) return null;

        var config = new MapperConfiguration(
            cfg => cfg.CreateMap<MetaObjectDto, BdoMetaObject>()
                .ForMember(q => q.Reference, opt => opt.MapFrom(q => q.Reference.ToPoco()))
                .ForMember(q => q.DataType, opt => opt.Ignore())
                .ForMember(q => q.Items, opt => opt.Ignore())
                .ForMember(q => q.Parent, opt => opt.Ignore())
                .ForMember(q => q.Spec, opt => opt.Ignore())
            );

        var mapper = new Mapper(config);
        var poco = mapper.Map<BdoMetaObject>(dto);

        poco.DataType = new BdoDataType(dto?.ClassReference?.ToPoco())
        {
            DefinitionUniqueName = dto.DefinitionUniqueName,
            Identifier = dto.Identifier,
            ValueType = dto.ValueType
        };
        poco.Spec = dto.Spec.ToPoco();

        poco.With(dto.MetaItems?.Select(q => q.ToPoco()).ToArray());

        return poco;
    }
}
