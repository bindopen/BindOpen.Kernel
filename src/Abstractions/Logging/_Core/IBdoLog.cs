using BindOpen.Kernel.Data;
using BindOpen.Kernel.Data.Meta;
using BindOpen.Kernel.Processing;
using System;

namespace BindOpen.Kernel.Logging
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoLog :
        IIdentified, INamed,
        IDisplayNamed, IDescribed,
        ITTreeNode<IBdoLog>,
        IBdoDetailed, IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        string ResultCode { get; }


        /// <summary>
        /// 
        /// </summary>
        IBdoConfiguration TaskConfig { get; set; }

        /// <summary>
        /// Creates a new instance of IBdoLog.
        /// </summary>
        /// <returns></returns>
        IBdoLog NewLog();

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
            IBdoLog childLog = null,
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
        int RemoveEvents(bool isRecursive = true, params EventKinds[] kinds);

        // Logs

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
    }
}