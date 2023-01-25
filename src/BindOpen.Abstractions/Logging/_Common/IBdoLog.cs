using BindOpen.Data;
using BindOpen.Extensions.Processing;
using System;
using System.Collections.Generic;

namespace BindOpen.Logging
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoLog :
        ITIdentifiedPoco<IBdoLog>,
        ITNamedPoco<IBdoLog>,
        IDisplayNamed, IDescribed,
        ITDetailedPoco<IBdoLog>,
        IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="displayName"></param>
        /// <returns></returns>
        IBdoLog WithDisplayName(string displayName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        IBdoLog WithDescription(string description);

        /// <summary>
        /// 
        /// </summary>
        int Level { get; }

        /// <summary>
        /// 
        /// </summary>
        IBdoTaskConfiguration Task { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        IBdoLog WithTask(IBdoTaskConfiguration task);

        // Logs

        /// <summary>
        /// 
        /// </summary>
        List<IBdoLog> SubLogs { get; }

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
        /// <param name="parent"></param>
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
        /// <param name="filterFinder"></param>
        /// <param name="eventKind"></param>
        /// <param name="title"></param>
        /// <param name="criticality"></param>
        /// <param name="description"></param>
        /// <param name="resultCode"></param>
        /// <param name="source"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        IBdoLog InsertSubLog(Predicate<IBdoLog> filterFinder = null, EventKinds eventKind = EventKinds.Any, string title = null, Criticalities criticality = Criticalities.None, string description = null, string resultCode = null, string source = null, DateTime? date = null);

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
        IBdoLog AddSubLog(Predicate<IBdoLog> filterFinder = null, EventKinds eventKind = EventKinds.Any, string title = null, Criticalities criticality = Criticalities.None, string description = null, string resultCode = null, string source = null, DateTime? date = null);

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
        IBdoLog AddSubLog(IBdoLog childLog, Predicate<IBdoLog> logFinder = null, EventKinds eventKind = EventKinds.Any, string title = null, Criticalities criticality = Criticalities.None, string description = null, string resultCode = null, string source = null, DateTime? date = null);

        // Errors, Warnings, Exceptions, Checkpoint, Message

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
        IBdoLog AddCheckpoint(string title, Criticalities criticality = Criticalities.None, string description = null, string resultCode = null, string source = null, DateTime? date = null, IBdoLog childLog = null, Predicate<IBdoLog> logFinder = null);

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
        IBdoLog AddError(string title, Criticalities criticality = Criticalities.None, string description = null, string resultCode = null, string source = null, DateTime? date = null, IBdoLog childLog = null, Predicate<IBdoLog> logFinder = null);

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
        IBdoLog AddException(Exception exception, Criticalities criticality = Criticalities.None, string resultCode = null, string source = null, IBdoLog childLog = null, Predicate<IBdoLog> logFinder = null);

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
        IBdoLog AddException(string title, Criticalities criticality = Criticalities.None, string description = null, string resultCode = null, string source = null, DateTime? date = null, IBdoLog childLog = null, Predicate<IBdoLog> logFinder = null);

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
        IBdoLog AddMessage(string title, Criticalities criticality = Criticalities.None, string description = null, string resultCode = null, string source = null, DateTime? date = null, IBdoLog childLog = null, Predicate<IBdoLog> logFinder = null);

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
        IBdoLog AddWarning(string title, Criticalities criticality = Criticalities.None, string description = null, string resultCode = null, string source = null, DateTime? date = null, IBdoLog childLog = null, Predicate<IBdoLog> logFinder = null);

        // Events

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
        IBdoLog AddEvent(EventKinds kind, string title, Criticalities criticality = Criticalities.None, string description = null, string resultCode = null, string source = null, DateTime? date = null, IBdoLog childLog = null, Predicate<IBdoLog> logFinder = null);

        /// <summary>
        /// Adds the events of this instance to the specified log.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <param name="logFinder"></param>
        /// <param name="kinds"></param>
        /// <returns></returns>
        IBdoLog AddEventsTo(IBdoLog log, Predicate<IBdoLog> logFinder = null, params EventKinds[] kinds);

        /// <summary>
        /// Adds the events of this instance to the specified log.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <param name="kinds"></param>
        /// <returns></returns>
        IBdoLog AddEventsTo(IBdoLog log, params EventKinds[] kinds);

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
        /// <param name="kinds"></param>
        /// <returns></returns>
        bool HasEvent(bool isRecursive = true, params EventKinds[] kinds);

        /// <summary>
        /// Adds the specified events to this instance.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <param name="logFinder"></param>
        /// <param name="kinds"></param>
        /// <returns></returns>
        IBdoLog AddEvents(IBdoLog log, Predicate<IBdoLog> logFinder = null, params EventKinds[] kinds);

        /// <summary>
        /// Adds the specified events to this instance.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <param name="kinds"></param>
        /// <returns></returns>
        IBdoLog AddEvents(IBdoLog log, params EventKinds[] kinds);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isRecursive"></param>
        /// <param name="kinds"></param>
        IBdoLog ClearEvents(bool isRecursive = true, params EventKinds[] kinds);

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
        EventKinds GetMaxEventKind(bool isRecursive = true, params EventKinds[] kinds);

        // Logs

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isRecursive"></param>
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
        /// <param name="childLog"></param>
        /// <param name="isRecursive"></param>
        /// <returns></returns>
        IBdoLog RemoveSubLog(IBdoLog childLog, bool isRecursive = true);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isRecursive"></param>
        /// <returns></returns>
        IBdoLog RemoveSubLog(string id, bool isRecursive = true);

        // Clone

        /// <summary>
        /// Clones this instance considering the parent log.
        /// </summary>
        /// <param name="parent"></param>
        IBdoLog Clone(IBdoLog parent, params string[] areas);

        /// <summary>
        /// Clones this instance considering the parent log.
        /// </summary>
        /// <param name="parent"></param>
        T Clone<T>(IBdoLog parent, params string[] areas) where T : class;

        // Execution

        /// <summary>
        /// 
        /// </summary>
        IProcessExecution Execution { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="execution"></param>
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
        /// <param name="status"></param>
        IBdoLog End(ProcessExecutionStatus status = ProcessExecutionStatus.Completed);
    }
}