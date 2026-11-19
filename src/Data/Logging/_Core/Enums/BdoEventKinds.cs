using System;
using System.Xml.Serialization;

namespace BindOpen.Logging;

/// <summary>
/// Enumerates the possible kinds of events for logging and filtering purposes.
/// Supports bitwise combinations for complex log queries.
/// </summary>
[Flags()]
[XmlType("BdoEventKinds", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
public enum BdoEventKinds
{
    /// <summary>
    /// No event kind specified.
    /// </summary>
    None = 0,

    /// <summary>
    /// A standard informational message.
    /// </summary>
    Message = 1 << 0,

    /// <summary>
    /// A significant milestone or progression point in the process.
    /// </summary>
    Checkpoint = 1 << 1,

    /// <summary>
    /// A non-critical issue that might require attention but doesn't halt execution.
    /// </summary>
    Warning = 1 << 2,

    /// <summary>
    /// A critical failure that affected the outcome of a specific task.
    /// </summary>
    Error = 1 << 3,

    /// <summary>
    /// A runtime system exception, typically including stack trace information.
    /// </summary>
    Exception = 1 << 4,

    /// <summary>
    /// An event type that does not fall into standard categories.
    /// </summary>
    Other = 1 << 5,

    /// <summary>
    /// Represents a combination of all standard event kinds. Used for broad filtering.
    /// </summary>
    Any = Checkpoint | Error | Exception | Message | Other | Warning
};