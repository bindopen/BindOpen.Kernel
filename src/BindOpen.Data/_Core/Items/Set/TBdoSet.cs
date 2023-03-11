using BindOpen.Data.Helpers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a data item set.
    /// </summary>
    /// <typeparam name="T">The class of the named data items.</typeparam>
    public class TBdoSet<T> : BdoItem, ITBdoSet<T>
        where T : IReferenced
    {
        // ------------------------------------------
        // ITDataList Implementation
        // ------------------------------------------

        #region ITDataList

        /// <summary>
        /// Converts from string.
        /// </summary>
        /// <param key="st">The string to consider.</param>
        public static implicit operator TBdoSet<T>(T[] items)
        {
            return BdoData.NewSet(items);
        }

        #endregion

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
        /// Instantiates a new instance of the DataList class.
        /// </summary>
        public TBdoSet() : base()
        {
        }

        #endregion

        // ------------------------------------------
        // ITDataList Implementation
        // ------------------------------------------

        #region ITDataList

        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
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
        public T this[int index] => Get(index);

        /// <summary>
        /// Returns the item of the specified name.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="defaultKey">The default name to consider.</param>
        /// <returns>Returns the specified text.</returns>
        public T this[string name, string alternateKey] => Get(name, alternateKey);

        /// <summary>
        /// Returns the element with the specified name.
        /// </summary>
        public T this[string name] => Get(name);

        /// <summary>
        /// Returns the element with the specified name.
        /// </summary>
        IReferenced IBdoSet.this[string name] => Get(name);

        /// <summary>
        /// Clears the items of this instance.
        /// </summary>
        public virtual void Clear()
        {
            _items = null;
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param key="item">The item of the item to add.</param>
        /// <returns>Returns the new item that has been added.
        /// Returns null if the new item is null or else its name is null.</returns>
        /// <remarks>The new item must have a name.</remarks>
        public virtual T Insert(T item)
        {
            _items ??= new List<T>();

            if (item is IReferenced referencedDataItem)
            {
                var key = referencedDataItem?.Key();
                this.Remove(key);
            }

            _items.Add(item);

            return item;
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param key="items">The items of the item to add.</param>
        /// <returns>Returns the new item that has been added.
        /// Returns null if the new item is null or else its name is null.</returns>
        /// <remarks>The new item must have a name.</remarks>
        public ITBdoSet<T> Add(params T[] items)
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

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param key="items">The items of the item to add.</param>
        /// <returns>Returns the new item that has been added.
        /// Returns null if the new item is null or else its name is null.</returns>
        /// <remarks>The new item must have a name.</remarks>
        public ITBdoSet<T> AddRange(
            ITBdoSet<T> list)
        {
            var items = list?.Items?.ToArray();
            return Add(items);
        }

        /// <summary>
        /// Sets the specified single item of this instance.
        /// </summary>
        /// <param key="items">The items to apply to this instance.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the values will be the default ones..</remarks>
        public ITBdoSet<T> With(params T[] items)
        {
            Clear();
            Add(items);

            return this;
        }

        /// <summary>
        /// Checks if this instance has an item with the specified name.
        /// </summary>
        /// <param key="name">The name of the item to check.</param>
        /// <returns>Returns true if the instance has an item with the specified name.</returns>
        public bool Has(string name = null)
        {
            if (name == null) return _items?.Count > 0;
            return _items?.Any(p => p.BdoKeyEquals(name)) == true;
        }

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public virtual T Get(string name = null, string alternateKey = null)
        {
            if (_items == null)
                return default;

            if (name == null && alternateKey == null)
                return _items.FirstOrDefault();

            var newKey = alternateKey == null || Has(name) ? name : alternateKey;

            return _items.FirstOrDefault(p => p.BdoKeyEquals(newKey));
        }

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param key="index">The name to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public virtual T Get(int index)
        {
            if (_items != null)
            {
                return _items.ToObjectList().Get<T>(index);
            }

            return default;
        }

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public virtual Q Get<Q>(string name = null, string alternateKey = null)
            where Q : T
        {
            var newKey = alternateKey == null || Has(name) ? name : alternateKey;
            return Get(newKey).As<Q>();
        }

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param key="index">The index to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public virtual Q Get<Q>(int index) where Q : T
        {
            var obj = this[index];
            if (obj is Q obj_Q)
                return obj_Q;

            return default;
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
        public IEnumerator<T> GetEnumerator()
            => _items?.GetEnumerator() ?? Enumerable.Empty<T>().GetEnumerator();

        /// <summary>
        /// Indicates the enumerator of this instance.
        /// </summary>
        /// <returns>Returns the enumerator of this instance.</returns>
        IEnumerator IEnumerable.GetEnumerator()
            => _items?.GetEnumerator() ?? Enumerable.Empty<T>().GetEnumerator();

        #endregion

        // ------------------------------------------
        // IIdentified Implementation
        // ------------------------------------------

        #region IIdentified

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        #endregion

        // ------------------------------------------
        // IReferenced Implementation
        // ------------------------------------------

        #region IReferenced

        /// <summary>
        /// 
        /// </summary>
        public virtual string Key() => Id;

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
            var obj = Clone<TBdoSet<T>>(areas);
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
