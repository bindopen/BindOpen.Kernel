using BindOpen.MetaData.Items;
using BindOpen.MetaData.Specification;

namespace BindOpen.MetaData.Elements
{
    /// <summary>
    /// This class represents a data source element specification.
    /// </summary>
    public class BdoMetaSourceSpec : BdoMetaElementSpec, IBdoMetaSourceSpec
    {
        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new data source element specification.
        /// </summary>
        public BdoMetaSourceSpec() : base()
        {
        }

        #endregion

        // --------------------------------------------------
        // CLONING
        // --------------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a cloned instance.</returns>
        public override object Clone(params string[] areas)
        {
            BdoMetaSourceSpec specification = base.Clone(areas) as BdoMetaSourceSpec;
            if (DefinitionFilter != null)
                specification.DefinitionFilter = DefinitionFilter.Clone() as DataValueFilter;
            return specification;
        }

        #endregion

        // --------------------------------------------------
        // ISourceElementSpec Implementation
        // --------------------------------------------------

        #region ISourceElementSpec

        /// <summary>
        /// The data source kind of this instance.
        /// </summary>
        public DatasourceKind DatasourceKind { get; set; } = DatasourceKind.Any;

        public IBdoMetaSourceSpec WithDatasourceKind(DatasourceKind kind)
        {
            DatasourceKind = kind;

            return this;
        }

        /// <summary>
        /// The definition filter of this instance.
        /// </summary>
        public IDataValueFilter DefinitionFilter { get; set; }

        public IBdoMetaSourceSpec WithDefinitionFilter(IDataValueFilter filter)
        {
            DefinitionFilter = filter;

            return this;
        }

        #endregion
    }
}
