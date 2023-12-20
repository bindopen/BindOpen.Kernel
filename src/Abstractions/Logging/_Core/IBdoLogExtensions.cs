using BindOpen.Data.Meta;
using System;

namespace BindOpen.Logging
{
    /// <summary>
    /// 
    /// </summary>
    public static class IBdoLogExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="execution"></param>
        /// <returns></returns>
        public static T WithExecution<T>(
            this T log,
            IBdoProcessExecution execution)
            where T : IBdoLog
        {
            if (log != null)
            {
                log.Execution = execution;
            }

            return log;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="execution"></param>
        /// <returns></returns>
        public static T WithResultCode<T>(
            this T log,
            string resultCode)
            where T : IBdoLog
        {
            if (log != null)
            {
                log.ResultCode = resultCode;
            }

            return log;
        }

        public static T WithTask<T>(
            this T log,
            IBdoConfiguration config)
            where T : IBdoLog
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
        public static bool HasEvent<T>(
            this T log,
            params EventKinds[] kinds)
            where T : IBdoLog
        {
            return log?.HasEvent(true, kinds) ?? false;
        }

        public static T AddException<T>(
            this T log,
            Exception exception,
            DateTime? date = null,
            string resultCode = null)
            where T : IBdoLog
        {
            log?.AddEvent(EventKinds.Exception, exception?.Message, exception?.ToString(), date, resultCode);

            return log;
        }
    }
}