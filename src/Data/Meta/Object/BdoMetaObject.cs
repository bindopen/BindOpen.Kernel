using BindOpen.System.Data.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This class represents a catalog el that is an el whose els are carriers.
    /// </summary>
    public partial class BdoMetaObject : BdoMetaData, IBdoMetaObject
    {
        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the BdoMetaObject class.
        /// </summary>
        public BdoMetaObject() : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the BdoMetaObject class.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="id">The ID to consider.</param>
        public BdoMetaObject(
            string name = null,
            string namePreffix = "object_",
            string id = null)
            : base(name, namePreffix, id)
        {
            this.WithDataType(DataValueTypes.Object);
        }

        #endregion

        // --------------------------------------------------
        // IBdoMetaObject Implementation
        // --------------------------------------------------

        #region IBdoMetaObject

        /// <summary>
        /// Returns the element with the specified name.
        /// </summary>
        IReferenced IBdoSet.this[string key] => Get(key);

        /// <summary>
        /// The items of this instance.
        /// </summary>
        protected IBdoMetaSet _propertySet;

        #endregion

        // --------------------------------------------------
        // ITEMS
        // --------------------------------------------------

        #region Items

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
        public override object Clone()
        {
            var el = base.Clone() as BdoMetaObject;
            return el;
        }

        #endregion

        // --------------------------------------------------
        // IDataList Implementation
        // --------------------------------------------------

        #region IDataList Implementation

        /// <summary>
        /// Returns the number of items.
        /// </summary>
        public IList<IBdoMetaData> Items
            => _propertySet?.Items;

        /// <summary>
        /// Returns the el with the specified indexed.
        /// </summary>
        public IBdoMetaData this[int index]
            => _propertySet?.Get(index);

        /// <summary>
        /// Returns the element with the specified name.
        /// </summary>
        IReferenced IBdoSet.this[int index] => Get(index);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="key"></param>
        /// <returns></returns>
        public IBdoMetaData this[string key]
            => _propertySet?.Get(key);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="key"></param>
        /// <returns></returns>
        public IBdoMetaData this[string key, string alternateKey]
            => _propertySet?.Get(key, alternateKey);

        /// <summary>
        /// Returns the number of items.
        /// </summary>
        public int Count
            => _propertySet?.Count ?? 0;

        public IBdoMetaData Insert(IBdoMetaData item)
        {
            _propertySet ??= BdoData.NewMetaNode();
            return _propertySet.Insert(item);
        }

        public override void Clear()
        {
            //base.Clear();
            _propertySet ??= BdoData.NewMetaNode();
            _propertySet.Clear();
        }

        public IBdoMetaNode Add(
            params IBdoMetaData[] items)
        {
            (this as ITBdoSet<IBdoMetaData>).Add(items);
            return this;
        }

        /// <summary>
        /// Removes the item with the specified name.
        /// </summary>
        /// <param key="keys">The keys of the item to remove.</param>
        public int Remove(params string[] keys)
        {
            return _propertySet?.Remove(keys) ?? 0;
        }

        public int Remove(Predicate<IBdoMetaData> filter)
        {
            return _propertySet?.Remove(filter) ?? 0;
        }

        public bool Has(string key = null)
            => _propertySet?.Has(key) ?? false;

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param key="key">The key to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public virtual IBdoMetaData Get(string key)
            => _propertySet?.Get(key);

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param key="key">The key to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public virtual IBdoMetaData Get(string key, string alternateKey = null)
            => _propertySet?.Get(key, alternateKey);

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param key="key">The key to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public IBdoMetaData Get(int index = 0)
            => _propertySet?.Get(index);

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param key="key">The key to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public virtual Q Get<Q>(string key)
            where Q : IBdoMetaData
        {
            if (_propertySet != null)
            {
                return _propertySet.Get<Q>(key);
            }

            return default;
        }

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param key="key">The key to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public virtual Q Get<Q>(string key, string alternateKey = null)
            where Q : IBdoMetaData
        {
            if (_propertySet != null)
            {
                return _propertySet.Get<Q>(key, alternateKey);
            }

            return default;
        }

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param key="index">The index to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public virtual Q Get<Q>(int index = 0)
            where Q : IBdoMetaData
        {
            if (_propertySet != null)
            {
                return _propertySet.Get<Q>(index);
            }

            return default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IBdoMetaData[] ToArray()
            => _propertySet?.ToArray();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<IBdoMetaData> ToList()
            => _propertySet?.ToList();

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
                if (key?.StartsWith(':') == true)
                {
                    object tokenInt = key.ToSubstring(1).ToObject(DataValueTypes.Integer);
                    if (tokenInt is int index)
                    {
                        child = this[index];
                    }
                }
                else
                {
                    child = this[key];
                }
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
        public IEnumerator<IBdoMetaData> GetEnumerator()
            => _propertySet?.GetEnumerator() ?? Enumerable.Empty<IBdoMetaData>().GetEnumerator();

        /// <summary>
        /// Indicates the enumerator of this instance.
        /// </summary>
        /// <returns>Returns the enumerator of this instance.</returns>
        IEnumerator IEnumerable.GetEnumerator()
            => _propertySet?.GetEnumerator() ?? Enumerable.Empty<IBdoMetaData>().GetEnumerator();

        #endregion
    }
}
