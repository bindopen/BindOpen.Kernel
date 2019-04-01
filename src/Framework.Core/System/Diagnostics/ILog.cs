using System;
using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Configuration.Tasks;
using BindOpen.Framework.Core.System.Diagnostics.Events;
using BindOpen.Framework.Core.System.Diagnostics.Loggers;
using BindOpen.Framework.Core.System.Processing;

namespace BindOpen.Framework.Core.System.Diagnostics
{
    public interface ILog : IDescribedDataItem
    {
        IEvent this[int index] { get; }
        IEvent this[string id] { get; }

        List<ILogEvent> Checkpoints { get; }
        IDataElementSet Detail { get; set; }
        List<ILogEvent> Errors { get; }
        ILogEvent Event { get; set; }
        List<ILogEvent> Events { get; set; }
        List<ILogEvent> Exceptions { get; }
        IProcessExecution Execution { get; set; }
        int Level { get; }
        List<ILogger> Loggers { get; set; }
        List<ILogEvent> Messages { get; }
        ILog Parent { get; set; }
        ILog Root { get; }
        List<ILog> SubLogs { get; }
        ITaskConfiguration Task { get; set; }
        List<ILogEvent> Warnings { get; }

        ILogEvent AddCheckpoint(string title, EventCriticality criticality = EventCriticality.None, string description = null, string resultCode = null, string source = null, DateTime? date = null, ILog childILog = null, Predicate<ILog> logFinder = null);
        ILogEvent AddError(string title, EventCriticality criticality = EventCriticality.None, string description = null, string resultCode = null, string aSource = null, DateTime? date = null, ILog childILog = null, Predicate<ILog> logFinder = null);
        ILogEvent AddEvent(EventKind kind, string title, EventCriticality criticality = EventCriticality.None, string description = null, string resultCode = null, string source = null, DateTime? date = null, ILog childILog = null, Predicate<ILog> logFinder = null);
        bool AddEvent(ILogEvent logEvent, ILog childILog = null, Predicate<ILog> logFinder = null);
        List<ILogEvent> AddEvents(ILog log, Predicate<ILog> logFinder = null, params EventKind[] kinds);
        List<ILogEvent> AddEvents(ILog log, params EventKind[] kinds);
        ILogEvent AddException(Exception exception, EventCriticality criticality = EventCriticality.None, string resultCode = null, string source = null, ILog childILog = null, Predicate<ILog> logFinder = null);
        ILogEvent AddException(string title, EventCriticality criticality = EventCriticality.None, string description = null, string resultCode = null, string source = null, DateTime? date = null, ILog childILog = null, Predicate<ILog> logFinder = null);
        void AddLoggers(params ILogger[] loggers);
        ILogEvent AddMessage(string title, EventCriticality criticality = EventCriticality.None, string description = null, string resultCode = null, string source = null, DateTime? date = null, ILog childILog = null, Predicate<ILog> logFinder = null);
        ILog AddSubLog(Predicate<ILog> filterFinder = null, EventKind eventKind = EventKind.Any, string title = null, EventCriticality criticality = EventCriticality.None, string description = null, string resultCode = null, string aSource = null, DateTime? date = null);
        ILogEvent AddSubLog(ILog childLog, Predicate<ILog> logFinder = null, EventKind eventKind = EventKind.Any, string title = null, EventCriticality criticality = EventCriticality.None, string description = null, string resultCode = null, string aSource = null, DateTime? date = null);
        ILogEvent AddWarning(string title, EventCriticality criticality = EventCriticality.None, string description = null, string resultCode = null, string aSource = null, DateTime? date = null, ILog childILog = null, Predicate<ILog> logFinder = null);
        List<ILogEvent> Append(ILog log);
        List<ILogEvent> Append(ILog log, Predicate<ILog> logFinder = null);
        void ClearEvents(bool isRecursive = true, params EventKind[] kinds);
        void End(ProcessExecutionStatus status = ProcessExecutionStatus.Completed);
        string GetDescription(string variantName = "*", string defaultVariantName = "*");
        int GetEventCount(bool isRecursive = false, params EventKind[] kinds);
        List<ILogEvent> GetEvents(bool isRecursive = false, params EventKind[] kinds);
        IEvent GetEventWithId(string id, bool isRecursive = false);
        ILogger GetLogger(string name = null);
        ILogger GetLogger(LogFormat format);
        List<ILogger> GetLoggers(params LogFormat[] formats);
        EventKind GetMaxEventKind(bool isRecursive = true, params EventKind[] kinds);
        ILog GetSubLogWithId(string id, bool isRecursive = false);
        string GetTitleText(string variantName = "*", string defaultVariantName = "*");
        bool HasErrors(bool isRecursive = true);
        bool HasErrorsOrExceptions(bool isRecursive = true);
        bool HasErrorsOrExceptionsOrWarnings(bool isRecursive = true);
        bool HasEvent(bool isRecursive = true, params EventKind[] kinds);
        bool HasEvent(params EventKind[] kinds);
        bool HasExceptions(bool isRecursive = true);
        bool HasMessages(bool isRecursive = true);
        bool HasSubLog();
        bool HasWarnings(bool isRecursive = true, params EventKind[] kinds);
        bool RemoveSubLog(ILog childLog, bool isRecursive = true);
        bool RemoveSubLog(string id, bool isRecursive = true);
        void Sanitize();
        bool Save();
        bool Save<T>(string logFilePath) where T : ILogger, new();
        void SetFilePath(string newFolderPath, bool isFileToBeMoved, string newFileName = null);
        void Start();
        string ToString<T>() where T : ILogger, new();
        void WriteLog(ILog childLog, LoggerMode mode = LoggerMode.Auto);
        void WriteLog(ILogEvent logEvent, LoggerMode mode = LoggerMode.Auto);
        void WriteLog(string elementName, object elementValue, LoggerMode mode = LoggerMode.Auto);
        void WriteLog(ITaskConfiguration task, LoggerMode mode = LoggerMode.Auto);
        void BuildTree();
        ILog GetRoot();
    }
}