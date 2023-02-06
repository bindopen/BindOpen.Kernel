using BindOpen.Logging;
using BindOpen.Runtime.Scopes;
using System.Collections.Generic;
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
        where TSpec : IBdoMetaSpec
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
        /// <param name="name">The name to consider.</param>
        /// <param name="namePreffix">The name preffix to consider.</param>
        /// <param name="id">The ID to consider.</param>
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
        /// Data constraint statement of this instance.
        /// </summary>
        public new List<TSpec> Specs
        {
            get => base.Specs?.Cast<TSpec>().ToList();
            set { base.WithSpecs(value?.Cast<IBdoMetaSpec>().ToArray()); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
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
        /// <param name="spec"></param>
        /// <returns></returns>
        public TElement WithSpecs(params TSpec[] specs)
        {
            return (TElement)base.WithSpecs(specs.Cast<IBdoMetaSpec>().ToArray());
        }

        // Data

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param name="log">The log to populate.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public new virtual TItem GetData(
            IBdoScope scope = null,
            IBdoMetaList varSet = null,
            IBdoLog log = null)
            => GetData<TItem>(scope, varSet, log);

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param name="log">The log to populate.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public abstract Q GetData<Q>(
            IBdoScope scope = null,
            IBdoMetaList varSet = null,
            IBdoLog log = null)
            where Q : TItem;

        #endregion
    }
}
