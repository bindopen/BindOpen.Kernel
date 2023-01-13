using BindOpen.Extensions.Connecting;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.MetaData.Items
{
    /// <summary>
    /// This class represents a data source.
    /// </summary>
    public class BdoDatasource : BdoItem, IBdoDataSource
    {
        // -----------------------------------------------
        // CONSTRUCTORS
        // -----------------------------------------------

        #region Constructors

        /// <summary>
        /// This instantiates a new instance of the Datasource class.
        /// </summary>
        public BdoDatasource() : base()
        {
        }

        /// <summary>
        /// This instantiates a new instance of the Datasource class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        public BdoDatasource(string name) : base()
        {
            WithName(name);
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
            var dataSource = base.Clone<BdoDatasource>(areas);

            dataSource.Configurations = Configurations?.Select(p => p.Clone<IBdoConnectorConfiguration>()).ToList();

            return dataSource;
        }

        #endregion

        // -----------------------------------------------
        // IDatasource Implementation
        // ----------------------------------------------

        #region IDatasource

        /// <summary>
        /// Kind of the data module of this instance. 
        /// </summary>
        public DatasourceKind Kind { get; set; } = DatasourceKind.Any;

        /// <summary>
        /// Sets the specified kind of this instance. 
        /// </summary>
        /// <param name="kind">The kind to consider.</param>
        public IBdoDataSource WithKind(DatasourceKind kind)
        {
            Kind = kind;
            return this;
        }

        /// <summary>
        /// The module name of this instance.
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// Sets the specified module name of this instance. 
        /// </summary>
        /// <param name="moduleName">The module name to consider.</param>
        public IBdoDataSource WithModuleName(string moduleName)
        {
            ModuleName = moduleName;
            return this;
        }

        /// <summary>
        /// Indicates whether this instance is default.
        /// </summary>
        public bool IsDefault { get; set; } = false;

        /// <summary>
        /// Specifies that this instance is the default. 
        /// </summary>
        /// <param name="isDefault"></param>
        public IBdoDataSource AsDefault(bool isDefault = true)
        {
            IsDefault = isDefault;
            return this;
        }

        /// <summary>
        /// The instance name of this instance.
        /// </summary>
        public string InstanceName { get; set; }

        /// <summary>
        /// Sets the specified module name of this instance. 
        /// </summary>
        /// <param name="instanceName">The instance name to consider.</param>
        public IBdoDataSource WithInstanceName(string instanceName)
        {
            InstanceName = instanceName;
            return this;
        }

        /// <summary>
        /// Description of this instance.
        /// </summary>
        public IBdoConnectorConfiguration Configuration => Configurations.FirstOrDefault();

        /// <summary>
        /// Description of this instance.
        /// </summary>
        public List<IBdoConnectorConfiguration> Configurations { get; set; }

        /// <summary>
        /// Adds the specified connector configuration.
        /// </summary>
        /// <param name="config">The connector to add.</param>
        public IBdoDataSource AddConfig(IBdoConnectorConfiguration config)
        {
            if (config != null)
            {
                Configurations ??= new List<IBdoConnectorConfiguration>();
                Configurations.Add(config as IBdoConnectorConfiguration);
            }

            return this;
        }

        /// <summary>
        /// Removes the specified connector configuration.
        /// </summary>
        /// <param name="definitionName">The unique ID of the connector definition to consider.</param>
        public IBdoDataSource RemoveConfig(string definitionName)
        {
            Configurations ??= new List<IBdoConnectorConfiguration>();
            Configurations?.RemoveAll(p => p.DefinitionUniqueId.BdoKeyEquals(definitionName));

            return this;
        }

        /// <summary>
        /// Sets the specified configurations.
        /// </summary>
        /// <param name="configs">The configurations to consider.</param>
        public IBdoDataSource WithConfig(params IBdoConnectorConfiguration[] configs)
        {
            foreach (var config in configs)
            {
                AddConfig(config);
            }

            return this;
        }

        /// <summary>
        /// Gets the specified connector configuration.
        /// </summary>
        /// <param name="definitionName">The unique ID of the connector definition to consider.</param>
        /// <returns>The specified connector.</returns>
        public IBdoConnectorConfiguration GetConfig(string definitionName = null)
        {
            return Configurations?.FirstOrDefault(p => definitionName == null || p.DefinitionUniqueId.BdoKeyEquals(definitionName));
        }

        /// <summary>
        /// Indicates whether this instance has the specified connector configuration.
        /// </summary>
        /// <param name="definitionName">The unique ID of the connector definition to consider.</param>
        /// <returns>The data source with the specified data module name.</returns>
        public bool HasConfig(string definitionName = null)
        {
            return Configurations?.Any(p => definitionName == null || p.DefinitionUniqueId.BdoKeyEquals(definitionName)) == true;
        }

        #endregion

        // ------------------------------------------
        // IReferenced Implementation
        // ------------------------------------------

        #region IReferenced

        /// <summary>
        /// 
        /// </summary>
        public string Key() => Name;

        #endregion

        // ------------------------------------------
        // IIdentifiedPoco Implementation
        // ------------------------------------------

        #region IIdentifiedPoco

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IBdoDataSource WithId(string id)
        {
            Id = id;
            return this;
        }

        #endregion

        // ------------------------------------------
        // INamedPoco Implementation
        // ------------------------------------------

        #region INamedPoco

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IBdoDataSource WithName(string name)
        {
            Name = BdoMeta.NewName(name, "source_");
            return this;
        }

        #endregion
    }
}
