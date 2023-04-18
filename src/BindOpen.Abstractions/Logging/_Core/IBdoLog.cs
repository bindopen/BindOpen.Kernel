using BindOpen.Data;
using BindOpen.Data.Meta;
using System;
using System.Collections.Generic;

namespace BindOpen.Logging
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoLog :
        IIdentified, INamed,
        IDisplayNamed, IDescribed,
        IBdoDetailed, IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        int Level { get; }

        /// <summary>
        /// 
        /// </summary>
        IBdoConfiguration TaskConfig { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="task"></param>
        /// <returns></returns>
        IBdoLog WithTask(IBdoConfiguration task);

        // Logs

        /// <summary>
        /// 
        /// </summary>
        IList<IBdoLog> SubLogs { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool HasSubLog();

        /// <summary>
        /// 
        /// </summary>
        IBdoLog Parent { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="parent"></param>
        /// <returns></returns>
        IBdoLog WithParent(IBdoLog parent);

        /// <summary>
        /// 
        /// </summary>
        IBdoLog Root { get; }

        // Logs

        /// <summary>
        /// 
        /// </summary>
        /// <param key="filterFinder"></param>
        /// <param key="eventKind"></param>
        /// <param key="title"></param>
        /// <param key="criticality"></param>
        /// <param key="description"></param>
        /// <param key="resultCode"></param>
        /// <param key="source"></param>
        /// <param key="date"></param>
        /// <returns></returns>
        IBdoLog InsertSubLog(Predicate<IBdoLog> filterFinder = null, EventKinds eventKind = EventKinds.Any, string title = null, Criticalities criticality = Criticalities.None, string description = null, string resultCode = null, string source = null, DateTime? date = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="filterFinder"></param>
        /// <param key="eventKind"></param>
        /// <param key="title"></param>
        /// <param key="criticality"></param>
        /// <param key="description"></param>
        /// <param key="resultCode"></param>
        /// <param key="source"></param>
        /// <param key="date"></param>
        /// <returns></returns>
        IBdoLog AddSubLog(Predicate<IBdoLog> filterFinder = null, EventKinds eventKind = EventKinds.Any, string title = null, Criticalities criticality = Criticalities.None, string description = null, string resultCode = null, string source = null, DateTime? date = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="childLog"></param>
        /// <param key="logFinder"></param>
        /// <param key="eventKind"></param>
        /// <param key="title"></param>
        /// <param key="criticality"></param>
        /// <param key="description"></param>
        /// <param key="resultCode"></param>
        /// <param key="source"></param>
        /// <param key="date"></param>
        /// <returns></returns>
        IBdoLog AddSubLog(IBdoLog childLog, Predicate<IBdoLog> logFinder = null, EventKinds eventKind = EventKinds.Any, string title = null, Criticalities criticality = Criticalities.None, string description = null, string resultCode = null, string source = null, DateTime? date = null);

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
        IBdoLog AddCheckpoint(string title, Criticalities criticality = Criticalities.None, string description = null, string resultCode = null, string source = null, DateTime? date = null, IBdoLog childLog = null, Predicate<IBdoLog> logFinder = null);

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
        IBdoLog AddError(string title, Criticalities criticality = Criticalities.None, string description = null, string resultCode = null, string source = null, DateTime? date = null, IBdoLog childLog = null, Predicate<IBdoLog> logFinder = null);

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
        IBdoLog AddException(Exception exception, Criticalities criticality = Criticalities.None, string resultCode = null, string source = null, IBdoLog childLog = null, Predicate<IBdoLog> logFinder = null);

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
        IBdoLog AddException(string title, Criticalities criticality = Criticalities.None, string description = null, string resultCode = null, string source = null, DateTime? date = null, IBdoLog childLog = null, Predicate<IBdoLog> logFinder = null);

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
        IBdoLog AddMessage(string title, Criticalities criticality = Criticalities.None, string description = null, string resultCode = null, string source = null, DateTime? date = null, IBdoLog childLog = null, Predicate<IBdoLog> logFinder = null);

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
        IBdoLog AddWarning(string title, Criticalities criticality = Criticalities.None, string description = null, string resultCode = null, string source = null, DateTime? date = null, IBdoLog childLog = null, Predicate<IBdoLog> logFinder = null);

        // Events

        /// <summary>
        /// 
        /// </summary>
        /// <param key="kind"></param>
        /// <param key="title"></param>
        /// <param key="criticality"></param>
        /// <param key="description"></param>
        /// <param key="resultCode"></param>
        /// <param key="source"></param>
        /// <param key="date"></param>
        /// <param key="childLog"></param>
        /// <param key="logFinder"></param>
        /// <returns></returns>
        IBdoLog AddEvent(EventKinds kind, string title, Criticalities criticality = Criticalities.None, string description = null, string resultCode = null, string source = null, DateTime? date = null, IBdoLog childLog = null, Predicate<IBdoLog> logFinder = null);

        /// <summary>
        /// Adds the events of this instance to the specified log.
        /// </summary>
        /// <param key="log">The log to consider.</param>
        /// <param key="logFinder"></param>
        /// <param key="kinds"></param>
        /// <returns></returns>
        IBdoLog AddEventsTo(IBdoLog log, Predicate<IBdoLog> logFinder = null, params EventKinds[] kinds);

        /// <summary>
        /// Adds the events of this instance to the specified log.
        /// </summary>
        /// <param key="log">The log to consider.</param>
        /// <param key="kinds"></param>
        /// <returns></returns>
        IBdoLog AddEventsTo(IBdoLog log, params EventKinds[] kinds);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="kinds"></param>
        /// <returns></returns>
        bool HasEvent(params EventKinds[] kinds);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="isRecursive"></param>
        /// <param key="kinds"></param>
        /// <returns></returns>
        bool HasEvent(bool isRecursive = true, params EventKinds[] kinds);

        /// <summary>
        /// Adds the specified events to this instance.
        /// </summary>
        /// <param key="log">The log to consider.</param>
        /// <param key="logFinder"></param>
        /// <param key="kinds"></param>
        /// <returns></returns>
        IBdoLog AddEvents(IBdoLog log, Predicate<IBdoLog> logFinder = null, params EventKinds[] kinds);

        /// <summary>
        /// Adds the specified events to this instance.
        /// </summary>
        /// <param key="log">The log to consider.</param>
        /// <param key="kinds"></param>
        /// <returns></returns>
        IBdoLog AddEvents(IBdoLog log, params EventKinds[] kinds);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="isRecursive"></param>
        /// <param key="kinds"></param>
        IBdoLog ClearEvents(bool isRecursive = true, params EventKinds[] kinds);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="isRecursive"></param>
        /// <param key="kinds"></param>
        /// <returns></returns>
        int GetEventCount(bool isRecursive = false, params EventKinds[] kinds);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="isRecursive"></param>
        /// <param key="kinds"></param>
        /// <returns></returns>
        EventKinds GetMaxEventKind(bool isRecursive = true, params EventKinds[] kinds);

        // Logs

        /// <summary>
        /// 
        /// </summary>
        /// <param key="id"></param>
        /// <param key="isRecursive"></param>
        /// <returns></returns>
        IBdoLog GetSubLogWithId(string id, bool isRecursive = false);

        /// <summary>
        /// Creates a new instance of IBdoLog.
        /// </summary>
        /// <returns></returns>
        IBdoLog NewLog();

        /// <summary>
        /// 
        /// </summary>
        /// <param key="childLog"></param>
        /// <param key="isRecursive"></param>
        /// <returns></returns>
        IBdoLog RemoveSubLog(IBdoLog childLog, bool isRecursive = true);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="id"></param>
        /// <param key="isRecursive"></param>
        /// <returns></returns>
        IBdoLog RemoveSubLog(string id, bool isRecursive = true);

        // Clone

        /// <summary>
        /// Clones this instance considering the parent log.
        /// </summary>
        /// <param key="parent"></param>
        IBdoLog Clone(IBdoLog parent, params string[] areas);

        /// <summary>
        /// Clones this instance considering the parent log.
        /// </summary>
        /// <param key="parent"></param>
        T Clone<T>(IBdoLog parent, params string[] areas) where T : class;

        // Execution

        /// <summary>
        /// 
        /// </summary>
        IProcessExecution Execution { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="execution"></param>
        /// <returns></returns>
        IBdoLog WithExecution(IProcessExecution execution);

        /// <summary>
        /// 
        /// </summary>
        IBdoLog Sanitize();

        /// <summary>
        /// 
        /// </summary>
        IBdoLog Start();

        /// <summary>
        /// 
        /// </summary>
        /// <param key="status"></param>
        IBdoLog End(ProcessExecutionStatus status = ProcessExecutionStatus.Completed);
    }
}