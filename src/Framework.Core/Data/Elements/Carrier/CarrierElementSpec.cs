using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Specification.Filters;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.Elements.Carrier
{

    /// <summary>
    /// This class represents a carrier element specification.
    /// </summary>
    [Serializable()]
    [XmlType("CarrierElementSpec", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "specification", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class CarrierElementSpec : DataElementSpec
    {

        // --------------------------------------------------
        // VARIABLES
        // --------------------------------------------------

        #region Variables

        private DataValueFilter _DefinitionFilter = null;

        #endregion


        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The class filter of this instance.
        /// </summary>
        [XmlElement("carrier.filter")]
        public DataValueFilter DefinitionFilter
        {
            get {
                if (this._DefinitionFilter == null) this._DefinitionFilter = new DataValueFilter();
                return this._DefinitionFilter;
            }
            set { this._DefinitionFilter = value; }
        }

        /// <summary>
        /// Specification of the DefinitionFilter property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool DefinitionFilterSpecified
        {
            get
            {
                return this._DefinitionFilter != null &&
                    (this._DefinitionFilter.AddedValues == null || this._DefinitionFilter.AddedValues.Count > 0) &&
                    (this._DefinitionFilter.RemovedValues == null || this._DefinitionFilter.RemovedValues.Count > 0);
            }
        }

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
            AccessibilityLevel accessibilityLevel = AccessibilityLevel.Public,
            List<SpecificationLevel> specificationLevels = null)
            : base(accessibilityLevel, specificationLevels)
        {
        }

        #endregion


        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        /// <summary>
        /// Indicates whether this instance is compatible with the specified data item.
        /// </summary>
        /// <param name="item">The data item to consider.</param>
        /// <returns>True if this instance is compatible with the specified data item.</returns>
        public override bool IsCompatibleWith(DataItem item)
        {
            bool isCompatible = base.IsCompatibleWith(item);

            if (isCompatible)
            {

            }

            return isCompatible;
        }

        /// <summary>
        /// Check the specified item.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="dataElement">The element to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>The log of check log.</returns>
        public override ILog CheckItem(
            object item,
            IDataElement dataElement = null,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null)
        {
            return new Log();
        }

        /// <summary>
        /// Check the specified item.
        /// </summary>
        /// <param name="dataElement">The element to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>The log of check log.</returns>
        public override ILog CheckElement(
            IDataElement dataElement,
            string[] specificationAreas = null,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null)
        {
            return new Log();
        }

        #endregion


        // --------------------------------------------------
        // UPDATE, CHECK, REPAIR
        // --------------------------------------------------

        #region Update_Check_Repair


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
