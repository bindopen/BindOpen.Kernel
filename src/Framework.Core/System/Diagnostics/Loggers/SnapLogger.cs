using System;
using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Items.Source;

namespace BindOpen.Framework.Core.System.Diagnostics.Loggers
{
    /// <summary>
    /// This class represents a Snap logger.
    /// </summary>
    public class SnapLogger : Logger
    {
        // ------------------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the SnapLogger class.
        /// </summary>
        public SnapLogger()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the SnapLogger class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="mode">The mode to consider.</param>
        /// <param name="folderPath">The folder path to consider.</param>
        /// <param name="fileName">The file name to consider.</param>
        /// <param name="outputKind">The output kind to consider.</param>
        /// <param name="isVerbose">Indicates whether .</param>
        /// <param name="uiCulture">The folder path to consider.</param>
        /// <param name="eventFinder">The function that filters event.</param>
        /// <param name="expirationDayNumber">The number of expiration days to consider.</param>
        /// <remarks>With expiration day number equaling to -1, no files expires. Equaling to 0, all files except the current one expires.</remarks>
        public SnapLogger(
            String name,
            LoggerMode mode,
            String folderPath,
            String fileName = null,
            DataSourceKind outputKind = DataSourceKind.Repository,
            bool isVerbose = false,
            String uiCulture = null,
            Predicate<ILogEvent> eventFinder = null,
            int expirationDayNumber = -1)
            : base(name, LogFormat.Snap, mode, outputKind, isVerbose, uiCulture, folderPath, fileName, eventFinder, expirationDayNumber)
        {
        }

        #endregion

        // ------------------------------------------
        // SERIALIZATION / UNSERIALIZATION
        // ------------------------------------------

        #region

        /// <summary>
        /// Gets the string representing to the specified log.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <param name="attributeNames">The attribute names to consider.</param>
        /// <returns>The string representing to the specified log.</returns>
        public override string ToString(
            ILog log,
            List<string> attributeNames = null)
        {
            if (log == null) return "";

            String st = "";
            if (log.Events != null)
                foreach (ILogEvent logEvent in log.Events)
                    st += ToString(logEvent);

            return st;
        }

        /// <summary>
        /// Gets the string representing to the specified event.
        /// </summary>
        /// <param name="logEvent">The log event to consider.</param>
        /// <param name="attributeNames">The attribute names to consider.</param>
        /// <returns>The string representing to the specified event.</returns>
        public override string ToString(
            ILogEvent logEvent,
            List<string> attributeNames = null)
        {
            if (logEvent == null) return "";

            String indent = new string('\t', (logEvent.Level <= 0 ? 0 : logEvent.Level - 1));

            String st = logEvent.Date + indent + " - " + logEvent.Kind.ToString() + ": " + logEvent.GetTitleText(UICulture) + 
                (logEvent.Description !=null ? " | " +  logEvent.GetDescription(UICulture) : "") + Environment.NewLine;

            if ((logEvent is LogEvent) && ((logEvent as LogEvent).Log != null))
                st += ToString((logEvent as LogEvent).Log);

            return st;
        }

        /// <summary>
        /// Logs the specified record.
        /// </summary>
        /// <param name="logEvent">The log event to consider.</param>
        public override bool WriteEvent(
            ILogEvent logEvent)
        {
            if (EventFinder?.Invoke(logEvent) != false)
                return Write(ToString(logEvent));
            else
                return false;
        }

        #endregion

    }
}
