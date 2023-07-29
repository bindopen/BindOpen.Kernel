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
        /// The module name of this instance.
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// Indicates whether this instance is default.
        /// </summary>
        public bool IsDefault { get; set; } = false;

        /// <summary>
        /// The instance name of this instance.
        /// </summary>
        public string InstanceName { get; set; }

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
