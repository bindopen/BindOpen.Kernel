using System;
using System.Linq;
using System.Xml.Serialization;

namespace BindOpen.Data.Common
{
    // --------------------------------------------------
    // ENUMERATIONS
    // --------------------------------------------------

    #region Enumerations

    /// <summary>
    /// This enumeration represents the possible levels of specification.
    /// </summary>
    [XmlType("SpecificationLevels", Namespace = "https://docs.bindopen.org/xsd")]
    [Flags]
    public enum SpecificationLevels
    {
        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// None.
        /// </summary>
        None = 0x01 << 0,

        /// <summary>
        /// Definition.
        /// </summary>
        Definition = 0x01 << 1,

        /// <summary>
        /// Design.
        /// </summary>
        Design = 0x01 << 2,

        /// <summary>
        /// Configuration.
        /// </summary>
        Configuration = 0x01 << 3,

        /// <summary>
        /// Runtime.
        /// </summary>
        Runtime = 0x01 << 4,

        /// <summary>
        /// All the specification levels.
        /// </summary>
        Any = Definition | Design | Configuration | Runtime,

        /// <summary>
        /// All the specification levels.
        /// </summary>
        All = Definition & Design & Configuration & Runtime
    }

    #endregion

    // --------------------------------------------------
    // EXTENSION
    // --------------------------------------------------

    #region Extension

    /// <summary>
    /// This class represents an specification level extension.
    /// </summary>
    public static class SpecificationLevelExtension
    {
        /// <summary>
        /// Indicates whether the specified specification level list contains the specified specification level.
        /// </summary>
        /// <param name="specificationLevels">The specified specification level list to consider.</param>
        /// <param name="specificationLevel">The specified specification level to consider.</param>
        /// <returns></returns>
        public static bool Has(
            this SpecificationLevels[] specificationLevels,
            SpecificationLevels specificationLevel)
        {
            return (specificationLevels.Aggregate((current, value) => current | value) & specificationLevel) == specificationLevel;
        }

        /// <summary>
        /// Indicates whether the specified specification level list contains the specified specification level.
        /// </summary>
        /// <param name="specificationLevels">The specified specification level list to consider.</param>
        /// <param name="referenceSpecificationLevels">The specified reference specification levels to consider.</param>
        /// <returns></returns>
        public static bool Has(
            this SpecificationLevels[] specificationLevels,
            SpecificationLevels[] referenceSpecificationLevels)
        {
            return referenceSpecificationLevels.Any(p => specificationLevels.Has(p));
        }

        /// <summary>
        /// Gets the specified list excluding the second specified list.
        /// </summary>
        /// <param name="specificationLevels">The specification levels to consider.</param>
        /// <param name="excludingSpecificationLevels">The excluding specification levels to consider.</param>
        /// <returns></returns>
        public static SpecificationLevels[] Excluding(
            this SpecificationLevels[] specificationLevels,
            params SpecificationLevels[] excludingSpecificationLevels)
        {
            SpecificationLevels specificationLevel = specificationLevels.Aggregate((current, value) => current | value) & ~excludingSpecificationLevels.Aggregate((current, value) => current | value);

            return Enum.GetValues(typeof(SpecificationLevels)).Cast<SpecificationLevels>().Where(p => (p & specificationLevel) == p).ToArray();
        }

        /// <summary>
        /// Gets the specified list excluding the secong specified list.
        /// </summary>
        /// <param name="specificationLevel">The specification level to consider.</param>
        /// <param name="excludingSpecificationLevels">The excluding specification levels to consider.</param>
        /// <returns></returns>
        public static SpecificationLevels[] Excluding(
            this SpecificationLevels specificationLevel,
            params SpecificationLevels[] excludingSpecificationLevels)
        {
            return (new[] { specificationLevel }).Excluding(excludingSpecificationLevels);
        }
    }

    #endregion
}
