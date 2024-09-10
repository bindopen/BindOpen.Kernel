using System.Linq;

namespace BindOpen.Data;

/// <summary>
/// This class represents a IO converter of dictionaries.
/// </summary>
public static class StringDictionaryConverter
{
    /// <summary>
    /// Converts a dictionary poco into a DTO one.
    /// </summary>
    /// <param key="poco">The poco to consider.</param>
    /// <returns>The DTO object.</returns>
    public static StringDictionaryDto ToDto<TItem>(this ITBdoDictionary<TItem> poco)
    {
        StringDictionaryDto dto = new();
        dto.UpdateFromPoco(poco);

        return dto;
    }

    public static StringDictionaryDto UpdateFromPoco<TItem>(
        this StringDictionaryDto dto,
        ITBdoDictionary<TItem> poco)
    {
        if (dto == null) return null;

        if (poco == null) return dto;

        dto.Identifier = poco.Identifier;
        dto.Values = poco?.Select(value =>
        {
            var valueDto = value.ToDto();
            valueDto.StringDictionary = dto;
            valueDto.StringDictionaryId = dto.Identifier;

            return valueDto;
        }).ToList();

        return dto;
    }

    /// <summary>
    /// Converts a dictionary DTO to a poco one.
    /// </summary>
    /// <param key="dto">The DTO to consider.</param>
    /// <returns>The poco object.</returns>
    public static ITBdoDictionary<TItem> ToPoco<TItem>(this StringDictionaryDto dto)
    {
        if (dto == null) return null;

        TBdoDictionary<TItem> poco = BdoData.NewDictionary(dto.Values?.Select(q => q.ToPoco<TItem>()).ToArray());
        poco.WithId(dto.Identifier);

        return poco;
    }
}
