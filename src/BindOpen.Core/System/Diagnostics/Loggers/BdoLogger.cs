using Microsoft.Extensions.Logging;

namespace BindOpen.System.Diagnostics
{
    /// <summary>
    /// This class represents a logger.
    /// </summary>
    public class BdoLogger
    {
        protected ILogger _nativeLogger;

        /// <summary>
        /// Sets the native logger.
        /// </summary>
        /// <param name="nativeLogger">The native logger to consider.</param>
        /// <returns>True if this instance has the specified events. False otherwise.</returns>
        public BdoLogger SetNative(ILogger nativeLogger)
        {
            _nativeLogger = nativeLogger;

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="ev"></typeparam>
        public void Log(IBdoLogEvent ev)
        {
            if (ev != null && _nativeLogger != null)
            {
                int level = ev.Level;
                string indent = new string(' ', (level < 0 ? 0 : level - 1) * 2);

                string st = indent + (ev.Log != null ? "o " : "- ")
                   + ev.DisplayName
                   + (!string.IsNullOrEmpty(ev.Description) ? " | " + ev.Description : "");

                switch (ev?.Kind ?? Events.EventKinds.None)
                {
                    case Events.EventKinds.Checkpoint:
                        _nativeLogger.LogTrace(st);
                        break;
                    case Events.EventKinds.Error:
                        _nativeLogger.LogError(st);
                        break;
                    case Events.EventKinds.Exception:
                        _nativeLogger.LogCritical(st);
                        break;
                    case Events.EventKinds.Message:
                        _nativeLogger.LogInformation(st);
                        break;
                    case Events.EventKinds.Warning:
                        _nativeLogger.LogWarning(st);
                        break;
                }

                if (ev?.Log != null)
                {
                    foreach (IBdoLogEvent logEvent in ev.Log.Events)
                    {
                        Log(logEvent);
                    }
                }
            }
        }
    }
}
