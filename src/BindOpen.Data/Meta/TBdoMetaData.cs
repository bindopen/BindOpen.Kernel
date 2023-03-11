using BindOpen.Logging;
using BindOpen.Scopes.Scopes;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data element.
    /// </summary>
    public abstract class TBdoMetaData<TItem> : BdoMetaData,
        ITBdoMetaData<TItem>
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
