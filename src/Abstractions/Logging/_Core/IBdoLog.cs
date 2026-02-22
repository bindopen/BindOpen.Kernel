using BindOpen.Data;
using BindOpen.Data.Meta;
using System;

namespace BindOpen.Logging;

/// <summary>
/// Defines a structured, hierarchical log within the BindOpen framework.
/// </summary>
public interface IBdoLog :
    IIdentified, INamed,
    ITitled, IDescribed,
    ITTreeNode<IBdoLog>,
    IBdoDetailed, IBdoObject
{
    /// <summary>
    /// Gets or sets the result code associated with this log (e.g., an error code or status identifier).
    /// </summary>
    string ResultCode { get; set; }

    /// <summary>
    /// Gets or sets the task configuration that generated this log.
    /// </summary>
    IBdoConfiguration TaskConfig { get; set; }

    /// <summary>
    /// Gets or sets the execution telemetry (start/end dates, progress, etc.) for this log.
    /// </summary>
    IBdoProcessExecution Execution { get; set; }

    /// <summary>
    /// Creates a new child instance of IBdoLog.
    /// </summary>
    IBdoLog NewLog();

    /// <summary>
    /// Inserts a new child log at the beginning of the children collection.
    /// </summary>
    IBdoLog InsertChild(
        BdoEventKinds kind,
        string title,
        string description = null,
        DateTime? date = null,
        string resultCode = null);

    /// <summary>
    /// Adds a child log or creates a new one with the specified event details.
    /// </summary>
    IBdoLog AddChild(
        IBdoLog childLog = null,
        BdoEventKinds kind = BdoEventKinds.Any,
        string title = null,
        string description = null,
        DateTime? date = null,
        string resultCode = null);

    /// <summary>
    /// Adds a specific event entry to this log.
    /// </summary>
    IBdoLog AddEvent(
        BdoEventKinds kind,
        string title,
        string description = null,
        DateTime? date = null,
        string resultCode = null);

    /// <summary>
    /// Checks if the log (and optionally its children) contains events of specific kinds.
    /// </summary>
    bool HasEvent(bool isRecursive = true, params BdoEventKinds[] kinds);

    /// <summary>
    /// Removes events of specific kinds from the log.
    /// </summary>
    int RemoveEvents(bool isRecursive = true, params BdoEventKinds[] kinds);

    /// <summary>
    /// Cleans up and normalizes the log entries.
    /// </summary>
    void Sanitize();
}