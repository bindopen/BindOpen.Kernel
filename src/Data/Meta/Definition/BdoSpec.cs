using BindOpen.Kernel.Data.Conditions;
using BindOpen.Kernel.Data.Helpers;
using BindOpen.Kernel.Logging;
using BindOpen.Kernel.Scoping;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Kernel.Data.Meta
{
    /// <summary>
    /// This class represents a data element specification.
    /// </summary>
    public partial class BdoSpec : TBdoSet<IBdoSpecRule>, IBdoSpec
    {
        // --------------------------------------------------
        // CONSTANTS
        // --------------------------------------------------

        #region Constants

        /// <summary>
        /// Names of the attribute areas of the TBdoSpec class.
        /// </summary>
        public static readonly string[] __AreaNames = new[]
        {
            nameof(DataAreaKind.Design),
            nameof(DataAreaKind.SpecRules),
            nameof(DataAreaKind.Properties),
            nameof(BdoMetaDataAreaKind.Element),
            nameof(DataAreaKind.Items)
        };

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the BdoSpec class.
        /// </summary>
        public BdoSpec() : base()
        {
        }

        #endregion

        // ------------------------------------------
        // ITParent Implementation
        // ------------------------------------------

        #region ITParent

        protected ITBdoSet<IBdoSpec> _children = null;

        public ITBdoSet<IBdoSpec> _Children { get => _children; set { _children = value; } }

        public IBdoSpec InsertChild(Action<IBdoSpec> updater)
        {
            var child = BdoData.NewSpec();
            updater?.Invoke(child);

            child.WithParent(this);

            return child;
        }

        public void RemoveChildren(Predicate<IBdoSpec> filter = null, bool isRecursive = false)
        {
            _children?.Remove(filter);

            if (isRecursive && _children?.Any() == true)
            {
                foreach (var child in _children)
                {
                    child.RemoveChildren(filter, true);
                }
            }
        }

        #endregion

        // --------------------------------------------------
        // IBdoSpec Implementation
        // --------------------------------------------------

        #region IBdoSpec

        /// <summary>
        /// The identifier of the group of this instance.
        /// </summary>
        public string GroupId { get; set; }

        /// <summary>
        /// The label of this instance.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// The level of inheritance of this instance.
        /// </summary>
        public InheritanceLevels InheritanceLevel { get; set; } = InheritanceLevels.None;

        /// <summary>
        /// Level of accessibility of this instance.
        /// </summary>
        public AccessibilityLevels AccessibilityLevel { get; set; } = AccessibilityLevels.Public;

        #endregion

        // ------------------------------------------
        // ITParent Implementation
        // ------------------------------------------

        #region ITParent

        public IBdoSpec Parent { get; set; }

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
        public override string Key() => Name;

        #endregion

        // ------------------------------------------
        // INamed Implementation
        // ------------------------------------------

        #region INamed

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        #endregion

        // ------------------------------------------
        // IGloballyTitled Implementation
        // ------------------------------------------

        #region IGloballyTitled

        /// <summary>
        /// 
        /// </summary>
        public ITBdoDictionary<string> Title { get; set; }

        #endregion

        // ------------------------------------------
        // IGloballyDescribed Implementation
        // ------------------------------------------

        #region IGloballyDescribed

        /// <summary>
        /// 
        /// </summary>
        public ITBdoDictionary<string> Description { get; set; }

        #endregion

        // ------------------------------------------
        // IGloballyDescribed Implementation
        // ------------------------------------------

        #region IGloballyDescribed

        /// <summary>
        /// 
        /// </summary>
        public IBdoMetaSet Detail { get; set; }

        #endregion

        // ------------------------------------------
        // IGroup Implementation
        // ------------------------------------------

        #region IGroup

        public void RemoveOfGroup(string groupId, bool isRecursive = false)
        {
            this.RemoveAll(q => q.OfGroup(groupId));

            if (isRecursive && _children?.Any() == true)
            {
                foreach (var child in _children)
                {
                    child.RemoveOfGroup(groupId, true);
                }
            }
        }

        #endregion

        // --------------------------------------------------
        // IBdoSpec Implementation
        // --------------------------------------------------

        #region IBdoSpec

        /// <summary>
        /// The script of this instance.
        /// </summary>
        public IBdoReference Reference { get; set; }

        /// <summary>
        /// The aliases of the entry.
        /// </summary>
        public IList<string> Aliases { get; set; }

        /// <summary>
        /// The script of this instance.
        /// </summary>
        public IBdoDataType DataType { get; set; }

        /// <summary>
        /// The available itemization modes of this instance.
        /// </summary>
        public IList<DataMode> AvailableDataModes { get; set; }

        /// <summary>
        /// The default item of this instance.
        /// </summary>
        public object DefaultData { get; set; }

        /// <summary>
        /// Minimum item number of this instance.
        /// </summary>
        public uint MinDataItemNumber { get; set; } = 0;

        /// <summary>
        /// Maximum item number of this instance.
        /// </summary>
        public uint? MaxDataItemNumber { get; set; }

        /// <summary>
        /// Indicates whether the value of this instance is a list.
        /// </summary>
        public bool IsValueList => MaxDataItemNumber == null || MaxDataItemNumber > 1;

        /// <summary>
        /// Indicates whether this instance is compatible with the specified data item.
        /// </summary>
        /// <param key="item">The data item to consider.</param>
        /// <returns>True if this instance is compatible with the specified data item.</returns>
        public virtual bool IsCompatibleWithData(
            object item)
        {
            return true;
        }

        public IBdoSpecRule Get(string groupId, BdoSpecRuleKinds ruleKind, IBdoScope scope = null, IBdoMetaSet varSet = null, IBdoLog log = null)
        {
            var rules = this.Where(q => q.OfGroup(groupId) && (ruleKind == BdoSpecRuleKinds.Any || q.Kind == ruleKind));

            foreach (var rule in rules)
            {
                if (rule?.Condition != null && scope?.Interpreter?.Evaluate(rule.Condition, varSet, log) == true)
                {
                    return rule;
                }
            }

            return rules.FirstOrDefault(q => q.Condition == null);
        }

        public object GetValue(string groupId, BdoSpecRuleKinds ruleKind, IBdoScope scope = null, IBdoMetaSet varSet = null, IBdoLog log = null)
        {
            var rule = Get(groupId, ruleKind, scope, varSet, log);

            return rule?.Value;
        }

        #endregion

        // ------------------------------------------
        // IConditional Implementation
        // ------------------------------------------

        #region IConditional

        /// <summary>
        /// The condition.
        /// </summary>
        public IBdoCondition Condition { get; set; }

        /// <summary>
        /// The item requirement level of this instance.
        /// </summary>
        public bool GetConditionValue(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var b = scope?.Interpreter?.Evaluate(Condition, varSet, log) == true;

            return b;
        }

        #endregion

        // --------------------------------------------------
        // IClonable
        // --------------------------------------------------

        #region IClonable

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a cloned instance.</returns>
        public override object Clone()
        {
            var obj = base.Clone().As<BdoSpec>();

            if (!string.IsNullOrEmpty(Id))
            {
                obj.Id = StringHelper.NewGuid();
            }

            obj._children = _children == null ? null : BdoData.NewItemSet(_children?.Select(q => q.Clone<IBdoSpec>()).ToArray());

            obj.WithAvailableDataModes(AvailableDataModes?.ToArray());
            obj.WithAliases(Aliases?.ToArray());
            obj.Condition = Condition?.Clone<BdoCondition>();
            obj.Reference = Reference?.Clone<BdoReference>();
            obj.DataType = DataType?.Clone<BdoDataType>();
            obj.DefaultData = DefaultData;
            obj.Description = Condition?.Clone<TBdoDictionary<string>>();
            obj.Detail = Condition?.Clone<BdoMetaSet>();
            obj.Title = Condition?.Clone<TBdoDictionary<string>>();

            return obj;
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

            Condition?.Dispose();
            this.

            _isDisposed = true;

            base.Dispose(isDisposing);
        }

        #endregion
    }
}
