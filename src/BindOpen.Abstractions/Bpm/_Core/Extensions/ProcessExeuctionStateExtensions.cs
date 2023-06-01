using System;

namespace BindOpen.Bpm
{
    /// <summary>
    /// This class represents a event kind extension.
    /// </summary>
    public static class ProcessExeuctionStateExtensions
    {

        /// <summary>
        /// Get the process execution state.
        /// </summary>
        /// <param name="st">The execution state string.</param>
        public static ProcessExecutionState ToState(this string st)
        {
            if (!Enum.TryParse(st, true, out ProcessExecutionState state))
            {
                state = ProcessExecutionState.None;
            }

            return state;
        }

        /// <summary>
        /// Get the process execution statuse corresponding to the specified state.
        /// </summary>
        /// <param name="status">The status to consider.</param>
        public static ProcessExecutionState ToState(this ProcessExecutionStatus status)
        {
            switch (status)
            {
                case ProcessExecutionStatus.Completed:
                case ProcessExecutionStatus.Stopped:
                case ProcessExecutionStatus.Stopped_Error:
                case ProcessExecutionStatus.Stopped_Exception:
                case ProcessExecutionStatus.Stopped_User:
                    return ProcessExecutionState.Ended;
                case ProcessExecutionStatus.Queueing:
                case ProcessExecutionStatus.Waiting:
                case ProcessExecutionStatus.Processing:
                    return ProcessExecutionState.Pending;
                case ProcessExecutionStatus.None:
                case ProcessExecutionStatus.NothingDone:
                    break;
            }

            return ProcessExecutionState.None;
        }
    }
}
