using BindOpen.System.Data.Helpers;
using BindOpen.System.Logging;
using BindOpen.System.Scoping;
using BindOpen.System.Scoping.Script;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This class represents a catalog el that is an el whose els are entities.
    /// </summary>
    public partial class BdoMetaSet : TBdoSet<IBdoMetaData>, IBdoMetaSet
    {
        // ------------------------------------------
        // CONVERTERS
        // ------------------------------------------

        #region Converters

        /// <summary>
        /// Converts from data element array.
        /// </summary>
        /// <param key="elems">The elems to consider.</param>
        public static explicit operator BdoMetaSet(IBdoMetaData[] elems)
        {
            return BdoMeta.NewSet(elems);
        }

        /// <summary>
        /// Converts from data element array.
        /// </summary>
        /// <param key="elems">The elems to consider.</param>
        public static explicit operator IBdoMetaData[](BdoMetaSet metaSet)
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
        public BdoMetaSet() : base()
        {
            DataMode = DataMode.Value;
            DataValueType = DataValueTypes.Any;
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
        // IBdoMetaSet Implementation
        // --------------------------------------------------

        #region IBdoMetaSet

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param key="log">The log to populate.</param>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="varSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        protected virtual object DataObject(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            switch (DataMode)
            {
                case DataMode.Value:
                    var list = new List<object>();
                    foreach (var item in Items)
                    {
                        list.Add(item?.GetData(scope, varSet, log));
                    }
                    return list;
                case DataMode.Reference:
                    if (scope == null)
                    {
                        log?.AddEvent(EventKinds.Warning, "Application scope missing");
                    }
                    else
                    {
                        if (Reference == null)
                        {
                            log?.AddEvent(EventKinds.Warning, "Script missing");
                        }

                        var obj = scope.Interpreter.Evaluate<object>(Reference, varSet, log);
                        return obj;
                    }
                    break;
            }

            return default;
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param key="items">The items of the item to add.</param>
        /// <returns>Returns the new item that has been added.
        /// Returns null if the new item is null or else its name is null.</returns>
        /// <remarks>The new item must have a name.</remarks>
        public new IBdoMetaSet Add(
            params IBdoMetaData[] items)
        {
            base.Add(items);
            return this;
        }

        /// <summary>
        /// Sets the specified single item of this instance.
        /// </summary>
        /// <param key="items">The items to apply to this instance.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the values will be the default ones..</remarks>
        public new IBdoMetaSet With(
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
        /// Indicates whether this instance is repeated in a set.
        /// </summary>
        public bool IsRepeated { get; set; }

        /// <summary>
        /// The label of this instance.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public BdoMetaDataKind MetaDataKind
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
        public DataValueTypes DataValueType { get; set; } = DataValueTypes.Any;

        /// <summary>
        /// The itemization mode of this instance.
        /// </summary>
        public DataMode DataMode { get; set; }

        /// <summary>
        /// The script of this instance.
        /// </summary>
        public IBdoReference Reference { get; set; }

        /// <summary>
        /// The identifier of the group of this instance.
        /// </summary>
        public string GroupId { get; set; }

        // Specification -------------------------------

        /// <summary>
        /// Specification of this instance.
        /// </summary>
        public ITBdoSet<IBdoSpec> Specs { get; set; }

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
        /// <param key="name"></param>
        /// <returns></returns>
        public IBdoSpec GetSpec(string name = null)
        {
            return Specs?.FirstOrDefault(
                q => (name == null && q.Name == null) || q.Name.BdoKeyEquals(name));
        }

        // Data

        /// <summary>
        /// Clears the item of this instance.
        /// </summary>
        public override void Clear()
        {
            base.Clear();
        }

        public virtual void SetData(object obj)
        {
        }

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param key="log">The log to populate.</param>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="varSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public object GetData(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var list = GetDataList(scope, varSet, log);
            return list;
        }

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param key="log">The log to populate.</param>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="varSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public Q GetData<Q>(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var list = GetDataList(scope, varSet, log);
            if (list == null)
            {
                return default;
            }

            return list.As<Q>();
        }

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param key="log">The log to populate.</param>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="varSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public object GetData(
            int index,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var obj = GetData<object>(index, scope, varSet, log); ;
            return obj;
        }

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param key="log">The log to populate.</param>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="varSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public Q GetData<Q>(
            int index,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
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
        /// <param key="log">The log to populate.</param>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="varSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public IList<object> GetDataList(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            return Items?
                .Select(q => q.GetData(scope, varSet, log))
                .ToList();
        }

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param key="log">The log to populate.</param>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="varSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public IList<Q> GetDataList<Q>(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
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
        public override object Clone()
        {
            var el = base.Clone<BdoMetaSet>();
            return el;
        }

        #endregion
    }
}
