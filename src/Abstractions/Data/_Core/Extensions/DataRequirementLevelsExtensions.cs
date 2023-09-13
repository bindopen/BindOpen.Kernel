namespace BindOpen.Kernel.Data
{
    /// <summary>
    /// This class extends data requirement levels.
    /// </summary>
    public static class DataRequirementLevelsExtensions
    {
        /// <summary>
        /// Indicates whether the specified requirement level means that it is possible.
        /// </summary>
        /// <param key="level">The requirement level to consider.</param>
        /// <returns>True if the specified requirement level means it is possible.</returns>
        public static bool IsPossible(this RequirementLevels level)
        {
            return level == RequirementLevels.Optional
                || level == RequirementLevels.Required
                || level == RequirementLevels.None;
        }
    }
}
