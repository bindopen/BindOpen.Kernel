﻿using BindOpen.Data.Assemblies;
using BindOpen.Data.Conditions;
using BindOpen.Data.Helpers;
using BindOpen.Data.Schema;
using BindOpen.Logging;
using BindOpen.Scoping;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data element.
    /// </summary>
    public abstract partial class BdoMetaData : BdoObject, IBdoMetaData
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
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The scope of the service.
        /// </summary>
        public IBdoScope Scope { get; set; }

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
            Identifier = id;
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
        public string Identifier { get; set; }

        #endregion

        // --------------------------------------------------
        // IBdoMetaData Implementation
        // --------------------------------------------------

        #region IBdoMetaData

        // Items --------------------------------------------

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
        public IBdoSchema Schema { get; set; }

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param key="log">The log to populate.</param>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="varSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        protected virtual object DataObject(
            IBdoScope scope,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            object obj = default;

            if (Reference != null)
            {
                if (scope == null)
                {
                    log?.AddEvent(BdoEventLevels.Error, "Application scope missing");
                }
                else
                {
                    if (Reference == null)
                    {
                        log?.AddEvent(BdoEventLevels.Warning, "Script missing");
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
            IBdoScope scope,
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
        public object GetData(
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
            => DataObject(Scope, varSet, log);

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param key="log">The log to populate.</param>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="varSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public virtual T GetData<T>(
            IBdoScope scope,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
            => DataObject(scope, varSet, log).As<T>();

        public virtual T GetData<T>(
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
            => DataObject(Scope, varSet, log).As<T>();

        /// <summary>
        /// Returns the item TItem of this instance.
        /// </summary>
        /// <param key="log">The log to populate.</param>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="metaSet">The variable meta set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public virtual IList<object> GetDataList(
            IBdoScope scope,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var obj = DataObject(scope, varSet, log);

            var list = obj?.ToObjectList();
            return list;
        }

        public IList<object> GetDataList(
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
            => GetDataList(Scope, varSet, log);

        /// <summary>
        /// Returns the item TItem of this instance.
        /// </summary>
        /// <param key="log">The log to populate.</param>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="metaSet">The variable meta set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public virtual IList<Q> GetDataList<Q>(
            IBdoScope scope,
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

        public IList<Q> GetDataList<Q>(
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
            => GetDataList<Q>(Scope, varSet, log);

        /// <summary>
        /// The item requirement level of this instance.
        /// </summary>
        public IBdoSchemaRule GetSchemaRule(
            IBdoScope scope,
            string groupId,
            BdoSchemaRuleKinds ruleKind = BdoSchemaRuleKinds.Requirement,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            if (Schema != null)
            {
                var localVarSet = BdoData.NewSet(varSet?.ToArray());
                localVarSet.Add(BdoData.__VarName_This, this);

                var rule = Schema?.GetRule(groupId, ruleKind, scope, localVarSet, log);

                return rule;
            }

            return null;
        }

        public IBdoSchemaRule GetSchemaRule(
            string groupId,
            BdoSchemaRuleKinds ruleKind = BdoSchemaRuleKinds.Requirement,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
            => GetSchemaRule(Scope, groupId, ruleKind, varSet, log);

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

            if (!string.IsNullOrEmpty(Identifier))
            {
                obj.Identifier = StringHelper.NewGuid();
            }

            obj.Reference = Reference?.Clone<BdoReference>();
            obj.DataType = DataType?.Clone<BdoDataType>();
            obj.Schema = Schema?.Clone<BdoSchema>();

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
            get => Schema?.Condition;
            set
            {
                this.GetOrAddSpec().Condition = value;
            }
        }

        /// <summary>
        /// The item requirement level of this instance.
        /// </summary>
        public bool GetConditionValue(
            IBdoScope scope,
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

        public bool GetConditionValue(
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
            => GetConditionValue(Scope, varSet, log);

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
