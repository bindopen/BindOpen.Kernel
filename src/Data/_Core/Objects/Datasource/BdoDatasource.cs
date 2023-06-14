using BindOpen.System.Data.Meta;

namespace BindOpen.System.Data
{
    /// <summary>
    /// This class represents a data source.
    /// </summary>
    public class BdoDatasource : BdoConfigurationSet, IBdoDatasource
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
        /// <param key="name">The name to consider.</param>
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
        public override object Clone()
        {
            var dataSource = Clone<BdoDatasource>();

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
        /// <param key="kind">The kind to consider.</param>
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
        /// <param key="moduleName">The module name to consider.</param>
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
        /// <param key="isDefault"></param>
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
        /// <param key="instanceName">The instance name to consider.</param>
        public IBdoDatasource WithInstanceName(string instanceName)
        {
            InstanceName = instanceName;
            return this;
        }

        /// <summary>
        /// Sets the specified single item of this instance.
        /// </summary>
        /// <param key="items">The items to apply to this instance.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the values will be the default ones..</remarks>
        public new IBdoDatasource With(params IBdoConfiguration[] items)
        {
            base.With(items);

            return this;
        }

        #endregion

        // ------------------------------------------
        // IReferenced Implementation
        // ------------------------------------------

        #region IReferenced

        /// <summary>
        /// 
        /// </summary>
        public override string Key() => Name;

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
