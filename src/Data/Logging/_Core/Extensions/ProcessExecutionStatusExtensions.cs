using System;
using System.Collections.Generic;

namespace BindOpen.Logging;

/// <summary>
/// This static class provides extension methods for <see cref="ProcessExecutionStatus"/>.
/// </summary>
/// <remarks>
/// These extensions facilitate the conversion between high-level states and detailed statuses,
/// enabling dynamic UI filtering and default state initialization.
/// </remarks>
public static class ProcessExecutionStatusExtensions
{
    /// <summary>
    /// Converts a string representation to a <see cref="ProcessExecutionStatus"/>.
    /// </summary>
    /// <param name="st">The execution status string.</param>
    /// <returns>Returns the corresponding status, or <see cref="ProcessExecutionStatus.None"/> if invalid.</returns>
    public static ProcessExecutionStatus ToStatus(this string st)
    {
        if (!Enum.TryParse(st, true, out ProcessExecutionStatus status))
            status = ProcessExecutionStatus.None;

        return status;
    }

    /// <summary>
    /// Returns all possible detailed statuses associated with a specific high-level state.
    /// </summary>
    /// <param name="state">The state to expand.</param>
    /// <returns>Returns a list of <see cref="ProcessExecutionStatus"/> corresponding to the state.</returns>
    /// <remarks>
    /// This is particularly useful for building search filters in monitoring dashboards.
    /// </remarks>
    public static List<ProcessExecutionStatus> ToStatuses(this ProcessExecutionState state)
    {
        return state switch
        {
            ProcessExecutionState.Ended => new List<ProcessExecutionStatus>()
                {
                    ProcessExecutionStatus.Completed,
                    ProcessExecutionStatus.NothingDone,
                    ProcessExecutionStatus.Stopped,
                    ProcessExecutionStatus.Stopped_Error,
                    ProcessExecutionStatus.Stopped_Exception,
                    ProcessExecutionStatus.Stopped_User
                },
            ProcessExecutionState.Pending => new List<ProcessExecutionStatus>()
                {
                    ProcessExecutionStatus.Processing,
                    ProcessExecutionStatus.Queueing,
                    ProcessExecutionStatus.Waiting
                },
            _ => new List<ProcessExecutionStatus>(),
        };
    }

    /// <summary>
    /// Gets the most logical default status when switching to a given state.
    /// </summary>
    /// <param name="state">The state to consider.</param>
    /// <returns>Returns the default <see cref="ProcessExecutionStatus"/> for that state.</returns>
    public static ProcessExecutionStatus ToDefaultStatus(this ProcessExecutionState state)
    {
        return state switch
        {
            ProcessExecutionState.Pending => ProcessExecutionStatus.Processing,
            ProcessExecutionState.Ended => ProcessExecutionStatus.Stopped,
            _ => ProcessExecutionStatus.NothingDone,
        };
    }
}