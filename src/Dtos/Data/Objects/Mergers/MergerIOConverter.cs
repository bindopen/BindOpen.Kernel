using System.Collections.Generic;

namespace BindOpen.Data;

/// <summary>
/// This class represents a IO converter of mergers.
/// </summary>
public static class MergerIOConverter
{
    /// <summary>
    /// Converts an assembly reference poco into a DTO one.
    /// </summary>
    /// <param key="poco">The poco to consider.</param>
    /// <returns>The DTO object.</returns>
    public static MergerDto ToDto(this IBdoMerger poco)
    {
        if (poco == null) return null;

        MergerDto dto = new();
        dto.UpdateFromPoco(poco);

        return dto;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="poco"></param>
    /// <returns></returns>
    public static MergerDto UpdateFromPoco(
        this MergerDto dto,
        IBdoMerger poco)
    {
        if (dto == null) return null;

        if (poco == null) return dto;

        dto.Identifier = poco.Identifier;
        dto.AddedValues = poco.AddedValues == null ? null : [.. poco.AddedValues];
        dto.RemovedValues = poco.RemovedValues == null ? null : [.. poco.RemovedValues];

        return dto;
    }

    /// <summary>
    /// Converts an assembly reference DTO into a poco one.
    /// </summary>
    /// <param key="dto">The DTO to consider.</param>
    /// <returns>The poco object.</returns>
    public static IBdoMerger ToPoco(
        this MergerDto dto)
    {
        if (dto == null) return null;

        BdoMerger poco = new();
        poco.UpdateFromDto(dto);

        return poco;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="poco"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    public static IBdoMerger UpdateFromDto(
        this IBdoMerger poco,
        MergerDto dto)
    {
        if (poco == null) return null;

        if (dto == null) return poco;

        poco.Identifier = dto.Identifier;
        poco.AddedValues = dto.AddedValues == null ? null : new List<string>(dto.AddedValues);
        poco.RemovedValues = dto.RemovedValues == null ? null : new List<string>(dto.RemovedValues);

        return poco;
    }
}
