using BindOpen.MetaData.Items;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.MetaData.Elements
{
    /// <summary>
    /// This class represents a catalog el that is an el whose els are entities.
    /// </summary>
    public class BdoMetaCollection :
        TBdoMetaElement<IBdoMetaCollection, IBdoMetaCollectionSpec, IBdoMetaElement>,
        IBdoMetaCollection
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        // Specification -----------------------

        List<IBdoMetaElement> ITBdoItemSet<IBdoMetaElement>.Items
        {
            get => Items();
        }

        // IDataItemSet -----------------------

        /// <summary>
        /// Returns the el with the specified indexed.
        /// </summary>
        public IBdoMetaElement this[int index]
            => Items().GetAt(index) as IBdoMetaElement;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IBdoMetaElement this[string key] => Get(key);

        /// <summary>
        /// Returns the number of items.
        /// </summary>
        public int Count
        {
            get => Items()?.Count ?? 0;
        }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CollectionElement class.
        /// </summary>
        public BdoMetaCollection() : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the CollectionElement class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="id">The ID to consider.</param>
        public BdoMetaCollection(string name = null, string id = null)
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
        IBdoMetaElementSpec IBdoMetaElement.NewSpecification()
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
            var el = base.Clone(areas) as BdoMetaCollection;
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
        public new ITBdoItemSet<IBdoMetaElement> WithId(string id)
        {
            Id = id;

            return this;
        }

        #endregion

        // --------------------------------------------------
        // IDataItemSet Implementation
        // --------------------------------------------------

        #region IBdoElementSet Implementation

        public ITBdoItemSet<IBdoMetaElement> ClearItems()
        {
            WithItems(null);
            return this;
        }

        public ITBdoItemSet<IBdoMetaElement> Add(params IBdoMetaElement[] items)
        {
            if (items != null)
            {
                foreach (var item in items)
                {
                    Insert(item);
                }
            }

            return this;
        }

        public IBdoMetaElement Insert(IBdoMetaElement item)
        {
            if (item != null)
            {
                if (item is IReferenced referencedDataItem)
                {
                    var key = referencedDataItem?.Key();
                    Remove(key);
                }

                var items = Items() ?? new List<IBdoMetaElement>();
                items.Add(item);
                _item = items;
            }

            return item;
        }

        public ITBdoItemSet<IBdoMetaElement> WithItems(params IBdoMetaElement[] items)
        {
            base.WithItems(items?.ToList());
            return this;
        }

        public bool HasItem(string key = null)
        {
            var items = Items();
            if (key == null) return items.Count > 0;
            return items?.Any(p => p.BdoKeyEquals(key)) == true;
        }

        public ITBdoItemSet<IBdoMetaElement> Remove(params string[] keys)
        {
            var items = Items();
            if (keys != null)
            {
                items?.RemoveAll(p => keys.Any(q => p.BdoKeyEquals(q)));
            }
            _item = items;

            return this;
        }

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public virtual IBdoMetaElement Get(string key)
        {
            var items = Items();
            if (key == null) return items[0];
            return items.FirstOrDefault(q => q.BdoKeyEquals(key));
        }

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public virtual Q Get<Q>(string key) where Q : class, IBdoMetaElement
        {
            return Get(key) as Q;
        }

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="index">The index to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public virtual Q Get<Q>(int index) where Q : class, IBdoMetaElement
        {
            return this[index] as Q;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IBdoMetaElement[] ToArray()
        {
            return Items()?.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<IBdoMetaElement> ToList()
        {
            return Items()?.ToList();
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
        public IEnumerator<IBdoMetaElement> GetEnumerator() => Items().GetEnumerator();

        /// <summary>
        /// Indicates the enumerator of this instance.
        /// </summary>
        /// <returns>Returns the enumerator of this instance.</returns>
        IEnumerator IEnumerable.GetEnumerator() => Items().GetEnumerator();

        #endregion
    }
}
