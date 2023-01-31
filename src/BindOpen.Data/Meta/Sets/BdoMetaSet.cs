using BindOpen.Data.Items;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a catalog el that is an el whose els are entities.
    /// </summary>
    public partial class BdoMetaSet :
        TBdoMetaData<IBdoMetaSet, IBdoMetaSetSpec, IBdoMetaData>,
        IBdoMetaSet
    {
        // ------------------------------------------
        // CONVERTERS
        // ------------------------------------------

        #region Converters

        /// <summary><<                    
        /// Converts from data element array.
        /// </summary>
        /// <param name="elems">The elems to consider.</param>
        public static explicit operator BdoMetaSet(IBdoMetaData[] elems)
        {
            return BdoMeta.NewSet(elems);
        }

        /// <summary><<                    
        /// Converts from data element array.
        /// </summary>
        /// <param name="elems">The elems to consider.</param>
        public static explicit operator IBdoMetaData[](BdoMetaSet metaSet)
        {
            return metaSet?.Items()?.ToArray();
        }

        #endregion

        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        // Specification -----------------------

        List<IBdoMetaData> ITBdoItemSet<IBdoMetaData>.Items
        {
            get => Items();
        }

        // IDataItemSet -----------------------

        /// <summary>
        /// Returns the el with the specified indexed.
        /// </summary>
        public IBdoMetaData this[int index]
            => Items().Get(index);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IBdoMetaData this[string key]
        {
            get
            {
                var items = Items();
                if (key == null || items == null) return default;
                return items.FirstOrDefault(p => p.BdoKeyEquals(key));
            }
        }

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
        public BdoMetaSet() : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the CollectionElement class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="id">The ID to consider.</param>
        public BdoMetaSet(string name = null, string id = null)
            : base(name, "set_", id)
        {
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
        IBdoMetaDataSpec IBdoMetaData.NewSpecification()
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
            var el = base.Clone(areas) as BdoMetaSet;
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
        public new ITBdoItemSet<IBdoMetaData> WithId(string id)
        {
            Id = id;

            return this;
        }

        #endregion

        // --------------------------------------------------
        // IDataItemSet Implementation
        // --------------------------------------------------

        #region IBdoElementSet Implementation

        public ITBdoItemSet<IBdoMetaData> Add(params IBdoMetaData[] items)
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

        public IBdoMetaData Insert(IBdoMetaData item)
        {
            var items_ = Items() ?? new List<IBdoMetaData>();

            if (item is IReferenced referencedDataItem)
            {
                var key = referencedDataItem?.Key();
                Remove(key);
            }

            items_.Add(item);

            return item;
        }

        ITBdoItemSet<IBdoMetaData> ITBdoItemSet<IBdoMetaData>.ClearItems()
        {
            return base.ClearItems();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public new IBdoMetaSet WithItems(
            params IBdoMetaData[] items)
        {
            return base.WithItems(items);
        }

        ITBdoItemSet<IBdoMetaData> ITBdoItemSet<IBdoMetaData>.WithItems(
            params IBdoMetaData[] items)
        {
            return base.WithItems(items);
        }

        public bool HasItem(string key = null)
        {
            var items = Items();
            if (key == null) return items?.Count > 0;
            return items?.Any(p => p.BdoKeyEquals(key)) == true;
        }

        public ITBdoItemSet<IBdoMetaData> Remove(params string[] keys)
        {
            var items = Items();
            if (keys != null)
            {
                items?.RemoveAll(p => keys.Any(q => p.BdoKeyEquals(q)));
            }
            base.WithItems(items);

            return this;
        }

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public virtual IBdoMetaData Get(string key = null)
        {
            if (key == null) return this[0];
            return this[key];
        }

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public virtual Q Get<Q>(string key = null)
            where Q : IBdoMetaData
        {
            var obj = Get(key);
            if (obj is Q obj_Q)
                return obj_Q;

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
            var obj = this[index];
            if (obj is Q obj_Q)
                return obj_Q;

            return default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IBdoMetaData[] ToArray()
        {
            return Items()?.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<IBdoMetaData> ToList()
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
        public IEnumerator<IBdoMetaData> GetEnumerator() => Items().GetEnumerator();

        /// <summary>
        /// Indicates the enumerator of this instance.
        /// </summary>
        /// <returns>Returns the enumerator of this instance.</returns>
        IEnumerator IEnumerable.GetEnumerator() => Items().GetEnumerator();

        #endregion
    }
}
