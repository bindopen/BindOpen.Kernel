using System.Xml.Serialization;

namespace BindOpen.Logging;

/// <summary>
/// This enumeration represents the high-level states of a process execution.
/// </summary>
/// <remarks>
/// Use this to determine if a process is still active (Pending) or has reached 
/// a final state (Ended), regardless of whether it succeeded or failed.
/// </remarks>
[XmlType("ProcessExecutionState", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
public enum ProcessExecutionState
{
    /// <summary>
    /// The state is not specified or unknown.
    /// </summary>
    None,

    /// <summary>
    /// The process is currently active, queued, or waiting to run.
    /// </summary>
    Pending,

    /// <summary>
    /// The process has finished its execution.
    /// </summary>
    Ended
}