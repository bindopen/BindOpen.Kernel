using BindOpen.Logging;
using BindOpen.Runtime.Scopes;
using System.Linq;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data element.
    /// </summary>
    public abstract class TBdoMetaData<TElement, TSpec, TItem> :
        BdoMetaData,
        ITBdoMetaData<TElement, TSpec, TItem>
        where TElement : IBdoMetaData
        where TSpec : IBdoSpec
        where TItem : class
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

        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        // Specifications ---------------------

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
        public TElement WithSpecs(params TSpec[] specs)
        {
            return (TElement)base.WithSpecs(specs.Cast<IBdoSpec>().ToArray());
        }

        // Data

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param key="log">The log to populate.</param>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="varSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public new virtual TItem GetData(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
            => GetData<TItem>(scope, varSet, log);

        #endregion
    }
}
