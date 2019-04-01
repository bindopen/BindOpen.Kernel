using System.ComponentModel;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Definition.Connectors;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Extensions.Configuration.Connectors
{
    /// <summary>
    /// This class represents a connector configuration.
    /// </summary>
    [XmlType("ConnectorConfiguration", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "connector", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class ConnectorConfiguration : TAppExtensionTitledItemConfiguration<IConnectorDefinition>, IConnectorConfiguration
    {
        // -----------------------------------------------
        // VARIABLES
        // -----------------------------------------------

        #region Variables

        private IDataElementSet _detail = null;
        private string _connectionString = null;

        #endregion

        // -----------------------------------------------
        // PROPERTIES
        // -----------------------------------------------

        #region Properties

        /// <summary>
        /// The data source kind of this instance.
        /// </summary>
        [XmlIgnore()]
        public DataSourceKind DataSourceKind => Definition?.DataSourceKind ?? DataSourceKind.None;

        /// <summary>
        /// Detail of this instance.
        /// </summary>
        [XmlElement("detail")]
        public IDataElementSet Detail
        {
            get => _detail;
            set
            {
                _detail = value;
                _detail.PropertyChanged += (object sender, PropertyChangedEventArgs e) => UpdateConnectionString();
            }
        }

        /// <summary>
        /// Specification of the Detail property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool DetailSpecified => _detail != null && (_detail.ElementsSpecified || _detail.DescriptionSpecified);

        /// <summary>
        /// The connection string of this instance.
        /// </summary>
        [XmlElement("connectionString")]
        public string ConnectionString
        {
            get => _connectionString;
            set
            {
                _connectionString = (value ?? "").Replace("\n", "").Trim();
            }
        }

        /// <summary>
        /// Specification of the Connectionstring property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool ConnectionStringSpecified => !string.IsNullOrEmpty(_connectionString);

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ConnectorConfiguration class.
        /// </summary>
        public ConnectorConfiguration()
            : base(null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the ConnectorConfiguration class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="definition">The definition to consider.</param>
        /// <param name="namePreffix">The name preffix to consider.</param>
        /// <param name="connectionString">The connection string of this instance.</param>
        /// <param name="detail">The detail to consider.</param>
        protected ConnectorConfiguration(
            string name,
            IConnectorDefinition definition = default,
            string namePreffix = "connector_",
            string connectionString = null,
            IDataElementSet detail = null)
            : this(name, definition?.Key(), namePreffix, connectionString, detail)
        {
            _definition = definition;
        }

        /// <summary>
        /// Instantiates a new instance of the ConnectorConfiguration class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="definitionUniqueId">The definition unique ID to consider.</param>
        /// <param name="namePreffix">The name preffix to consider.</param>
        /// <param name="connectionString">The connection string of this instance.</param>
        /// <param name="detail">The detail to consider.</param>
        protected ConnectorConfiguration(
            string name,
            string definitionUniqueId,
            string namePreffix = "connector_",
            string connectionString = null,
            IDataElementSet detail = null)
            : base(name, definitionUniqueId, namePreffix)
        {
            DefinitionUniqueId = definitionUniqueId;
            _connectionString = connectionString;
            _detail = detail;
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Gets the detail of this instance and instantiates it if it is null.
        /// </summary>
        /// <returns>Returns the detail of this instance.</returns>
        public IDataElementSet NewDetail()
        {
            return _detail = _detail ?? new DataElementSet();
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Sets the connection string with the specified string.
        /// </summary>
        /// <param name="connectionString">The connection string to consider.</param>
        /// <returns>Returns a clone of this instance.</returns>
        public virtual void UpdateConnectionString(string connectionString= null)
        {
            if (connectionString != null)
                _connectionString = connectionString;
        }

        #endregion

        // ------------------------------------------
        // CLONING
        // ------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>A cloned connector of this instance.</returns>
        public override object Clone()
        {
            ConnectorConfiguration dataConnector = base.Clone() as ConnectorConfiguration;
            if (_detail != null)
                dataConnector.Detail = _detail.Clone() as DataElementSet;

            return dataConnector;
        }

        #endregion

        // --------------------------------------------------
        // UPDATE, CHECK, REPAIR
        // --------------------------------------------------

        #region Update_Check_Repair

        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="updateModes">The update modes to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>Log of the operation.</returns>
        /// <remarks>Put reference collections as null if you do not want to repair this instance.</remarks>
        public override ILog Update<T>(
            T item = default,
            string[] specificationAreas = null,
            UpdateMode[] updateModes = null,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null)
        {
            ILog log = new Log();

            if (item is ConnectorConfiguration)
            {
                ConnectorConfiguration connectorConfiguration = item as ConnectorConfiguration;
                _connectionString = connectorConfiguration.ConnectionString;
                if (connectorConfiguration.Detail != null)
                {
                    if (Detail == null) Detail = new DataElementSet();
                    Detail.Update(connectorConfiguration.Detail, specificationAreas, updateModes, appScope, scriptVariableSet);
                }
            }
            return log;
        }

        /// <summary>
        /// Checks this instance.
        /// </summary>
        /// <param name="isExistenceChecked">Indicates whether the carrier existence is checked.</param>
        /// <param name="item">The item to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>Returns the check log.</returns>
        public override ILog Check<T>(
            bool isExistenceChecked = true,
            T item = default,
            string[] specificationAreas = null,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null)
        {
            ILog log = new Log();

            if (item is ConnectorConfiguration)
            {
                ConnectorConfiguration connectorConfiguration = item as ConnectorConfiguration;
                _connectionString = connectorConfiguration.ConnectionString;
                if (connectorConfiguration.Detail != null)
                {
                    if (Detail == null) Detail = new DataElementSet();
                    Detail.Check(isExistenceChecked, connectorConfiguration.Detail, specificationAreas, appScope, scriptVariableSet);
                }
            }
            return log;
        }

        /// <summary>
        /// Repairs this instance with the specified definition.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="updateModes">The update modes to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>Log of the operation.</returns>
        public override ILog Repair<T>(
            T item = default,
            string[] specificationAreas = null,
            UpdateMode[] updateModes = null,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null)
        {
            ILog log = new Log();

            if (item is ConnectorConfiguration)
            {
                ConnectorConfiguration connectorConfiguration = item as ConnectorConfiguration;
                _connectionString = connectorConfiguration.ConnectionString;
                if (connectorConfiguration.Detail != null)
                {
                    if (Detail == null) Detail = new DataElementSet();
                    Detail.Repair(connectorConfiguration.Detail, specificationAreas, updateModes, appScope, scriptVariableSet);
                }
            }
            return log;
        }

        #endregion
    }
}
