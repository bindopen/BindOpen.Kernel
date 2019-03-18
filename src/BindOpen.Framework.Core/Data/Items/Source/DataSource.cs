using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Extensions.Configuration.Connectors;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Data.Items.Source
{
    /// <summary>
    /// This class represents a data source.
    /// </summary>
    [Serializable()]
    [XmlType("DataSource", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "dataSource", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class DataSource : NamedDataItem
    {
        // -----------------------------------------------
        // PROPERTIES
        // ----------------------------------------------

        #region Properties

        /// <summary>
        /// Kind of the data module of this instance. 
        /// </summary>
        [XmlAttribute("kind")]
        public DataSourceKind Kind { get; set; } = DataSourceKind.Any;

        /// <summary>
        /// Specification of the Kind property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean KindSpecified => Kind != DataSourceKind.Any;

        /// <summary>
        /// The module name of this instance.
        /// </summary>
        [XmlAttribute("moduleName")]
        public String ModuleName { get; set; } = null;

        /// <summary>
        /// Specification of the ModuleName property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean ModuleNameSpecified => !string.IsNullOrEmpty(ModuleName);

        /// <summary>
        /// The instance name of this instance.
        /// </summary>
        [XmlAttribute("instanceName")]
        public String InstanceName { get; set; } = null;

        /// <summary>
        /// Specification of the InstanceName property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean InstanceNameSpecified => !string.IsNullOrEmpty(InstanceName);

        /// <summary>
        /// The connectors for this instance.
        /// </summary>
        [XmlElement("configuration")]
        public List<ConnectorConfiguration> Configurations { get; set; } = null;

        /// <summary>
        /// Specification of the ConnectorConfigurations property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool ConfigurationsSpecified => Configurations?.Count > 0;

        #endregion

        // -----------------------------------------------
        // CONSTRUCTORS
        // -----------------------------------------------

        #region Constructors

        /// <summary>
        /// This instantiates a new instance of the DataSource class.
        /// </summary>
        public DataSource() : base(null, "dataSource_")
        {
        }

        /// <summary>
        /// This instantiates a new instance of the DataSource class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="kind">The kind of the data source to consider.</param>
        /// <param name="configurations">The configurations to consider.</param>
        public DataSource(
            String name,
            DataSourceKind kind,
            params ConnectorConfiguration[] configurations) : base(name, "dataSource_")
        {
            this.Kind = kind;
            this.Configurations = configurations?.ToList();
        }

        #endregion

        // -----------------------------------------------
        // MUTATORS
        // -----------------------------------------------

        #region Mutators

        /// <summary>
        /// Adds the specified connector configuration.
        /// </summary>
        /// <param name="connector">The connector to add.</param>
        public void AddConfiguration(ConnectorConfiguration connector)
        {
            if (connector != null)
                this.Configurations.Add(connector);
        }

        /// <summary>
        /// Adds the specified new connector.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="name">The name of connector to create.</param>
        /// <param name="definitionName">The unique name of connector to create.</param>
        /// <param name="connectionString">The connection string of this instance.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The created connector.</returns>
        public void AddConfiguration(
            IAppScope appScope,
            String name,
            String definitionName,
            String connectionString = null,
            Log log = null)
        {
            if (appScope != null)
                this.AddConfiguration(ConnectorConfigurationFactory.Create(appScope, name, definitionName, connectionString, log));
        }

        /// <summary>
        /// Adds the specified new connector.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="name">The name of connector to create.</param>
        /// <param name="definitionName">The unique name of connection to create.</param>
        /// <param name="detail">The detail to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The created connector.</returns>
        public void AddConfiguration(
            IAppScope appScope,
            String name,
            String definitionName,
            DataElementSet detail = null,
            Log log = null)
        {
            if (appScope != null)
                this.AddConfiguration(ConnectorConfigurationFactory.Create(appScope, name, definitionName, detail, log));
        }

        /// <summary>
        /// Adds the specified new connector.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="name">The name of connector to create.</param>
        /// <param name="definitionName">The unique name of connection to create.</param>
        /// <param name="dynamicObject">The object parameters to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The created connector.</returns>
        public void AddConfiguration(
            IAppScope appScope,
            String name,
            String definitionName,
            DynamicObject dynamicObject,
            Log log = null)
        {
            if (appScope != null)
                this.AddConfiguration(ConnectorConfigurationFactory.Create(appScope, name, definitionName, dynamicObject, log));
        }

        /// <summary>
        /// Removes the specified connector configuration.
        /// </summary>
        /// <param name="definitionName">The unique name of the connector definition to consider.</param>
        public void RemoveConfiguration(String definitionName)
        {
                this.Configurations?.RemoveAll(p => p.DefinitionUniqueId.KeyEquals(definitionName));
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        // Connectors ------------------------------------------

        /// <summary>
        /// Gets the specified connector configuration.
        /// </summary>
        /// <param name="definitionName">The unique name of the connector definition to consider.</param>
        /// <returns>The specified connector.</returns>
        public ConnectorConfiguration GetConfiguration(String definitionName)
        {
            return this.Configurations?.Find(p => definitionName ==null || p.DefinitionUniqueId.KeyEquals(definitionName));
        }

        /// <summary>
        /// Indicates whether this instance has the specified connector configuration.
        /// </summary>
        /// <param name="definitionName">The unique name of the connector definition to consider.</param>
        /// <returns>The data source with the specified data module name.</returns>
        public Boolean HasConfiguration(String definitionName)
        {
            return this.Configurations!=null && this.Configurations.Any(p => p.DefinitionUniqueId.KeyEquals(definitionName));
        }

        #endregion

        // ------------------------------------------
        // CLONING
        // ------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a clone of this instance.</returns>
        public override Object Clone()
        {
            DataSource dataSource = base.Clone() as DataSource;

            if (this.Configurations != null)
                dataSource.Configurations = this.Configurations?.Select(p => p.Clone() as ConnectorConfiguration).ToList();

            return dataSource;
        }

        #endregion

        // --------------------------------------------------
        // SERIALIZATION
        // --------------------------------------------------

        #region Saving_Loading

        /// <summary>
        /// Updates information for storage.
        /// </summary>
        /// <param name="log">The log to update.</param>
        public override void UpdateStorageInfo(Log log = null)
        {
            base.UpdateStorageInfo(log);

            if (this.Configurations != null)
                foreach(ConnectorConfiguration connector in this.Configurations)
                    connector.UpdateStorageInfo(log);
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IAppScope appScope = null, Log log = null)
        {
            base.UpdateRuntimeInfo(appScope, log);

            if (this.Configurations != null)
            {
                foreach (ConnectorConfiguration connector in this.Configurations)
                {
                    connector.UpdateRuntimeInfo(appScope, log);
                }
            }
        }

        #endregion
    }
}
