using BindOpen.Framework.Standard.Extensions.Common;
using System;

namespace BindOpen.Framework.Standard.Extensions.Routines
{

    /// <summary>
    /// This enumeration lists all the possible kinds of the 'Standard' routines.
    /// </summary>
    public enum RoutineConfigurationKind_standard
    {
        /// <summary>
        /// None.
        /// </summary>
        None,
        /// <summary>
        /// Any.
        /// </summary>
        Any,
        /// <summary>
        /// Item is required.
        /// </summary>
        ItemIsRequired,
        /// <summary>
        /// Text format must sbe.
        /// </summary>
        TextFormatMustBe
    }


    // --------------------------------------------------
    // EXTENSION
    // --------------------------------------------------

    #region Extension

    /// <summary>
    /// This class represents an extension of the RoutineConfigurationKind_standard enumeration.
    /// </summary>
    public static class RoutineConfigurationKind_standardExtension
    {
        /// <summary>
        /// Gets the unique name corresponding to the specified routine kind.
        /// </summary>
        /// <param name="routineKind_standard">The routine kind to consider.</param>
        /// <returns>The result object.</returns>
        public static string GetUniqueName(this RoutineConfigurationKind_standard routineKind_standard)
        {
            return routineKind_standard.ToString().ToLower().GetUniqueName_standard();
        }
    }

    #endregion


}
