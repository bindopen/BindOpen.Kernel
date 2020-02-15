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
    [Serializable()]
    [XmlType("RequirementLevel", Namespace = "https://bindopen.org/xsd")]
    public enum RequirementLevel
    {
        /// <summary>
        /// No special requirement.
        /// </summary>
        None,

        /// <summary>
        /// Forbidden.
        /// </summary>
        Forbidden,

        /// <summary>
        /// Optional.
        /// </summary>
        Optional,

        /// <summary>
        /// Exclusively optional i.e. if the data item is a group then only it can have value.
        /// </summary>
        OptionalExclusively,

        /// <summary>
        /// Required.
        /// </summary>
        Required
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
        public static bool IsPossible(this RequirementLevel requirementLevel)
        {
            return (requirementLevel == RequirementLevel.Optional)
                || (requirementLevel == RequirementLevel.OptionalExclusively)
                || (requirementLevel == RequirementLevel.Required);
        }

    }

    #endregion
}
