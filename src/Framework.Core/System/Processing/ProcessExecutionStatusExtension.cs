using System.Collections.Generic;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.System.Processing
{
    /// <summary>
    /// This class represents a process execution status helper.
    /// </summary>
    public static class ProcessExecutionStatusExtension
    {
        /// <summary>
        /// Executes this instance.
        /// </summary>
        /// <param name="events">The events to consider.</param>
        /// <returns>Returns the output value of the execution.</returns>
        public static ProcessExecutionStatus GetStatus(this List<ILogEvent> events)
        {
            if (events != null)
            {
                if (events.HasExceptions())
                    return ProcessExecutionStatus.Stopped_Exception;
                else if (events.HasExceptions())
                    return ProcessExecutionStatus.Stopped_Error;
                else
                    return ProcessExecutionStatus.Completed;
            }

            return ProcessExecutionStatus.None;
        }
    }
}
