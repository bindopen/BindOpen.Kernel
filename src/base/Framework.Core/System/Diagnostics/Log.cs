using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Dictionary;
using BindOpen.Framework.Core.Extensions.Items.Tasks;
using BindOpen.Framework.Core.System.Diagnostics.Events;
using BindOpen.Framework.Core.System.Diagnostics.Loggers;
using BindOpen.Framework.Core.System.Processing;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.System.Diagnostics
{
    /// <summary>
    /// This class represents a logger of tasks.
    /// </summary>
    [Serializable()]
    [XmlType("Log", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "log", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class Log : DescribedDataItem, ILog
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        // Execution ----------------------------------

        private TaskConfiguration _task = null;

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        // Execution ----------------------------------

        /// <summary>
        /// Execution of this instance.
        /// </summary>
        [XmlElement("execution")]
        public ProcessExecution Execution { get; set; } = null;

        /// <summary>
        /// Specification of the Execution property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool ExecutionSpecified => Execution != null;

        // Task ----------------------------------

        /// <summary>
        /// Logged by this instance. By default, a new task is initialized when this instance is initialized.
        /// </summary>
        [XmlElement("task")]
        public TaskConfiguration Task
        {
            get => _task;
            set => WriteLog(_task = value, LoggerMode.Auto);
        }

        /// <summary>
        /// Specification of the Task property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool TaskSpecified => _task != null;

        /// <summary>
        /// Function that filters event.
        /// </summary>
        [XmlIgnore()]
        public Predicate<ILogEvent> SubLogEventPredicate
        {
            get;
            set;
        }

        // Detail ----------------------------------

        /// <summary>
        /// Detail of this instance.
        /// </summary>
        [XmlElement("detail")]
        public DataElementSet Detail { get; set; } = null;

        /// <summary>
        /// Specification of the Detail property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool DetailSpecified => Detail?.Elements.Count > 0;

        // Events ----------------------------------

        /// <summary>
        /// The event with the specified ID.
        /// </summary>
        /// <param name="id"></param>
        [XmlIgnore()]
        public LogEvent this[string id] => id == null ? null : Events?.Find(p => p.Id.KeyEquals(id));

        /// <summary>
        /// The event with the specified ID.
        /// </summary>
        /// <param name="index"></param>
        [XmlIgnore()]
        public LogEvent this[int index] => Events?.Cast<object>().ToArray().GetObjectAtIndex(index) as LogEvent;

        /// <summary>
        /// Events of this instance.
        /// </summary>
        /// <seealso cref="Errors"/>
        /// <seealso cref="Warnings"/>
        /// <seealso cref="Messages"/>
        /// <seealso cref="Exceptions"/>
        /// <seealso cref="Checkpoints"/>
        /// <seealso cref="SubLogs"/>
        [XmlArray("events")]
        [XmlArrayItem("event")]
        public List<LogEvent> Events { get; set; } = null;

        /// <summary>
        /// Specification of the Events property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool EventsSpecified => Events?.Count > 0;

        /// <summary>
        /// Errors of this instance.
        /// </summary>
        /// <seealso cref="Events"/>
        /// <seealso cref="Warnings"/>
        /// <seealso cref="Messages"/>
        /// <seealso cref="Exceptions"/>
        /// <seealso cref="Checkpoints"/>
        /// <seealso cref="SubLogs"/>
        [XmlIgnore()]
        public List<ILogEvent> Errors => Events == null ? new List<ILogEvent>() : Events.Where(p => p.Kind == EventKinds.Error).ToList<ILogEvent>();

        /// <summary>
        /// Warnings of this instance.
        /// </summary>
        /// <seealso cref="Errors"/>
        /// <seealso cref="Events"/>
        /// <seealso cref="Messages"/>
        /// <seealso cref="Exceptions"/>
        /// <seealso cref="Checkpoints"/>
        /// <seealso cref="SubLogs"/>
        [XmlIgnore()]
        public List<ILogEvent> Warnings => Events == null ? new List<ILogEvent>() : Events.Where(p => p.Kind == EventKinds.Warning).ToList<ILogEvent>();

        /// <summary>
        /// Messages of this instance.
        /// </summary>
        /// <seealso cref="Errors"/>
        /// <seealso cref="Warnings"/>
        /// <seealso cref="Events"/>
        /// <seealso cref="Exceptions"/>
        /// <seealso cref="Checkpoints"/>
        /// <seealso cref="SubLogs"/>
        [XmlIgnore()]
        public List<ILogEvent> Messages => Events == null ? new List<ILogEvent>() : Events.Where(p => p.Kind == EventKinds.Message).ToList<ILogEvent>();

        /// <summary>
        /// Exceptions of this instance.
        /// </summary>
        /// <seealso cref="Errors"/>
        /// <seealso cref="Warnings"/>
        /// <seealso cref="Messages"/>
        /// <seealso cref="Events"/>
        /// <seealso cref="Checkpoints"/>
        /// <seealso cref="SubLogs"/>
        [XmlIgnore()]
        public List<ILogEvent> Exceptions => Events == null ? new List<ILogEvent>() : Events.Where(p => p.Kind == EventKinds.Exception).ToList<ILogEvent>();

        /// <summary>
        /// Checkpoints of this instance.
        /// </summary>
        /// <seealso cref="Errors"/>
        /// <seealso cref="Warnings"/>
        /// <seealso cref="Messages"/>
        /// <seealso cref="Exceptions"/>
        /// <seealso cref="Events"/>
        /// <seealso cref="SubLogs"/>
        [XmlIgnore()]
        public List<ILogEvent> Checkpoints => Events == null ? new List<ILogEvent>() : Events.Where(p => p.Kind == EventKinds.Checkpoint).ToList<ILogEvent>();

        /// <summary>
        /// Logs of this instance.
        /// </summary>
        /// <seealso cref="Errors"/>
        /// <seealso cref="Warnings"/>
        /// <seealso cref="Messages"/>
        /// <seealso cref="Exceptions"/>
        /// <seealso cref="Events"/>
        /// <seealso cref="SubLogs"/>
        [XmlIgnore()]
        public List<ILog> SubLogs => Events == null ? new List<ILog>() : Events.Where(p => p.Log != null).Select(p => p.Log as Log).ToList<ILog>();

        // Tree ----------------------------------

        /// <summary>
        /// Parent of this instance.
        /// </summary>
        [XmlIgnore()]
        public ILog Parent { get; set; } = null;

        /// <summary>
        /// Root of this instance.
        /// </summary>
        [XmlIgnore()]
        public ILog Root
        {
            get { return GetRoot(); }
        }

        /// <summary>
        /// Specification of the Task property of this instance.
        /// </summary>
        [XmlIgnore()]
        public int Level => Parent == null ? 0 : Parent.Level + 1;

        // Logger ----------------------------------

        /// <summary>
        /// Loggers of this instance.
        /// </summary>
        [XmlIgnore()]
        public List<ILogger> Loggers { get; set; } = null;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the Log class.
        /// </summary>
        public Log() : base(null, "log_")
        {
        }

        /// <summary>
        /// Instantiates a new instance of the Log class.
        /// </summary>
        /// <param name="loggers">The loggers to consider.</param>
        public Log(
            params ILogger[] loggers) : this(null, loggers)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the Log class.
        /// </summary>
        /// <param name="eventFinder">The function that filters event.</param>
        /// <param name="loggers">The loggers to consider.</param>
        public Log(
            Predicate<ILogEvent> eventFinder = null,
            params ILogger[] loggers) : this()
        {
            SubLogEventPredicate = eventFinder;
            Loggers = loggers.Where(p => p != null).ToList();
            foreach (ILogger logger in Loggers)
                logger.SetLog(this);
        }

        /// <summary>
        /// Instantiates a new instance of the Log class.
        /// </summary>
        /// <param name="task">The task to consider.</param>
        /// <param name="eventFinder">The function that filters event.</param>
        /// <param name="loggers">The loggers to consider.</param>
        public Log(
            ITaskConfiguration task,
            Predicate<ILogEvent> eventFinder = null,
            params ILogger[] loggers)
            : this(eventFinder, loggers)
        {
            _task = task as TaskConfiguration;
        }

        /// <summary>
        /// Instantiates a new instance of the Log class specifying parent.
        /// </summary>
        /// <param name="parentLog">The parent logger to consider.</param>
        /// <param name="task">The task to consider.</param>
        /// <param name="eventFinder">The function that filters event.</param>
        public Log(
            ILog parentLog,
            ITaskConfiguration task = null,
            Predicate<ILogEvent> eventFinder = null)
            : this(eventFinder, (parentLog != null ? parentLog.Loggers.ToArray() : new Logger[0]))
        {
            _task = task as TaskConfiguration;
            if (parentLog != null)
            {
                Parent = parentLog as Log;
            }
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        // Logging ----------------------------------------

        /// <summary>
        /// Adds the specified loggers.
        /// </summary>
        /// <param name="loggers">The loggers to add.</param>
        public void AddLoggers(params ILogger[] loggers)
        {
            if (loggers != null)
            {
                if (Loggers == null)
                    Loggers = new List<ILogger>();

                foreach (ILogger logger in loggers)
                {
                    if (logger != null)
                    {
                        Loggers.Add(logger);
                    }
                }
            }
        }

        /// <summary>
        /// Logs the specified task.
        /// </summary>
        /// <param name="task">The task to log.</param>
        /// <param name="mode">The mode to log.</param>
        public void WriteLog(ITaskConfiguration task, LoggerMode mode = LoggerMode.Auto)
        {
            foreach (ILogger logger in Loggers)
            {
                if ((logger.Mode != LoggerMode.Off && mode == LoggerMode.Any) || (logger.Mode == mode))
                {
                    logger.WriteTask(this, task);
                }
            }
        }

        /// <summary>
        /// Logs the specified event.
        /// </summary>
        /// <param name="logEvent">The event to log.</param>
        /// <param name="mode">The mode to log.</param>
        public void WriteLog(ILogEvent logEvent, LoggerMode mode = LoggerMode.Auto)
        {
            if (Loggers != null)
            {
                foreach (ILogger logger in Loggers)
                {
                    if ((logger.Mode != LoggerMode.Off && mode == LoggerMode.Any) || (logger.Mode == mode))
                    {
                        logger.WriteEvent(logEvent);
                    }
                }
            }
        }

        /// <summary>
        /// Logs the specified element.
        /// </summary>
        /// <param name="elementName">The element name to log.</param>
        /// <param name="elementValue">The element value to log.</param>
        /// <param name="mode">The mode to log.</param>
        public void WriteLog(string elementName, object elementValue, LoggerMode mode = LoggerMode.Auto)
        {
            foreach (ILogger logger in Loggers)
            {
                if ((logger.Mode != LoggerMode.Off && mode == LoggerMode.Any) || (logger.Mode == mode))
                {
                    logger.WriteDetailElement(this, elementName, elementValue);
                }
            }
        }

        /// <summary>
        /// Logs the specified child log.
        /// </summary>
        /// <param name="childLog">The child log to consider.</param>
        /// <param name="mode">The mode to log.</param>
        public void WriteLog(ILog childLog, LoggerMode mode = LoggerMode.Auto)
        {
            foreach (ILogger logger in Loggers)
            {
                if ((logger.Mode != LoggerMode.Off && mode == LoggerMode.Any) || (logger.Mode == mode))
                {
                    logger.WriteChildLog(this, childLog);
                }
            }
        }

        // Events ------------------------------------

        /// <summary>
        /// Adds a new log event.
        /// </summary>
        /// <param name="logEvent">The log event to add.</param>
        /// <param name="childLog">The child log of this instance.</param>
        /// <param name="logFinder">The filter function to consider. If true then the child log is added otherwise it is not.</param>
        public bool AddEvent(
            ILogEvent logEvent,
            ILog childLog = null,
            Predicate<ILog> logFinder = null)
        {
            bool isAdded = false;
            if (logEvent != null)
            {
                if (logFinder == null || (childLog != null && logFinder.Invoke(childLog)))
                {
                    if (Loggers?.Any(q => q.IsHistoryRequired()) != false
                        || (SubLogEventPredicate == null || SubLogEventPredicate.Invoke(logEvent)))
                    {
                        if (childLog != null)
                        {
                            if (logEvent.Title == null && childLog.Title != null) logEvent.Title = childLog.Title.Clone() as DictionaryDataItem;
                            if (logEvent.Description == null && childLog.Description != null) logEvent.Description = childLog.Description.Clone() as DictionaryDataItem;
                            if (logEvent.Kind == EventKinds.Any) logEvent.Kind = childLog.GetMaxEventKind();
                            childLog.Parent = this;
                            childLog.Loggers = Loggers;
                            logEvent.Log = childLog;
                            //childLog.Event = logEvent;
                        }

                        //if (Event != null && (Event.Kind == EventKind.None || Event.Kind == EventKind.Any))
                        //    Event.Kind = Event.Kind.Max(logEvent.Kind);

                        (Events ?? (Events = new List<LogEvent>())).Add(logEvent as LogEvent);

                        isAdded = true;
                    }

                    logEvent.Parent = this;
                    WriteLog(logEvent, LoggerMode.Auto);
                }
            }

            return isAdded;
        }

        /// <summary>
        /// Adds the specified log event.
        /// </summary>
        /// <param name="kind">The kind of this instance.</param>
        /// <param name="title">The title of this instance.</param>
        /// <param name="criticality">The criticality of this instance.</param>
        /// <param name="description">The description of this instance.</param>
        /// <param name="resultCode">The result code of this instance.</param>
        /// <param name="source">The ExtensionDataContext of this instance.</param>
        /// <param name="date">The date to consider.</param>
        /// <param name="childLog">The child log of this instance.</param>
        /// <param name="logFinder">The filter function to consider. If true then the child log is added otherwise it is not.</param>
        public ILogEvent AddEvent(
            EventKinds kind,
            string title,
            EventCriticality criticality = EventCriticality.None,
            string description = null,
            string resultCode = null,
            string source = null,
            DateTime? date = null,
            ILog childLog = null,
            Predicate<ILog> logFinder = null)
        {
            LogEvent logEvent;
            AddEvent(
                logEvent = new LogEvent(
                    kind,
                    title ?? childLog?.Title,
                    criticality,
                    description,
                    resultCode,
                    source,
                    date),
                childLog,
                logFinder);

            return logEvent;
        }

        /// <summary>
        /// Adds the specified warning.
        /// </summary>
        /// <param name="title">The title of this instance.</param>
        /// <param name="description">The description of this instance.</param>
        /// <param name="criticality">The criticality of this instance.</param>
        /// <param name="resultCode">The result code of this instance.</param>
        /// <param name="aSource">The ExtensionDataContext of this instance.</param>
        /// <param name="date">The date to consider.</param>
        /// <param name="childLog">The child log of this instance.</param>
        /// <param name="logFinder">The filter function to consider. If true then the child log is added otherwise it is not.</param>
        public ILogEvent AddWarning(
            string title,
            EventCriticality criticality = EventCriticality.None,
            string description = null,
            string resultCode = null,
            string aSource = null,
            DateTime? date = null,
            ILog childLog = null,
            Predicate<ILog> logFinder = null)
        {
            return AddEvent(
                EventKinds.Warning,
                title,
                criticality,
                description,
                resultCode,
                aSource,
                date);
        }

        /// <summary>
        /// Adds the specified error.
        /// </summary>
        /// <param name="title">The title of this instance.</param>
        /// <param name="description">The description of this instance.</param>
        /// <param name="criticality">The criticality of this instance.</param>
        /// <param name="resultCode">The result code of this instance.</param>
        /// <param name="aSource">The ExtensionDataContext of this instance.</param>
        /// <param name="date">The date to consider.</param>
        /// <param name="childLog">The child log of this instance.</param>
        /// <param name="logFinder">The filter function to consider. If true then the child log is added otherwise it is not.</param>
        public ILogEvent AddError(
            string title,
            EventCriticality criticality = EventCriticality.None,
            string description = null,
            string resultCode = null,
            string aSource = null,
            DateTime? date = null,
            ILog childLog = null,
            Predicate<ILog> logFinder = null)
        {
            return AddEvent(
                EventKinds.Error,
                title,
                criticality,
                description,
                resultCode,
                aSource,
                date);
        }

        /// <summary>
        /// Adds the specified checkpoint.
        /// </summary>
        /// <param name="title">The title of this instance.</param>
        /// <param name="description">The description of this instance.</param>
        /// <param name="criticality">The criticality of this instance.</param>
        /// <param name="resultCode">The result code of this instance.</param>
        /// <param name="source">The ExtensionDataContext of this instance.</param>
        /// <param name="date">The date to consider.</param>
        /// <param name="childLog">The child log of this instance.</param>
        /// <param name="logFinder">The filter function to consider. If true then the child log is added otherwise it is not.</param>
        public ILogEvent AddCheckpoint(
            string title,
            EventCriticality criticality = EventCriticality.None,
            string description = null,
            string resultCode = null,
            string source = null,
            DateTime? date = null,
            ILog childLog = null,
            Predicate<ILog> logFinder = null)
        {
            return AddEvent(
                EventKinds.Checkpoint,
                title,
                criticality,
                description,
                resultCode,
                source,
                date);
        }

        /// <summary>
        /// Adds the specified message.
        /// </summary>
        /// <param name="title">The title of this instance.</param>
        /// <param name="description">The description of this instance.</param>
        /// <param name="criticality">The criticality of this instance.</param>
        /// <param name="resultCode">The result code of this instance.</param>
        /// <param name="source">The ExtensionDataContext of this instance.</param>
        /// <param name="date">The date to consider.</param>
        /// <param name="childLog">The child log of this instance.</param>
        /// <param name="logFinder">The filter function to consider. If true then the child log is added otherwise it is not.</param>
        public ILogEvent AddMessage(
            string title,
            EventCriticality criticality = EventCriticality.None,
            string description = null,
            string resultCode = null,
            string source = null,
            DateTime? date = null,
            ILog childLog = null,
            Predicate<ILog> logFinder = null)
        {
            return AddEvent(
                EventKinds.Message,
                title,
                criticality,
                description,
                resultCode,
                source,
                date);
        }

        /// <summary>
        /// Adds the specified exception.
        /// </summary>
        /// <param name="title">The title of this instance.</param>
        /// <param name="description">The description of this instance.</param>
        /// <param name="criticality">The criticality of this instance.</param>
        /// <param name="resultCode">The result code of this instance.</param>
        /// <param name="source">The ExtensionDataContext of this instance.</param>
        /// <param name="date">The date to consider.</param>
        /// <param name="childLog">The child log of this instance.</param>
        /// <param name="logFinder">The filter function to consider. If true then the child log is added otherwise it is not.</param>
        public ILogEvent AddException(
            string title,
            EventCriticality criticality = EventCriticality.None,
            string description = null,
            string resultCode = null,
            string source = null,
            DateTime? date = null,
            ILog childLog = null,
            Predicate<ILog> logFinder = null)
        {
            return AddEvent(
                EventKinds.Exception,
                title,
                criticality,
                description,
                resultCode,
                source,
                date);
        }

        /// <summary>
        /// Adds the specified exception.
        /// </summary>
        /// <param name="exception">The exception to consider.</param>
        /// <param name="criticality">The criticality to consider.</param>
        /// <param name="resultCode">The result code to consider.</param>
        /// <param name="source">The ExtensionDataContext to consider.</param>
        /// <param name="childLog">The child log of this instance.</param>
        /// <param name="logFinder">The filter function to consider. If true then the child log is added otherwise it is not.</param>
        public ILogEvent AddException(
            Exception exception,
            EventCriticality criticality = EventCriticality.None,
            string resultCode = null,
            string source = null,
            ILog childLog = null,
            Predicate<ILog> logFinder = null)
        {
            LogEvent logEvent = null;
            AddEvent(
                logEvent = new LogEvent(
                    exception,
                    criticality,
                    resultCode,
                    source));
            return logEvent;
        }

        /// <summary>
        /// Adds the events of the specified log.
        /// </summary>
        /// <param name="log">The log whose task results must be added.</param>
        /// <param name="kinds">The event kinds to add.</param>
        /// <returns>Returns the added events.</returns>
        public List<ILogEvent> AddEvents(
            ILog log,
            params EventKinds[] kinds)
        {
            return AddEvents(log, null, kinds);
        }

        /// <summary>
        /// Adds the events of the specified log.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <param name="logFinder">The filter function to consider. If true then the child log is added otherwise it is not.</param>
        /// <param name="kinds">The event kinds to add.</param>
        /// <returns>Returns the added events.</returns>
        public List<ILogEvent> AddEvents(
            ILog log,
            Predicate<ILog> logFinder = null,
            params EventKinds[] kinds)
        {
            List<ILogEvent> logEvents = new List<ILogEvent>();

            if ((log?.Events != null) && (logFinder?.Invoke(log) != false))
            {
                logEvents = log.Events.Where(p => kinds.Length == 0 || kinds.Contains(p.Kind)).ToList<ILogEvent>();
                if (logEvents != null)
                {
                    foreach (ILogEvent currentEvent in logEvents)
                    {
                        AddEvent(currentEvent);
                    }
                }
            }

            return logEvents;
        }

        /// <summary>
        /// Adds the events of the specified log.
        /// </summary>
        /// <param name="log">The log whose task results must be added.</param>
        /// <returns>Returns the added events.</returns>
        /// <remarks>This function equals to AddEventsexcept except that it does not allow to filter with log event kinds.</remarks>
        public List<ILogEvent> Append(
            ILog log)
        {
            return AddEvents(log);
        }

        /// <summary>
        /// Adds the events of the specified log.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <param name="logFinder">The filter function to consider. If true then the child log is added otherwise it is not.</param>
        /// <returns>Returns the added events.</returns>
        /// <remarks>This function equals to AddEventsexcept except that it does not allow to filter with log event kinds.</remarks>
        public List<ILogEvent> Append(
            ILog log,
            Predicate<ILog> logFinder = null)
        {
            return AddEvents(log);
        }

        /// <summary>
        /// Clears the specified events.
        /// </summary>
        /// <param name="isRecursive">Indicate whether the search is recursive.</param>
        /// <param name="kinds">The kinds to consider.</param>
        public void ClearEvents(
            bool isRecursive = true,
            params EventKinds[] kinds)
        {
            Events?.RemoveAll(p => kinds.Contains(p.Kind));

            if (isRecursive)
            {
                foreach (Log childLog in SubLogs)
                {
                    ClearEvents(isRecursive, kinds);
                }
            }
        }

        /// <summary>
        /// Sanitize this instance.
        /// </summary>
        public void Sanitize()
        {
            // we clear the task check points if there is no special results in load task
            if (!HasErrorsOrExceptionsOrWarnings())
                ClearEvents(true, EventKinds.Checkpoint);
        }

        // Sub logs ------------------------------------

        /// <summary>
        /// Adds the specified warning.
        /// </summary>
        /// <param name="eventKind">The event kind of this instance.</param>
        /// <param name="childLog">The child log of this instance.</param>
        /// <param name="logFinder">The filter function to consider. If true then the child log is added otherwise it is not.</param>
        /// <param name="title">The title of this instance.</param>
        /// <param name="description">The description of this instance.</param>
        /// <param name="criticality">The criticality of this instance.</param>
        /// <param name="resultCode">The result code of this instance.</param>
        /// <param name="source">The ExtensionDataContext of this instance.</param>
        /// <param name="date">The date to consider.</param>
        public ILogEvent AddSubLog(
            ILog childLog,
            Predicate<ILog> logFinder = null,
            EventKinds eventKind = EventKinds.Any,
            string title = null,
            EventCriticality criticality = EventCriticality.None,
            string description = null,
            string resultCode = null,
            string source = null,
            DateTime? date = null)
        {
            return AddEvent(
                eventKind,
                title,
                criticality,
                description,
                resultCode,
                source,
                date,
                childLog,
                logFinder);
        }

        /// <summary>
        /// Adds the specified warning.
        /// </summary>
        /// <param name="eventKind">The event kind of this instance.</param>
        /// <param name="filterFinder">The filter function to consider. If true then the child log is added otherwise it is not.</param>
        /// <param name="title">The title of this instance.</param>
        /// <param name="description">The description of this instance.</param>
        /// <param name="criticality">The criticality of this instance.</param>
        /// <param name="resultCode">The result code of this instance.</param>
        /// <param name="source">The ExtensionDataContext of this instance.</param>
        /// <param name="date">The date to consider.</param>
        public ILog AddSubLog(
            Predicate<ILog> filterFinder = null,
            EventKinds eventKind = EventKinds.Any,
            string title = null,
            EventCriticality criticality = EventCriticality.None,
            string description = null,
            string resultCode = null,
            string source = null,
            DateTime? date = null)
        {
            Log childLog = new Log();
            AddEvent(
                eventKind,
                title,
                criticality,
                description,
                resultCode,
                source,
                date,
                childLog,
                filterFinder);
            return childLog;
        }

        ///// <summary>
        ///// Adds a new child log.
        ///// </summary>
        ///// <returns>The added child log.</returns>
        //public ILog AddLog(Task task)
        //{
        //    Log childLog = null;
        //    AddSubLog(childLog = new Log(task)
        //    {
        //        Parent = this,
        //        Loggers= new List<ILogger>(_Loggers)
        //    });
        //    return childLog;
        //}

        /// <summary>
        /// Removes the specified child log.
        /// </summary>
        /// <param name="childLog">The child log to remove.</param>
        /// <param name="isRecursive">Indicate whether the search is recursive.</param>
        public bool RemoveSubLog(ILog childLog, bool isRecursive = true)
        {
            return childLog == null ? false : RemoveSubLog(childLog.Id, isRecursive);
        }

        /// <summary>
        /// Removes the child log with the specified ID.
        /// </summary>
        /// <param name="id">The ID to consider.</param>
        /// <param name="isRecursive">Indicate whether the search is recursive.</param>
        public bool RemoveSubLog(string id, bool isRecursive = true)
        {
            bool isRemoved = false;
            if ((id != null) && (Events != null) && (Events.RemoveAll(p => p.Log != null && id.KeyEquals(id)) == 0))
                foreach (Log subLog in SubLogs)
                {
                    if (subLog.RemoveSubLog(id, isRecursive))
                    {
                        isRemoved = true;
                        break;
                    }
                }

            return isRemoved;
        }

        // Loggers ---------------------------------------------

        /// <summary>
        /// Gets the logger with the specified name.
        /// </summary>
        /// <param name="name">The name of the logger to consider.</param>
        /// <returns>Returns the logger with the specified name.</returns>
        public ILogger GetLogger(string name = null)
        {
            if (name == null)
                return Loggers.Count > 0 ? Loggers[0] : null;
            else
                return Loggers.Find(p => p.KeyEquals(name));
        }

        /// <summary>
        /// Gets the logger with the specified format.
        /// </summary>
        /// <param name="format">The name of the format to consider.</param>
        /// <returns>Returns the logger with the specified format.</returns>
        public ILogger GetLogger(LoggerFormat format)
        {
            return Loggers.Find(p => p.Format == format);
        }

        /// <summary>
        /// Gets the loggers with the specified formats.
        /// </summary>
        /// <param name="formats">The log formats to consider.</param>
        /// <returns>The loggers with the specified formats.</returns>
        public List<ILogger> GetLoggers(params LoggerFormat[] formats)
        {
            return Loggers.Where(p => formats.Contains(p.Format)).ToList();
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors       

        /// <summary>
        /// Returns the title label.
        /// </summary>
        /// <param name="variantName">The variant variant name to consider.</param>
        /// <param name="defaultVariantName">The default variant name to consider.</param>
        public override string GetTitle(string variantName = "*", string defaultVariantName = "*")
        {
            if (Title == null && Task != null)
                return Task.GetTitle(variantName, defaultVariantName);
            else
                return base.GetTitle(variantName, defaultVariantName);
        }

        /// <summary>
        /// Returns the description label.
        /// </summary>
        /// <param name="variantName">The variant variant name to consider.</param>
        /// <param name="defaultVariantName">The default variant name to consider.</param>
        public override string GetDescription(string variantName = "*", string defaultVariantName = "*")
        {
            if (Description == null && Task != null)
                return Task.GetDescription(variantName, defaultVariantName);
            else
                return base.GetDescription(variantName, defaultVariantName);
        }

        // Events --------------------------------

        /// <summary>
        /// Returns the event of this instance with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the event to return.</param>
        /// <param name="isRecursive">Indicate whether the search is recursive.</param>
        /// <returns>The event of this instance with the specified ID.</returns>
        public ILogEvent GetEventWithId(string id, bool isRecursive = false)
        {
            if (id == null || Events == null) return null;

            ILogEvent logEvent = Events.Find(p => p.Id.KeyEquals(id));
            if (isRecursive)
            {
                foreach (Log childLog in SubLogs)
                {
                    logEvent = childLog.GetEventWithId(id, true);
                    if (logEvent != null) return logEvent;
                }
            }

            return logEvent;
        }

        /// <summary>
        /// Gets the specified events of this instance.
        /// </summary>
        /// <param name="isRecursive">Indicate whether the search is recursive.</param>
        /// <param name="kinds">The kinds to consider.</param>
        /// <returns>Returns the specified events of this instance.</returns>
        public List<ILogEvent> GetEvents(
            bool isRecursive = false,
            params EventKinds[] kinds)
        {
            if (Events == null) return new List<ILogEvent>();

            List<ILogEvent> events = Events.ToList<ILogEvent>().GetEvents(kinds);

            if (isRecursive)
            {
                foreach (Log childLog in SubLogs)
                {
                    events.AddRange(childLog.GetEvents(isRecursive, kinds));
                }
            }

            return events;
        }

        /// <summary>
        /// Returns the number of the specified events of this instance.
        /// </summary>
        /// <param name="isRecursive">Indicate whether the search is recursive.</param>
        /// <param name="kinds">The kinds to consider.</param>
        /// <returns>The number of the specified events of this instance.</returns>
        public int GetEventCount(
            bool isRecursive = false,
            params EventKinds[] kinds)
        {
            if (Events == null) return 0;

            int i = Events.Count(p => kinds.Contains(p.Kind));

            if (isRecursive)
                foreach (ILog childLog in SubLogs)
                    i += GetEventCount(isRecursive, kinds);

            return i;
        }

        /// <summary>
        /// Gets the warnings, errors or exceptions of this instance.
        /// </summary>
        /// <param name="isRecursive">Indicate whether the search is recursive.</param>
        /// <param name="kinds">The kinds to consider.</param>
        /// <returns>True if this instance has the specified events. False otherwise.</returns>
        public EventKinds GetMaxEventKind(
            bool isRecursive = true,
            params EventKinds[] kinds)
        {
            return GetEvents(isRecursive, kinds).Select(p => p.Kind).ToList().Max();
        }

        // Has events -----------------------------------

        /// <summary>
        /// Indicates whether this instance has the specified events.
        /// </summary>
        /// <param name="isRecursive">Indicates whether the search must be recursive.</param>
        /// <param name="kinds">The event kinds to consider.</param>
        /// <returns>True if this instance has the specified events. False otherwise.</returns>
        public bool HasEvent(
            bool isRecursive = true,
            params EventKinds[] kinds)
        {
            if (Events == null) return false;

            bool aHasEvents = Events.ToList<ILogEvent>().Has(kinds);

            if (!aHasEvents && isRecursive)
                foreach (Log childLog in SubLogs)
                    if (aHasEvents = childLog.HasEvent(isRecursive, kinds))
                        return true;

            return aHasEvents;
        }

        /// <summary>
        /// Indicates whether this instance has the specified events.
        /// </summary>
        /// <param name="kinds">The event kinds to consider.</param>
        /// <returns>True if this instance has the specified events. False otherwise.</returns>
        public bool HasEvent(
            params EventKinds[] kinds)
        {
            return HasEvent(false, kinds);
        }

        /// <summary>
        /// Checks this instance has any warnings.
        /// </summary>
        /// <param name="kinds">The event kinds to consider.</param>
        /// <param name="isRecursive">Indicates whether the search must be recursive.</param>
        /// <returns>True if this instance has the specified events. False otherwise.</returns>
        public bool HasWarnings(
            bool isRecursive = true,
            params EventKinds[] kinds)
        {
            return HasEvent(isRecursive, EventKinds.Warning);
        }

        /// <summary>
        /// Checks this instance has any errors.
        /// </summary>
        /// <param name="isRecursive">Indicates whether the search must be recursive.</param>
        /// <returns>True if this instance has the specified events. False otherwise.</returns>
        public bool HasErrors(bool isRecursive = true)
        {
            return HasEvent(isRecursive, EventKinds.Error);
        }

        /// <summary>
        /// Checks this instance has any exceptions.
        /// </summary>
        /// <param name="isRecursive">Indicates whether the search must be recursive.</param>
        /// <returns>True if this instance has the specified events. False otherwise.</returns>
        public bool HasExceptions(bool isRecursive = true)
        {
            return HasEvent(isRecursive, EventKinds.Exception);
        }

        /// <summary>
        /// Checks this instance has any messages.
        /// </summary>
        /// <param name="isRecursive">Indicates whether the search must be recursive.</param>
        /// <returns>True if this instance has the specified events. False otherwise.</returns>
        public bool HasMessages(bool isRecursive = true)
        {
            return HasEvent(isRecursive, EventKinds.Message);
        }

        /// <summary>
        /// Checks this instance has any errors or exceptions.
        /// </summary>
        /// <param name="isRecursive">Indicates whether the search must be recursive.</param>
        /// <returns>True if this instance has the specified events. False otherwise.</returns>
        public bool HasErrorsOrExceptions(bool isRecursive = true)
        {
            return HasEvent(isRecursive, EventKinds.Error, EventKinds.Exception);
        }

        /// <summary>
        /// Checks this instance has any warnings, errors or exceptions.
        /// </summary>
        /// <param name="isRecursive">Indicates whether the search must be recursive.</param>
        /// <returns>True if this instance has the specified events. False otherwise.</returns>
        public bool HasErrorsOrExceptionsOrWarnings(bool isRecursive = true)
        {
            return HasEvent(isRecursive, EventKinds.Warning, EventKinds.Error, EventKinds.Exception);
        }

        // Tree --------------------------------

        /// <summary>
        /// Returns the log root.
        /// </summary>
        /// <returns>The log root.</returns>
        public ILog GetRoot()
        {
            return Parent == null ? this : Parent.GetRoot();
        }

        /// <summary>
        /// Returns the sub log with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the log to return.</param>
        /// <param name="isRecursive">Indicates whether the search must be recursive.</param>
        /// <returns>The child with the specified ID.</returns>
        public ILog GetSubLogWithId(string id, bool isRecursive = false)
        {
            if (Id.Equals(id, StringComparison.OrdinalIgnoreCase))
                return this;
            ILog log = null;
            if (isRecursive)
            {
                foreach (Log currentChildLog in SubLogs)
                {
                    log = currentChildLog.GetSubLogWithId(id);
                    if (log != null) return log;
                }
            }

            return null;
        }

        /// <summary>
        /// Checks this instance has child log.
        /// </summary>
        /// <returns>True if this instance has child log. False otherwise.</returns>
        public bool HasSubLog()
        {
            return SubLogs.Count > 0;
        }

        /// <summary>
        /// Builds the tree of this instance.
        /// </summary>
        public void BuildTree()
        {
                foreach (ILogEvent aEvent in Events)
                {
                    aEvent.Parent = this;
                    if (aEvent.Log != null)
                    {
                        aEvent.Log.Parent = this;
                        aEvent.Log.BuildTree();
                    }
                }
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Sets the log file location.
        /// </summary>
        /// <param name="newFolderPath">The new folder path to consider.</param>
        /// <param name="isFileToBeMoved">Indicates whether the file must be moved.</param>
        /// <param name="newFileName">The new file name to consider.</param>
        public void SetFilePath(string newFolderPath, bool isFileToBeMoved, string newFileName = null)
        {
            foreach (ILogger logger in Loggers)
                logger.SetFilePath(newFolderPath, isFileToBeMoved, newFileName);
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            Execution = Execution ?? new ProcessExecution();
            Execution.Start();
        }

        /// <summary>
        /// Ends this instance specifying the status.
        /// </summary>
        /// <param name="status">The new status to consider.</param>
        public void End(ProcessExecutionStatus status = ProcessExecutionStatus.Completed)
        {
            Execution = Execution ?? new ProcessExecution();
            Execution.End(status);
        }

        #endregion

        // ------------------------------------------
        // SERIALIZATION / UNSERIALIZATION
        // ------------------------------------------

        #region Serialization_Unserialization

        /// <summary>
        /// Updates information for storage.
        /// </summary>
        /// <param name="log">The log to update.</param>
        public override void UpdateStorageInfo(ILog log = null)
        {
            base.UpdateStorageInfo(log);

            Detail?.UpdateStorageInfo(log);
            Execution?.UpdateStorageInfo(log);

            if (Events != null)
            {
                foreach (Event currentEvent in Events)
                {
                    currentEvent.UpdateStorageInfo(log);
                }
            }
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The set of script variables to consider.</param>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IAppScope appScope = null, IScriptVariableSet scriptVariableSet = null, ILog log = null)
        {
            base.UpdateRuntimeInfo(appScope, scriptVariableSet, log);

            Detail?.UpdateRuntimeInfo(appScope, scriptVariableSet, log);

            Execution?.UpdateRuntimeInfo(appScope, scriptVariableSet, log);

            if (Events != null)
            {
                foreach (Event currentEvent in Events)
                {
                    currentEvent?.UpdateRuntimeInfo(appScope, scriptVariableSet, log);
                }
            }
        }

        // Unserialization ---------------------------------

        /// <summary>
        /// Instantiates a new instance of Log class from a xml file.
        /// </summary>
        /// <param name="filePath">The path of the Xml file to load.</param>
        /// <param name="isCheckXml">Indicates whether the file must be checked.</param>
        /// <param name="loadLog">The output log of the load task.</param>
        /// <param name="mustFileExist">Indicates whether the file must exist.</param>
        /// <returns>The load log.</returns>
        public static ILog Load<T>(
            string filePath,
            bool isCheckXml,
            ILog loadLog,
            bool mustFileExist = true) where T : ILogger, new()
        {
            ILog log = (new T()).LoadLog(filePath, loadLog, mustFileExist);
            log?.BuildTree();
            return log;
        }

        /// <summary>
        /// Instantiates a new instance of Log class from a xml string.
        /// </summary>
        /// <param name="xmlString">The Xml string to load.</param>
        /// <param name="isCheckXml">Indicates whether the file must be checked.</param>
        /// <param name="loadLog">The output log of the load task.</param>
        /// <returns>The log defined in the Xml file.</returns>
        public static ILog LoadFromString<T>(
            string xmlString,
            bool isCheckXml,
            ILog loadLog = null) where T : ILogger, new()
        {
            ILog log = (new T()).LoadLogFromString(xmlString, loadLog);
            log?.BuildTree();
            return log;
        }

        // Serialization ---------------------------------

        /// <summary>
        /// Saves this instance.
        /// </summary>
        /// <returns>Returns the saving log.</returns>
        public bool Save()
        {
            bool abool = true;

            if (Loggers.Count == 0)
                return false;
            else
                foreach (Logger logger in Loggers)
                    abool &= logger.Save(this, logger.Filepath);

            return abool;
        }

        /// <summary>
        /// Saves this instance in the specified log file.
        /// </summary>
        /// <param name="logFilePath">The path of the log file to save.</param>
        /// <returns>Returns the saving log.</returns>
        public bool Save<T>(string logFilePath)
            where T : ILogger, new()
        {
            return (new T()).Save(this, logFilePath);
        }

        /// <summary>
        /// Gets the xml string of this instance.
        /// </summary>
        /// <returns>The Xml string of this instance.</returns>
        public string ToString<T>()
             where T : ILogger, new()
        {
            return (new T()).ToString(this);
        }

        #endregion
    }
}