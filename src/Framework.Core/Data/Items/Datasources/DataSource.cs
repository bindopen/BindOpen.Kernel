using BindOpen.Framework.Application.Scopes;
using BindOpen.Framework.Data.Helpers.Objects;
using BindOpen.Framework.Extensions.Runtime;
using BindOpen.Framework.System.Diagnostics;
using BindOpen.Framework.System.Scripting;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace BindOpen.Framework.Data.Items
{
    /// <summary>
    /// This class represents a data source.
    /// </summary>
    [XmlType("Datasource", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "dataSource", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class Datasource : NamedDataItem, IDatasource
    {
        // -----------------------------------------------
        // PROPERTIES
        // ----------------------------------------------

        #region Properties

        /// <summary>
        /// Kind of the data module of this instance. 
        /// </summary>
        [XmlAttribute("kind")]
        public DatasourceKind Kind { get; set; } = DatasourceKind.Any;

        /// <summary>
        /// Specification of the Kind property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool KindSpecified => Kind != DatasourceKind.Any;

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
        public List<BdoConnectorConfiguration> Configurations { get; set; } = null;

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
        /// This instantiates a new instance of the Datasource class.
        /// </summary>
        public Datasource() : base(null, "dataSource_")
        {
        }

        /// <summary>
        /// This instantiates a new instance of the Datasource class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="kind">The kind of the data source to consider.</param>
        /// <param name="configurations">The configurations to consider.</param>
        public Datasource(
            string name,
            DatasourceKind kind,
            params IBdoConnectorConfiguration[] configurations) : base(name, "dataSource_")
        {
            Kind = kind;
            Configurations = configurations?.Select(p => p as BdoConnectorConfiguration).ToList();
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
        public void AddConfiguration(IBdoConnectorConfiguration config)
        {
            if (config != null)
                Configurations.Add(config as BdoConnectorConfiguration);
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
        public IBdoConnectorConfiguration GetConfiguration(string definitionName)
        {
            return Configurations?.Find(p => definitionName == null || p.DefinitionUniqueId.KeyEquals(definitionName));
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
            Datasource dataSource = base.Clone() as Datasource;

            if (Configurations != null)
                dataSource.Configurations = Configurations?.Select(p => p.Clone() as BdoConnectorConfiguration).ToList();

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
        public override void UpdateStorageInfo(IBdoLog log = null)
        {
            base.UpdateStorageInfo(log);

            if (Configurations != null)
            {
                foreach (IBdoConnectorConfiguration connector in Configurations)
                {
                    connector.UpdateStorageInfo(log);
                }
            }
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of script variables to consider.</param>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IBdoScope scope = null, IBdoScriptVariableSet scriptVariableSet = null, IBdoLog log = null)
        {
            base.UpdateRuntimeInfo(scope, scriptVariableSet, log);

            if (Configurations != null)
            {
                foreach (IBdoConnectorConfiguration configuration in Configurations)
                {
                    configuration.UpdateRuntimeInfo(scope, scriptVariableSet, log);
                }
            }
        }

        #endregion
    }
}
