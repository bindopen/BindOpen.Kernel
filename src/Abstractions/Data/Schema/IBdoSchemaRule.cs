using BindOpen.Logging;

namespace BindOpen.Data.Schema;

/// <summary>
/// This interface defines a schema rule.
/// </summary>
public interface IBdoSchemaRule :
    IBdoObject, IIdentified, IReferenced, IIndexed,
    IBdoConditional, IBdoReferenced, IGrouped
{
    /// <summary>
    /// The kind.
    /// </summary>
    BdoSchemaRuleKinds Kind { get; set; }

    /// <summary>
    /// The result event kind.
    /// </summary>
    EventKinds ResultEventKind { get; set; }

    /// <summary>
    /// The result title.
    /// </summary>
    string ResultTitle { get; set; }

    /// <summary>
    /// The result description.
    /// </summary>
    string ResultDescription { get; set; }

    /// <summary>
    /// The result code.
    /// </summary>
    string ResultCode { get; set; }

    /// <summary>
    /// The value.
    /// </summary>
    object Value { get; set; }
}