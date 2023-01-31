using BindOpen.Data;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents an accessibility level extension.
    /// </summary>
    public static class AccessibilityLevelExtension
    {
        /// <summary>
        /// Gets the accessibility level corresponding to the specified string.
        /// </summary>
        /// <param name="levelString">The visibility to consider.</param>
        /// <param name="defaultLevelString">The default visibility to consider.</param>
        /// <returns>Returns the visibility corresponding to the specified strings.</returns>
        public static AccessibilityLevels ToAccessibilityLevel(
            string level,
            string defaultLevel)
        {
            AccessibilityLevels visibility = level.ToEnum(AccessibilityLevels.None);

            if (visibility == AccessibilityLevels.Inherited)
            {
                visibility = defaultLevel.ToEnum(AccessibilityLevels.None);
            }

            return visibility;
        }

    }
}
