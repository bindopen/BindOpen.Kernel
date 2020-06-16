using BindOpen.System.Diagnostics.Events;
using System.Collections.Generic;

namespace BindOpen.System.Diagnostics
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoLogEvent : IBdoEvent
    {
        /// <summary>
        /// 
        /// </summary>
        int Level { get; }

        /// <summary>
        /// 
        /// </summary>
        IBdoLog Log { get; set; }

        /// <summary>
        /// 
        /// </summary>
        BdoLog LogDto { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoLog Parent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ResultCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoLog Root { get; }

        /// <summary>
        /// 
        /// </summary>
        string Source { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<LogEventStackTrace> StackTraces { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isRecursive"></param>
        /// <param name="kinds"></param>
        /// <returns></returns>
        EventKinds GetMaxEventKind(bool isRecursive = true, params EventKinds[] kinds);

        /// <summary>
        /// Clones this instance considering the parent log.
        /// </summary>
        /// <param name="parent"></param>
        IBdoLogEvent Clone(IBdoLog parent, params string[] areas);

        /// <summary>
        /// Clones this instance considering the parent log.
        /// </summary>
        /// <param name="parent"></param>
        T Clone<T>(IBdoLog parent, params string[] areas) where T : class;
    }
}