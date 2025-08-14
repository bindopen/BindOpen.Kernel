using BindOpen.Data;
using BindOpen.Data.Assemblies;
using BindOpen.Data.Schema;
using BindOpen.Scoping.Script;
using System.Linq;

namespace BindOpen.Data.Meta;

/// <summary>
/// This class represents a IO converter of meta nodes.
/// </summary>
public static class MetaNodeConverter
{
    /// <summary>
    /// Converts an assembly reference poco into a DTO one.
    /// </summary>
    /// <param key="poco">The poco to consider.</param>
    /// <returns>The DTO object.</returns>
    public static MetaNodeDto ToDto(this IBdoMetaNode poco)
    {
        if (poco == null) return null;

        MetaNodeDto dto = new();
        dto.UpdateFromPoco(poco);

        return dto;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="poco"></param>
    /// <returns></returns>
    public static MetaNodeDto UpdateFromPoco(
        this MetaNodeDto dto,
        IBdoMetaNode poco)
    {
        if (dto == null) return null;

        if (poco == null) return dto;

        dto.ClassReference = poco.DataType.IsSpecified() ? poco.DataType.ToDto() : null;
        dto.DefinitionUniqueName = poco?.DataType?.DefinitionUniqueName;
        dto.Index = poco.Index;
        dto.Identifier = poco.Identifier;
        dto.MetaItems = poco.Items?.Select(q => q.ToDto()).ToList();
        dto.Name = poco.Name;
        dto.Reference = poco.Reference.ToDto();
        dto.Schema = poco.Schema.ToDto();
        dto.ValueType = poco.DataType?.ValueType ?? DataValueTypes.Any;
        if (poco.Schema?.DataType.ValueType == poco.DataType?.ValueType)
        {
            dto.ValueType = DataValueTypes.Any;
        }

        return dto;
    }

    /// <summary>
    /// Converts an assembly reference DTO into a poco one.
    /// </summary>
    /// <param key="dto">The DTO to consider.</param>
    /// <returns>The poco object.</returns>
    public static IBdoMetaNode ToPoco(
        this MetaNodeDto dto)
    {
        if (dto == null) return null;

        BdoMetaNode poco = new();
        poco.UpdateFromDto(dto);

        return poco;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="poco"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    public static IBdoMetaNode UpdateFromDto(
        this IBdoMetaNode poco,
        MetaNodeDto dto)
    {
        if (poco == null) return null;

        if (dto == null) return poco;

        poco.DataType = new BdoDataType(dto?.ClassReference?.ToPoco())
        {
            DefinitionUniqueName = dto.DefinitionUniqueName,
            Identifier = dto.Identifier,
            ValueType = dto.ValueType
        };
        poco.Index = dto.Index;
        poco.Identifier = dto.Identifier;
        poco.Name = dto.Name;
        poco.Reference = dto.Reference.ToPoco();
        poco.Schema = dto.Schema.ToPoco();
        poco.With(dto.MetaItems?.Select(q => q.ToPoco()).ToArray());

        return poco;
    }
}
