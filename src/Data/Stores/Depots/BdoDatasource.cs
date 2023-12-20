using BindOpen.Data.Meta;

namespace BindOpen.Data.Stores
{
    /// <summary>
    /// This class represents a data source.
    /// </summary>
    public class BdoDatasource : TBdoMetaWrapper<BdoMetaNode>, IBdoDatasource
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        public string Name { get; set; }

        public IBdoDataType DataType { get; set; }

        [BdoProperty("datasourceKind")]
        public DatasourceKind DatasourceKind { get; set; }

        [BdoProperty("connectionString")]
        public string ConnectionString { get; set; }

        public new IBdoMetaNode Detail { get => base.Detail; set { base.Detail = value as BdoMetaNode; } }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoDatasource class.
        /// </summary>
        public BdoDatasource() : base()
        {
        }

        #endregion

        /// <summary>
        /// Returns the key of this instance.
        /// </summary>
        /// <returns></returns>
        public override string Key()
        {
            return Name;
        }

    }
}
