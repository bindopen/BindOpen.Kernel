namespace BindOpen.System.Diagnostics
{
    /// <summary>
    /// This class represents a logger formater.
    /// </summary>
    public class BdoLoggerFormater_Snap : IBdoLoggerFormater
    {
        /// <summary>
        /// Converts the log to the string.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the string that converts the specified log.</returns>
        public string ToString(IBdoLog log)
        {
            if (log != null)
            {
                var st = log.DisplayName
                   + (!string.IsNullOrEmpty(log.Description) ? " | " + log.Description : "");
                foreach (var ev in log.Events)
                {
                    st += ToString(ev);
                }
                return st;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="ev"></typeparam>
        public string ToString(IBdoLogEvent ev)
        {
            if (ev != null)
            {
                int level = ev.Level;
                string indent = new string(' ', (level < 0 ? 0 : level - 1) * 2);

                return indent + (ev.Log != null ? "o " : "- ")
                   + ev.DisplayName
                   + (!string.IsNullOrEmpty(ev.Description) ? " | " + ev.Description : "");
            }

            return null;
        }
    }
}
