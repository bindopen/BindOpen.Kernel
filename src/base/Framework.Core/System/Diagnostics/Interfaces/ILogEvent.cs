using System.Collections.Generic;
using BindOpen.Framework.Core.System.Diagnostics.Events;

namespace BindOpen.Framework.Core.System.Diagnostics
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILogEvent : IEvent
    {
        /// <summary>
        /// 
        /// </summary>
        int Level { get; }

        /// <summary>
        /// 
        /// </summary>
        ILog Log { get; set; }

        /// <summary>
        /// 
        /// </summary>
        Log LogDto { get; set; }

        /// <summary>
        /// 
        /// </summary>
        ILog Parent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ResultCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        ILog Root { get; }

        /// <summary>
        /// 
        /// </summary>
        string Source { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<LogEvent.LogEventStackTrace> StackTraces { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isRecursive"></param>
        /// <param name="kinds"></param>
        /// <returns></returns>
        EventKinds GetMaxEventKind(bool isRecursive = true, params EventKinds[] kinds);
    }
}