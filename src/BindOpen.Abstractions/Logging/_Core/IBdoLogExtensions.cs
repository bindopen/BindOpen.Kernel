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
            IProcessExecution execution)
            where T : IBdoLog
        {
            if (log != null)
            {
                log.Execution = execution;
            }

            return log;
        }

        public static T WithParent<T>(
            this T log,
            IBdoLog parent)
            where T : IBdoLog
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
        public static T Sanitize<T>(
            this T log,
            params IBdoLog[] childLogs)
            where T : IBdoLog
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
            where T : IBdoLog
        {
            return log?.HasEvent(true, kinds) ?? false;
        }

        // Errors, Warnings, Exceptions, Checkpoint, Message

        /// <summary>
        /// 
        /// </summary>
        /// <param key="title"></param>
        /// <param key="criticality"></param>
        /// <param key="description"></param>
        /// <param key="resultCode"></param>
        /// <param key="source"></param>
        /// <param key="date"></param>
        /// <param key="childLog"></param>
        /// <param key="logFinder"></param>
        /// <returns></returns>
        public static T AddCheckpoint<T>(
            this T log,
            string title,
            string description = null,
            DateTime? date = null,
            string resultCode = null)
            where T : IBdoLog
        {
            log?.AddEvent(EventKinds.Checkpoint, title, description, date, resultCode);

            return log;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="title"></param>
        /// <param key="criticality"></param>
        /// <param key="description"></param>
        /// <param key="resultCode"></param>
        /// <param key="source"></param>
        /// <param key="date"></param>
        /// <param key="childLog"></param>
        /// <param key="logFinder"></param>
        /// <returns></returns>
        public static T AddError<T>(
            this T log,
            string title,
            string description = null,
            DateTime? date = null,
            string resultCode = null)
            where T : IBdoLog
        {
            log?.AddEvent(EventKinds.Error, title, description, date, resultCode, log);

            return log;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="exception"></param>
        /// <param key="criticality"></param>
        /// <param key="resultCode"></param>
        /// <param key="source"></param>
        /// <param key="childLog"></param>
        /// <param key="logFinder"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param key="title"></param>
        /// <param key="criticality"></param>
        /// <param key="description"></param>
        /// <param key="resultCode"></param>
        /// <param key="source"></param>
        /// <param key="date"></param>
        /// <param key="childLog"></param>
        /// <param key="logFinder"></param>
        /// <returns></returns>
        public static T AddException<T>(
            this T log,
            string title,
            string description = null,
            DateTime? date = null,
            string resultCode = null)
            where T : IBdoLog
        {
            log?.AddEvent(EventKinds.Exception, title, description, date, resultCode);

            return log;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="title"></param>
        /// <param key="criticality"></param>
        /// <param key="description"></param>
        /// <param key="resultCode"></param>
        /// <param key="source"></param>
        /// <param key="date"></param>
        /// <param key="childLog"></param>
        /// <param key="logFinder"></param>
        /// <returns></returns>
        public static T AddMessage<T>(
            this T log,
            string title,
            string description = null,
            DateTime? date = null,
            string resultCode = null)
            where T : IBdoLog
        {
            log?.AddEvent(EventKinds.Message, title, description, date, resultCode);

            return log;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="title"></param>
        /// <param key="criticality"></param>
        /// <param key="description"></param>
        /// <param key="resultCode"></param>
        /// <param key="source"></param>
        /// <param key="date"></param>
        /// <param key="childLog"></param>
        /// <param key="logFinder"></param>
        /// <returns></returns>
        public static T AddWarning<T>(
            this T log,
            string title,
            string description = null,
            DateTime? date = null,
            string resultCode = null)
             where T : IBdoLog
        {
            log?.AddEvent(EventKinds.Warning, title, description, date, resultCode);

            return log;
        }

    }
}