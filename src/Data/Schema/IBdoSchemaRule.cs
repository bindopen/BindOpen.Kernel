using BindOpen.Data.Objects.Interfaces;
using BindOpen.Logging;

namespace BindOpen.Data.Schema;

/// <summary>
/// Defines a schema rule used to validate metadata elements.
/// A rule encapsulates the logic for requirements or constraints and specifies 
/// the feedback to provide if the rule is triggered.
/// </summary>
public interface IBdoSchemaRule :
    IBdoObject, IIdentified, IReferenced, IIndexed,
    IBdoConditional, IBdoReferenced, IGrouped
{
    /// <summary>
    /// Gets or sets the kind of this rule (e.g., Requirement or Constraint).
    /// </summary>
    BdoSchemaRuleKinds Kind { get; set; }

    /// <summary>
    /// Gets or sets the severity level of the event generated when this rule is triggered 
    /// (e.g., Error, Warning, Information).
    /// </summary>
    BdoEventKinds ResultEventLevel { get; set; }

    /// <summary>
    /// Gets or sets the title of the result message to display or log.
    /// </summary>
    string ResultTitle { get; set; }

    /// <summary>
    /// Gets or sets the detailed description of the result message.
    /// </summary>
    string ResultDescription { get; set; }

    /// <summary>
    /// Gets or sets the specific result code (e.g., "Missing", "InvalidData").
    /// This matches the constants defined in BdoSchemaRuleResultCodes.
    /// </summary>
    string ResultCode { get; set; }

    /// <summary>
    /// Gets or sets the value associated with this rule.
    /// This could be a regex pattern, a numeric range, or a script to evaluate.
    /// </summary>
    object Value { get; set; }
}