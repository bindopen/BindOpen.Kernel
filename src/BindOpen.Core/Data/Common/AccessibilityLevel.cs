using BindOpen.Data.Helpers.Strings;
using System;
using System.Xml.Serialization;

namespace BindOpen.Data.Common
{
    /// <summary>
    /// This enumerates the possible levels of accesibility.
    /// </summary>
    [XmlType("AccessibilityLevel", Namespace = "https://bindopen.org/xsd")]
    [Flags]
    public enum AccessibilityLevels
    {
        /// <summary>
        /// None can access.
        /// </summary>
        None = 0x0,

        /// <summary>
        /// Inherited.
        /// </summary>
        Inherited = 0x1 << 0,

        /// <summary>
        /// Anyone can access.
        /// </summary>
        Public = 0x1 << 1,

        /// <summary>
        /// Only authentized people can access.
        /// </summary>
        Private = 0x1 << 2,

        /// <summary>
        /// Only allowed people can access.
        /// </summary>
        Restricted = 0x1 << 3,

        /// <summary>
        /// Only people who bought for it can access.
        /// </summary>
        Payable = 0x1 << 4,
    }


    // --------------------------------------------------
    // EXTENSION
    // --------------------------------------------------

    #region Extension

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
            String levelString,
            String defaultLevelString)
        {
            AccessibilityLevels visibility = levelString.ToEnum<AccessibilityLevels>(AccessibilityLevels.None);

            if (visibility == AccessibilityLevels.Inherited)
                visibility = defaultLevelString.ToEnum<AccessibilityLevels>(AccessibilityLevels.None);

            return visibility;
        }

    }

    #endregion
}
