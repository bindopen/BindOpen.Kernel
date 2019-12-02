using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Diagnostics.Loggers;
using System;
using System.Collections.Generic;

namespace BindOpen.Framework.Runtime.System.Diagnostics.Loggers
{
    /// <summary>
    /// This class represents a Snap logger.
    /// </summary>
    public class BdoSnapLogger : BdoLogger
    {
        // ------------------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the SnapLogger class.
        /// </summary>
        public BdoSnapLogger()
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
        /// <remarks>With expiration day number equaling to -1, no files expires. Equaling to 0, all files except the current one expires.</remarks>
        public BdoSnapLogger(
            string name,
            BdoLoggerMode mode,
            string folderPath,
            string fileName = null,
            DatasourceKind outputKind = DatasourceKind.Repository,
            bool isVerbose = false,
            string uiCulture = null,
            Predicate<IBdoLogEvent> eventFinder = null)
            : base(name, BdoLoggerFormat.Snap, mode, outputKind, isVerbose, uiCulture, folderPath, fileName, eventFinder)
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
            IBdoLog log,
            List<string> attributeNames = null)
        {
            if (log == null) return "";

            string st = "";
            if (log.Events != null)
            {
                foreach (IBdoLogEvent logEvent in log.Events)
                {
                    st += ToString(logEvent);
                }
            }

            return st;
        }

        /// <summary>
        /// Gets the string representing to the specified event.
        /// </summary>
        /// <param name="logEvent">The log event to consider.</param>
        /// <param name="attributeNames">The attribute names to consider.</param>
        /// <returns>The string representing to the specified event.</returns>
        public override string ToString(
            IBdoLogEvent logEvent,
            List<string> attributeNames = null)
        {
            if (logEvent == null) return "";

            int level = logEvent.Level;
            string indent = new string(' ', (level < 0 ? 0 : level - 1));
            indent += indent;
            indent += indent;

            string st = logEvent.Date + indent + (logEvent.Log != null ? " o " : " - ") + logEvent.Kind.ToString() + ": " + logEvent.GetTitle(UICulture)
                + (logEvent.Description != null ? " | " + logEvent.GetDescription(UICulture) : "") + Environment.NewLine;

            if (logEvent?.Log != null)
                st += ToString(logEvent?.Log);

            return st;
        }

        /// <summary>
        /// Logs the specified record.
        /// </summary>
        /// <param name="logEvent">The log event to consider.</param>
        public override bool WriteEvent(
            IBdoLogEvent logEvent)
        {
            if (EventFinder?.Invoke(logEvent) != false)
                return Write(ToString(logEvent));
            else
                return false;
        }

        #endregion
    }
}
