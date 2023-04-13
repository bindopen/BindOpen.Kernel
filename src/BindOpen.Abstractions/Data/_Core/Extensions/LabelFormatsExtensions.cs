namespace BindOpen.Data
{
    /// <summary>
    /// This class represents an extension of the DataValueType enumeration.
    /// </summary>
    public static class LabelFormatsExtensions
    {
        /// <summary>
        /// The name of this meta data.
        /// </summary>
        private static readonly string __Script_This_Name = "$(this).prop('name')";

        /// <summary>
        /// The pattern empty value.
        /// </summary>
        private static readonly string __Script_This_Value = "$(this).value()";

        /// <summary>
        /// Indicates whether the specified kind has name.
        /// </summary>
        /// <param name="kind">The kind of the option name.</param>
        /// <returns>Returns true or false.</returns>
        public static bool HasName(this LabelFormats label)
        {
            return label is LabelFormats.NameColonValue
                or LabelFormats.NameEqualsValue
                or LabelFormats.NameSpaceValue
                or LabelFormats.OnlyName;
        }

        /// <summary>
        /// Indicates whether the specified kind has value.
        /// </summary>
        /// <param name="kind">The kind of the option name.</param>
        /// <returns>Returns true or false.</returns>
        public static bool HasValue(this LabelFormats label)
        {
            return label is LabelFormats.NameColonValue
                or LabelFormats.NameEqualsValue
                or LabelFormats.NameSpaceValue
                or LabelFormats.OnlyValue;
        }

        /// <summary>
        /// Sanitizes the specified string.
        /// </summary>
        /// <param key="st">The string to consider.</param>
        /// <returns>The formated string.</returns>
        public static string GetScript(this LabelFormats label)
        {
            string format = label switch
            {
                LabelFormats.NameColonValue => "{{{{{0}}}}}:{{{{{1}}}}}",
                LabelFormats.NameEqualsValue => "{{{{{0}}}}}={{{{{1}}}}}",
                LabelFormats.NameSpaceValue => "{{{{{0}}}}} {{{{{1}}}}}",
                LabelFormats.OnlyValue => "{{{{{1}}}}}",
                _ => "{{{{{0}}}}}"
            };

            return string.Format(format, __Script_This_Name, __Script_This_Value);
        }
    }
}
