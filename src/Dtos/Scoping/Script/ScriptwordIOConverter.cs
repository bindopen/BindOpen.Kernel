using BindOpen.Data;
using BindOpen.Data.Assemblies;
using BindOpen.Data.Meta;
using BindOpen.Data.Schema;
using BindOpen.Scoping.Script;
using System.Linq;

namespace BindOpen.Scoping.Script;

/// <summary>
/// This class represents a IO converter of script words.
/// </summary>
public static class ScriptwordIOConverter
{
    /// <summary>
    /// Converts an assembly reference poco into a DTO one.
    /// </summary>
    /// <param key="poco">The poco to consider.</param>
    /// <returns>The DTO object.</returns>
    public static ScriptwordDto ToDto(this IBdoScriptword poco)
    {
        if (poco == null) return null;

        ScriptwordDto dto = new();
        dto.UpdateFromPoco(poco);

        return dto;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="poco"></param>
    /// <returns></returns>
    public static ScriptwordDto UpdateFromPoco(
        this ScriptwordDto dto,
        IBdoScriptword poco)
    {
        if (dto == null) return null;

        if (poco == null) return dto;

        dto.Child = poco.Child.ToDto();
        dto.ClassReference = poco.DataType.IsSpecified() ? poco.DataType.ToDto() : null;
        dto.DefinitionUniqueName = poco?.DataType?.DefinitionUniqueName;
        dto.Index = poco.Index;
        dto.Identifier = poco.Identifier;
        dto.MetaItems = poco.Items?.Select(q => q.ToDto()).ToList();
        dto.Name = poco.Name;
        dto.Reference = poco.Reference.ToDto();
        dto.Schema = poco.Schema.ToDto();
        dto.ValueType = DataValueTypes.Any;
        dto.Text = poco.Text;

        return dto;
    }

    /// <summary>
    /// Converts an assembly reference DTO into a poco one.
    /// </summary>
    /// <param key="dto">The DTO to consider.</param>
    /// <returns>The poco object.</returns>
    public static IBdoScriptword ToPoco(
        this ScriptwordDto dto)
    {
        if (dto == null) return null;

        BdoScriptword poco = new();
        poco.UpdateFromDto(dto);

        return poco;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="poco"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    public static IBdoScriptword UpdateFromDto(
        this IBdoScriptword poco,
        ScriptwordDto dto)
    {
        if (poco == null) return null;

        if (dto == null) return poco;

        poco.DataType = new BdoDataType(dto?.ClassReference?.ToPoco())
        {
            DefinitionUniqueName = dto.DefinitionUniqueName,
            Identifier = dto.Identifier,
            ValueType = DataValueTypes.Scriptword
        };
        poco.Index = dto.Index;
        poco.Identifier = dto.Identifier;
        poco.Name = dto.Name;
        poco.Reference = dto.Reference.ToPoco();
        poco.Schema = dto.Schema.ToPoco();
        poco.Text = dto.Text;
        poco.WithChild(dto.Child.ToPoco());
        poco.With(dto.MetaItems?.Select(q => q.ToPoco()).ToArray());

        return poco;
    }
}
