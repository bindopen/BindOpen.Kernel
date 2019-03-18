using BindOpen.Framework.Core.Data.Common;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Data.Specification
{

    /// <summary>
    /// This class represents a data area specification.
    /// </summary>
    [Serializable()]
    [XmlType("DataAreaSpecification", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "areaSpecification", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class DataAreaSpecification : DataSpecification
    {

        // --------------------------------------------------
        // VARIABLES
        // --------------------------------------------------

        #region Variables

        private String _Arename = "";

        #endregion


        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The name of the area of this instance.
        /// </summary>
        [XmlAttribute("area")]
        public String AreaName
        {
            get
            {
                return this._Arename;
            }
            set { this._Arename = value; }
        }

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
        public DataAreaSpecification(String arename)
        {
            this.AreaName = arename;
        }

        /// <summary>
        /// Initializes a new instance of the DataAreaSpecification class.
        /// </summary>
        /// <param name="accessibilityLevel">The accessibilty level of this instance.</param>
        /// <param name="specificationLevels">The specification levels of this instance.</param>
        public DataAreaSpecification(
            AccessibilityLevel accessibilityLevel = AccessibilityLevel.Public,
            List<SpecificationLevel> specificationLevels = null) : base(accessibilityLevel,specificationLevels)
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
        public override Object Clone()
        {
            DataAreaSpecification dataAreaSpecification = this.MemberwiseClone() as DataAreaSpecification;
            return dataAreaSpecification;
        }

        #endregion

    }

}
