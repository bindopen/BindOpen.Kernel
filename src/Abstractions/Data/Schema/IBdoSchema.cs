using BindOpen.Data.Assemblies;
using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Scoping;
using System.Collections.Generic;

namespace BindOpen.Data.Schema;

/// <summary>
/// This interface defines a data schema.
/// </summary>
public interface IBdoSchema :
    IBdoObject, ITTreeNode<IBdoSchema>,
    IReferenced, IBdoDataTyped, IBdoConditional, IGrouped,
    IIdentified, INamed, IIndexed, IBdoReferenced,
    IBdoTitled, IBdoDescribed, IBdoDetailed,
    IUpdatable
{
    /// <summary>
    /// The rule set.
    /// </summary>
    ITBdoGroupsOf<IBdoSchemaRule> RuleSet { get; set; }

    /// <summary>
    /// The schema item set.
    /// </summary>
    IBdoSchemaSet ItemSet { get; set; }

    /// <summary>
    /// The aliases.
    /// </summary>
    IList<string> Aliases { get; set; }

    /// <summary>
    /// The minimum number of data items.
    /// </summary>
    uint MinDataItemNumber { get; set; }

    /// <summary>
    /// The maximum number of data items.
    /// </summary>
    uint? MaxDataItemNumber { get; set; }

    // Data

    /// <summary>
    /// The label of this instance.
    /// </summary>
    string Label { get; set; }

    // Levels

    /// <summary>
    /// The available data modes.
    /// </summary>
    IList<DataMode> AvailableDataModes { get; set; }

    /// <summary>
    /// The available accessibility levels.
    /// </summary>
    AccessibilityLevels AccessibilityLevel { get; set; }

    /// <summary>
    /// The inheritance levels.
    /// </summary>
    InheritanceLevels InheritanceLevel { get; set; }

    /// <summary>
    /// The default data.
    /// </summary>
    object DefaultData { get; set; }

    /// <summary>
    /// Gets the specified rule.
    /// </summary>
    /// <param name="groupId">The identifier of the group to consider.</param>
    /// <param name="ruleKind">The kind of rules to consider.</param>
    /// <param name="scope">The scope to consider.</param>
    /// <param name="varSet">The variable set to consider.</param>
    /// <param name="log">The log to consider.</param>
    /// <returns>Returns the schema rule.</returns>
    IBdoSchemaRule GetRule(
        string groupId,
        BdoSchemaRuleKinds ruleKind = BdoSchemaRuleKinds.Requirement,
        IBdoScope scope = null,
        IBdoMetaSet varSet = null,
        IBdoLog log = null);

    /// <summary>
    /// Gets the specified rule value.
    /// </summary>
    /// <param name="groupId">The identifier of the group to consider.</param>
    /// <param name="ruleKind">The kind of rules to consider.</param>
    /// <param name="scope">The scope to consider.</param>
    /// <param name="varSet">The variable set to consider.</param>
    /// <param name="log">The log to consider.</param>
    /// <returns>Returns the schema rule value object.</returns>
    object GetRuleValue(
        string groupId,
        BdoSchemaRuleKinds ruleKind = BdoSchemaRuleKinds.Requirement,
        IBdoScope scope = null,
        IBdoMetaSet varSet = null,
        IBdoLog log = null);
}