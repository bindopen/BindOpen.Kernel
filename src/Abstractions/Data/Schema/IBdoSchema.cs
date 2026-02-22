using BindOpen.Data.Assemblies;
using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Scoping;
using System.Collections.Generic;

namespace BindOpen.Data.Schema;

/// <summary>
/// Defines a BindOpen schema, which serves as a detailed specification for metadata elements.
/// It governs validation rules, cardinality, default values, and accessibility levels.
/// </summary>
public interface IBdoSchema :
    IBdoObject, ITTreeNode<IBdoSchema>,
    IReferenced, IBdoDataTyped, IBdoConditional, IGrouped,
    IIdentified, INamed, IIndexed, IBdoReferenced,
    IBdoTitled, IBdoDescribed, IBdoDetailed,
    IUpdatable
{
    // ------------------------------------------
    // STRUCTURE & RULES
    // ------------------------------------------

    /// <summary>
    /// Gets or sets the collection of schema rules organized by groups.
    /// These rules define the requirements and constraints of the schema.
    /// </summary>
    ITBdoGroupsOf<IBdoSchemaRule> RuleSet { get; set; }

    /// <summary>
    /// Gets or sets the set of child schema items, allowing for nested specifications.
    /// </summary>
    IBdoSchemaSet ItemSet { get; set; }

    /// <summary>
    /// Gets or sets the list of alternative names for this schema.
    /// </summary>
    IList<string> Aliases { get; set; }

    // ------------------------------------------
    // CARDINALITY
    // ------------------------------------------

    /// <summary>
    /// Gets or sets the minimum number of data items required by this schema.
    /// </summary>
    uint MinDataItemNumber { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of data items allowed by this schema.
    /// A null value typically indicates no upper limit.
    /// </summary>
    uint? MaxDataItemNumber { get; set; }

    // ------------------------------------------
    // METADATA & DISPLAY
    // ------------------------------------------

    /// <summary>
    /// Gets or sets the display label of this instance.
    /// </summary>
    string Label { get; set; }

    // ------------------------------------------
    // ACCESS & BEHAVIOR
    // ------------------------------------------

    /// <summary>
    /// Gets or sets the list of supported data modes (e.g., Any, Scalar, Set).
    /// </summary>
    IList<DataMode> AvailableDataModes { get; set; }

    /// <summary>
    /// Gets or sets the accessibility level, defining visibility and modification rights.
    /// </summary>
    AccessibilityLevels AccessibilityLevel { get; set; }

    /// <summary>
    /// Gets or sets the inheritance level, defining how this schema merges with parent or child schemas.
    /// </summary>
    InheritanceLevels InheritanceLevel { get; set; }

    /// <summary>
    /// Gets or sets the default data value to be used if no value is explicitly provided.
    /// </summary>
    object DefaultData { get; set; }

    // ------------------------------------------
    // RULE RESOLUTION
    // ------------------------------------------

    /// <summary>
    /// Resolves a specific schema rule based on a group ID and rule kind.
    /// </summary>
    /// <param name="groupId">The identifier of the group (e.g., "Requirement").</param>
    /// <param name="ruleKind">The kind of rule to retrieve.</param>
    /// <param name="scope">The resolution scope for dynamic evaluation.</param>
    /// <param name="varSet">The set of variables available for resolution.</param>
    /// <param name="log">The log for tracking the resolution process.</param>
    /// <returns>Returns the matching schema rule; otherwise, null.</returns>
    IBdoSchemaRule GetRule(
        string groupId,
        BdoSchemaRuleKinds ruleKind = BdoSchemaRuleKinds.Requirement,
        IBdoScope scope = null,
        IBdoMetaSet varSet = null,
        IBdoLog log = null);

    /// <summary>
    /// Resolves and returns the actual value of a schema rule.
    /// </summary>
    /// <param name="groupId">The identifier of the group.</param>
    /// <param name="ruleKind">The kind of rule.</param>
    /// <param name="scope">The resolution scope.</param>
    /// <param name="varSet">The variable set.</param>
    /// <param name="log">The log for tracking.</param>
    /// <returns>Returns the evaluated rule value.</returns>
    object GetRuleValue(
        string groupId,
        BdoSchemaRuleKinds ruleKind = BdoSchemaRuleKinds.Requirement,
        IBdoScope scope = null,
        IBdoMetaSet varSet = null,
        IBdoLog log = null);
}