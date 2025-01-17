﻿using System;
using System.Collections.Generic;
using BindOpen.Logging;

namespace BindOpen.Logging
{
    /// <summary>
    /// This class represents a event level extension.
    /// </summary>
    public static class ProcessExeuctionStatusExtensions
    {
        /// <summary>
        /// Get the process execution status.
        /// </summary>
        /// <param name="st">The execution status string.</param>
        public static ProcessExecutionStatus ToStatus(this string st)
        {
            if (!Enum.TryParse(st, true, out ProcessExecutionStatus status))
                status = ProcessExecutionStatus.None;

            return status;
        }

        /// <summary>
        /// Get the process execution statuses corresponding to the specified state.
        /// </summary>
        /// <param name="state">The state to consider.</param>
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
        /// Gets the default status of the specified state.
        /// </summary>
        /// <param name="state">The state to consider.</param>
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
}
