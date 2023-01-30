using BindOpen.Data.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This class represents a data source.
    /// </summary>
    public class BdoDatasource : BdoItem, IBdoDatasource
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
            this.WithName(name);
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

            dataSource.ConfigList = ConfigList?.Select(p => p.Clone<IBdoConfiguration>()).ToList();

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
        public IBdoDatasource WithKind(DatasourceKind kind)
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
        public IBdoDatasource WithModuleName(string moduleName)
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
        public IBdoDatasource AsDefault(bool isDefault = true)
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
        public IBdoDatasource WithInstanceName(string instanceName)
        {
            InstanceName = instanceName;
            return this;
        }

        /// <summary>
        /// Description of this instance.
        /// </summary>
        public IBdoConfiguration Config() => ConfigList.FirstOrDefault();

        /// <summary>
        /// Description of this instance.
        /// </summary>
        public List<IBdoConfiguration> ConfigList { get; set; }

        /// <summary>
        /// Adds the specified connector config.
        /// </summary>
        /// <param name="config">The connector to add.</param>
        public IBdoDatasource AddConfig(IBdoConfiguration config)
        {
            if (config != null)
            {
                ConfigList ??= new List<IBdoConfiguration>();
                ConfigList.Add(config);
            }

            return this;
        }

        /// <summary>
        /// Removes the specified connector config.
        /// </summary>
        /// <param name="definitionName">The unique ID of the connector definition to consider.</param>
        public IBdoDatasource RemoveConfig(string definitionName)
        {
            ConfigList ??= new List<IBdoConfiguration>();
            ConfigList?.RemoveAll(p => p.DefinitionUniqueId.BdoKeyEquals(definitionName));

            return this;
        }

        /// <summary>
        /// Sets the specified configs.
        /// </summary>
        /// <param name="configs">The configs to consider.</param>
        public IBdoDatasource WithConfig(params IBdoConfiguration[] configs)
        {
            foreach (var config in configs)
            {
                AddConfig(config);
            }

            return this;
        }

        /// <summary>
        /// Gets the specified connector config.
        /// </summary>
        /// <param name="definitionName">The unique ID of the connector definition to consider.</param>
        /// <returns>The specified connector.</returns>
        public IBdoConfiguration GetConfig(string definitionName = null)
        {
            return ConfigList?.FirstOrDefault(p => definitionName == null || p.DefinitionUniqueId.BdoKeyEquals(definitionName));
        }

        /// <summary>
        /// Indicates whether this instance has the specified connector config.
        /// </summary>
        /// <param name="definitionName">The unique ID of the connector definition to consider.</param>
        /// <returns>The data source with the specified data module name.</returns>
        public bool HasConfig(string definitionName = null)
        {
            return ConfigList?.Any(p => definitionName == null || p.DefinitionUniqueId.BdoKeyEquals(definitionName)) == true;
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
        // IIdentified Implementation
        // ------------------------------------------

        #region IIdentified

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        #endregion

        // ------------------------------------------
        // INamed Implementation
        // ------------------------------------------

        #region INamed

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        #endregion
    }
}
