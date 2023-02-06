using BindOpen.Data.Assemblies;
using BindOpen.Data.Items;
using BindOpen.Logging;
using BindOpen.Runtime.Scopes;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a catalog el that is an el whose els are carriers.
    /// </summary>
    public partial class BdoMetaObject :
        TBdoMetaData<IBdoMetaObject, IBdoMetaObjectSpec, object>,
        IBdoMetaObject
    {
        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CollectionElement class.
        /// </summary>
        public BdoMetaObject() : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the CollectionElement class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="id">The ID to consider.</param>
        public BdoMetaObject(string name = null, string id = null)
            : base(name, "object_", id)
        {
            this.WithDataValueType(DataValueTypes.Object);
        }

        #endregion

        // --------------------------------------------------
        // IBdoDataObject Implementation
        // --------------------------------------------------

        #region IBdoDataObject

        /// <summary>
        /// The items of this instance.
        /// </summary>
        public IBdoMetaList PropertySet { get; set; }

        /// <summary>
        /// The class full name of this instance.
        /// </summary>
        public IBdoClassReference ClassReference { get; set; }

        public IBdoMetaObject WithClassReference(IBdoClassReference reference)
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
        /// Returns the item object of this instance.
        /// </summary>
        /// <param name="log">The log to populate.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public override Q GetData<Q>(
            IBdoScope scope = null,
            IBdoMetaList varSet = null,
            IBdoLog log = null)
        {
            return _data.As<Q>();
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
        IBdoMetaSpec IBdoMetaData.NewSpec()
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
        /// <param name="item">The string item of this instance.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the items will be the default ones..</remarks>
        /// <returns>Returns True if the specified has been well added.</returns>
        public IBdoMetaObject WithData(object obj)
        {
            _data = obj.ToBdoData(GetSpec());

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
            => PropertySet?.Items;

        /// <summary>
        /// Returns the el with the specified indexed.
        /// </summary>
        public IBdoMetaData this[int index]
            => PropertySet?.Get(index);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IBdoMetaData this[string key]
            => PropertySet?.Get(key);

        /// <summary>
        /// Returns the number of items.
        /// </summary>
        public int Count
            => PropertySet?.Count ?? 0;

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="items">The items of the item to add.</param>
        /// <returns>Returns the new item that has been added.
        /// Returns null if the new item is null or else its name is null.</returns>
        /// <remarks>The new item must have a name.</remarks>
        public IBdoMetaObject Add(
            params IBdoMetaList[] items)
        {
            (this as ITBdoList<IBdoMetaData>).Add(items);
            return this;
        }

        IBdoMetaList IBdoMetaList.Add(
            params IBdoMetaData[] items)
        {
            (this as ITBdoList<IBdoMetaData>).Add(items);
            return this;
        }

        ITBdoList<IBdoMetaData> ITBdoList<IBdoMetaData>.Add(
            params IBdoMetaData[] items)
        {
            PropertySet ??= BdoMeta.NewList();
            PropertySet.Add(items);
            return this;
        }

        public IBdoMetaObject With(
            params IBdoMetaData[] items)
        {
            (this as ITBdoList<IBdoMetaData>).With(items);
            return this;
        }

        IBdoMetaList IBdoMetaList.With(
            params IBdoMetaData[] items)
        {
            (this as ITBdoList<IBdoMetaData>).With(items);
            return this;
        }

        ITBdoList<IBdoMetaData> ITBdoList<IBdoMetaData>.With(
            params IBdoMetaData[] items)
        {
            PropertySet ??= BdoMeta.NewList();
            return PropertySet.With(items);
        }

        public IBdoMetaData Insert(IBdoMetaData item)
        {
            PropertySet ??= BdoMeta.NewList();
            return PropertySet.Insert(item);
        }

        public override void Clear()
        {
            base.Clear();
            PropertySet.Clear();
        }

        public bool Has(string key = null)
            => PropertySet?.Has(key) ?? false;

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public virtual IBdoMetaData Get(string key = null)
            => PropertySet?.Get(key);

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public IBdoMetaData Get(int index)
            => PropertySet?.Get(index);

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public virtual Q Get<Q>(string key = null)
            where Q : IBdoMetaData
        {
            if (PropertySet != null)
            {
                return PropertySet.Get<Q>(key);
            }

            return default;
        }

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="index">The index to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public virtual Q Get<Q>(int index)
            where Q : IBdoMetaData
        {
            if (PropertySet != null)
            {
                return PropertySet.Get<Q>(index);
            }

            return default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IBdoMetaData[] ToArray()
            => PropertySet?.ToArray();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<IBdoMetaData> ToList()
            => PropertySet?.ToList();

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
            => PropertySet?.GetEnumerator();

        /// <summary>
        /// Indicates the enumerator of this instance.
        /// </summary>
        /// <returns>Returns the enumerator of this instance.</returns>
        IEnumerator IEnumerable.GetEnumerator()
            => PropertySet?.GetEnumerator();

        #endregion
    }
}
