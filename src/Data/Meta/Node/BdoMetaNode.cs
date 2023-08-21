using BindOpen.System.Data.Helpers;
using BindOpen.System.Logging;
using BindOpen.System.Scoping;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This class represents a catalog el that is an el whose els are entities.
    /// </summary>
    public partial class BdoMetaNode : BdoMetaSet, IBdoMetaNode
    {
        // ------------------------------------------
        // CONVERTERS
        // ------------------------------------------

        #region Converters

        /// <summary>
        /// Converts from data element array.
        /// </summary>
        /// <param key="elems">The elems to consider.</param>
        public static explicit operator BdoMetaNode(IBdoMetaData[] elems)
        {
            return BdoData.NewMetaNode(elems);
        }

        /// <summary>
        /// Converts from data element array.
        /// </summary>
        /// <param key="elems">The elems to consider.</param>
        public static explicit operator IBdoMetaData[](BdoMetaNode metaSet)
        {
            return metaSet?.ToArray();
        }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the BdoMetaNode class.
        /// </summary>
        public BdoMetaNode() : base()
        {
            DataType = BdoData.NewDataType(DataValueTypes.Any);
        }

        #endregion

        // ------------------------------------------
        // IReferenced Implementation
        // ------------------------------------------

        #region IReferenced

        /// <summary>
        /// 
        /// </summary>
        public override string Key() => string.IsNullOrEmpty(Name) ? DataReference?.Key() : Name;

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
            if (DataReference != null)
            {
                if (scope == null)
                {
                    log?.AddEvent(EventKinds.Warning, "Application scope missing");
                }
                else
                {
                    if (DataReference == null)
                    {
                        log?.AddEvent(EventKinds.Warning, "Script missing");
                    }

                    var obj = scope.Interpreter.Evaluate<object>(DataReference, varSet, log);
                    return obj;
                }
            }
            else
            {
                var list = new List<object>();
                foreach (var item in Items)
                {
                    list.Add(item?.GetData(scope, varSet, log));
                }
                return list;
            }

            return default;
        }

        #endregion

        // --------------------------------------------------
        // BdoMetaNode Implementation
        // --------------------------------------------------

        #region IBdoMetaData

        public virtual void Update(
            IBdoMetaData refItem,
            string[] areas = null,
            UpdateModes[] updateModes = null,
            IBdoLog log = null)
        {
            BdoMetaDataExtensions.Update(this, refItem, areas, updateModes, log);
        }

        // Items --------------------------------------------

        /// <summary>
        /// The label of this instance.
        /// </summary>
        public DataMode DataMode { get; set; }

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
        public int? Index { get; set; }

        /// <summary>
        /// The script of this instance.
        /// </summary>
        public IBdoReference DataReference { get; set; }

        /// <summary>
        /// The identifier of the group of this instance.
        /// </summary>
        public string GroupId { get; set; }

        /// <summary>
        /// The script of this instance.
        /// </summary>
        public IBdoDataType DataType { get; set; }

        // Specification -------------------------------

        /// <summary>
        /// Specification of this instance.
        /// </summary>
        public IBdoSpec Spec { get; set; }

        // Specification ---------------------

        // Data

        public virtual void SetData(object obj)
        {
            if (obj is IBdoReference reference)
            {
                DataReference = reference;
            }
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
        public IList<object> GetDataList(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            return Items?
                .Select(q => q?.GetData(scope, varSet, log))
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
            var el = base.Clone().As<BdoMetaNode>();
            return el;
        }

        #endregion
    }
}
