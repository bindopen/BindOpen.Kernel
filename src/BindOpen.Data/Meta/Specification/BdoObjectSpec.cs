using BindOpen.Data.Items;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a catalog element specification.
    /// </summary>
    public class BdoObjectSpec : BdoSpec,
        IBdoObjectSpec
    {
        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CollectionElementSpec class.
        /// </summary>
        public BdoObjectSpec() : base()
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
            BdoObjectSpec specification = base.Clone(areas) as BdoObjectSpec;
            if (ClassFilter != null)
                specification.ClassFilter = ClassFilter.Clone() as StringFilter;
            //if (FormatUniqueNameFilter != null)
            //    entityElementSpec.FormatUniqueNameFilter = FormatUniqueNameFilter.Clone() as StringFilter;
            return specification;
        }

        #endregion

        // --------------------------------------------------
        // IBdoMetaObjectSpec Implementation
        // --------------------------------------------------

        #region IBdoMetaObjectSpec

        /// <summary>
        /// The class filter of this instance.
        /// </summary>
        public IStringFilter ClassFilter { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IBdoObjectSpec WithClassFilter(IStringFilter filter)
        {
            ClassFilter = filter;

            return this;
        }

        /// <summary>
        /// The definition filter of this instance.
        /// </summary>
        public IStringFilter DefinitionFilter { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IBdoObjectSpec WithDefinitionFilter(IStringFilter filter)
        {
            DefinitionFilter = filter;

            return this;
        }

        /// <summary>
        /// Indicates whether this instance is compatible with the specified item.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <returns></returns>
        public override bool IsCompatibleWithData(object item)
        {
            return (ValueType == DataValueTypes.Any || item.GetValueType().IsCompatibleWith(ValueType));
        }

        #endregion
    }
}
