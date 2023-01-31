using BindOpen.Data.Specification;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a catalog element specification.
    /// </summary>
    public class BdoMetaSetSpec : BdoMetaDataSpec, IBdoMetaSetSpec
    {
        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CollectionElementSpec class.
        /// </summary>
        public BdoMetaSetSpec() : base()
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
            BdoMetaSetSpec specification = base.Clone(areas) as BdoMetaSetSpec;
            if (ClassFilter != null)
                specification.ClassFilter = ClassFilter.Clone() as DataValueFilter;
            //if (FormatUniqueNameFilter != null)
            //    entityElementSpec.FormatUniqueNameFilter = FormatUniqueNameFilter.Clone() as DataValueFilter;
            return specification;
        }

        #endregion

        // --------------------------------------------------
        // ICollectionElementSpec Implementation
        // --------------------------------------------------

        #region ICollectionElementSpec

        /// <summary>
        /// The class filter of this instance.
        /// </summary>
        public IDataValueFilter ClassFilter { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IBdoMetaSetSpec WithClassFilter(IDataValueFilter filter)
        {
            ClassFilter = filter;

            return this;
        }

        /// <summary>
        /// The definition filter of this instance.
        /// </summary>
        public IDataValueFilter DefinitionFilter { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IBdoMetaSetSpec WithDefinitionFilter(IDataValueFilter filter)
        {
            DefinitionFilter = filter;

            return this;
        }

        #endregion
    }
}
