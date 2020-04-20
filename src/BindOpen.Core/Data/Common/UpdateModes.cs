using System;
using System.Linq;
using System.Xml.Serialization;

namespace BindOpen.Data.Common
{
    // --------------------------------------------------
    // ENUMERATIONS
    // --------------------------------------------------

    #region Enumerations

    /// <summary>
    /// This enumeration represents the possible update modes.
    /// </summary>
    [XmlType("UpdateModes", Namespace = "https://bindopen.org/xsd")]
    [Flags]
    public enum UpdateModes
    {
        /// <summary>
        /// None.
        /// </summary>
        None = 0,

        /// <summary>
        /// Adds Source items that are not in Target.
        /// </summary>
        /// <remarks>Used in incremental update.</remarks>
        Incremental_AddItemsMissingInTarget = 1 << 0,

        /// <summary>
        /// Removes Target items that are not in Source.
        /// </summary>
        /// <remarks>Used in incremental update.</remarks>
        Incremental_RemoveItemsMissingInSource = 1 << 1,

        /// <summary>
        /// Updates Target items that are in Source.
        /// </summary>
        /// <remarks>Used in incremental update.</remarks>
        Incremental_UpdateCommonItems = 1 << 2,

        /// <summary>
        /// Source fully replaces Target.
        /// </summary>
        /// <remarks>Used in full update.</remarks>
        Full = Incremental_AddItemsMissingInTarget | Incremental_RemoveItemsMissingInSource | Incremental_UpdateCommonItems
    }

    #endregion

    // --------------------------------------------------
    // EXTENSION
    // --------------------------------------------------

    #region Extension

    /// <summary>
    /// This class represents an update mode extension.
    /// </summary>
    public static class UpdateModeExtension
    {
        /// <summary>
        /// Indicates whether the specified update mode list contains the specified update mode.
        /// </summary>
        /// <param name="updateModes">The specified update mode list to consider.</param>
        /// <param name="updateMode">The specified update mode to consider.</param>
        /// <returns></returns>
        public static bool Has(
            this UpdateModes[] updateModes,
            UpdateModes updateMode)
        {
            return (updateModes.Aggregate((current, value) => current | value) & updateMode) == updateMode;
        }

        /// <summary>
        /// Gets the specified list excluding the second specified list.
        /// </summary>
        /// <param name="updateModes">The update modes to consider.</param>
        /// <param name="excludingUpdateModes">The excluding update modes to consider.</param>
        /// <returns></returns>
        public static UpdateModes[] Excluding(
            this UpdateModes[] updateModes,
            params UpdateModes[] excludingUpdateModes)
        {
            UpdateModes updateMode = updateModes.Aggregate((current, value) => current | value) & ~excludingUpdateModes.Aggregate((current, value) => current | value);

            return Enum.GetValues(typeof(UpdateModes)).Cast<UpdateModes>().Where(p => (p & updateMode) == p).ToArray();
        }

        /// <summary>
        /// Gets the specified list excluding the secong specified list.
        /// </summary>
        /// <param name="updateMode">The update mode to consider.</param>
        /// <param name="excludingUpdateModes">The excluding update modes to consider.</param>
        /// <returns></returns>
        public static UpdateModes[] Excluding(
            this UpdateModes updateMode,
            params UpdateModes[] excludingUpdateModes)
        {
            return (new[] { updateMode }).Excluding(excludingUpdateModes);
        }
    }

    #endregion

}
