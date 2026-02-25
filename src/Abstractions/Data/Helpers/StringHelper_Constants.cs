namespace BindOpen.Data.Helpers;

/// <summary>
/// This structure represents a string helper.
/// </summary>
public static partial class StringHelper
{
    /// <summary>
    /// The unique token.
    /// </summary>
    public static readonly string __UniqueToken = "|*|";

    /// <summary>
    /// The pattern empty value.
    /// </summary>
    public static readonly string __PatternStar = "{{*}}";

    /// <summary>
    /// The string that is returned when the instance is not found.
    /// </summary>
    public static readonly string __NoneString = "<!--NONE-->";

    /// <summary>
    /// The string that is returned when the instance is not found.
    /// </summary>
    /// <summary>
    /// The default DateTime format used for serialization.
    /// Uses the round-trip "o" format (ISO 8601) to ensure invariant, lossless representation.
    /// </summary>
    public static readonly string __DateTimeFormat = "o";

    /// <summary>
    /// The string that is returned when the instance is not found.
    /// </summary>
    public static readonly string __DateTimeFormatShort = @"yyyy-MM-dd";

    /// <summary>
    /// The string that is returned when the instance is not found.
    /// </summary>
    public static readonly string __TimeFormat = @"c";

    /// <summary>
    /// The string that presents all items.
    /// </summary>
    public const string __Star = "*";
}
