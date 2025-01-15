using BindOpen.Data.Assemblies;
using BindOpen.Data.Conditions;
using BindOpen.Data.Helpers;
using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Scoping;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data.Schema;

/// <summary>
/// This class represents a data element schema.
/// </summary>
public partial class BdoSchema : BdoObject, IBdoSchema
{
    // --------------------------------------------------
    // CONSTANTS
    // --------------------------------------------------

    #region Constants

    /// <summary>
    /// Names of the attribute areas of the TBdoSchema class.
    /// </summary>
    public static readonly string[] __AreaNames = new[]
    {
        nameof(DataAreaKind.Design),
        nameof(DataAreaKind.SchemaRules),
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
    /// Initializes a new instance of the BdoSchema class.
    /// </summary>
    public BdoSchema() : base()
    {
    }

    #endregion

    // ------------------------------------------
    // ITParent Implementation
    // ------------------------------------------

    #region ITParent

    protected ITBdoSet<IBdoSchema> _children = null;

    public ITBdoSet<IBdoSchema> _Children { get => _children; set { _children = value; } }

    public Q InsertChild<Q>(Action<Q> updater) where Q : IBdoSchema, new()
    {
        var child = BdoData.NewSchema<Q>();
        updater?.Invoke(child);

        child.WithParent((IBdoSchema)this);

        return child;
    }

    public void RemoveChildren(Predicate<IBdoSchema> filter = null, bool isRecursive = false)
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
    // IBdoSchema Implementation
    // --------------------------------------------------

    #region IBdoSchema

    /// <summary>
    /// 
    /// </summary>
    public ITBdoGroupsOf<IBdoSchemaRule> RuleSet { get; set; }

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

    public IBdoSchema Parent { get; set; }

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
    // IIdentified Implementation
    // ------------------------------------------

    #region IIdentified

    /// <summary>
    /// 
    /// </summary>
    public string Identifier { get; set; }

    #endregion

    // ------------------------------------------
    // IReferenced Implementation
    // ------------------------------------------

    #region IReferenced

    /// <summary>
    /// 
    /// </summary>
    public string Key() => Name;

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

    // --------------------------------------------------
    // IBdoSchema Implementation
    // --------------------------------------------------

    #region IBdoSchema

    public IBdoSchemaSet ItemSet { get; set; }

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

    public IBdoSchemaRule GetRule(string groupId, BdoSchemaRuleKinds ruleKind, IBdoScope scope = null, IBdoMetaSet varSet = null, IBdoLog log = null)
    {
        var rules = RuleSet?.Where(q => q.OfGroup(groupId) && (ruleKind == BdoSchemaRuleKinds.Any || q.Kind == ruleKind));

        if (rules == null) return null;

        foreach (var rule in rules)
        {
            if (rule?.Condition != null && scope?.Interpreter?.Evaluate(rule.Condition, varSet, log) == true)
            {
                return rule;
            }
        }

        return rules.FirstOrDefault(q => q.Condition == null);
    }

    public object GetRuleValue(string groupId, BdoSchemaRuleKinds ruleKind, IBdoScope scope = null, IBdoMetaSet varSet = null, IBdoLog log = null)
    {
        var rule = GetRule(groupId, ruleKind, scope, varSet, log);

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
        var obj = base.Clone().As<BdoSchema>();

        if (!string.IsNullOrEmpty(Identifier))
        {
            obj.Identifier = StringHelper.NewGuid();
        }

        obj._children = _children == null ? null : BdoData.NewItemSet(_children?.Select(q => q.Clone<IBdoSchema>()).ToArray());

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
