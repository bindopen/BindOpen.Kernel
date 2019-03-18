using System;
using System.Collections.Generic;
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
    public class ConnectorConfiguration : TAppExtensionTitledItemConfiguration<ConnectorDefinition>
    {

        // -----------------------------------------------
        // VARIABLES
        // -----------------------------------------------

        #region Variables

        private DataElementSet _Detail = null;
        private String _ConnectionString = null;

        #endregion


        // -----------------------------------------------
        // PROPERTIES
        // -----------------------------------------------

        #region Properties

        /// <summary>
        /// The data source kind of this instance.
        /// </summary>
        [XmlIgnore()]
        public DataSourceKind DataSourceKind
        {
            get { return (this.Definition == null ? DataSourceKind.None : this.Definition.DataSourceKind); }
        }

        /// <summary>
        /// Detail of this instance.
        /// </summary>
        [XmlElement("detail")]
        public DataElementSet Detail
        {
            get { return this._Detail; }
            set
            {
                this._Detail = value;
                this._Detail.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
                {
                    this.UpdateConnectionString();
                };
            }
        }

        /// <summary>
        /// Specification of the Detail property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean DetailSpecified
        {
            get
            {
                return this._Detail != null && (this._Detail.ElementsSpecified || this._Detail.DescriptionSpecified);
            }
        }

        /// <summary>
        /// The connection string of this instance.
        /// </summary>
        [XmlElement("connectionString")]
        public String ConnectionString
        {
            get { return this._ConnectionString; }
            set
            {
                this._ConnectionString = (value ?? "").Replace("\n","").Trim();
            }
        }

        /// <summary>
        /// Specification of the ConnectionString property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean ConnectionStringSpecified
        {
            get
            {
                return !String.IsNullOrEmpty(this._ConnectionString);
            }
        }

        #endregion


        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ConnectorConfiguration class.
        /// </summary>
        public ConnectorConfiguration() : this(null)
        {
        }

        /// <summary>
        /// This instantiates a new instance of the ConnectorConfiguration class.
        /// </summary>
        /// <param name="name">The name of this instance.</param>
        /// <param name="definitionName">The definition name to consider.</param>
        /// <param name="connectionString">The connection string of this instance.</param>
        /// <param name="detail">The path detail to consider.</param>
        public ConnectorConfiguration(
            String name,
            String definitionName = null,
            String connectionString = null,
            DataElementSet detail = null)
            : base(name, definitionName, null, "connector_")
        {
            this._ConnectionString = connectionString;
            this._Detail = detail;
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
        public DataElementSet NewDetail()
        {
            return this._Detail = this._Detail ?? new DataElementSet();
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
        public virtual void UpdateConnectionString(String connectionString= null)
        {
            if (connectionString != null)
                this._ConnectionString = connectionString;
        }

        /// <summary>
        /// Sets the definition of this instance.
        /// </summary>
        /// <param name="definition">The definition to consider.</param>
        /// <param name="isDefinitionChecked">Indicates whether the definition must be checked.</param>
        public override void SetDefinition(ConnectorDefinition definition = null, Boolean isDefinitionChecked = true)
        {
            base.SetDefinition(definition, isDefinitionChecked);

            if (definition!=null)
            {
                (this._Detail ?? (this._Detail = new DataElementSet())).Repair(this.Definition != null ? this.Definition.DetailSpecification : null);
                this._Detail.Update<DataElementSet>();
            }
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
        public override Object Clone()
        {
            ConnectorConfiguration dataConnector = base.Clone() as ConnectorConfiguration;
            if (this._Detail != null)
                dataConnector.Detail = this._Detail.Clone() as DataElementSet;

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
        public override Log Update<T>(
            T item = null,
            List<String> specificationAreas = null,
            List<UpdateMode> updateModes = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null)
        {
            Log log = new Log();

            if (item is ConnectorConfiguration)
            {
                ConnectorConfiguration connectorConfiguration = item as ConnectorConfiguration;
                this._ConnectionString = connectorConfiguration.ConnectionString;
                if (connectorConfiguration.Detail != null)
                {
                    if (this.Detail == null) this.Detail = new DataElementSet();
                    this.Detail.Update(connectorConfiguration.Detail, specificationAreas, updateModes, appScope, scriptVariableSet);
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
        public override Log Check<T>(
            Boolean isExistenceChecked = true,
            T item = null,
            List<String> specificationAreas = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null)
        {
            Log log = new Log();

            if (item is ConnectorConfiguration)
            {
                ConnectorConfiguration connectorConfiguration = item as ConnectorConfiguration;
                this._ConnectionString = connectorConfiguration.ConnectionString;
                if (connectorConfiguration.Detail != null)
                {
                    if (this.Detail == null) this.Detail = new DataElementSet();
                    this.Detail.Check(isExistenceChecked, connectorConfiguration.Detail, specificationAreas, appScope, scriptVariableSet);
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
        public override Log Repair<T>(
            T item = null,
            List<String> specificationAreas = null,
            List<UpdateMode> updateModes = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null)
        {
            Log log = new Log();

            if (item is ConnectorConfiguration)
            {
                ConnectorConfiguration connectorConfiguration = item as ConnectorConfiguration;
                this._ConnectionString = connectorConfiguration.ConnectionString;
                if (connectorConfiguration.Detail != null)
                {
                    if (this.Detail == null) this.Detail = new DataElementSet();
                    this.Detail.Repair(connectorConfiguration.Detail, specificationAreas, updateModes, appScope, scriptVariableSet);
                }
            }
            return log;
        }

        #endregion

    }
}
