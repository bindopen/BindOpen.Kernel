using System.Collections.Generic;
using BindOpen.Framework.Core.System.Diagnostics.Events;

namespace BindOpen.Framework.Core.System.Diagnostics
{
    public interface ILogEvent : IEvent
    {
        int Level { get; }
        ILog Log { get; set; }
        Log LogDto { get; set; }
        ILog Parent { get; set; }
        string ResultCode { get; set; }
        ILog Root { get; }
        string Source { get; set; }
        List<LogEvent.LogEventStackTrace> StackTraces { get; set; }

        EventKinds GetMaxEventKind(bool isRecursive = true, params EventKinds[] kinds);
    }
}