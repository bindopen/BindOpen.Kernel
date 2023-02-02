namespace BindOpen.Extensions
{
    // --------------------------------------------------
    // ENUMERATIONS
    // --------------------------------------------------

    #region Enumerations

    /// <summary>
    /// This enumeration represents the known routine kinds.
    /// </summary>
    public static class KnownRoutineKinds
    {
        /// <summary>
        /// None.
        /// </summary>
        public static readonly string None = "standard$None";

        /// <summary>
        /// RoutineConfiguration that checks that item is not empty.
        /// </summary>
        public static readonly string ItemMustNotBeEmpty = "standard$ItemMustNotBeEmpty";

        /// <summary>
        /// RoutineConfiguration that checks that item is in list.
        /// </summary>
        public static readonly string ItemMustBeInList = "standard$ItemMustBeInList";
    }

    #endregion
}
