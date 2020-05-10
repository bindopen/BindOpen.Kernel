using System;
using System.Xml.Serialization;

namespace BindOpen.Data.Common
{
    // --------------------------------------------------
    // ENUMERATIONS
    // --------------------------------------------------

    #region Enumerations

    /// <summary>
    /// This enumeration represents the possible levels of requirement.
    /// </summary>
    [XmlType("RequirementLevels", Namespace = "https://docs.bindopen.org/xsd")]
    [Flags]
    public enum RequirementLevels
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
        /// Forbidden.
        /// </summary>
        Forbidden = 0x01 << 1,

        /// <summary>
        /// Optional.
        /// </summary>
        Optional = 0x01 << 2,

        /// <summary>
        /// Exclusively optional i.e. if the data item is a group then only it can have value.
        /// </summary>
        OptionalExclusively = 0x01 << 3,

        /// <summary>
        /// Required.
        /// </summary>
        Required = 0x01 << 4,

        /// <summary>
        /// Any.
        /// </summary>
        Any = Forbidden | Optional | OptionalExclusively | Required
    }

    #endregion

    // --------------------------------------------------
    // EXTENSION
    // --------------------------------------------------

    #region Extension

    /// <summary>
    /// This class represents an extension of the RequirementLevel enumeration.
    /// </summary>
    public static class RequirementLevelExtension
    {

        /// <summary>
        /// Indicates whether the specified requirement level means that it is possible.
        /// </summary>
        /// <param name="requirementLevel">The requirement level to consider.</param>
        /// <returns>The result object.</returns>
        public static bool IsPossible(this RequirementLevels requirementLevel)
        {
            return (requirementLevel == RequirementLevels.Optional)
                || (requirementLevel == RequirementLevels.OptionalExclusively)
                || (requirementLevel == RequirementLevels.Required);
        }

    }

    #endregion
}
