using System.Linq;

namespace BindOpen.Data;

/// <summary>
/// This class represents a IO converter of dictionaries.
/// </summary>
public static class StringDictionaryIOConverter
{
    /// <summary>
    /// Converts an assembly reference poco into a DTO one.
    /// </summary>
    /// <param key="poco">The poco to consider.</param>
    /// <returns>The DTO object.</returns>
    public static StringDictionaryDto ToDto<TItem>(this ITBdoDictionary<TItem> poco)
    {
        StringDictionaryDto dto = new();
        dto.UpdateFromPoco(poco);

        return dto;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="poco"></param>
    /// <returns></returns>
    public static StringDictionaryDto UpdateFromPoco<TItem>(
        this StringDictionaryDto dto,
        ITBdoDictionary<TItem> poco)
    {
        if (dto == null) return null;

        if (poco == null) return dto;

        dto.Identifier = poco.Identifier;
        dto.Values = poco?.Select(value => value.ToDto()).ToList();

        return dto;
    }

    /// <summary>
    /// Converts an assembly reference DTO into a poco one.
    /// </summary>
    /// <param key="dto">The DTO to consider.</param>
    /// <returns>The poco object.</returns>
    public static ITBdoDictionary<TItem> ToPoco<TItem>(
        this StringDictionaryDto dto)
    {
        if (dto == null) return null;

        var poco = new TBdoDictionary<TItem>();
        poco.UpdateFromDto(dto);

        return poco;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="poco"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    public static ITBdoDictionary<TItem> UpdateFromDto<TItem>(
        this ITBdoDictionary<TItem> poco,
        StringDictionaryDto dto)
    {
        if (poco == null) return null;

        if (dto == null) return poco;

        poco
            .With(dto.Values?.Select(q => q.ToPoco<TItem>()).ToArray())
            .WithId(dto.Identifier);

        return poco;
    }
}
