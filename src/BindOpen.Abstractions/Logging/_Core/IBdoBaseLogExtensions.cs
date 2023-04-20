using BindOpen.Data.Meta;
using System;

namespace BindOpen.Logging
{
    /// <summary>
    /// 
    /// </summary>
    public static class IBdoBaseLogExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="execution"></param>
        /// <returns></returns>
        public static T WithExecution<T>(
            this T log,
            IProcessExecution execution)
            where T : IBdoBaseLog
        {
            if (log != null)
            {
                log.Execution = execution;
            }

            return log;
        }

        public static T WithParent<T>(
            this T log,
            IBdoBaseLog parent)
            where T : IBdoBaseLog
        {
            if (log != null)
            {
                log.Parent = parent;
            }

            return log;
        }

        public static T WithTaskConfig<T>(
            this T log,
            IBdoConfiguration config)
            where T : IBdoBaseLog
        {
            if (log != null)
            {
                log.TaskConfig = config;
            }

            return log;
        }

        /// <summary>
        /// 
        /// </summary>
        public static T Sanitize<T>(
            this T log,
            params IBdoBaseLog[] childLogs)
            where T : IBdoBaseLog
        {
            log?.ClearEvents();

            return log;
        }

        /// <summary>
        /// 
        /// </summary>
        public static bool HasEvent<T>(
            this T log,
            params EventKinds[] kinds)
            where T : IBdoBaseLog
        {
            return log?.HasEvent(true, kinds) ?? false;
        }

        public static T AddException<T>(
            this T log,
            Exception exception,
            DateTime? date = null,
            string resultCode = null)
            where T : IBdoBaseLog
        {
            log?.AddEvent(EventKinds.Exception, exception?.Message, exception?.ToString(), date, resultCode);

            return log;
        }
    }
}