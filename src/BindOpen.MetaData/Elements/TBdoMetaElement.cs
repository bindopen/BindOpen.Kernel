using BindOpen.MetaData.Items;
using BindOpen.Logging;
using BindOpen.Runtime.Scopes;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.MetaData.Elements
{
    /// <summary>
    /// This class represents a data element.
    /// </summary>
    public abstract class TBdoMetaElement<TElement, TSpec, TItem> : BdoMetaElement,
        ITBdoMetaElement<TElement, TSpec, TItem>
        where TElement : IBdoMetaElement
        where TSpec : IBdoMetaElementSpec
        where TItem : class
    {
        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the TBdoElement class.
        /// </summary>
        public TBdoMetaElement() : this(null)
        {
        }

        /// <summary>
        /// Initializes a new data element.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="namePreffix">The name preffix to consider.</param>
        /// <param name="id">The ID to consider.</param>
        protected TBdoMetaElement(
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
            get => base.Specs.Cast<TSpec>().ToList();
            set { base.WithSpecifications(value?.Cast<IBdoMetaElementSpec>().ToArray()); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public new TSpec GetSpecification(string name = null)
        {
            return (TSpec)base.GetSpecification(name);
        }

        /// <summary>
        /// Gets a new specification.
        /// </summary>
        /// <returns>Returns the new specifcation.</returns>
        public new TSpec NewSpecification()
        {
            return (TSpec)base.NewSpecification();
        }

        // Items ----------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public new TElement ClearItem()
        {
            return (TElement)base.ClearItem();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public new TElement WithIndex(int? index)
        {
            return (TElement)base.WithIndex(index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public new TElement WithItemizationMode(DataItemizationMode mode)
        {
            return (TElement)base.WithItemizationMode(mode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reference"></param>
        /// <returns></returns>
        public new TElement WithItemReference(IBdoReference reference)
        {
            return (TElement)base.WithItemReference(reference);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="script"></param>
        /// <returns></returns>
        public new TElement WithItemScript(string script)
        {
            return (TElement)base.WithItemScript(script);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public new TElement WithValueType(DataValueTypes valueType)
        {
            return (TElement)base.WithValueType(valueType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public new TElement WithSpecifications(params IBdoMetaElementSpec[] specs)
        {
            return (TElement)base.WithSpecifications(specs);
        }

        /// <summary>
        /// Sets the item of this instance.
        /// </summary>
        /// <param name="item">The string item of this instance.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the items will be the default ones..</remarks>
        /// <returns>Returns True if the specified has been well added.</returns>
        public virtual TElement WithItem(params TItem[] objs)
        {
            return (TElement)base.WithItem(objs);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="varElementSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public List<TItem> GetItemList(
            IBdoScope scope = null,
            IBdoElementSet varElementSet = null,
            IBdoLog log = null)
        {
            var item = GetItem(scope, varElementSet, log);
            List<object> itemList = item.GetType().IsList() ? item as List<object> : new List<object> { item };

            return itemList.Cast<TItem>().ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="varElementSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public TItem GetFirstItem(
            IBdoScope scope = null,
            IBdoElementSet varElementSet = null,
            IBdoLog log = null)
            => GetItemList(scope, varElementSet, log)?.FirstOrDefault();

        #endregion
    }
}
