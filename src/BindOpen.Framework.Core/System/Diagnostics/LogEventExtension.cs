using System;
using System.Collections.Generic;
using System.Linq;
using BindOpen.Framework.Core.System.Diagnostics.Events;

namespace BindOpen.Framework.Core.System.Diagnostics
{
    /// <summary>
    /// This class represents a log event extension.
    /// </summary>
    public static class LogEventExtension
    {
        // Gets -------------------------

        /// <summary>
        /// Gets the specified events of this instance.
        /// </summary>
        /// <param name="logEvents">The log events to consider.</param>
        /// <param name="kinds">The event kinds to consider.</param>
        /// <returns>True if this instance has the specified events. False otherwise.</returns>
        public static List<LogEvent> GetEvents(
            this List<LogEvent> logEvents,
            params EventKind[] kinds)
        {
            return logEvents ==null ? new List<LogEvent>() : logEvents.Where(p => kinds.Length==0 || kinds.Contains(p.Kind)).ToList();
        }

        /// <summary>
        /// Gets the warnings of this instance.
        /// </summary>
        /// <param name="logEvents">The log events to consider.</param>
        /// <returns>True if this instance has the specified events. False otherwise.</returns>
        public static List<LogEvent> GetWarnings(
            this List<LogEvent> logEvents)
        {
            return logEvents.GetEvents(EventKind.Warning);
        }

        /// <summary>
        /// Gets the errors of this instance.
        /// </summary>
        /// <param name="logEvents">The log events to consider.</param>
        /// <returns>True if this instance has the specified events. False otherwise.</returns>
        public static List<LogEvent> GetErrors(
            this List<LogEvent> logEvents)
        {
            return logEvents.GetEvents(EventKind.Error);
        }

        /// <summary>
        /// Gets the exceptions of this instance.
        /// </summary>
        /// <param name="logEvents">The log events to consider.</param>
        /// <returns>True if this instance has the specified events. False otherwise.</returns>
        public static List<LogEvent> GetExceptions(
            this List<LogEvent> logEvents)
        {
            return logEvents.GetEvents(EventKind.Exception);
        }

        /// <summary>
        /// Gets the messages of this instance.
        /// </summary>
        /// <param name="logEvents">The log events to consider.</param>
        /// <returns>True if this instance has the specified events. False otherwise.</returns>
        public static List<LogEvent> GetMessages(
            this List<LogEvent> logEvents)
        {
            return logEvents.GetEvents(EventKind.Message);
        }

        /// <summary>
        /// Gets the errors or exceptions of this instance.
        /// </summary>
        /// <param name="logEvents">The log events to consider.</param>
        /// <returns>True if this instance has the specified events. False otherwise.</returns>
        public static List<LogEvent> GetErrorOrExceptions(
            this List<LogEvent> logEvents)
        {
            return logEvents.GetEvents(EventKind.Error, EventKind.Exception);
        }

        /// <summary>
        /// Gets the warnings, errors or exceptions of this instance.
        /// </summary>
        /// <param name="logEvents">The log events to consider.</param>
        /// <returns>True if this instance has the specified events. False otherwise.</returns>
        public static List<LogEvent> GetErrorOrExceptionOrWarnings(
            this List<LogEvent> logEvents)
        {
            return logEvents.GetEvents(EventKind.Warning, EventKind.Error, EventKind.Exception);
        }

        // Has --------------------------

        /// <summary>
        /// Indicates whether this instance has the specified events.
        /// </summary>
        /// <param name="logEvents">The log events to consider.</param>
        /// <param name="kinds">The event kinds to consider.</param>
        /// <returns>True if this instance has the specified events. False otherwise.</returns>
        public static Boolean Has(
            this List<LogEvent> logEvents,
            params EventKind[] kinds)
        {
            return logEvents?.Any(p => kinds.Contains(p.Kind)) ?? false;
        }

        /// <summary>
        /// Indicates whether this instance has any warnings.
        /// </summary>
        /// <param name="logEvents">The log events to consider.</param>
        /// <returns>True if this instance has the specified events. False otherwise.</returns>
        public static Boolean HasWarnings(
            this List<LogEvent> logEvents)
        {
            return logEvents.Has(EventKind.Warning);
        }

        /// <summary>
        /// Indicates whether this instance has any errors.
        /// </summary>
        /// <param name="logEvents">The log events to consider.</param>
        /// <returns>True if this instance has the specified events. False otherwise.</returns>
        public static Boolean HasErrors(
            this List<LogEvent> logEvents)
        {
            return logEvents.Has(EventKind.Error);
        }

        /// <summary>
        /// Indicates whether this instance has any exceptions.
        /// </summary>
        /// <param name="logEvents">The log events to consider.</param>
        /// <returns>True if this instance has the specified events. False otherwise.</returns>
        public static Boolean HasExceptions(
            this List<LogEvent> logEvents)
        {
            return logEvents.Has(EventKind.Exception);
        }

        /// <summary>
        /// Indicates whether this instance has any messages.
        /// </summary>
        /// <param name="logEvents">The log events to consider.</param>
        /// <returns>True if this instance has the specified events. False otherwise.</returns>
        public static Boolean HasMessages(
            this List<LogEvent> logEvents)
        {
            return logEvents.Has(EventKind.Message);
        }

        /// <summary>
        /// Indicates whether this instance has any errors or exceptions.
        /// </summary>
        /// <param name="logEvents">The log events to consider.</param>
        /// <returns>True if this instance has the specified events. False otherwise.</returns>
        public static Boolean HasErrorsOrExceptions(
            this List<LogEvent> logEvents)
        {
            return logEvents.Has(EventKind.Error, EventKind.Exception);
        }

        /// <summary>
        /// Indicates whether this instance has any warnings, errors or exceptions.
        /// </summary>
        /// <param name="logEvents">The log events to consider.</param>
        /// <returns>True if this instance has the specified events. False otherwise.</returns>
        public static Boolean HasErrorOrExceptionOrWarnings(
            this List<LogEvent> logEvents)
        {
            return logEvents.Has(EventKind.Warning, EventKind.Error, EventKind.Exception);
        }
    }
}
