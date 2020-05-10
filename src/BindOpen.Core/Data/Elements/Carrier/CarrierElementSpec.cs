using BindOpen.Data.Common;
using BindOpen.Data.Specification;
using BindOpen.System.Diagnostics;
using System.Xml.Serialization;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This class represents a carrier element specification.
    /// </summary>
    [XmlType("CarrierElementSpec", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "specification", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
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

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new carrier element specification.
        /// </summary>
        public CarrierElementSpec() : base()
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
        public override object Clone(params string[] areas)
        {
            CarrierElementSpec dataCarrierElementSpec = base.Clone(areas) as CarrierElementSpec;
            if (this.DefinitionFilter != null)
                dataCarrierElementSpec.DefinitionFilter = this.DefinitionFilter.Clone() as DataValueFilter;
            return dataCarrierElementSpec;
        }

        #endregion

        // ------------------------------------------
        // IDISPOSABLE METHODS
        // ------------------------------------------

        #region IDisposable_Methods

        private bool _isDisposed = false;

        /// <summary>
        /// Disposes this instance. 
        /// </summary>
        /// <param name="isDisposing">Indicates whether this instance is disposing</param>
        protected override void Dispose(bool isDisposing)
        {
            if (_isDisposed)
            {
                return;
            }

            _definitionFilter?.Dispose();

            _isDisposed = true;

            base.Dispose(isDisposing);
        }

        #endregion
    }

}
