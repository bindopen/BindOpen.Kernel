using BindOpen.Data.Items;
using BindOpen.Logging;
using BindOpen.Runtime.Scopes;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a catalog el that is an el whose els are entities.
    /// </summary>
    public partial class BdoMetaList :
        TBdoList<IBdoMetaData>,
        IBdoMetaList
    {
        // ------------------------------------------
        // CONVERTERS
        // ------------------------------------------

        #region Converters

        /// <summary><<                    
        /// Converts from data element array.
        /// </summary>
        /// <param name="elems">The elems to consider.</param>
        public static explicit operator BdoMetaList(IBdoMetaData[] elems)
        {
            return BdoMeta.NewList(elems);
        }

        /// <summary><<                    
        /// Converts from data element array.
        /// </summary>
        /// <param name="elems">The elems to consider.</param>
        public static explicit operator IBdoMetaData[](BdoMetaList metaSet)
        {
            return metaSet?.ToArray();
        }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CollectionElement class.
        /// </summary>
        public BdoMetaList() : base()
        {
        }

        #endregion

        // ------------------------------------------
        // IReferenced Implementation
        // ------------------------------------------

        #region IReferenced

        /// <summary>
        /// 
        /// </summary>
        public override string Key() => Name;

        #endregion

        // --------------------------------------------------
        // IBdoMetaList Implementation
        // --------------------------------------------------

        #region IBdoMetaList

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="items">The items of the item to add.</param>
        /// <returns>Returns the new item that has been added.
        /// Returns null if the new item is null or else its name is null.</returns>
        /// <remarks>The new item must have a name.</remarks>
        public new IBdoMetaList Add(
            params IBdoMetaData[] items)
        {
            base.Add(items);
            return this;
        }

        /// <summary>
        /// Sets the specified single item of this instance.
        /// </summary>
        /// <param name="items">The items to apply to this instance.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the values will be the default ones..</remarks>
        public new IBdoMetaList With(
            params IBdoMetaData[] items)
        {
            base.With(items);

            return this;
        }

        #endregion

        // --------------------------------------------------
        // IBdoMetaData Implementation
        // --------------------------------------------------

        #region IBdoMetaData

        // Items --------------------------------------------

        /// <summary>
        /// 
        /// </summary>
        public BdoMetaDataKind Kind
            => BdoMetaDataKind.Set;

        /// <summary>
        /// 
        /// </summary>
        public IBdoMetaData Parent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Index { get; set; }

        /// <summary>
        /// The value type of this instance.
        /// </summary>
        public DataValueTypes ValueType { get; set; } = DataValueTypes.Any;

        /// <summary>
        /// The itemization mode of this instance.
        /// </summary>
        public DataItemizationMode ItemizationMode { get; set; }

        /// <summary>
        /// Item reference of this instance.
        /// </summary>
        public IBdoReference Reference { get; set; }

        /// <summary>
        /// The script of this instance.
        /// </summary>
        public IBdoExpression Expression { get; set; }

        // Specification -------------------------------

        /// <summary>
        /// Specification of this instance.
        /// </summary>
        public List<IBdoSpec> Specs { get; set; }

        // Specification ---------------------

        /// <summary>
        /// Gets a new specification.
        /// </summary>
        /// <returns>Returns the new specifcation.</returns>
        public IBdoSpec NewSpec()
        {
            return null;
        }

        // Specification

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IBdoSpec GetSpec(string name = null)
        {
            return Specs?.FirstOrDefault(
                q => (name == null && q.Name == null) || q.Name.BdoKeyEquals(name));
        }

        /// <summary>
        /// 
        /// </summary>
        public IBdoMetaData WithSpecs(params IBdoSpec[] specs)
        {
            Specs = specs?.ToList();

            return this;
        }

        // Data

        /// <summary>
        /// Clears the item of this instance.
        /// </summary>
        public override void Clear()
        {
            base.Clear();
        }

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param name="log">The log to populate.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public object GetData(
            IBdoScope scope = null,
            IBdoMetaList varSet = null,
            IBdoLog log = null)
        {
            var list = GetDataList(scope, varSet, log);
            return list?.FirstOrDefault();
        }

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param name="log">The log to populate.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public Q GetData<Q>(
            IBdoScope scope = null,
            IBdoMetaList varSet = null,
            IBdoLog log = null)
        {
            var list = GetDataList<Q>(scope, varSet, log);
            if (list == null)
            {
                return default;
            }

            return list.FirstOrDefault();
        }

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param name="log">The log to populate.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public object GetData(
            int index,
            IBdoScope scope = null,
            IBdoMetaList varSet = null,
            IBdoLog log = null)
        {
            var obj = GetData<object>(index, scope, varSet, log); ;
            return obj;
        }

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param name="log">The log to populate.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public Q GetData<Q>(
            int index,
            IBdoScope scope = null,
            IBdoMetaList varSet = null,
            IBdoLog log = null)
        {
            var list = GetDataList(scope, varSet, log);
            var obj = list?.FirstOrDefault(q =>
                q is Q
                && q is IIndexed indexed
                && indexed.Index == index);
            if (obj == null)
            {
                obj = list?.GetAt(index);
                if (obj is not Q)
                    obj = default;
            }
            return (Q)obj;
        }

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param name="log">The log to populate.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public List<object> GetDataList(
            IBdoScope scope = null,
            IBdoMetaList varSet = null,
            IBdoLog log = null)
        {
            return Items
                .Select(q => q.GetData(scope, varSet, log))
                .ToList();
        }

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param name="log">The log to populate.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public List<Q> GetDataList<Q>(
            IBdoScope scope = null,
            IBdoMetaList varSet = null,
            IBdoLog log = null)
        {
            var list = GetDataList(scope, varSet, log);
            return list?.Select(q =>
            {
                if (q is Q q_Q)
                    return q_Q;

                return default;
            }).ToList();
        }

        // Accessors --------------------------

        /// <summary>
        /// Returns a text node representing this instance.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Empty;
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
            var el = base.Clone(areas) as BdoMetaList;
            return el;
        }

        #endregion
    }
}
