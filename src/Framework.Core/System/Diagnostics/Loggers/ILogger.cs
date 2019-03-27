using System;
using System.Collections.Generic;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Configuration.Tasks;

namespace BindOpen.Framework.Core.System.Diagnostics.Loggers
{
    /// <summary>
    /// This interface represents a logger.
    /// </summary>
    /// <remarks>The output format is YAML.</remarks>
    public interface ILogger
    {
        // ------------------------------------------------------
        // PROPERTIES
        // ------------------------------------------------------

        #region Properties

        /// <summary>
        /// File path of this instance.
        /// </summary>
        String Filepath { get; }

        /// <summary>
        /// Folder path of this instance.
        /// </summary>
        String FolderPath { get; }

        /// <summary>
        /// The mode of this instance.
        /// </summary>
        LoggerMode Mode { get; set; }

        /// <summary>
        /// The output kind of this instance.
        /// </summary>
        DataSourceKind OutputKind { get; set; }

        /// <summary>
        /// The format of this instance.
        /// </summary>
        LogFormat Format { get; set; }

        /// <summary>
        /// Indicates whether this instance is verbose.
        /// </summary>
        Boolean IsVerbose { get; set; }

        /// <summary>
        /// The UI culture of this instance.
        /// </summary>
        String UICulture { get; set; }

        /// <summary>
        /// The log of this instance.
        /// </summary>
        Log Log { get; }

        /// <summary>
        /// Function that filters event.
        /// </summary>
        Predicate<LogEvent> EventFinder { get; set; }

        #endregion

        // ------------------------------------------------------
        // WRITING
        // ------------------------------------------------------

        #region Writing

        /// <summary>
        /// Logs the specified log.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        Boolean WriteLog(Log log);

        /// <summary>
        /// Logs the specified task.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <param name="task">The task to log.</param>
        Boolean WriteTask(
            Log log, TaskConfiguration task);

        /// <summary>
        /// Logs the specified event.
        /// </summary>
        /// <param name="logEvent">The log event to consider.</param>
        Boolean WriteEvent(LogEvent logEvent);

        /// <summary>
        /// Logs the specified element.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <param name="elementName">The element name to consider.</param>
        /// <param name="elementValue">The element value to consider.</param>
        Boolean WriteDetailElement(
            Log log,
            String elementName,
            Object elementValue);

        /// <summary>
        /// Logs the specified child log.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <param name="childLog">The child log to log.</param>
        Boolean WriteChildLog(
            Log log,
            Log childLog);

        // Write to output data sources

        /// <summary>
        /// Writes the specified text to the output kind of this instance.
        /// </summary>
        /// <param name="text">The text to write.</param>
        /// <returns>Returns true whether the text has been written.</returns>
        Boolean Write(String text);

        #endregion

        // ------------------------------------------------------
        // ACCESSORS
        // ------------------------------------------------------

        #region Accessors

        /// <summary>
        /// Indicates whether this instance requires all the event history to be maintained.
        /// </summary>
        /// <returns>Returns True if this instance requires all the event history.</returns>
        Boolean IsHistoryRequired();

        #endregion

        // ------------------------------------------------------
        // MANAGEMENT
        // ------------------------------------------------------

        #region Management

        /// <summary>
        /// Sets the specified log.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        void SetLog(Log log);

        /// <summary>
        /// Delete the logs older than the specified day number.
        /// </summary>
        /// <param name="expirationDayNumber">The number of expiration days to consider.</param>
        /// <remarks>With expiration day number equaling to -1, no files expires. Equaling to 0, all files except the current one expires.</remarks>
        void DeleteExpiredLogs(int expirationDayNumber);

        /// <summary>
        /// Sets the name of the file of this instance.
        /// </summary>
        /// <param name="fileName">The name of the file to consider.</param>
        void SetFileName(String fileName);

        /// <summary>
        /// Sets the log file location.
        /// </summary>
        /// <param name="newFolderPath">The new folder path to consider.</param>
        /// <param name="isFileToBeMoved">Indicates whether the file must be moved.</param>
        /// <param name="newFileName">The new file name to consider.</param>
        void SetFilePath(String newFolderPath, Boolean isFileToBeMoved, String newFileName = null);

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
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="mustFileExist">Indicates whether the file must exist.</param>
        /// <returns>The load log.</returns>
        Log LoadLog(
            String filePath,
            Log loadLog = null,
            IAppScope appScope = null,
            Boolean mustFileExist = true);

        /// <summary>
        /// Instantiates a new instance of Log class from a xml string.
        /// </summary>
        /// <param name="xmlString">The Xml string to load.</param>
        /// <param name="loadLog">The output log of the load task.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <returns>The log defined in the Xml file.</returns>
        Log LoadLogFromString(
            String xmlString,
            Log loadLog = null,
            AppScope appScope = null);

        // Serialization ---------------------------------

        /// <summary>
        /// Saves this instance in the specified log file.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <param name="logFilePath">The path of the log file to save.</param>
        /// <param name="isAppended">Indicates whether the new content is appended if one alreay exists.</param>
        /// <returns>Returns the saving log.</returns>
        Boolean Save(Log log, String logFilePath, Boolean isAppended = false);

        /// <summary>
        /// Saves this instance in the specified log file.
        /// </summary>
        /// <param name="isAppended">Indicates whether the new content is appended if one alreay exists.</param>
        /// <returns>Returns the saving log.</returns>
        Boolean Save(Boolean isAppended = false);

        /// <summary>
        /// Gets the string representing to the specified log.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <param name="attributeNames">The attribute names to consider.</param>
        /// <returns>The string representing to the specified log.</returns>
        String ToString(
            Log log,
            List<String> attributeNames = null);

        /// <summary>
        /// Gets the string representing to the specified event.
        /// </summary>
        /// <param name="logEvent">The log event to consider.</param>
        /// <param name="attributeNames">The attribute names to consider.</param>
        /// <returns>The string representing to the specified event.</returns>
        String ToString(
            LogEvent logEvent,
            List<String> attributeNames = null);

        #endregion
    }
}
