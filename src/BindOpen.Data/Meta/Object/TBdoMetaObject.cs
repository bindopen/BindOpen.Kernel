using BindOpen.Data.Assemblies;
using BindOpen.Data.Helpers;
using BindOpen.Data.Items;
using BindOpen.Logging;
using BindOpen.Runtime.Scopes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a catalog el that is an el whose els are carriers.
    /// </summary>
    public partial class TBdoMetaObject<TItem> :
        TBdoMetaData<TItem, IBdoObjectSpec>,
        ITBdoMetaObject<TItem>
    {
        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CollectionElement class.
        /// </summary>
        public TBdoMetaObject() : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the CollectionElement class.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="id">The ID to consider.</param>
        public TBdoMetaObject(string name = null, string id = null)
            : base(name, "TItem_", id)
        {
            this.WithDataValueType(DataValueTypes.Object);
        }

        #endregion

        // --------------------------------------------------
        // IBdoDataObject Implementation
        // --------------------------------------------------

        #region IBdoDataObject

        /// <summary>
        /// Returns the element with the specified name.
        /// </summary>
        IReferenced IBdoSet.this[string name] => Get(name);

        /// <summary>
        /// The items of this instance.
        /// </summary>
        protected IBdoMetaSet _propertySet;

        /// <summary>
        /// The class full name of this instance.
        /// </summary>
        public IBdoClassReference ClassReference { get; set; }

        public ITBdoMetaObject<TItem> WithClassReference(IBdoClassReference reference)
        {
            ClassReference = reference;

            return this;
        }

        public Type GetClassType(
            IBdoScope scope = null,
            IBdoLog log = null)
        {
            var type = scope?.CreateType(ClassReference);
            return type;
        }

        // Data

        /// <summary>
        /// Returns the item TItem of this instance.
        /// </summary>
        /// <param key="log">The log to populate.</param>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="varSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public override T GetData<T>(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            return _data.As<T>();
        }

        #endregion

        // --------------------------------------------------
        // ITEMS
        // --------------------------------------------------

        #region Items

        // Items ----------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IBdoSpec IBdoMetaData.NewSpec()
        {
            return NewSpec();
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

        // Data

        /// <summary>
        /// Sets the item of this instance.
        /// </summary>
        /// <param key="item">The string item of this instance.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the items will be the default ones..</remarks>
        /// <returns>Returns True if the specified has been well added.</returns>
        public ITBdoMetaObject<TItem> WithData(TItem obj)
        {
            _data = obj.ToBdoData();

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
            var el = base.Clone(areas) as BdoMetaObject;
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
        public List<IBdoMetaData> Items
            => _propertySet?.Items;

        /// <summary>
        /// Returns the el with the specified indexed.
        /// </summary>
        public IBdoMetaData this[int index]
            => _propertySet?.Get(index);

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

        public IBdoMetaSet Add(
            params IBdoMetaData[] items)
        {
            (this as ITBdoSet<IBdoMetaData>).Add(items);
            return this;
        }

        ITBdoSet<IBdoMetaData> ITBdoSet<IBdoMetaData>.Add(
            params IBdoMetaData[] items)
        {
            _propertySet ??= BdoMeta.NewSet();
            _propertySet.Add(items);
            return this;
        }

        public IBdoMetaSet With(
            params IBdoMetaData[] items)
        {
            (this as ITBdoSet<IBdoMetaData>).With(items);
            return this;
        }

        ITBdoSet<IBdoMetaData> ITBdoSet<IBdoMetaData>.With(
            params IBdoMetaData[] items)
        {
            _propertySet ??= BdoMeta.NewSet();
            return _propertySet.With(items);
        }

        public IBdoMetaData Insert(IBdoMetaData item)
        {
            _propertySet ??= BdoMeta.NewSet();
            return _propertySet.Insert(item);
        }

        public override void Clear()
        {
            base.Clear();
            _propertySet.Clear();
        }

        public bool Has(string key = null)
            => _propertySet?.Has(key) ?? false;

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param key="key">The key to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public virtual IBdoMetaData Get(string key = null, string alternateKey = null)
            => _propertySet?.Get(key, alternateKey);

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param key="key">The key to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public IBdoMetaData Get(int index)
            => _propertySet?.Get(index);

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param key="key">The key to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public virtual Q Get<Q>(string key = null, string alternateKey = null)
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
        public virtual Q Get<Q>(int index)
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
        public List<IBdoMetaData> ToList()
            => _propertySet?.ToList();

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
