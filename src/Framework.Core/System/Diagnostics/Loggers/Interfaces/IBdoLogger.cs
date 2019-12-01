using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Runtime.Items;
using System;
using System.Collections.Generic;

namespace BindOpen.Framework.Core.System.Diagnostics.Loggers
{
    /// <summary>
    /// This interface represents a logger.
    /// </summary>
    /// <remarks>The output format is YAML.</remarks>
    public interface IBdoLogger : INamedDataItem
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
        BdoLoggerMode Mode { get; set; }

        /// <summary>
        /// The output kind of this instance.
        /// </summary>
        DatasourceKind OutputKind { get; set; }

        /// <summary>
        /// The format of this instance.
        /// </summary>
        BdoLoggerFormat Format { get; set; }

        /// <summary>
        /// Indicates whether this instance is verbose.
        /// </summary>
        bool IsVerbose { get; set; }

        /// <summary>
        /// The UI culture of this instance.
        /// </summary>
        string UICulture { get; set; }

        /// <summary>
        /// The log of this instance.
        /// </summary>
        IBdoLog Log { get; }

        /// <summary>
        /// Function that filters event.
        /// </summary>
        Predicate<IBdoLogEvent> EventFinder { get; set; }

        #endregion

        // ------------------------------------------------------
        // WRITING
        // ------------------------------------------------------

        #region Writing

        /// <summary>
        /// Logs the specified log.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        bool WriteLog(IBdoLog log);

        /// <summary>
        /// Logs the specified task.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <param name="task">The task to log.</param>
        bool WriteTask(
            IBdoLog log, IBdoTaskConfiguration task);

        /// <summary>
        /// Logs the specified event.
        /// </summary>
        /// <param name="logEvent">The log event to consider.</param>
        bool WriteEvent(IBdoLogEvent logEvent);

        /// <summary>
        /// Logs the specified element.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <param name="elementName">The element name to consider.</param>
        /// <param name="elementValue">The element value to consider.</param>
        bool WriteDetailElement(
            IBdoLog log,
            string elementName,
            object elementValue);

        /// <summary>
        /// Logs the specified child log.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <param name="childLog">The child log to log.</param>
        bool WriteChildLog(
            IBdoLog log,
            IBdoLog childLog);

        // Write to output data sources

        /// <summary>
        /// Writes the specified text to the output kind of this instance.
        /// </summary>
        /// <param name="text">The text to write.</param>
        /// <returns>Returns true whether the text has been written.</returns>
        bool Write(string text);

        #endregion

        // ------------------------------------------------------
        // ACCESSORS
        // ------------------------------------------------------

        #region Accessors

        /// <summary>
        /// Indicates whether this instance requires all the event history to be maintained.
        /// </summary>
        /// <returns>Returns True if this instance requires all the event history.</returns>
        bool IsHistoryRequired();

        #endregion

        // ------------------------------------------------------
        // MANAGEMENT
        // ------------------------------------------------------

        #region Management

        /// <summary>
        /// Sets the specified log.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        void SetLog(IBdoLog log);

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
        void SetFilePath(String newFolderPath, bool isFileToBeMoved, String newFileName = null);

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
        IBdoLog LoadLog(
            String filePath,
            IBdoLog loadLog = null,
            bool mustFileExist = true);

        /// <summary>
        /// Instantiates a new instance of Log class from a xml string.
        /// </summary>
        /// <param name="xmlString">The Xml string to load.</param>
        /// <param name="loadLog">The output log of the load task.</param>
        /// <returns>The log defined in the Xml file.</returns>
        IBdoLog LoadLogFromString(
            String xmlString,
            IBdoLog loadLog = null);

        // Serialization ---------------------------------

        /// <summary>
        /// Saves this instance in the specified log file.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <param name="logFilePath">The path of the log file to save.</param>
        /// <param name="isAppended">Indicates whether the new content is appended if one alreay exists.</param>
        /// <returns>Returns the saving log.</returns>
        bool Save(IBdoLog log, String logFilePath, bool isAppended = false);

        /// <summary>
        /// Saves this instance in the specified log file.
        /// </summary>
        /// <param name="isAppended">Indicates whether the new content is appended if one alreay exists.</param>
        /// <returns>Returns the saving log.</returns>
        bool Save(bool isAppended = false);

        /// <summary>
        /// Gets the string representing to the specified log.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <param name="attributeNames">The attribute names to consider.</param>
        /// <returns>The string representing to the specified log.</returns>
        String ToString(
            IBdoLog log,
            List<string> attributeNames = null);

        /// <summary>
        /// Gets the string representing to the specified event.
        /// </summary>
        /// <param name="logEvent">The log event to consider.</param>
        /// <param name="attributeNames">The attribute names to consider.</param>
        /// <returns>The string representing to the specified event.</returns>
        String ToString(
            IBdoLogEvent logEvent,
            List<string> attributeNames = null);

        #endregion
    }
}
