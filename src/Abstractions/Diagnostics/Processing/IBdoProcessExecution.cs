using BindOpen.System.Data;
using System;

namespace BindOpen.System.Diagnostics.Processing
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoProcessExecution : IBdoObject, IBdoDetailed
    {
        /// <summary>
        /// 
        /// </summary>
        string CustomStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        TimeSpan? Duration { get; }

        /// <summary>
        /// 
        /// </summary>
        DateTime? EndDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string Location { get; set; }

        /// <summary>
        /// 
        /// </summary>
        float ProgressIndex { get; set; }

        /// <summary>
        /// 
        /// </summary>
        float ProgressMax { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DateTime? RestartDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        int ResultLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DateTime? StartDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        ProcessExecutionState State { get; set; }

        /// <summary>
        /// 
        /// </summary>
        ProcessExecutionStatus Status { get; set; }
    }
}