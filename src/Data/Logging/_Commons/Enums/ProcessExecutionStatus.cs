using System.Xml.Serialization;

namespace BindOpen.Logging;

/// <summary>
/// This enumeration represents the detailed execution statuses of a process.
/// </summary>
/// <remarks>
/// These statuses provide specific context for the high-level ProcessExecutionState.
/// </remarks>
[XmlType("ProcessExecutionStatus", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
public enum ProcessExecutionStatus
{
    /// <summary>
    /// The status is not specified.
    /// </summary>
    None,

    /// <summary>
    /// The process finished without performing any action.
    /// </summary>
    NothingDone,

    /// <summary>
    /// The process is currently active and executing.
    /// </summary>
    Processing,

    /// <summary>
    /// The process is waiting for an external resource or signal.
    /// </summary>
    Waiting,

    /// <summary>
    /// The process has been temporarily suspended.
    /// </summary>
    Paused,

    /// <summary>
    /// The process is in a queue, waiting for an available execution slot.
    /// </summary>
    Queueing,

    /// <summary>
    /// The process has stopped, usually due to a standard termination.
    /// </summary>
    Stopped,

    /// <summary>
    /// The process stopped due to a system exception (e.g., a crash or unhandled runtime error).
    /// </summary>
    Stopped_Exception,

    /// <summary>
    /// The process stopped due to a functional error (e.g., invalid configuration or data validation failure).
    /// </summary>
    Stopped_Error,

    /// <summary>
    /// The process was manually terminated by a user or an administrative command.
    /// </summary>
    Stopped_User,

    /// <summary>
    /// The process has successfully completed all its tasks.
    /// </summary>
    Completed
}