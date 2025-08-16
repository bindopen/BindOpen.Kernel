namespace BindOpen.Data.Assemblies;

/// <summary>
/// This class represents a IO converter of assembly references.
/// </summary>
public static class AssemblyReferenceIOConverter
{
    /// <summary>
    /// Converts an assembly reference poco into a DTO one.
    /// </summary>
    /// <param key="poco">The poco to consider.</param>
    /// <returns>The DTO object.</returns>
    public static AssemblyReferenceDto ToDto(this IBdoAssemblyReference poco)
    {
        if (poco == null) return null;

        AssemblyReferenceDto dto = new();
        dto.UpdateFromPoco(poco);

        return dto;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="poco"></param>
    /// <returns></returns>
    public static AssemblyReferenceDto UpdateFromPoco(
        this AssemblyReferenceDto dto,
        IBdoAssemblyReference poco)
    {
        if (dto == null) return null;

        if (poco == null) return dto;

        dto.Alias = poco.Alias;
        dto.AssemblyFileName = poco.AssemblyFileName;
        dto.AssemblyName = poco.AssemblyName;
        dto.AssemblyVersion = poco.AssemblyVersion?.ToString();
        dto.Identifier = poco.Identifier;

        return dto;
    }

    /// <summary>
    /// Converts an assembly reference DTO into a poco one.
    /// </summary>
    /// <param key="dto">The DTO to consider.</param>
    /// <returns>The poco object.</returns>
    public static IBdoAssemblyReference ToPoco(
        this AssemblyReferenceDto dto)
    {
        if (dto == null) return null;

        BdoAssemblyReference poco = new();
        poco.UpdateFromDto(dto);

        return poco;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="poco"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    public static IBdoAssemblyReference UpdateFromDto(
        this IBdoAssemblyReference poco,
        AssemblyReferenceDto dto)
    {
        if (poco == null) return null;

        if (dto == null) return poco;

        poco.Alias = dto.Alias;
        poco.AssemblyFileName = dto.AssemblyFileName;
        poco.AssemblyName = dto.AssemblyName;
        poco.AssemblyVersion = dto.AssemblyVersion == null ? null : new(dto.AssemblyVersion);
        poco.Identifier = dto.Identifier;

        return poco;
    }
}
