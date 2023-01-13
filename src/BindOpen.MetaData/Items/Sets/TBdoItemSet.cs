using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.MetaData.Items
{
    /// <summary>
    /// This class represents a data item set.
    /// </summary>
    /// <typeparam name="T">The class of the named data items.</typeparam>
    public class TBdoItemSet<T> : BdoItem, ITBdoItemSet<T>
        where T : IReferenced
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        /// <summary>
        /// The items of this instance.
        /// </summary>
        private List<T> _items;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DataItemSet class.
        /// </summary>
        public TBdoItemSet() : base()
        {
        }

        #endregion

        // ------------------------------------------
        // ITDataItemSet Implementation
        // ------------------------------------------

        #region ITDataItemSet

        /// <summary>
        /// Converts from string.
        /// </summary>
        /// <param name="st">The string to consider.</param>
        public static implicit operator TBdoItemSet<T>(T[] items)
        {
            return BdoMeta.NewItemSet<T>(items);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<T> Items => _items;

        /// <summary>
        /// Returns the number of items.
        /// </summary>
        public int Count
        {
            get
            {
                return _items?.Count ?? 0;
            }
        }

        /// <summary>
        /// Returns the element with the specified indexed.
        /// </summary>
        public T this[int index] => _items.Get<T>(index);

        /// <summary>
        /// Returns the element with the specified key.
        /// </summary>
        public T this[string key]
        {
            get
            {
                if (key == null || _items == null) return default;
                return _items.FirstOrDefault(p => p.BdoKeyEquals(key));
            }
        }

        /// <summary>
        /// Clears the items of this instance.
        /// </summary>
        public ITBdoItemSet<T> ClearItems()
        {
            _items = null;

            return this;
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="items">The items of the item to add.</param>
        /// <returns>Returns the new item that has been added.
        /// Returns null if the new item is null or else its name is null.</returns>
        /// <remarks>The new item must have a name.</remarks>
        public ITBdoItemSet<T> Add(params T[] items)
        {
            if (items != null)
            {
                foreach (T item in items)
                {
                    Insert(item);
                }
            }

            return this;
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item of the item to add.</param>
        /// <returns>Returns the new item that has been added.
        /// Returns null if the new item is null or else its name is null.</returns>
        /// <remarks>The new item must have a name.</remarks>
        public virtual T Insert(T item)
        {
            if (item is IReferenced referencedDataItem)
            {
                var key = referencedDataItem?.Key();

                if (_items == null)
                {
                    _items = new List<T>();
                }

                Remove(key);
            }

            _items.Add(item);

            return item;
        }

        /// <summary>
        /// Sets the specified single item of this instance.
        /// </summary>
        /// <param name="items">The items to apply to this instance.</param>
        /// <remarks>_items of this instance must be allowed and must not be forbidden. Otherwise, the values will be the default ones..</remarks>
        public ITBdoItemSet<T> WithItems(params T[] items)
        {
            ClearItems();
            Add(items);

            return this;
        }

        /// <summary>
        /// Removes the item with the specified name.
        /// </summary>
        /// <param name="keys">The keys of the item to remove.</param>
        public virtual ITBdoItemSet<T> Remove(params string[] keys)
        {
            if (keys != null)
            {
                _items?.RemoveAll(p => keys.Any(q => p.BdoKeyEquals(q)));
            }

            return this;
        }

        /// <summary>
        /// Checks if this instance has an item with the specified name.
        /// </summary>
        /// <param name="key">The key of the item to check.</param>
        /// <returns>Returns true if the instance has an item with the specified name.</returns>
        public bool HasItem(string key = null)
        {
            if (key == null) return _items.Count > 0;
            return _items?.Any(p => p.BdoKeyEquals(key)) == true;
        }

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public virtual T Get(string key = null)
        {
            if (key == null) return this[0];
            return this[key];
        }

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public virtual Q Get<Q>(string key = null) where Q : class, T
        {
            return Get(key) as Q;
        }

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="index">The index to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public virtual Q Get<Q>(int index) where Q : class, T
        {
            return this[index] as Q;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public T[] ToArray()
        {
            return _items?.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<T> ToList()
        {
            return _items?.ToList();
        }

        #endregion

        // ------------------------------------------
        // IEnumerator Implementation
        // ------------------------------------------

        #region IEnumerator

        /// <summary>
        /// Indicates the enumerator of this instance.
        /// </summary>
        /// <returns>Returns the enumerator of this instance.</returns>
        public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();

        /// <summary>
        /// Indicates the enumerator of this instance.
        /// </summary>
        /// <returns>Returns the enumerator of this instance.</returns>
        IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();

        #endregion

        // ------------------------------------------
        // IIdentifiedPoco Implementation
        // ------------------------------------------

        #region IIdentifiedPoco

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ITBdoItemSet<T> WithId(string id)
        {
            Id = id;
            return this;
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
            var obj = base.Clone<TBdoItemSet<T>>(areas);
            obj.Id = StringHelper.NewGuid();
            obj._items = _items?.Select(p =>
            {
                if (p is IClonable dataItem)
                {
                    return (T)dataItem.Clone();
                }
                else
                {
                    return p;
                }
            }).ToList();

            return obj;
        }

        #endregion
    }
}
