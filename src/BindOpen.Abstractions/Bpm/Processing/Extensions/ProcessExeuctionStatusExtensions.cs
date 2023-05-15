using System;
using System.Collections.Generic;

namespace BindOpen.Bpm.Processing
{
    /// <summary>
    /// This class represents a event kind extension.
    /// </summary>
    public static class ProcessExeuctionStatusExtensions
    {
        /// <summary>
        /// Get the process execution status.
        /// </summary>
        /// <param name="aString">The execution status string.</param>
        public static ProcessExecutionStatus GetStatus(string aString)
        {
            if (!Enum.TryParse(aString, true, out ProcessExecutionStatus status))
                status = ProcessExecutionStatus.None;

            return status;
        }

        /// <summary>
        /// Get the process execution statuses corresponding to the specified state.
        /// </summary>
        /// <param name="state">The state to consider.</param>
        public static List<ProcessExecutionStatus> GetStatuses(ProcessExecutionState state)
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
        /// Gets the default status of the specified state.
        /// </summary>
        /// <param name="state">The state to consider.</param>
        public static ProcessExecutionStatus GetDefaultStatus(ProcessExecutionState state)
        {
            return state switch
            {
                ProcessExecutionState.Pending => ProcessExecutionStatus.Processing,
                ProcessExecutionState.Ended => ProcessExecutionStatus.Stopped,
                _ => ProcessExecutionStatus.NothingDone,
            };
        }
    }
}
