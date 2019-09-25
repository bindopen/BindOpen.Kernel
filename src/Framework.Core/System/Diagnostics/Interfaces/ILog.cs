using System;
using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Items.Tasks;
using BindOpen.Framework.Core.System.Diagnostics.Events;
using BindOpen.Framework.Core.System.Diagnostics.Loggers;
using BindOpen.Framework.Core.System.Processing;

namespace BindOpen.Framework.Core.System.Diagnostics
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILog : IDescribedDataItem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        LogEvent this[int index] { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        LogEvent this[string id] { get; }

        /// <summary>
        /// 
        /// </summary>
        DataElementSet Detail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<LogEvent> Events { get; set; }

        /// <summary>
        /// 
        /// </summary>
        ProcessExecution Execution { get; set; }

        /// <summary>
        /// 
        /// </summary>
        int Level { get; }

        /// <summary>
        /// 
        /// </summary>
        TaskConfiguration Task { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<ILogEvent> Checkpoints { get; }

        /// <summary>
        /// 
        /// </summary>
        List<ILogEvent> Errors { get; }

        /// <summary>
        /// 
        /// </summary>
        List<ILogEvent> Exceptions { get; }

        /// <summary>
        /// 
        /// </summary>
        List<ILogEvent> Messages { get; }

        /// <summary>
        /// 
        /// </summary>
        List<ILogEvent> Warnings { get; }

        /// <summary>
        /// 
        /// </summary>
        List<ILog> SubLogs { get; }

        /// <summary>
        /// 
        /// </summary>
        List<ILogger> Loggers { get; set; }

        /// <summary>
        /// 
        /// </summary>
        ILog Parent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        ILog Root { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="criticality"></param>
        /// <param name="description"></param>
        /// <param name="resultCode"></param>
        /// <param name="source"></param>
        /// <param name="date"></param>
        /// <param name="childLog"></param>
        /// <param name="logFinder"></param>
        /// <returns></returns>
        ILogEvent AddCheckpoint(string title, EventCriticality criticality = EventCriticality.None, string description = null, string resultCode = null, string source = null, DateTime? date = null, ILog childLog = null, Predicate<ILog> logFinder = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="criticality"></param>
        /// <param name="description"></param>
        /// <param name="resultCode"></param>
        /// <param name="source"></param>
        /// <param name="date"></param>
        /// <param name="childLog"></param>
        /// <param name="logFinder"></param>
        /// <returns></returns>
        ILogEvent AddError(string title, EventCriticality criticality = EventCriticality.None, string description = null, string resultCode = null, string source = null, DateTime? date = null, ILog childLog = null, Predicate<ILog> logFinder = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kind"></param>
        /// <param name="title"></param>
        /// <param name="criticality"></param>
        /// <param name="description"></param>
        /// <param name="resultCode"></param>
        /// <param name="source"></param>
        /// <param name="date"></param>
        /// <param name="childLog"></param>
        /// <param name="logFinder"></param>
        /// <returns></returns>
        ILogEvent AddEvent(EventKinds kind, string title, EventCriticality criticality = EventCriticality.None, string description = null, string resultCode = null, string source = null, DateTime? date = null, ILog childLog = null, Predicate<ILog> logFinder = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logEvent"></param>
        /// <param name="childLog"></param>
        /// <param name="logFinder"></param>
        /// <returns></returns>
        bool AddEvent(ILogEvent logEvent, ILog childLog = null, Predicate<ILog> logFinder = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        /// <param name="logFinder"></param>
        /// <param name="kinds"></param>
        /// <returns></returns>
        List<ILogEvent> AddEvents(ILog log, Predicate<ILog> logFinder = null, params EventKinds[] kinds);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        /// <param name="kinds"></param>
        /// <returns></returns>
        List<ILogEvent> AddEvents(ILog log, params EventKinds[] kinds);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="criticality"></param>
        /// <param name="resultCode"></param>
        /// <param name="source"></param>
        /// <param name="childLog"></param>
        /// <param name="logFinder"></param>
        /// <returns></returns>
        ILogEvent AddException(Exception exception, EventCriticality criticality = EventCriticality.None, string resultCode = null, string source = null, ILog childLog = null, Predicate<ILog> logFinder = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="criticality"></param>
        /// <param name="description"></param>
        /// <param name="resultCode"></param>
        /// <param name="source"></param>
        /// <param name="date"></param>
        /// <param name="childLog"></param>
        /// <param name="logFinder"></param>
        /// <returns></returns>
        ILogEvent AddException(string title, EventCriticality criticality = EventCriticality.None, string description = null, string resultCode = null, string source = null, DateTime? date = null, ILog childLog = null, Predicate<ILog> logFinder = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loggers"></param>
        void AddLoggers(params ILogger[] loggers);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="criticality"></param>
        /// <param name="description"></param>
        /// <param name="resultCode"></param>
        /// <param name="source"></param>
        /// <param name="date"></param>
        /// <param name="childLog"></param>
        /// <param name="logFinder"></param>
        /// <returns></returns>
        ILogEvent AddMessage(string title, EventCriticality criticality = EventCriticality.None, string description = null, string resultCode = null, string source = null, DateTime? date = null, ILog childLog = null, Predicate<ILog> logFinder = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterFinder"></param>
        /// <param name="eventKind"></param>
        /// <param name="title"></param>
        /// <param name="criticality"></param>
        /// <param name="description"></param>
        /// <param name="resultCode"></param>
        /// <param name="source"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        ILog AddSubLog(Predicate<ILog> filterFinder = null, EventKinds eventKind = EventKinds.Any, string title = null, EventCriticality criticality = EventCriticality.None, string description = null, string resultCode = null, string source = null, DateTime? date = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="childLog"></param>
        /// <param name="logFinder"></param>
        /// <param name="eventKind"></param>
        /// <param name="title"></param>
        /// <param name="criticality"></param>
        /// <param name="description"></param>
        /// <param name="resultCode"></param>
        /// <param name="source"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        ILogEvent AddSubLog(ILog childLog, Predicate<ILog> logFinder = null, EventKinds eventKind = EventKinds.Any, string title = null, EventCriticality criticality = EventCriticality.None, string description = null, string resultCode = null, string source = null, DateTime? date = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="criticality"></param>
        /// <param name="description"></param>
        /// <param name="resultCode"></param>
        /// <param name="source"></param>
        /// <param name="date"></param>
        /// <param name="childLog"></param>
        /// <param name="logFinder"></param>
        /// <returns></returns>
        ILogEvent AddWarning(string title, EventCriticality criticality = EventCriticality.None, string description = null, string resultCode = null, string source = null, DateTime? date = null, ILog childLog = null, Predicate<ILog> logFinder = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        List<ILogEvent> Append(ILog log);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        /// <param name="logFinder"></param>
        /// <returns></returns>
        List<ILogEvent> Append(ILog log, Predicate<ILog> logFinder = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isRecursive"></param>
        /// <param name="kinds"></param>
        void ClearEvents(bool isRecursive = true, params EventKinds[] kinds);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        void End(ProcessExecutionStatus status = ProcessExecutionStatus.Completed);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isRecursive"></param>
        /// <param name="kinds"></param>
        /// <returns></returns>
        int GetEventCount(bool isRecursive = false, params EventKinds[] kinds);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isRecursive"></param>
        /// <param name="kinds"></param>
        /// <returns></returns>
        List<ILogEvent> GetEvents(bool isRecursive = false, params EventKinds[] kinds);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isRecursive"></param>
        /// <returns></returns>
        ILogEvent GetEventWithId(string id, bool isRecursive = false);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        ILogger GetLogger(string name = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        ILogger GetLogger(LoggerFormat format);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formats"></param>
        /// <returns></returns>
        List<ILogger> GetLoggers(params LoggerFormat[] formats);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isRecursive"></param>
        /// <param name="kinds"></param>
        /// <returns></returns>
        EventKinds GetMaxEventKind(bool isRecursive = true, params EventKinds[] kinds);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isRecursive"></param>
        /// <returns></returns>
        ILog GetSubLogWithId(string id, bool isRecursive = false);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="variantName"></param>
        /// <param name="defaultVariantName"></param>
        /// <returns></returns>
        new string GetTitle(string variantName = "*", string defaultVariantName = "*");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isRecursive"></param>
        /// <returns></returns>
        bool HasErrors(bool isRecursive = true);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isRecursive"></param>
        /// <returns></returns>
        bool HasErrorsOrExceptions(bool isRecursive = true);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isRecursive"></param>
        /// <returns></returns>
        bool HasErrorsOrExceptionsOrWarnings(bool isRecursive = true);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isRecursive"></param>
        /// <param name="kinds"></param>
        /// <returns></returns>
        bool HasEvent(bool isRecursive = true, params EventKinds[] kinds);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kinds"></param>
        /// <returns></returns>
        bool HasEvent(params EventKinds[] kinds);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isRecursive"></param>
        /// <returns></returns>
        bool HasExceptions(bool isRecursive = true);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isRecursive"></param>
        /// <returns></returns>
        bool HasMessages(bool isRecursive = true);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool HasSubLog();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isRecursive"></param>
        /// <param name="kinds"></param>
        /// <returns></returns>
        bool HasWarnings(bool isRecursive = true, params EventKinds[] kinds);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="childLog"></param>
        /// <param name="isRecursive"></param>
        /// <returns></returns>
        bool RemoveSubLog(ILog childLog, bool isRecursive = true);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isRecursive"></param>
        /// <returns></returns>
        bool RemoveSubLog(string id, bool isRecursive = true);

        /// <summary>
        /// 
        /// </summary>
        void Sanitize();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool Save();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="logFilePath"></param>
        /// <returns></returns>
        bool Save<T>(string logFilePath) where T : ILogger, new();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newFolderPath"></param>
        /// <param name="isFileToBeMoved"></param>
        /// <param name="newFileName"></param>
        void SetFilePath(string newFolderPath, bool isFileToBeMoved, string newFileName = null);

        /// <summary>
        /// 
        /// </summary>
        void Start();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        string ToString<T>() where T : ILogger, new();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="childLog"></param>
        /// <param name="mode"></param>
        void WriteLog(ILog childLog, LoggerMode mode = LoggerMode.Auto);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logEvent"></param>
        /// <param name="mode"></param>
        void WriteLog(ILogEvent logEvent, LoggerMode mode = LoggerMode.Auto);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementName"></param>
        /// <param name="elementValue"></param>
        /// <param name="mode"></param>
        void WriteLog(string elementName, object elementValue, LoggerMode mode = LoggerMode.Auto);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        /// <param name="mode"></param>
        void WriteLog(ITaskConfiguration task, LoggerMode mode = LoggerMode.Auto);

        /// <summary>
        /// 
        /// </summary>
        void BuildTree();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ILog GetRoot();
    }
}