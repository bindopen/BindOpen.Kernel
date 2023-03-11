namespace BindOpen.Data
{
    /// <summary>
    /// This class represents an extension of the RequirementLevels enumeration.
    /// </summary>
    public static class RequirementLevelsExtensions
    {

        /// <summary>
        /// Indicates whether the specified requirement level means that it is possible.
        /// </summary>
        /// <param key="requirementLevel">The requirement level to consider.</param>
        /// <returns>The result object.</returns>
        public static bool IsPossible(this RequirementLevels requirementLevel)
        {
            return requirementLevel == RequirementLevels.Optional
                || requirementLevel == RequirementLevels.Required;
        }
    }
}
