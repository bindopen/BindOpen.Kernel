namespace BindOpen.Kernel.Data
{
    /// <summary>
    /// This enumeration lists the possible kinds of text formats.
    /// </summary>
    public enum TextFormatKind
    {
        /// <summary>
        /// No format.
        /// </summary>
        None,

        /// <summary>
        /// Email format.
        /// </summary>
        Email,

        /// <summary>
        /// Strong password (Must be at least 10 characters,
        /// Must contain at least one one lower case letter, one upper case letter, one digit and one special character
        /// </summary>
        StrongPassword
    }
}
