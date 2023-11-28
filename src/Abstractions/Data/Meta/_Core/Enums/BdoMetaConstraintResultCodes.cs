namespace BindOpen.Kernel.Data.Meta
{
    /// <summary>
    /// This enumeration represents the possible specification constraint groups.
    /// </summary>
    public static class BdoMetaConstraintResultCodes
    {
        /// <summary>
        /// Requirement.
        /// </summary>
        public static readonly string Missing = "Missing";

        /// <summary>
        /// Item requirement.
        /// </summary>
        public static readonly string ItemRequirement = "ItemRequirement";

        /// <summary>
        /// Item must be in list.
        /// </summary>
        public static readonly string BadValueType = "ItemMustBeInList";

        public static readonly string ItemMustBeInList = "ItemMustBeInList";        
    }
}
