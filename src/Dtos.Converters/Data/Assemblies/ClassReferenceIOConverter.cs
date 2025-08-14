namespace BindOpen.Data.Assemblies;

/// <summary>
/// This class represents a IO converter of assembly references.
/// </summary>
public static class ClassReferenceIOConverter
{
    /// <summary>
    /// Converts an assembly reference poco into a DTO one.
    /// </summary>
    /// <param key="poco">The poco to consider.</param>
    /// <returns>The DTO object.</returns>
    public static ClassReferenceDto ToDto(this IBdoClassReference poco)
    {
        if (poco == null) return null;

        ClassReferenceDto dto = new();
        dto.UpdateFromPoco(poco);

        return dto;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="poco"></param>
    /// <returns></returns>
    public static ClassReferenceDto UpdateFromPoco(
        this ClassReferenceDto dto,
        IBdoClassReference poco)
    {
        if (dto == null) return null;

        if (poco == null) return dto;

        dto.UpdateFromPoco(poco as IBdoAssemblyReference);
        dto.ClassName = poco.ClassName;

        return dto;
    }

    /// <summary>
    /// Converts an assembly reference DTO into a poco one.
    /// </summary>
    /// <param key="dto">The DTO to consider.</param>
    /// <returns>The poco object.</returns>
    public static IBdoClassReference ToPoco(
        this ClassReferenceDto dto)
    {
        if (dto == null) return null;

        BdoClassReference poco = new();
        poco.UpdateFromDto(dto);

        return poco;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="poco"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    public static IBdoClassReference UpdateFromDto(
        this IBdoClassReference poco,
        ClassReferenceDto dto)
    {
        if (poco == null) return null;

        if (dto == null) return poco;

        poco.UpdateFromDto(dto as AssemblyReferenceDto);
        poco.ClassName = dto.ClassName;

        return poco;
    }
}
