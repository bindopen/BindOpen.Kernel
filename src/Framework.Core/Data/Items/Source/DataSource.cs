using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Extensions.Items.Connectors;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.Items.Source
{
    /// <summary>
    /// This class represents a data source.
    /// </summary>
    [Serializable()]
    [XmlType("DataSource", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "dataSource", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class DataSource : NamedDataItem, IDataSource
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
        public bool KindSpecified => Kind != DataSourceKind.Any;

        /// <summary>
        /// The module name of this instance.
        /// </summary>
        [XmlAttribute("moduleName")]
        public string ModuleName { get; set; } = null;

        /// <summary>
        /// Specification of the ModuleName property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool ModuleNameSpecified => !string.IsNullOrEmpty(ModuleName);

        /// <summary>
        /// The instance name of this instance.
        /// </summary>
        [XmlAttribute("instanceName")]
        public string InstanceName { get; set; } = null;

        /// <summary>
        /// Specification of the InstanceName property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool InstanceNameSpecified => !string.IsNullOrEmpty(InstanceName);

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
            string name,
            DataSourceKind kind,
            params IConnectorConfiguration[] configurations) : base(name, "dataSource_")
        {
            Kind = kind;
            Configurations = configurations?.Select(p=>p as ConnectorConfiguration).ToList();
        }

        #endregion

        // -----------------------------------------------
        // MUTATORS
        // -----------------------------------------------

        #region Mutators

        /// <summary>
        /// Adds the specified connector configuration.
        /// </summary>
        /// <param name="config">The connector to add.</param>
        public void AddConfiguration(IConnectorConfiguration config)
        {
            if (config != null)
                Configurations.Add(config as ConnectorConfiguration);
        }

        /// <summary>
        /// Removes the specified connector configuration.
        /// </summary>
        /// <param name="definitionName">The unique ID of the connector definition to consider.</param>
        public void RemoveConfiguration(string definitionName)
        {
                Configurations?.RemoveAll(p => p.DefinitionUniqueId.KeyEquals(definitionName));
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
        /// <param name="definitionName">The unique ID of the connector definition to consider.</param>
        /// <returns>The specified connector.</returns>
        public IConnectorConfiguration GetConfiguration(string definitionName)
        {
            return Configurations?.Find(p => definitionName ==null || p.DefinitionUniqueId.KeyEquals(definitionName));
        }

        /// <summary>
        /// Indicates whether this instance has the specified connector configuration.
        /// </summary>
        /// <param name="definitionName">The unique ID of the connector definition to consider.</param>
        /// <returns>The data source with the specified data module name.</returns>
        public bool HasConfiguration(string definitionName)
        {
            return Configurations?.Any(p => p.DefinitionUniqueId.KeyEquals(definitionName)) == true;
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
        public override object Clone()
        {
            DataSource dataSource = base.Clone() as DataSource;

            if (Configurations != null)
                dataSource.Configurations = Configurations?.Select(p => p.Clone() as ConnectorConfiguration).ToList();

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
        public override void UpdateStorageInfo(ILog log = null)
        {
            base.UpdateStorageInfo(log);

            if (Configurations != null)
            {
                foreach (IConnectorConfiguration connector in Configurations)
                {
                    connector.UpdateStorageInfo(log);
                }
            }
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IAppScope appScope = null, IScriptVariableSet scriptVariableSet = null, ILog log = null)
        {
            base.UpdateRuntimeInfo(appScope, scriptVariableSet, log);

            if (Configurations != null)
            {
                foreach (IConnectorConfiguration configuration in Configurations)
                {
                    configuration.UpdateRuntimeInfo(appScope, scriptVariableSet, log);
                }
            }
        }

        #endregion
    }
}
