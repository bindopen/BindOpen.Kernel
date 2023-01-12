using BindOpen.Meta.Specification;

namespace BindOpen.Meta.Elements
{
    /// <summary>
    /// This class represents an object element specification.
    /// </summary>
    public class BdoMetaObjectSpec : BdoMetaElementSpec, IBdoMetaObjectSpec
    {
        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ObjectElementSpec class.
        /// </summary>
        public BdoMetaObjectSpec() : base()
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
            BdoMetaObjectSpec specification = base.Clone(areas) as BdoMetaObjectSpec;
            if (ClassFilter != null)
                specification.ClassFilter = ClassFilter.Clone() as DataValueFilter;
            //if (FormatUniqueNameFilter != null)
            //    entityElementSpec.FormatUniqueNameFilter = FormatUniqueNameFilter.Clone() as DataValueFilter;
            return specification;
        }

        #endregion

        // --------------------------------------------------
        // IObjectElementSpec Implementation
        // --------------------------------------------------

        #region IObjectElementSpec

        // Entity ----------------------------------

        /// <summary>
        /// The class filter of this instance.
        /// </summary>
        public IDataValueFilter ClassFilter { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IBdoMetaObjectSpec WithClassFilter(IDataValueFilter filter)
        {
            ClassFilter = filter;

            return this;
        }

        /// <summary>
        /// The definition filter of this instance.
        /// </summary>
        public IDataValueFilter DefinitionFilter { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IBdoMetaObjectSpec WithDefinitionFilter(IDataValueFilter filter)
        {
            DefinitionFilter = filter;

            return this;
        }

        #endregion
    }
}
