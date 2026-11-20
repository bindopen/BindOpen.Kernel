using System;

namespace BindOpen.Logging;

/// <summary>
/// This static class provides extension methods for <see cref="ProcessExecutionState"/>.
/// </summary>
/// <remarks>
/// These extensions allow for easy conversion from strings or detailed statuses 
/// to high-level execution states, facilitating process orchestration.
/// </remarks>
public static class ProcessExecutionStateExtensions
{
    /// <summary>
    /// Converts a string representation to a <see cref="ProcessExecutionState"/>.
    /// </summary>
    /// <param name="st">The execution state string.</param>
    /// <returns>Returns the corresponding state, or <see cref="ProcessExecutionState.None"/> if invalid.</returns>
    public static ProcessExecutionState ToState(this string st)
    {
        if (!Enum.TryParse(st, true, out ProcessExecutionState state))
        {
            state = ProcessExecutionState.None;
        }

        return state;
    }

    /// <summary>
    /// Maps a detailed <see cref="ProcessExecutionStatus"/> to its corresponding high-level <see cref="ProcessExecutionState"/>.
    /// </summary>
    /// <param name="status">The detailed status to consider.</param>
    /// <returns>Returns <see cref="ProcessExecutionState.Ended"/> for any final status, 
    /// <see cref="ProcessExecutionState.Pending"/> for active ones, otherwise <see cref="ProcessExecutionState.None"/>.</returns>
    public static ProcessExecutionState ToState(this ProcessExecutionStatus status)
    {
        switch (status)
        {
            // Tout ce qui définit une fin de cycle (succès ou échec)
            case ProcessExecutionStatus.Completed:
            case ProcessExecutionStatus.Stopped:
            case ProcessExecutionStatus.Stopped_Error:
            case ProcessExecutionStatus.Stopped_Exception:
            case ProcessExecutionStatus.Stopped_User:
                return ProcessExecutionState.Ended;

            // Tout ce qui définit une activité en cours ou prévue
            case ProcessExecutionStatus.Queueing:
            case ProcessExecutionStatus.Waiting:
            case ProcessExecutionStatus.Processing:
            case ProcessExecutionStatus.Paused: // Note: On pourrait aussi ajouter Paused ici
                return ProcessExecutionState.Pending;

            case ProcessExecutionStatus.None:
            case ProcessExecutionStatus.NothingDone:
            default:
                break;
        }

        return ProcessExecutionState.None;
    }
}