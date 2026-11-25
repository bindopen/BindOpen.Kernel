using BindOpen.Data;
using BindOpen.Data.Assemblies;
using BindOpen.Data.Meta.Reflection;
using BindOpen.Data.Schema;
using BindOpen.Scoping.Script;
using System.Linq;

namespace BindOpen.Data.Meta;

/// <summary>
/// Provides extension methods for converting between <see cref="IBdoMetaObject"/> domain instances
/// and <see cref="MetaObjectDto"/> data transfer objects.
/// </summary>
public static class MetaObjectConverter
{
    /// <summary>
    /// Converts an <see cref="IBdoMetaObject"/> domain object into a <see cref="MetaObjectDto"/>.
    /// </summary>
    /// <param name="poco">The <see cref="IBdoMetaObject"/> instance to convert.</param>
    /// <returns>The converted <see cref="MetaObjectDto"/>, or <c>null</c> if <paramref name="poco"/> is <c>null</c>.</returns>
    public static MetaObjectDto ToDto(this IBdoMetaObject poco)
    {
        if (poco == null) return null;

        MetaObjectDto dto = new();
        dto.UpdateFromPoco(poco);

        return dto;
    }

    /// <summary>
    /// Updates an existing <see cref="MetaObjectDto"/> from an <see cref="IBdoMetaObject"/> instance,
    /// ensuring underlying object data is synchronized into its meta items before mapping.
    /// </summary>
    /// <param name="dto">The target <see cref="MetaObjectDto"/> to update.</param>
    /// <param name="poco">The source <see cref="IBdoMetaObject"/> instance.</param>
    /// <returns>The updated <see cref="MetaObjectDto"/>, or <c>null</c> if <paramref name="dto"/> is <c>null</c>.</returns>
    public static MetaObjectDto UpdateFromPoco(
        this MetaObjectDto dto,
        IBdoMetaObject poco)
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
    /// Converts a <see cref="MetaObjectDto"/> into an <see cref="IBdoMetaObject"/> domain object.
    /// </summary>
    /// <param name="dto">The <see cref="MetaObjectDto"/> instance to convert.</param>
    /// <returns>The converted <see cref="IBdoMetaObject"/>, or <c>null</c> if <paramref name="dto"/> is <c>null</c>.</returns>
    public static IBdoMetaObject ToPoco(
        this MetaObjectDto dto)
    {
        if (dto == null) return null;

        BdoMetaObject poco = new();
        poco.UpdateFromDto(dto);

        return poco;
    }

    /// <summary>
    /// Updates an existing <see cref="IBdoMetaObject"/> from a <see cref="MetaObjectDto"/> instance,
    /// restoring its meta items and synchronizing its underlying object data.
    /// </summary>
    /// <param name="poco">The target <see cref="IBdoMetaObject"/> to update.</param>
    /// <param name="dto">The source <see cref="MetaObjectDto"/> instance.</param>
    /// <returns>The updated <see cref="IBdoMetaObject"/>, or <c>null</c> if <paramref name="poco"/> is <c>null</c>.</returns>
    public static IBdoMetaObject UpdateFromDto(
        this IBdoMetaObject poco,
        MetaObjectDto dto)
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
        poco.AddItems(dto.MetaItems?.Select(q => q.ToPoco()).ToArray());

        poco.UpdateObjectData();

        return poco;
    }
}