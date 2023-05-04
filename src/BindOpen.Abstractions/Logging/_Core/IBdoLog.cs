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
        IEnumerable<IBdoLog> Children();

        bool HasChild();

        IBdoLog InsertChild(
            EventKinds kind,
            string title,
            string description = null,
            DateTime? date = null,
            string resultCode = null);

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
            string resultCode = null);

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
            string resultCode = null);

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
        void RemoveEvents(bool isRecursive = true, params EventKinds[] kinds);

        // Logs

        /// <summary>
        /// 
        /// </summary>
        /// <param key="id"></param>
        /// <param key="isRecursive"></param>
        /// <returns></returns>
        IBdoLog GetChild(string id, bool isRecursive = false);

        /// <summary>
        /// Creates a new instance of IBdoLog.
        /// </summary>
        /// <returns></returns>
        IBdoLog NewLog();

        /// <summary>
        /// Creates a new instance of IBdoLog.
        /// </summary>
        /// <returns></returns>
        void Sanitize();

        // Execution

        /// <summary>
        /// 
        /// </summary>
        IBdoProcessExecution Execution { get; set; }

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