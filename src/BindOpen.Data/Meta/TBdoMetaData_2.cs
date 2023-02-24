using System.Linq;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data element.
    /// </summary>
    public abstract class TBdoMetaData<TItem, TSpec> : TBdoMetaData<TItem>,
        ITBdoMetaData<TItem>
        where TSpec : IBdoSpec
    {
        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the TBdoElement class.
        /// </summary>
        public TBdoMetaData() : this(null)
        {
        }

        /// <summary>
        /// Initializes a new data element.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="namePreffix">The name preffix to consider.</param>
        /// <param key="id">The ID to consider.</param>
        protected TBdoMetaData(
            string name = null,
            string namePreffix = null,
            string id = null)
            : base(name, namePreffix, id)
        {
        }

        #endregion

        // --------------------------------------------------
        // ITBdoElement implementation
        // --------------------------------------------------

        #region ITBdoElement

        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        /// <returns></returns>
        public new TSpec GetSpec(string name = null)
        {
            return (TSpec)base.GetSpec(name);
        }

        /// <summary>
        /// Gets a new specification.
        /// </summary>
        /// <returns>Returns the new specifcation.</returns>
        public new TSpec NewSpec()
        {
            return (TSpec)base.NewSpec();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="spec"></param>
        /// <returns></returns>
        public ITBdoMetaData<TItem, TSpec> WithSpecs(params TSpec[] specs)
        {
            return (ITBdoMetaData<TItem, TSpec>)base.WithSpecs(specs.Cast<IBdoSpec>().ToArray());
        }

        #endregion
    }
}
