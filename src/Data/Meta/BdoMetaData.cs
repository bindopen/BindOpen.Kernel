using BindOpen.Kernel.Data.Conditions;
using BindOpen.Kernel.Data.Helpers;
using BindOpen.Kernel.Logging;
using BindOpen.Kernel.Scoping;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Kernel.Data.Meta
{
    /// <summary>
    /// This class represents a data element.
    /// </summary>
    public abstract partial class BdoMetaData : BdoObject,
        IBdoMetaData
    {
        // --------------------------------------------------
        // VARIABLES
        // --------------------------------------------------

        #region Variables

        /// <summary>
        /// The item of this instance.
        /// </summary>
        protected object _data;

        protected readonly string _namePreffix;

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new data element.
        /// </summary>
        protected BdoMetaData() : this(null)
        {
        }

        /// <summary>
        /// Initializes a new data element.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="namePreffix">The name preffix to consider.</param>
        /// <param key="id">The ID to consider.</param>
        protected BdoMetaData(
            string name = null,
            string namePreffix = null,
            string id = null) : base()
        {
            _namePreffix = namePreffix ?? "element_";
            this.WithName(name);
            Id = id;
        }

        #endregion

        // --------------------------------------------------
        // CONVERTERS
        // --------------------------------------------------

        #region Converters

        // String

        /// <summary>
        /// Converts from string.
        /// </summary>
        /// <param key="st">The string to consider.</param>
        public static explicit operator BdoMetaData(string st)
            => BdoData.NewScalar(DataValueTypes.Any, st);

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <param key="meta">The meta to consider.</param>
        public static explicit operator string(BdoMetaData meta)
        {
            return meta?.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="items"></param>
        public static implicit operator BdoMetaData((string Name, object Value) item)
        {
            var meta = BdoData.NewMeta(item.Name, item.Value);

            return meta as BdoMetaData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="items"></param>
        public static implicit operator BdoMetaData((string Name, DataValueTypes ValueType, object Value) item)
        {
            var meta = BdoData.NewMeta(item.Name, item.ValueType, item.Value);

            return meta as BdoMetaData;
        }

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

        // --------------------------------------------------
        // IBdoMetaData Implementation
        // --------------------------------------------------

        #region IBdoMetaData

        // Items --------------------------------------------

        /// <summary>
        /// 
        /// </summary>
        public BdoMetaDataKind MetaDataKind
        {
            get
            {
                if (this is IBdoMetaObject)
                    return BdoMetaDataKind.Object;
                else if (this is IBdoMetaScalar)
                    return BdoMetaDataKind.Scalar;
                else if (this is IBdoMetaNode)
                    return BdoMetaDataKind.Set;
                return BdoMetaDataKind.None;
            }
        }
        public DataMode DataMode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [BdoProperty("parent")]
        public IBdoMetaData Parent { get; set; }

        /// <summary>
        /// The script of this instance.
        /// </summary>
        public IBdoDataType DataType { get; set; }

        /// <summary>
        /// The script of this instance.
        /// </summary>
        public IBdoReference Reference { get; set; }

        // Specification -------------------------------

        /// <summary>
        /// Specification of this instance.
        /// </summary>
        public IBdoSpec Spec { get; set; }

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
            object obj = default;

            if (Reference != null)
            {
                if (scope == null)
                {
                    log?.AddEvent(EventKinds.Error, "Application scope missing");
                }
                else
                {
                    if (Reference == null)
                    {
                        log?.AddEvent(EventKinds.Warning, "Script missing");
                    }

                    obj = scope.Interpreter.Evaluate<object>(Reference, varSet, log);
                }
            }
            else
            {
                obj = _data;
            }

            return obj;
        }

        // Data

        /// <summary>
        /// Clears the item of this instance.
        /// </summary>
        public virtual void Clear()
        {
            _data = null;
        }

        public virtual void SetData(object obj)
        {
            if (obj is IBdoReference reference)
            {
                Reference = reference;
            }
            else
            {
                _data = obj.ToBdoData();
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
            => DataObject(scope, varSet, log);

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param key="log">The log to populate.</param>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="varSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public virtual T GetData<T>(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
            => DataObject(scope, varSet, log).As<T>();

        /// <summary>
        /// Returns the item TItem of this instance.
        /// </summary>
        /// <param key="log">The log to populate.</param>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="metaSet">The variable meta set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public virtual IList<object> GetDataList(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var obj = DataObject(scope, varSet, log);

            var list = obj?.ToObjectList();
            return list;
        }

        /// <summary>
        /// Returns the item TItem of this instance.
        /// </summary>
        /// <param key="log">The log to populate.</param>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="metaSet">The variable meta set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public virtual IList<Q> GetDataList<Q>(
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

        /// <summary>
        /// The item requirement level of this instance.
        /// </summary>
        public IBdoSpecRule GetSpecRule(
            string groupId,
            BdoSpecRuleKinds ruleKind = BdoSpecRuleKinds.Requirement,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            if (Spec != null)
            {
                var localVarSet = BdoData.NewSet(varSet?.ToArray());
                localVarSet.Add(BdoData.__VarName_This, this);

                var rule = Spec?.Get(groupId, ruleKind, scope, localVarSet, log);

                return rule;
            }

            return null;
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
            var obj = base.Clone().As<BdoMetaData>();

            if (!string.IsNullOrEmpty(Id))
            {
                obj.Id = StringHelper.NewGuid();
            }

            obj.Reference = Reference?.Clone<BdoReference>();
            obj.DataType = DataType?.Clone<BdoDataType>();
            obj.Spec = Spec?.Clone<BdoSpec>();

            return obj;
        }

        #endregion

        // ------------------------------------------
        // IIndexed Implementation
        // ------------------------------------------

        #region IIndexed

        /// <summary>
        /// The index of this instance.
        /// </summary>
        public int? Index { get; set; }

        #endregion

        // ------------------------------------------
        // IReferenced Implementation
        // ------------------------------------------

        #region IReferenced

        /// <summary>
        /// 
        /// </summary>
        public virtual string Key() => string.IsNullOrEmpty(Name) ? Reference?.MetaData?.Name : Name;

        #endregion

        // ------------------------------------------
        // INamed Implementation
        // ------------------------------------------

        #region INamed

        /// <summary>
        /// 
        /// </summary>
        [BdoProperty("name")]
        public string Name { get; set; }

        #endregion

        // ------------------------------------------
        // IConditional Implementation
        // ------------------------------------------

        #region IConditional

        /// <summary>
        /// The condition.
        /// </summary>
        public IBdoCondition Condition
        {
            get => Spec?.Condition;
            set
            {
                this.GetOrAddSpec().Condition = value;
            }
        }

        /// <summary>
        /// The item requirement level of this instance.
        /// </summary>
        public bool GetConditionValue(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            if (Condition != null)
            {
                var localVarSet = BdoData.NewSet(varSet?.ToArray());
                localVarSet.Add(BdoData.__VarName_This, this);

                var b = scope?.Interpreter?.Evaluate(Condition, localVarSet, log) == true;

                return b;
            }

            return true;
        }

        #endregion

        // ------------------------------------------
        // IDISPOSABLE METHODS
        // ------------------------------------------

        #region IDisposable_Methods

        private bool _isDisposed = false;

        /// <summary>
        /// Disposes this instance. 
        /// </summary>
        /// <param key="isDisposing">Indicates whether this instance is disposing</param>
        protected override void Dispose(bool isDisposing)
        {
            if (_isDisposed)
            {
                return;
            }

            _isDisposed = true;

            base.Dispose(isDisposing);
        }

        #endregion
    }
}
