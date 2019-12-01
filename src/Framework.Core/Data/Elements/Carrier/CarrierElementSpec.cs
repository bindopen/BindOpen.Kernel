using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Specification.Filters;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Data.Elements.Carrier
{
    /// <summary>
    /// This class represents a carrier element specification.
    /// </summary>
    [Serializable()]
    [XmlType("CarrierElementSpec", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "specification", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class CarrierElementSpec : DataElementSpec, ICarrierElementSpec
    {
        // --------------------------------------------------
        // VARIABLES
        // --------------------------------------------------

        #region Variables

        private DataValueFilter _definitionFilter = null;

        #endregion

        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The definition filter of this instance.
        /// </summary>
        [XmlElement("definition.filter")]
        public DataValueFilter DefinitionFilter
        {
            get => this._definitionFilter ?? (this._definitionFilter = new DataValueFilter());
            set { this._definitionFilter = value; }
        }

        /// <summary>
        /// Specification of the DefinitionFilter property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool DefinitionFilterSpecified => this._definitionFilter != null
                    && (this._definitionFilter.AddedValues == null || this._definitionFilter.AddedValues.Count > 0)
                    && (this._definitionFilter.RemovedValues == null || this._definitionFilter.RemovedValues.Count > 0);

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new carrier element specification.
        /// </summary>
        public CarrierElementSpec(): base()
        {
        }

        /// <summary>
        /// Initializes a new carrier element specification.
        /// </summary>
        /// <param name="accessibilityLevel">The accessibilty level of this instance.</param>
        /// <param name="specificationLevels">The specification levels of this instance.</param>
        public CarrierElementSpec(
            AccessibilityLevels accessibilityLevel = AccessibilityLevels.Public,
            SpecificationLevels[] specificationLevels = null)
            : base(accessibilityLevel, specificationLevels)
        {
        }

        #endregion

        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        /// <summary>
        /// Check the specified item.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="dataElement">The element to consider.</param>
        /// <returns>The log of check log.</returns>
        public override IBdoLog CheckItem(
            object item,
            IDataElement dataElement = null)
        {
            return new BdoLog();
        }

        /// <summary>
        /// Check the specified item.
        /// </summary>
        /// <param name="dataElement">The element to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <returns>The log of check log.</returns>
        public override IBdoLog CheckElement(
            IDataElement dataElement,
            string[] specificationAreas = null)
        {
            return new BdoLog();
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
            CarrierElementSpec dataCarrierElementSpec = base.Clone() as CarrierElementSpec;
            if (this.DefinitionFilter!= null)
                dataCarrierElementSpec.DefinitionFilter = this.DefinitionFilter.Clone() as DataValueFilter;
            return dataCarrierElementSpec;
        }

        #endregion
    }

}
