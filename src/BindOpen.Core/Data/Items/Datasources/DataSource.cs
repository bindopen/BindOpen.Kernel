using BindOpen.Application.Scopes;
using BindOpen.Data.Helpers.Objects;
using BindOpen.Extensions.Runtime;
using BindOpen.System.Diagnostics;
using BindOpen.System.Scripting;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This class represents a data source.
    /// </summary>
    [XmlType("Datasource", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "datasource", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
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
        [DefaultValue(DatasourceKind.Any)]
        public DatasourceKind Kind { get; set; } = DatasourceKind.Any;

        /// <summary>
        /// The module name of this instance.
        /// </summary>
        [XmlAttribute("moduleName")]
        public string ModuleName { get; set; } = null;

        /// <summary>
        /// Indicates whether this instance is default.
        /// </summary>
        [XmlAttribute("isDefault")]
        [DefaultValue(false)]
        public bool IsDefault { get; set; } = false;

        /// <summary>
        /// The instance name of this instance.
        /// </summary>
        [XmlAttribute("instanceName")]
        public string InstanceName { get; set; } = null;

        /// <summary>
        /// The configuration items for this instance.
        /// </summary>
        [XmlArray("configuration")]
        [XmlArrayItem("add")]
        public List<BdoConnectorConfiguration> Configurations { get; set; } = null;

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
        public Datasource(string name) : base(name, "dataSource_")
        {
        }

        #endregion

        // -----------------------------------------------
        // MUTATORS
        // -----------------------------------------------

        #region Mutators

        /// <summary>
        /// Sets the specified kind of this instance. 
        /// </summary>
        /// <param name="kind">The kind to consider.</param>
        public IDatasource WithKind(DatasourceKind kind)
        {
            Kind = kind;
            return this;
        }

        /// <summary>
        /// Sets the specified module name of this instance. 
        /// </summary>
        /// <param name="moduleName">The module name to consider.</param>
        public IDatasource WithModuleName(string moduleName)
        {
            ModuleName = moduleName;
            return this;
        }

        /// <summary>
        /// Sets the specified module name of this instance. 
        /// </summary>
        /// <param name="instanceName">The instance name to consider.</param>
        public IDatasource WithInstanceName(string instanceName)
        {
            InstanceName = instanceName;
            return this;
        }

        /// <summary>
        /// Specifies that this instance is the default. 
        /// </summary>
        public IDatasource AsDefault()
        {
            IsDefault = true;
            return this;
        }

        /// <summary>
        /// Adds the specified connector configuration.
        /// </summary>
        /// <param name="config">The connector to add.</param>
        public IDatasource AddConfiguration(IBdoConnectorConfiguration config)
        {
            if (config != null)
            {
                if (Configurations == null)
                {
                    Configurations = new List<BdoConnectorConfiguration>();
                }

                Configurations.Add(config as BdoConnectorConfiguration);
            }
            return this;
        }

        /// <summary>
        /// Removes the specified connector configuration.
        /// </summary>
        /// <param name="definitionName">The unique ID of the connector definition to consider.</param>
        public IDatasource RemoveConfiguration(string definitionName)
        {
            if (Configurations == null)
            {
                Configurations = new List<BdoConnectorConfiguration>();
            }

            Configurations?.RemoveAll(p => p.DefinitionUniqueId.KeyEquals(definitionName));
            return this;
        }

        /// <summary>
        /// Sets the specified configurations.
        /// </summary>
        /// <param name="configs">The configurations to consider.</param>
        public IDatasource WithConfiguration(params IBdoConnectorConfiguration[] configs)
        {
            foreach (var config in configs)
            {
                AddConfiguration(config);
            }
            return this;
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
        public IBdoConnectorConfiguration GetConfiguration(string definitionName = null)
        {
            return Configurations?.Find(p => definitionName == null || p.DefinitionUniqueId.KeyEquals(definitionName));
        }

        /// <summary>
        /// Indicates whether this instance has the specified connector configuration.
        /// </summary>
        /// <param name="definitionName">The unique ID of the connector definition to consider.</param>
        /// <returns>The data source with the specified data module name.</returns>
        public bool HasConfiguration(string definitionName = null)
        {
            return Configurations?.Any(p => definitionName == null || p.DefinitionUniqueId.KeyEquals(definitionName)) == true;
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
        public override object Clone(params string[] areas)
        {
            Datasource dataSource = base.Clone(areas) as Datasource;

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
                foreach (IBdoConnectorConfiguration configuration in Configurations)
                {
                    configuration.UpdateStorageInfo(log);
                }
            }
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of script variables to consider.</param>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IBdoScope scope = null, IScriptVariableSet scriptVariableSet = null, IBdoLog log = null)
        {
            if (Configurations != null)
            {
                foreach (IBdoConnectorConfiguration configuration in Configurations)
                {
                    configuration.UpdateRuntimeInfo(scope, scriptVariableSet, log);
                }
            }

            base.UpdateRuntimeInfo(scope, scriptVariableSet, log);
        }

        #endregion
    }
}
