using BindOpen.Data;
using BindOpen.Data.Objects;
using System;

namespace BindOpen.Logging;

/// <summary>
/// Defines the execution tracking information for a BindOpen process.
/// </summary>
public interface IBdoProcessExecution : IBdoObject, IIdentified
{
    /// <summary>
    /// A custom string representing the business-logic status of the process.
    /// </summary>
    string CustomStatus { get; set; }

    /// <summary>
    /// The total duration of the execution. Typically calculated as EndDate - StartDate.
    /// </summary>
    TimeSpan? Duration { get; }

    /// <summary>
    /// The date and time when the process finished.
    /// </summary>
    DateTime? EndDate { get; set; }

    /// <summary>
    /// The machine name or environment URI where the process is executing.
    /// </summary>
    string Location { get; set; }

    /// <summary>
    /// The current progress value (e.g., number of items processed).
    /// </summary>
    float ProgressIndex { get; set; }

    /// <summary>
    /// The maximum progress value (e.g., total items to process).
    /// </summary>
    float ProgressMax { get; set; }

    /// <summary>
    /// The date and time when the process was last restarted.
    /// </summary>
    DateTime? RestartDate { get; set; }

    /// <summary>
    /// A numerical representation of the result quality (e.g., 0 for success, high for critical error).
    /// </summary>
    int ResultLevel { get; set; }

    /// <summary>
    /// The date and time when the process began.
    /// </summary>
    DateTime? StartDate { get; set; }

    /// <summary>
    /// The high-level state of the execution (e.g., Pending, Running, Stopped).
    /// </summary>
    ProcessExecutionState State { get; set; }

    /// <summary>
    /// The detailed status of the execution (e.g., Success, Error, Warning).
    /// </summary>
    ProcessExecutionStatus Status { get; set; }
}