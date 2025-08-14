using BindOpen.Data.Helpers;
using System.Linq;

namespace BindOpen.Data.Meta;

/// <summary>
/// This class represents a IO converter of configurations.
/// </summary>
public static class ConfigurationIOConverter
{
    /// <summary>
    /// Converts an assembly reference poco into a DTO one.
    /// </summary>
    /// <param key="poco">The poco to consider.</param>
    /// <returns>The DTO object.</returns>
    public static ConfigurationDto ToDto(this IBdoConfiguration poco)
    {
        if (poco == null) return null;

        ConfigurationDto dto = new();
        dto.UpdateFromPoco(poco);

        return dto;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="poco"></param>
    /// <returns></returns>
    public static ConfigurationDto UpdateFromPoco(
        this ConfigurationDto dto,
        IBdoConfiguration poco)
    {
        if (dto == null) return null;

        if (poco == null) return dto;

        dto.Children = poco._Children?.Select(q => q.ToDto()).ToList();
        dto.CreationDate = StringHelper.ToString(poco.CreationDate);
        dto.Description = poco.Description.ToDto();
        dto.Identifier = poco.Identifier;
        dto.Index = poco.Index ?? -1;
        dto.Items = poco.Items?.Select(q => q.ToDto()).ToList();
        dto.LastModificationDate = StringHelper.ToString(poco.CreationDate);
        dto.Name = poco.Name;
        dto.Title = poco.Title.ToDto();
        dto.UsedItemIds = poco.UsedItemIds == null ? null : [.. poco.UsedItemIds];

        return dto;
    }

    /// <summary>
    /// Converts an assembly reference DTO into a poco one.
    /// </summary>
    /// <param key="dto">The DTO to consider.</param>
    /// <returns>The poco object.</returns>
    public static IBdoConfiguration ToPoco(
        this ConfigurationDto dto)
    {
        if (dto == null) return null;

        BdoConfiguration poco = new();
        poco.UpdateFromDto(dto);

        return poco;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="poco"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    public static IBdoConfiguration UpdateFromDto(
        this IBdoConfiguration poco,
        ConfigurationDto dto)
    {
        if (poco == null) return null;

        if (dto == null) return poco;

        poco.CreationDate = StringHelper.ToDateTime(dto.CreationDate);
        poco.Description = dto.Description.ToPoco<string>();
        poco.Identifier = dto.Identifier;
        poco.Index = dto.Index;
        poco.LastModificationDate = StringHelper.ToDateTime(dto.CreationDate);
        poco.Name = dto.Name;
        poco.Title = dto.Title.ToPoco<string>();
        poco.UsedItemIds = dto.UsedItemIds;
        poco.WithChildren(dto.Children?.Select(q => q.ToPoco()).ToArray());
        poco.With(dto.Items?.Select(q => q.ToPoco()).ToArray());

        return poco;
    }
}
