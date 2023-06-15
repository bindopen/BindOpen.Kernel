using System;

namespace BindOpen.System.Processing
{
    /// <summary>
    /// 
    /// </summary>
    public static class IBdoProcessExecutionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="execution"></param>
        /// <returns></returns>
        public static T WithCustomStatus<T>(
            this T execution,
            string status)
            where T : IBdoProcessExecution
        {
            if (execution != null)
            {
                execution.CustomStatus = status;
            }

            return execution;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="execution"></param>
        /// <returns></returns>
        public static T WithEndDate<T>(
            this T execution,
            DateTime? endDate)
            where T : IBdoProcessExecution
        {
            if (execution != null)
            {
                execution.EndDate = endDate;
            }

            return execution;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="execution"></param>
        /// <returns></returns>
        public static T WithLocation<T>(
            this T execution,
            string location)
            where T : IBdoProcessExecution
        {
            if (execution != null)
            {
                execution.Location = location;
            }

            return execution;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="execution"></param>
        /// <returns></returns>
        public static T WithProgressIndex<T>(
            this T execution,
            float index)
            where T : IBdoProcessExecution
        {
            if (execution != null)
            {
                execution.ProgressIndex = index;
            }

            return execution;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="execution"></param>
        /// <returns></returns>
        public static T WithProgressMax<T>(
            this T execution,
            float max)
            where T : IBdoProcessExecution
        {
            if (execution != null)
            {
                execution.ProgressMax = max;
            }

            return execution;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="execution"></param>
        /// <returns></returns>
        public static T WithRestartDate<T>(
            this T execution,
            DateTime? date)
            where T : IBdoProcessExecution
        {
            if (execution != null)
            {
                execution.RestartDate = date;
            }

            return execution;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="execution"></param>
        /// <returns></returns>
        public static T WithResultLevel<T>(
            this T execution,
            int level)
            where T : IBdoProcessExecution
        {
            if (execution != null)
            {
                execution.ResultLevel = level;
            }

            return execution;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="execution"></param>
        /// <returns></returns>
        public static T WithStartDate<T>(
            this T execution,
            DateTime? date)
            where T : IBdoProcessExecution
        {
            if (execution != null)
            {
                execution.StartDate = date;
            }

            return execution;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="execution"></param>
        /// <returns></returns>
        public static T WithState<T>(
            this T execution,
            ProcessExecutionState state)
            where T : IBdoProcessExecution
        {
            if (execution != null)
            {
                execution.State = state;
            }

            return execution;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="execution"></param>
        /// <returns></returns>
        public static T WithStatus<T>(
            this T execution,
            ProcessExecutionStatus status)
            where T : IBdoProcessExecution
        {
            if (execution != null)
            {
                execution.Status = status;
            }

            return execution;
        }

        // Actions

        public static void SetAsStarted<T>(
            this T execution)
            where T : IBdoProcessExecution
        {
            execution
                .WithStartDate(DateTime.Now)
                .WithRestartDate(null)
                .WithEndDate(null)
                .WithState(ProcessExecutionState.Pending)
                .WithStatus(ProcessExecutionStatus.Processing)
                .WithProgressIndex(0);
        }

        /// <summary>
        /// Restarts this instance.
        /// </summary>
        public static void SetAsRestarted<T>(
            this T execution)
            where T : IBdoProcessExecution
        {
            execution
                .WithRestartDate(DateTime.Now)
                .WithEndDate(null)
                .WithState(ProcessExecutionState.Pending)
                .WithStatus(ProcessExecutionStatus.Processing)
                .WithProgressIndex(0);
        }

        /// <summary>
        /// Resumes this instance.
        /// </summary>
        public static void SetAsResumed<T>(
            this T execution)
            where T : IBdoProcessExecution
        {
            if (!(execution?.State == ProcessExecutionState.Pending && execution?.Status == ProcessExecutionStatus.Paused)) return;

            execution
                .WithEndDate(null)
                .WithState(ProcessExecutionState.Pending)
                .WithStatus(ProcessExecutionStatus.Processing);
        }

        /// <summary>
        /// Resumes this instance.
        /// </summary>
        public static void SetAsPaused<T>(
            this T execution)
            where T : IBdoProcessExecution
        {
            if (execution?.State != ProcessExecutionState.Pending) return;

            execution
                .WithEndDate(null)
                .WithState(ProcessExecutionState.Pending)
                .WithStatus(ProcessExecutionStatus.Paused);
        }

        /// <summary>
        /// Ends this instance specifying the status.
        /// </summary>
        /// <param name="status">The new status to consider.</param>
        public static void SetAsEnded<T>(
            this T execution,
            ProcessExecutionStatus status = ProcessExecutionStatus.Completed)
            where T : IBdoProcessExecution
        {
            if (!ProcessExecutionState.Ended.ToStatuses().Contains(status)) return;

            execution
                .WithEndDate(DateTime.Now)
                .WithState(ProcessExecutionState.Ended)
                .WithStatus(status);
        }
    }
}