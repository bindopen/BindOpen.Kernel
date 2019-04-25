namespace BindOpen.Framework.Core.Extensions.Items.Routines
{
    // --------------------------------------------------
    // ENUMERATIONS
    // --------------------------------------------------

    #region Enumerations

    /// <summary>
    /// This enumeration represents the basic routine kinds.
    /// </summary>
    public enum KnownRoutineKind
    {
        /// <summary>
        /// None.
        /// </summary>
        None,

        /// <summary>
        /// RoutineConfiguration that checks that item is not empty.
        /// </summary>
        ItemMustNotBeEmpty,

        /// <summary>
        /// RoutineConfiguration that checks that item is in list.
        /// </summary>
        ItemMustBeInList
    }

    #endregion
}
