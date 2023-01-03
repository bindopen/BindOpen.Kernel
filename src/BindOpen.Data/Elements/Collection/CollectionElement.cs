using BindOpen.Data.Items;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This class represents a catalog el that is an el whose els are entities.
    /// </summary>
    public class CollectionElement :
        TBdoElement<ICollectionElement, ICollectionElementSpec, IBdoElement>,
        ICollectionElement
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        // Specification -----------------------

        public List<IBdoElement> Items
        {
            get => GetItemList();
            set { WithItem(value?.ToArray()); }
        }

        // IDataItemSet -----------------------

        /// <summary>
        /// Returns the el with the specified indexed.
        /// </summary>
        public IBdoElement this[int index]
            => Items.GetAt(index) as IBdoElement;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IBdoElement this[string key] => Get(key);

        /// <summary>
        /// Returns the number of items.
        /// </summary>
        public int Count
        {
            get => Items?.Count ?? 0;
        }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CollectionElement class.
        /// </summary>
        public CollectionElement() : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the CollectionElement class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="id">The ID to consider.</param>
        public CollectionElement(string name = null, string id = null)
            : base(name, "collectionElem_", id)
        {
            WithValueType(DataValueTypes.Element);
        }

        #endregion

        // --------------------------------------------------
        // ITEMS
        // --------------------------------------------------

        #region Items

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IBdoElementSpec IBdoElement.NewSpecification()
        {
            return NewSpecification();
        }

        // Items ----------------------------

        /// <summary>
        /// Returns a text node representing this instance.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "";
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
            var el = base.Clone(areas) as CollectionElement;
            return el;
        }

        #endregion


        // ------------------------------------------
        // IIdentifiedPoco Implementation
        // ------------------------------------------

        #region IIdentifiedPoco Implementation

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public new ITBdoItemSet<IBdoElement> WithId(string id)
        {
            Id = id;

            return this;
        }

        #endregion

        // --------------------------------------------------
        // IDataItemSet Implementation
        // --------------------------------------------------

        #region IBdoElementSet Implementation

        public ITBdoItemSet<IBdoElement> ClearItems()
        {
            Items = null;
            return this;
        }

        public ITBdoItemSet<IBdoElement> Add(params IBdoElement[] items)
        {
            var items_ = Items ?? new List<IBdoElement>();

            if (items != null)
            {
                foreach (var item in items)
                {
                    if (item is IReferenced referencedDataItem)
                    {
                        var key = referencedDataItem?.Key();
                        items_?.RemoveAll(p => p.BdoKeyEquals(key));
                    }

                    items_.Add(item);
                }
            }

            return this;
        }

        public IBdoElement Insert(IBdoElement item)
        {
            Add(item);

            return item;
        }

        public ITBdoItemSet<IBdoElement> WithItems(params IBdoElement[] items)
        {
            Items = items?.ToList();
            return this;
        }

        public bool HasItem(string key = null)
        {
            var items = Items;
            if (key == null) return items.Count > 0;
            return items?.Any(p => p.BdoKeyEquals(key)) == true;
        }

        public ITBdoItemSet<IBdoElement> Remove(params string[] keys)
        {
            var items = Items;
            if (keys != null)
            {
                items?.RemoveAll(p => keys.Any(q => p.BdoKeyEquals(q)));
            }
            Items = items;

            return this;
        }

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public virtual IBdoElement Get(string key)
        {
            var items = Items;
            if (key == null) return items[0];
            return items.FirstOrDefault(q => q.BdoKeyEquals(key));
        }

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public virtual Q Get<Q>(string key) where Q : class, IBdoElement
        {
            return Get(key) as Q;
        }

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="index">The index to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public virtual Q Get<Q>(int index) where Q : class, IBdoElement
        {
            return this[index] as Q;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IBdoElement[] ToArray()
        {
            return Items?.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<IBdoElement> ToList()
        {
            return Items?.ToList();
        }

        #endregion

        // --------------------------------------------------
        // IEnumerator<T> Implementation
        // --------------------------------------------------

        #region IEnumerator<T>

        /// <summary>
        /// Indicates the enumerator of this instance.
        /// </summary>
        /// <returns>Returns the enumerator of this instance.</returns>
        public IEnumerator<IBdoElement> GetEnumerator() => Items.GetEnumerator();

        /// <summary>
        /// Indicates the enumerator of this instance.
        /// </summary>
        /// <returns>Returns the enumerator of this instance.</returns>
        IEnumerator IEnumerable.GetEnumerator() => Items.GetEnumerator();

        #endregion
    }
}
