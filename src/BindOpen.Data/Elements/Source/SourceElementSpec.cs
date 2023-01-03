using BindOpen.Data.Items;
using BindOpen.Data.Specification;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This class represents a data source element specification.
    /// </summary>
    public class SourceElementSpec : BdoElementSpec, ISourceElementSpec
    {
        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new data source element specification.
        /// </summary>
        public SourceElementSpec() : base()
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
            SourceElementSpec specification = base.Clone(areas) as SourceElementSpec;
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

        public ISourceElementSpec WithDatasourceKind(DatasourceKind kind)
        {
            DatasourceKind = kind;

            return this;
        }

        /// <summary>
        /// The definition filter of this instance.
        /// </summary>
        public IDataValueFilter DefinitionFilter { get; set; }

        public ISourceElementSpec WithDefinitionFilter(IDataValueFilter filter)
        {
            DefinitionFilter = filter;

            return this;
        }

        #endregion
    }
}
