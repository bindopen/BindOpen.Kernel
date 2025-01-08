using System.Collections.Generic;

namespace BindOpen.Data;

/// <summary>
/// This class represents a IO converter of mergers.
/// </summary>
public static class MergerIOConverter
{
    /// <summary>
    /// Converts a merger poco into a DTO one.
    /// </summary>
    /// <param key="poco">The poco to consider.</param>
    /// <returns>The DTO object.</returns>
    public static MergerDto ToDto(this IBdoMerger poco)
    {
        MergerDto dto = new();
        dto.UpdateFromPoco(poco);

        return dto;
    }

    public static MergerDto UpdateFromPoco(
        this MergerDto dto,
        IBdoMerger poco)
    {
        if (dto == null) return null;

        if (poco == null) return dto;

        dto.Identifier = poco.Identifier;
        dto.AddedValues = poco.AddedValues == null ? null : new List<string>(poco.AddedValues);
        dto.RemovedValues = poco.RemovedValues == null ? null : new List<string>(poco.RemovedValues);

        return dto;
    }

    /// <summary>
    /// Converts a merger DTO to a poco one.
    /// </summary>
    /// <param key="dto">The DTO to consider.</param>
    /// <returns>The poco object.</returns>
    public static IBdoMerger ToPoco(this MergerDto dto)
    {
        if (dto == null) return null;

        BdoMerger poco = new()
        {
            Identifier = dto.Identifier,
            AddedValues = dto.AddedValues == null ? null : new List<string>(dto.AddedValues),
            RemovedValues = dto.RemovedValues == null ? null : new List<string>(dto.RemovedValues)
        };

        return poco;
    }
}
