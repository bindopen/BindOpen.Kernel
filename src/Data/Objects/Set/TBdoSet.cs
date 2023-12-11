using BindOpen.Kernel.Data;
using BindOpen.Kernel.Data.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Kernel.Data
{
    /// <summary>
    /// This class represents a data item set.
    /// </summary>
    /// <typeparam name="T">The class of the named data items.</typeparam>
    public partial class TBdoSet<T> : BdoObject, ITBdoSet<T>
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
            return BdoData.NewItemSet(items);
        }

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

        private List<T> _items;

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
        /// Returns the element with the specified name.
        /// </summary>
        IReferenced IBdoSet.this[int index] => Get(index);

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

            var key = item?.Key();
            Remove(key);

            _items.Add(item);

            return item;
        }

        /// <summary>
        /// Removes the item with the specified name.
        /// </summary>
        /// <param key="keys">The keys of the item to remove.</param>
        public int Remove(params string[] keys)
        {
            if (_items != null && keys != null)
            {
                return _items.RemoveAll(p => keys.Any(q => p.BdoKeyEquals(q)));
            }

            return 0;
        }

        public int Remove(Predicate<T> filter)
        {
            if (_items != null && filter != null)
            {
                return _items.RemoveAll(p => filter(p));
            }

            return 0;
        }

        /// <summary>
        /// Checks if this instance has an item with the specified name.
        /// </summary>
        /// <param key="name">The name of the item to check.</param>
        /// <returns>Returns true if the instance has an item with the specified name.</returns>
        public bool Has(string key = null)
        {
            if (key == null) return _items?.Any() == true;
            return _items?.Any(p => p.BdoKeyEquals(key)) == true;
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
        public virtual T Get(string key)
        {
            if (_items == null)
                return default;

            return _items.FirstOrDefault(p => p.BdoKeyEquals(key));
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
        public T Get(string key, string alternateKey = null)
        {
            if (_items == null)
                return default;

            var newKey = Has(key) ? key : alternateKey;

            return _items.FirstOrDefault(p => p.BdoKeyEquals(newKey));
        }

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param key="index">The name to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public virtual T Get(int index = 0)
        {
            if (_items != null)
            {
                return Items.ToObjectList().Get<T>(index);
            }

            return default;
        }

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param key="key">The name to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public virtual Q Get<Q>(string key)
            where Q : T
        {
            return Get(key).As<Q>();
        }

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public Q Get<Q>(string key, string alternateKey = null)
            where Q : T
        {
            return Get(key, alternateKey).As<Q>();
        }

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param key="index">The index to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public virtual Q Get<Q>(int index = 0) where Q : T
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
            return Items?.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<T> ToList()
        {
            return Items?.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual TChild Descendant<TChild>(
            params object[] tokens)
            where TChild : IReferenced
        {
            var token = tokens?.FirstOrDefault();

            object child = default;

            if (token is string key)
            {
                child = this[key];
            }
            else if (token is int index)
            {
                child = this[index];
            }

            if (tokens?.Length == 1) return child.As<TChild>();

            if (child is IBdoSet childSet)
            {
                tokens = tokens?.Skip(1).ToArray();
                return childSet.Descendant<TChild>(tokens);
            }
            else
                return default;
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
            => Items?.GetEnumerator() ?? Enumerable.Empty<T>().GetEnumerator();

        /// <summary>
        /// Indicates the enumerator of this instance.
        /// </summary>
        /// <returns>Returns the enumerator of this instance.</returns>
        IEnumerator IEnumerable.GetEnumerator()
            => Items?.GetEnumerator() ?? Enumerable.Empty<T>().GetEnumerator();

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
        public override object Clone()
        {
            var obj = base.Clone().As<TBdoSet<T>>();

            if (!string.IsNullOrEmpty(Id))
            {
                obj.Id = StringHelper.NewGuid();
            }
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
