using BindOpen.Data.Assemblies;
using BindOpen.Data.Conditions;
using BindOpen.Data.Helpers;
using BindOpen.Data.Objects.Interfaces;
using BindOpen.Data.Schema;
using BindOpen.Logging;
using BindOpen.Scoping;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a catalog el that is an el whose els are entities.
    /// </summary>
    public partial class BdoMetaSet : IBdoMetaSet
    {
        // ------------------------------------------
        // IIndexed Implementation
        // ------------------------------------------

        #region IIndexed

        /// <summary>
        /// 
        /// </summary>
        public int? Index { get; set; }

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
            IBdoScope scope,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            if (Reference != null)
            {
                if (scope == null)
                {
                    log?.AddEvent(BdoEventKinds.Warning, "Application scope missing");
                }
                else
                {
                    if (Reference == null)
                    {
                        log?.AddEvent(BdoEventKinds.Warning, "Script missing");
                    }

                    var obj = scope.Interpreter.Evaluate<object>(Reference, varSet, log);
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
        // IBdoMetaSet Implementation
        // --------------------------------------------------

        #region IBdoMetaSet

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
                var localVarSet = BdoData.NewNode(varSet?.ToArray());
                localVarSet.With(BdoData.__VarName_This, this);

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

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param key="item">The item of the item to add.</param>
        /// <returns>Returns the new item that has been added.
        /// Returns null if the new item is null or else its name is null.</returns>
        /// <remarks>The new item must have a name.</remarks>
        public override IBdoMetaData Insert(IBdoMetaData item)
        {
            base.Insert(item);

            return item;
        }

        public virtual void Update(
            IBdoMetaData refItem,
            string[] areas = null,
            UpdateModes[] updateModes = null,
            IBdoLog log = null)
        {
            if (refItem is ITBdoSet<IBdoMetaData> set)
            {
                TBdoSetExtensions.Update(this, set, updateModes, areas, log);
            }
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
        public BdoMetaDataKind MetaDataKind => BdoMetaDataKind.Set;

        /// <summary>
        /// 
        /// </summary>
        public IBdoMetaData Parent { get; set; }

        /// <summary>
        /// The script of this instance.
        /// </summary>
        public IBdoReference Reference { get; set; }

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
        public IBdoSchema Schema { get; set; }

        // Specification ---------------------

        // Data

        public virtual void SetData(object obj)
        {
            if (obj is IBdoReference reference)
            {
                Reference = reference;
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
        {
            var list = GetDataList(scope, varSet, log);
            return list;
        }

        public object GetData(
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
            => GetData(Scope, varSet, log);

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param key="log">The log to populate.</param>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="varSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public Q GetData<Q>(
            IBdoScope scope,
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

        public Q GetData<Q>(
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
            => GetData<Q>(Scope, varSet, log);

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param key="log">The log to populate.</param>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="varSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public IList<object> GetDataList(
            IBdoScope scope,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            return Items?
                .Select(q => q?.GetData(scope, varSet, log))
                .ToList();
        }

        public IList<object> GetDataList(
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
            => GetDataList(Scope, varSet, log);

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param key="log">The log to populate.</param>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="varSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public IList<Q> GetDataList<Q>(
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
                var localVarSet = BdoData.NewNode(varSet?.ToArray());
                localVarSet.With(BdoData.__VarName_This, this);

                var b = scope?.Interpreter?.Evaluate(Condition, localVarSet, log) == true;

                return b;
            }

            return true;
        }

        public bool GetConditionValue(
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
            => GetConditionValue(Scope, varSet = null, log = null);

        #endregion

        // --------------------------------------------------
        // CLONING
        // --------------------------------------------------

        #region Cloning

        ///// <summary>
        ///// Clones this instance.
        ///// </summary>
        ///// <returns>Returns a cloned instance.</returns>
        //public override object Clone()
        //{
        //    var el = base.Clone().As<BdoMetaSet>();
        //    return el;
        //}

        #endregion

        public override void Update(
            object item,
            string[] areas = null,
            UpdateModes[] updateModes = null,
            IBdoLog log = null)
        {
            if (item is IBdoMetaData meta)
            {
                BdoMetaDataExtensions.Update(this, meta, areas, updateModes, log);
            }

            base.Update(item, areas, updateModes, log);
        }
    }
}
