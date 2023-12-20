using BindOpen.Data.Helpers;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This enumeration represents the possible meta data properties.
    /// </summary>
    public static class BdoMetaDataProperties
    {
        /// <summary>
        /// The name of this meta data.
        /// </summary>
        public static readonly string Name = "$(this).(name)";

        /// <summary>
        /// The pattern empty value.
        /// </summary>
        public static readonly string Value = "$(this).value()";

        /// <summary>
        /// Requirement.
        /// </summary>
        public static readonly string RequirementLevel = "$(this).requirementLevel()";

        /// <summary>
        /// Item requirement.
        /// </summary>
        public static readonly string ItemRequirementLevel = "$(this).itemRequirementLevel()";

        /// <summary>
        /// Gets the script corresponding to the specified property.
        /// </summary>
        /// <param name="name">The name of the property to consider.</param>
        /// <returns>Returns the specified script.</returns>
        public static string Property(string name) => string.Format("$(this).descendant('{0}').value()", name.ToUnquoted());
    }
}
