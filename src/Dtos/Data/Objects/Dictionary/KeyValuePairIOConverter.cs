using BindOpen.Data.Helpers;
using System.Collections.Generic;

namespace BindOpen.Data;

/// <summary>
/// This class represents a IO converter of key value pairs.
/// </summary>
public static class KeyValuePairIOConverter
{
    /// <summary>
    /// Converts an assembly reference poco into a DTO one.
    /// </summary>
    /// <param key="poco">The poco to consider.</param>
    /// <returns>The DTO object.</returns>
    public static KeyValuePairDto ToDto<TItem>(this KeyValuePair<string, TItem> poco)
    {
        var valueType = typeof(TItem).GetValueType();

        var dto = new KeyValuePairDto()
        {
            Value = poco.Value.ToString(valueType),
            Key = poco.Key
        };

        return dto;
    }

    /// <summary>
    /// Converts an assembly reference DTO into a poco one.
    /// </summary>
    /// <param key="dto">The DTO to consider.</param>
    /// <returns>The poco object.</returns>
    public static KeyValuePair<string, TItem> ToPoco<TItem>(
        this KeyValuePairDto dto)
    {
        var valueType = typeof(TItem).GetValueType();

        TItem item = dto == null ? default : dto.Value.ToObject(valueType).As<TItem>();

        KeyValuePair<string, TItem> poco = new(dto?.Key, item);

        return poco;
    }
}
