using System;

namespace BindOpen.Bpm.Processing
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
            this T log,
            string status)
            where T : IBdoProcessExecution
        {
            if (log != null)
            {
                log.CustomStatus = status;
            }

            return log;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="execution"></param>
        /// <returns></returns>
        public static T WithEndDate<T>(
            this T log,
            DateTime? endDate)
            where T : IBdoProcessExecution
        {
            if (log != null)
            {
                log.EndDate = endDate;
            }

            return log;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="execution"></param>
        /// <returns></returns>
        public static T WithLocation<T>(
            this T log,
            string location)
            where T : IBdoProcessExecution
        {
            if (log != null)
            {
                log.Location = location;
            }

            return log;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="execution"></param>
        /// <returns></returns>
        public static T WithProgressIndex<T>(
            this T log,
            float index)
            where T : IBdoProcessExecution
        {
            if (log != null)
            {
                log.ProgressIndex = index;
            }

            return log;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="execution"></param>
        /// <returns></returns>
        public static T WithProgressMax<T>(
            this T log,
            float max)
            where T : IBdoProcessExecution
        {
            if (log != null)
            {
                log.ProgressMax = max;
            }

            return log;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="execution"></param>
        /// <returns></returns>
        public static T WithRestartDate<T>(
            this T log,
            DateTime? date)
            where T : IBdoProcessExecution
        {
            if (log != null)
            {
                log.RestartDate = date;
            }

            return log;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="execution"></param>
        /// <returns></returns>
        public static T WithResultLevel<T>(
            this T log,
            int level)
            where T : IBdoProcessExecution
        {
            if (log != null)
            {
                log.ResultLevel = level;
            }

            return log;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="execution"></param>
        /// <returns></returns>
        public static T WithStartDate<T>(
            this T log,
            DateTime? date)
            where T : IBdoProcessExecution
        {
            if (log != null)
            {
                log.StartDate = date;
            }

            return log;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="execution"></param>
        /// <returns></returns>
        public static T WithState<T>(
            this T log,
            ProcessExecutionState state)
            where T : IBdoProcessExecution
        {
            if (log != null)
            {
                log.State = state;
            }

            return log;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="execution"></param>
        /// <returns></returns>
        public static T WithStatus<T>(
            this T log,
            ProcessExecutionStatus status)
            where T : IBdoProcessExecution
        {
            if (log != null)
            {
                log.Status = status;
            }

            return log;
        }
    }
}