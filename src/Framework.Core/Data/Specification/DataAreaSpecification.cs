using BindOpen.Framework.Core.Data.Common;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Data.Specification
{
    /// <summary>
    /// This class represents a data area specification.
    /// </summary>
    [XmlType("DataAreaSpecification", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "areaSpecification", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class DataAreaSpecification : DataSpecification, IDataAreaSpecification
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The name of the area of this instance.
        /// </summary>
        [XmlAttribute("area")]
        public string AreaName { get; set; } = "";

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the DataAreaSpecification class.
        /// </summary>
        public DataAreaSpecification()
        {
        }

        /// <summary>
        /// Initializes a new instance of the DataAreaSpecification class.
        /// </summary>
        /// <param name="arename">The name of the area to consider.</param>
        public DataAreaSpecification(string arename)
        {
            this.AreaName = arename;
        }

        /// <summary>
        /// Initializes a new instance of the DataAreaSpecification class.
        /// </summary>
        /// <param name="accessibilityLevel">The accessibilty level of this instance.</param>
        /// <param name="specificationLevels">The specification levels of this instance.</param>
        public DataAreaSpecification(
            AccessibilityLevels accessibilityLevel = AccessibilityLevels.Public,
            SpecificationLevels[] specificationLevels = null) : base(accessibilityLevel, specificationLevels)
        {
        }

        #endregion

        // --------------------------------------------------
        // CLONING
        // --------------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a cloned instance.</returns>
        public override object Clone()
        {
            DataAreaSpecification dataAreaSpecification = base.Clone() as DataAreaSpecification;
            return dataAreaSpecification;
        }

        #endregion
    }
}
