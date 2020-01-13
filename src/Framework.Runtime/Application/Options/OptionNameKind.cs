namespace BindOpen.Framework.Application.Options
{
    /// <summary>
    /// This enumeration lists all the possible option name kinds.
    /// </summary>
    public enum OptionNameKind
    {
        /// <summary>
        /// Any.
        /// </summary>
        Any,

        /// <summary>
        /// Only name.
        /// </summary>
        OnlyName,

        /// <summary>
        /// Name with value.
        /// </summary>
        NameWithValue,

        /// <summary>
        /// Name then value.
        /// </summary>
        NameThenValue,

        /// <summary>
        /// Only value.
        /// </summary>
        OnlyValue,
    }

    /// <summary>
    /// The extension of the option name kind.
    /// </summary>
    public static class OptionNameKindExtension
    {
        /// <summary>
        /// Indicates whether the specified kind has name.
        /// </summary>
        /// <param name="kind">The kind of the option name.</param>
        /// <returns>Returns true or false.</returns>
        public static bool HasName(this OptionNameKind kind)
        {
            return kind == OptionNameKind.NameThenValue || kind == OptionNameKind.NameWithValue || kind == OptionNameKind.OnlyName;
        }

        /// <summary>
        /// Indicates whether the specified kind has value.
        /// </summary>
        /// <param name="kind">The kind of the option name.</param>
        /// <returns>Returns true or false.</returns>
        public static bool HasValue(this OptionNameKind kind)
        {
            return kind == OptionNameKind.NameThenValue || kind == OptionNameKind.NameWithValue || kind == OptionNameKind.OnlyValue;
        }
    }
}