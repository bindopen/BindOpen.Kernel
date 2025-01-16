using System;
using System.Linq;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents an schema level extension.
    /// </summary>
    public static class SpecificationLevelExtensions
    {
        /// <summary>
        /// Indicates whether the specified schema level list contains the specified schema level.
        /// </summary>
        /// <param key="schemaLevels">The specified schema level list to consider.</param>
        /// <param key="schemaLevel">The specified schema level to consider.</param>
        /// <returns></returns>
        public static bool Has(
            this SpecificationLevels[] schemaLevels,
            SpecificationLevels schemaLevel)
        {
            return (schemaLevels.Aggregate((current, value) => current | value) & schemaLevel) == schemaLevel;
        }

        /// <summary>
        /// Indicates whether the specified schema level list contains the specified schema level.
        /// </summary>
        /// <param key="schemaLevels">The specified schema level list to consider.</param>
        /// <param key="referenceSpecificationLevels">The specified reference schema levels to consider.</param>
        /// <returns></returns>
        public static bool Has(
            this SpecificationLevels[] schemaLevels,
            SpecificationLevels[] referenceSpecificationLevels)
        {
            return referenceSpecificationLevels.Any(p => schemaLevels.Has(p));
        }

        /// <summary>
        /// Gets the specified list excluding the second specified list.
        /// </summary>
        /// <param key="schemaLevels">The schema levels to consider.</param>
        /// <param key="excludingSpecificationLevels">The excluding schema levels to consider.</param>
        /// <returns></returns>
        public static SpecificationLevels[] Excluding(
            this SpecificationLevels[] schemaLevels,
            params SpecificationLevels[] excludingSpecificationLevels)
        {
            SpecificationLevels schemaLevel = schemaLevels.Aggregate((current, value) => current | value) & ~excludingSpecificationLevels.Aggregate((current, value) => current | value);

            return Enum.GetValues(typeof(SpecificationLevels)).Cast<SpecificationLevels>().Where(p => (p & schemaLevel) == p).ToArray();
        }

        /// <summary>
        /// Gets the specified list excluding the secong specified list.
        /// </summary>
        /// <param key="schemaLevel">The schema level to consider.</param>
        /// <param key="excludingSpecificationLevels">The excluding schema levels to consider.</param>
        /// <returns></returns>
        public static SpecificationLevels[] Excluding(
            this SpecificationLevels schemaLevel,
            params SpecificationLevels[] excludingSpecificationLevels)
        {
            return (new[] { schemaLevel }).Excluding(excludingSpecificationLevels);
        }
    }
}
