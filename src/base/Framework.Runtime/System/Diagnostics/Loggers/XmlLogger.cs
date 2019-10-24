using System;
using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Helpers.Serialization;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Items.Tasks;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Diagnostics.Loggers;

namespace BindOpen.Framework.Runtime.System.Diagnostics.Loggers
{
    /// <summary>
    /// This class represents a XML logger.
    /// </summary>
    /// <remarks>The output format is YAML.</remarks>
    public class XmlLogger : Logger
    {
        // ------------------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the XmlLogger class.
        /// </summary>
        public XmlLogger()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the XmlLogger class.
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
        public XmlLogger(
            String name,
            LoggerMode mode,
            String folderPath,
            String fileName = null,
            DataSourceKind outputKind = DataSourceKind.Repository,
            bool isVerbose = false,
            String uiCulture = null,
            Predicate<ILogEvent> eventFinder = null,
            int expirationDayNumber = -1)
            : base(name, LoggerFormat.Xml, mode, outputKind, isVerbose, uiCulture, folderPath, fileName, eventFinder, expirationDayNumber)
        {
        }

        #endregion

        // ------------------------------------------------------
        // LOGGING
        // ------------------------------------------------------

        #region Logging

        /// <summary>
        /// Logs the specified task.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <param name="task">The task to log.</param>
        public override bool WriteTask(
            ILog log, ITaskConfiguration task)
        {
            return Save(log.Root, Filepath);
        }

        /// <summary>
        /// Logs the specified record.
        /// </summary>
        /// <param name="logEvent">The log event to consider.</param>
        public override bool WriteEvent(
            ILogEvent logEvent)
        {
            if (EventFinder == null || EventFinder.Invoke(logEvent))
                return Save(logEvent?.Root, Filepath);
            else
                return false;
        }

        /// <summary>
        /// Logs the specified element.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <param name="elementName">The element name to consider.</param>
        /// <param name="elementValue">The element value to consider.</param>
        public override bool WriteDetailElement(
            ILog log,
            String elementName,
            Object elementValue)
        {
            return false;
        }

        /// <summary>
        /// Logs the specified record.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <param name="childLog">The child log to log.</param>
        public override bool WriteChildLog(
            ILog log,
            ILog childLog)
        {
            return false;
        }

        #endregion

        // ------------------------------------------------------
        // ACCESSORS
        // ------------------------------------------------------

        #region Accessors

        /// <summary>
        /// Indicates whether this instance requires all the event history to be maintained.
        /// </summary>
        /// <returns>Returns True if this instance requires all the event history.</returns>
        public override bool IsHistoryRequired()
        {
            return true;
        }

        #endregion

        // ------------------------------------------
        // SERIALIZATION / UNSERIALIZATION
        // ------------------------------------------

        #region

        // Unserialization ---------------------------------

        /// <summary>
        /// Instantiates a new instance of Log class from a xml file.
        /// </summary>
        /// <param name="filePath">The path of the Xml file to load.</param>
        /// <param name="loadLog">The output log of the load task.</param>
        /// <param name="mustFileExist">Indicates whether the file must exist.</param>
        /// <returns>The load log.</returns>
        public override ILog LoadLog(
            String filePath,

            ILog loadLog = null,
            bool mustFileExist = true)
        {
            Log log = XmlHelper.Load<Log>(filePath, null, null, loadLog, null, mustFileExist);
            //if (log != null) log.UpdateRuntimeInfo(appScope, loadLog);
            return log;
        }

        /// <summary>
        /// Instantiates a new instance of Log class from a xml string.
        /// </summary>
        /// <param name="xmlString">The Xml string to load.</param>
        /// <param name="loadLog">The output log of the load task.</param>
        /// <returns>The log defined in the Xml file.</returns>
        public override ILog LoadLogFromString(
            String xmlString,
            ILog loadLog = null)
        {
            Log log = XmlHelper.LoadFromString<Log>(xmlString, null, null, loadLog, null);
            log?.UpdateRuntimeInfo(null, null, loadLog);

            return log;
        }

        // Serialization ---------------------------------

        /// <summary>
        /// Saves this instance in the specified log file.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <param name="logFilePath">The path of the log file to save.</param>
        /// <param name="isAppended">Indicates whether the new content is appended if one alreay exists.</param>
        /// <returns>Returns the saving log.</returns>
        public override bool Save(ILog log, String logFilePath, bool isAppended = false)
        {
            return log.SaveXml(logFilePath);
        }

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
            return ToString();
        }

        #endregion
    }
}

