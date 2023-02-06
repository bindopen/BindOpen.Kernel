using System;
using System.Linq;

namespace BindOpen.Data
{
    // --------------------------------------------------
    // EXTENSION
    // --------------------------------------------------

    #region Extension

    /// <summary>
    /// This class represents an update mode extension.
    /// </summary>
    public static class UpdateModesExtensions
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
