using System;
using System.Linq;

namespace BindOpen.Kernel
{
    /// <summary>
    /// This class represents an update mode extension.
    /// </summary>
    public static class UpdateModesExtensions
    {
        /// <summary>
        /// Indicates whether the specified update mode list contains the specified update mode.
        /// </summary>
        /// <param key="updateModes">The specified update mode list to consider.</param>
        /// <param key="updateMode">The specified update mode to consider.</param>
        /// <returns></returns>
        public static bool Has(
            this UpdateModes[] updateModes,
            params UpdateModes[] modes)
        {
            var b = false;
            foreach (var mode in modes)
            {
                b |= (updateModes.Aggregate((current, value) => current | value) & mode) == mode;
            }
            return b;
        }

        /// <summary>
        /// Gets the specified list excluding the second specified list.
        /// </summary>
        /// <param key="updateModes">The update modes to consider.</param>
        /// <param key="excludingUpdateModes">The excluding update modes to consider.</param>
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
        /// <param key="updateMode">The update mode to consider.</param>
        /// <param key="excludingUpdateModes">The excluding update modes to consider.</param>
        /// <returns></returns>
        public static UpdateModes[] Excluding(
            this UpdateModes updateMode,
            params UpdateModes[] excludingUpdateModes)
        {
            return (new[] { updateMode }).Excluding(excludingUpdateModes);
        }
    }
}
