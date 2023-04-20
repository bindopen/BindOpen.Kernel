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
        string ResultCode { get; }

        /// <summary>
        /// 
        /// </summary>
        IBdoLog Root { get; }

        /// <summary>
        /// 
        /// </summary>
        IBdoLog Parent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoConfiguration TaskConfig { get; set; }

        // Logs

        /// <summary>
        /// 
        /// </summary>
        IList<IBdoLog> Children();

        bool HasChild();

        IBdoLog InsertChild(
            EventKinds kind,
            string title,
            string description = null,
            DateTime? date = null);

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
        IBdoLog AddChild(
            IBdoLog childLog,
            EventKinds kind = EventKinds.Any,
            string title = null,
            string description = null,
            DateTime? date = null,
            string resultCode = null,
            Predicate<IBdoLog> filter = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="id"></param>
        /// <param key="isRecursive"></param>
        /// <returns></returns>
        void RemoveChild(Predicate<IBdoLog> filter, bool isRecursive = true);

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
        IBdoLog AddEvent(
            EventKinds kind,
            string title,
            string description = null,
            DateTime? date = null,
            string resultCode = null,
            IBdoLog childLog = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="isRecursive"></param>
        /// <param key="kinds"></param>
        /// <returns></returns>
        bool HasEvent(bool isRecursive = true, params EventKinds[] kinds);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="isRecursive"></param>
        /// <param key="kinds"></param>
        void ClearEvents(bool isRecursive = true, params EventKinds[] kinds);

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
        IBdoLog GetChildLogWithId(string id, bool isRecursive = false);

        /// <summary>
        /// Creates a new instance of IBdoLog.
        /// </summary>
        /// <returns></returns>
        IBdoLog NewLog();

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
        IProcessExecution Execution { get; set; }

        /// <summary>
        /// 
        /// </summary>
        void Start();

        /// <summary>
        /// 
        /// </summary>
        /// <param key="status"></param>
        void End(ProcessExecutionStatus status = ProcessExecutionStatus.Completed);
    }
}