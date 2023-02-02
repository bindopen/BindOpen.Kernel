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
    public class BdoMetaObject :
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
        /// 
        /// </summary>
        public IBdoMetaSet ItemMetaSet { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IBdoMetaSet PropertySet { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IBdoMetaObject WithProperties(
            params IBdoMetaData[] metas)
            => this.WithProperties(BdoMeta.NewSet(metas));

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
        IBdoMetaSpec IBdoMetaData.NewSpecification()
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
            var el = base.Clone(areas) as BdoMetaObject;
            return el;
        }

        #endregion

        // --------------------------------------------------
        // IDataItemSet Implementation
        // --------------------------------------------------

        #region IDataItemSet Implementation

        /// <summary>
        /// Returns the number of items.
        /// </summary>
        public List<IBdoMetaData> Items
            => ItemMetaSet?.Items;

        /// <summary>
        /// Returns the el with the specified indexed.
        /// </summary>
        public IBdoMetaData this[int index]
            => ItemMetaSet?.Get(index);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IBdoMetaData this[string key]
            => ItemMetaSet?.Get(key);

        /// <summary>
        /// Returns the number of items.
        /// </summary>
        public int Count
            => ItemMetaSet?.Count ?? 0;

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="items">The items of the item to add.</param>
        /// <returns>Returns the new item that has been added.
        /// Returns null if the new item is null or else its name is null.</returns>
        /// <remarks>The new item must have a name.</remarks>
        public IBdoMetaObject Add(
            params IBdoMetaData[] items)
        {
            ItemMetaSet?.Add(items);
            return this;
        }

        ITBdoItemSet<IBdoMetaData> ITBdoItemSet<IBdoMetaData>.Add(
            params IBdoMetaData[] items)
            => ItemMetaSet?.Add(items);

        public IBdoMetaObject WithItems(params IBdoMetaData[] items)
        {
            ItemMetaSet?.WithItems(items);
            return this;
        }

        ITBdoItemSet<IBdoMetaData> ITBdoItemSet<IBdoMetaData>.WithItems(
            params IBdoMetaData[] items)
            => ItemMetaSet?.WithItems(items);

        public IBdoMetaData Insert(IBdoMetaData item)
            => ItemMetaSet?.Insert(item);

        public void ClearItems()
            => ItemMetaSet?.ClearItems();

        public bool HasItem(string key = null)
            => ItemMetaSet?.HasItem(key) ?? false;

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public virtual IBdoMetaData Get(string key = null)
            => ItemMetaSet?.Get(key);

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public IBdoMetaData Get(int index)
            => ItemMetaSet?.Get(index);

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public virtual Q Get<Q>(string key = null)
            where Q : IBdoMetaData
        {
            if (ItemMetaSet != null)
            {
                return ItemMetaSet.Get<Q>(key);
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
            if (ItemMetaSet != null)
            {
                return ItemMetaSet.Get<Q>(index);
            }

            return default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IBdoMetaData[] ToArray()
            => ItemMetaSet?.ToArray();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<IBdoMetaData> ToList()
            => ItemMetaSet?.ToList();

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
            => ItemMetaSet?.GetEnumerator();

        /// <summary>
        /// Indicates the enumerator of this instance.
        /// </summary>
        /// <returns>Returns the enumerator of this instance.</returns>
        IEnumerator IEnumerable.GetEnumerator()
            => ItemMetaSet?.GetEnumerator();

        #endregion
    }
}
