using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Dictionary;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Data.Specification.Filters;
using BindOpen.Framework.Core.Extensions.Definition.Connectors;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.Elements.Source
{

    /// <summary>
    /// This class represents a data source element specification.
    /// </summary>
    [Serializable()]
    [XmlType("SourceElementSpec", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "specification", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class SourceElementSpec : DataElementSpec
    {

        // --------------------------------------------------
        // VARIABLES
        // --------------------------------------------------

        #region Variables

        private DataSourceKind _DataSourceKind = DataSourceKind.Any;
        private DataValueFilter _ConnectorFilter = null;

        #endregion


        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The data source kind of this instance.
        /// </summary>
        [XmlAttribute("kind")]
        public DataSourceKind DataSourceKind
        {
            get { return this._DataSourceKind; }
            set { this._DataSourceKind = value; }
        }

        /// <summary>
        /// The connector filter of this instance.
        /// </summary>
        [XmlElement("connector.filter")]
        public DataValueFilter ConnectorFilter
        {
            get {
                if (this._ConnectorFilter == null) this._ConnectorFilter = new DataValueFilter();
                return this._ConnectorFilter;
            }
            set { this._ConnectorFilter = value; }
        }

        /// <summary>
        /// Specification of the ClassFilter property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean ConnectorFilterSpecified
        {
            get
            {
                return this._ConnectorFilter != null &&
                    (this._ConnectorFilter.AddedValues == null || this._ConnectorFilter.AddedValues.Count > 0) &&
                    (this._ConnectorFilter.RemovedValues == null || this._ConnectorFilter.RemovedValues.Count > 0);
            }
        }

        #endregion


        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new data source element specification.
        /// </summary>
        public SourceElementSpec(): this(AccessibilityLevel.Public)
        {
        }

        /// <summary>
        /// Initializes a new data source element specification.
        /// </summary>
        /// <param name="accessibilityLevel">The accessibilty level of this instance.</param>
        /// <param name="specificationLevels">The specification levels of this instance.</param>
        public SourceElementSpec(
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
        public override Boolean IsCompatibleWith(DataItem item)
        {
            Boolean isCompatible = base.IsCompatibleWith(item);

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
        public override Log CheckItem(
            Object item,
            DataElement dataElement = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null)
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
        public override Log CheckElement(
            DataElement dataElement,
            List<String> specificationAreas = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null)
        {
            return new Log();
        }

        /// <summary>
        /// Creates a new data element respecting this instance.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="detail">The detail to consider.</param>
        /// <returns>Returns a new data element respecting this instance.</returns>
        public override DataElement NewElement(IAppScope appScope = null, DataElementSet detail = null)
        {
            DataSource dataSource = new DataSource(null, this._DataSourceKind);

            if (appScope!=null && this._ConnectorFilter != null)
            {
                List<String> connectorUniqueNames = this._ConnectorFilter.GetValues(
                    appScope.AppExtension.GetItemDefinitions<ConnectorDefinition>().Select(p=>p.Key()).ToList());
                if (connectorUniqueNames.Count > 0)
                    dataSource.AddConfiguration(appScope, null, connectorUniqueNames[0], detail);
            }

            SourceElement dataSourceElement = new SourceElement(this.Name, dataSource)
            {
                Title = this.Title.Clone() as DictionaryDataItem,
                Description = this.Description.Clone() as DictionaryDataItem,
            };

            return dataSourceElement;
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
        public override Object Clone()
        {
            SourceElementSpec dataSourceElementSpec = base.Clone() as SourceElementSpec;
            if (this.ConnectorFilter != null)
                dataSourceElementSpec.ConnectorFilter = this.ConnectorFilter.Clone() as DataValueFilter;
            return dataSourceElementSpec;
        }

        #endregion

    }

}
