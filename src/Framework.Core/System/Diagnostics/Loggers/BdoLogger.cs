using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Runtime.Items;
using System;
using System.Collections.Generic;
using System.IO;

namespace BindOpen.Framework.Core.System.Diagnostics.Loggers
{
    /// <summary>
    /// This class represents a logger.
    /// </summary>
    /// <remarks>The output format is YAML.</remarks>
    public abstract class BdoLogger : NamedDataItem, IBdoLogger
    {
        // ------------------------------------------------------
        // CONSTANTS
        // ------------------------------------------------------

        #region Constants

        /// <summary>
        /// The default name to consider.
        /// </summary>
        public const string __DefaultName = "standard";

        #endregion

        // ------------------------------------------------------
        // VARIABLES
        // ------------------------------------------------------

        #region Variables

        private IBdoLog _log = null;
        private string _fileName = null;
        private string _fileNameFormat = null;
        private string _folderPath = null;

        #endregion

        // ------------------------------------------------------
        // PROPERTIES
        // ------------------------------------------------------

        #region Properties

        /// <summary>
        /// File path of this instance.
        /// </summary>
        public string Filepath
        {
            get
            {
                return (!string.IsNullOrEmpty(_folderPath) & !string.IsNullOrEmpty(_fileName) ?
                    FolderPath + _fileName : "").ToPath();
            }
        }

        /// <summary>
        /// Folder path of this instance.
        /// </summary>
        public string FolderPath
        {
            get
            {
                return _folderPath.GetEndedString(@"\").ToPath();
            }
        }

        /// <summary>
        /// The mode of this instance.
        /// </summary>
        public BdoLoggerMode Mode { get; set; } = BdoLoggerMode.Manual;

        /// <summary>
        /// The output kind of this instance.
        /// </summary>
        public DatasourceKind OutputKind { get; set; } = DatasourceKind.Repository;

        /// <summary>
        /// The format of this instance.
        /// </summary>
        public BdoLoggerFormat Format { get; set; } = BdoLoggerFormat.None;

        /// <summary>
        /// Indicates whether this instance is verbose.
        /// </summary>
        public bool IsVerbose { get; set; } = false;

        /// <summary>
        /// The UI culture of this instance.
        /// </summary>
        public string UICulture { get; set; } = null;

        /// <summary>
        /// The log of this instance.
        /// </summary>
        public IBdoLog Log => _log;

        /// <summary>
        /// Function that filters event.
        /// </summary>
        public Predicate<IBdoLogEvent> EventFinder
        {
            get;
            set;
        }

        #endregion

        // ------------------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the Logger class.
        /// </summary>
        public BdoLogger()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the Logger class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="logFormat">The log format to consider.</param>
        /// <param name="mode">The mode to consider.</param>
        /// <param name="outputKind">The output kind to consider.</param>
        /// <param name="isVerbose">Indicates whether .</param>
        /// <param name="uiCulture">The folder path to consider.</param>
        /// <param name="folderPath">The folder path to consider.</param>
        /// <param name="fileName">The file name to consider.</param>
        /// <param name="eventFinder">The function that filters event.</param>
        protected BdoLogger(
            string name,
            BdoLoggerFormat logFormat,
            BdoLoggerMode mode,
            DatasourceKind outputKind,
            bool isVerbose = false,
            string uiCulture = null,
            string folderPath = null,
            string fileName = null,
            Predicate<IBdoLogEvent> eventFinder = null) : this()
        {
            Name = name;
            Format = logFormat;
            Mode = mode;
            OutputKind = outputKind;
            _folderPath = folderPath;
            SetFileName(fileName);
            IsVerbose = isVerbose;
            UICulture = uiCulture;
            EventFinder = eventFinder;
        }

        #endregion

        // ------------------------------------------------------
        // WRITING
        // ------------------------------------------------------

        #region Writing

        /// <summary>
        /// Logs the specified log.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        public virtual bool WriteLog(
            IBdoLog log)
        {
            return false;
        }

        /// <summary>
        /// Logs the specified task.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <param name="task">The task to log.</param>
        public virtual bool WriteTask(
            IBdoLog log, IBdoTaskConfiguration task)
        {
            return false;
        }

        /// <summary>
        /// Logs the specified event.
        /// </summary>
        /// <param name="logEvent">The log event to consider.</param>
        public virtual bool WriteEvent(
            IBdoLogEvent logEvent)
        {
            return false;
        }

        /// <summary>
        /// Logs the specified element.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <param name="elementName">The element name to consider.</param>
        /// <param name="elementValue">The element value to consider.</param>
        public virtual bool WriteDetailElement(
            IBdoLog log,
            string elementName,
            Object elementValue)
        {
            return false;
        }

        /// <summary>
        /// Logs the specified child log.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <param name="childLog">The child log to log.</param>
        public virtual bool WriteChildLog(
            IBdoLog log,
            IBdoLog childLog)
        {
            return false;
        }

        // Write to output data sources

        /// <summary>
        /// Writes the specified text to the output kind of this instance.
        /// </summary>
        /// <param name="text">The text to write.</param>
        /// <returns>Returns true whether the text has been written.</returns>
        public bool Write(string text)
        {
            switch (OutputKind)
            {
                case DatasourceKind.Console:
                    return WriteToConsole(text);
                case DatasourceKind.Repository:
                    return WriteToFile(text);
            }

            return false;
        }

        /// <summary>
        /// Writes the specified text to the console of this instance.
        /// </summary>
        /// <param name="text">The text to write.</param>
        /// <returns>Returns true whether the text has been written.</returns>
        private bool WriteToConsole(string text)
        {
            bool isLogged = false;

            if (!string.IsNullOrEmpty(text))
            {
                Console.Write(text);
                isLogged = true;
            }

            return isLogged;
        }

        /// <summary>
        /// Writes the specified text to the file of this instance.
        /// </summary>
        /// <param name="text">The text to write.</param>
        /// <returns>Returns true whether the text has been written.</returns>
        private bool WriteToFile(string text)
        {
            bool isLogged = false;

            if ((!string.IsNullOrEmpty(Filepath)) && (!string.IsNullOrEmpty(text)))
            {
                try
                {
                    if (!Directory.Exists(FolderPath))
                        Directory.CreateDirectory(FolderPath);

                    using (StreamWriter streamWriter = new global::System.IO.StreamWriter(Filepath, true))
                        streamWriter.Write(text);

                    isLogged = true;
                }
                catch
                {
                }
            }

            return isLogged;
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
        public virtual bool IsHistoryRequired()
        {
            return false;
        }

        #endregion

        // ------------------------------------------------------
        // MANAGEMENT
        // ------------------------------------------------------

        #region Management

        /// <summary>
        /// Sets the specified log.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        public void SetLog(IBdoLog log)
        {
            _log = log;
        }

        /// <summary>
        /// Delete the logs older than the specified day number.
        /// </summary>
        /// <param name="expirationDayNumber">The number of expiration days to consider.</param>
        /// <param name="fileFormat">The file format to consider.</param>
        /// <remarks>With expiration day number equaling to -1, no files expires. Equaling to 0, all files except the current one expires.</remarks>
        public void DeleteExpiredLogs(int expirationDayNumber, string fileFormat = null)
        {
            if (expirationDayNumber > -1 && Directory.Exists(_folderPath))
            {
                if (fileFormat == null)
                {
                    fileFormat = _fileNameFormat;
                }
                fileFormat = fileFormat.Replace("$(id)", "*", false).Replace("$(timestamp)", "*", false);

                string[] files = Directory.GetFiles(_folderPath, fileFormat);

                string logFilePath = Filepath;

                foreach (string file in files)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    if (fileInfo.LastAccessTime < DateTime.Now.AddDays(-expirationDayNumber))
                    {
                        if (!string.Equals(fileInfo.FullName, logFilePath, StringComparison.OrdinalIgnoreCase))
                        {
                            try
                            {
                                fileInfo.Delete();
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Sets the name of the file of this instance.
        /// </summary>
        /// <param name="fileName">The name of the file to consider.</param>
        /// <param name="id">The ID to consider.</param>
        public void SetFileName(string fileName, string id = null)
        {
            _fileNameFormat = fileName;
            fileName = (string.IsNullOrEmpty(fileName) ? "log_$(timeStamp).log" : fileName);
            fileName = fileName.Replace("$(timeStamp)", DateTime.Now.ToString("yyyyMMddHHmmss"), false);
            fileName = fileName.Replace("$(id)", id ?? Guid.NewGuid().ToString(), false);
            _fileName = fileName;
        }

        /// <summary>
        /// Sets the log file location.
        /// </summary>
        /// <param name="newFolderPath">The new folder path to consider.</param>
        /// <param name="isFileToBeMoved">Indicates whether the file must be moved.</param>
        /// <param name="newFileName">The new file name to consider.</param>
        /// <param name="id">The new file name to consider.</param>
        public void SetFilePath(string newFolderPath, bool isFileToBeMoved, string newFileName = null, string id = null)
        {
            if (string.IsNullOrEmpty(newFolderPath)) return;

            string oldFilePath = Filepath;
            newFileName = (string.IsNullOrEmpty(newFileName) ? (oldFilePath == null ? null : Path.GetFileName(oldFilePath)) : newFileName);

            _folderPath = newFolderPath;
            SetFileName(newFileName, id);

            if (isFileToBeMoved && File.Exists(oldFilePath) && !string.IsNullOrEmpty(oldFilePath))
            {
                // we move the old file to the new folder
                string newFilePath = Filepath;

                try
                {
                    if (!Directory.Exists(newFolderPath))
                        Directory.CreateDirectory(newFolderPath);
                    File.Move(oldFilePath, newFilePath);
                }
                catch
                {
                }
            }
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
        public virtual IBdoLog LoadLog(
            string filePath,
            IBdoLog loadLog = null,
            bool mustFileExist = true)
        {
            return new BdoLog();
        }

        /// <summary>
        /// Instantiates a new instance of Log class from a xml string.
        /// </summary>
        /// <param name="xmlString">The Xml string to load.</param>
        /// <param name="loadLog">The output log of the load task.</param>
        /// <returns>The log defined in the Xml file.</returns>
        public virtual IBdoLog LoadLogFromString(
            string xmlString,
            IBdoLog loadLog = null)
        {
            return new BdoLog();
        }

        // Serialization ---------------------------------

        /// <summary>
        /// Saves this instance in the specified log file.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <param name="logFilePath">The path of the log file to save.</param>
        /// <param name="isAppended">Indicates whether the new content is appended if one alreay exists.</param>
        /// <returns>Returns the saving log.</returns>
        public virtual bool Save(IBdoLog log, string logFilePath, bool isAppended = false)
        {
            return false;
        }

        /// <summary>
        /// Saves this instance in the specified log file.
        /// </summary>
        /// <param name="isAppended">Indicates whether the new content is appended if one alreay exists.</param>
        /// <returns>Returns the saving log.</returns>
        public bool Save(bool isAppended = false)
        {
            return Save(_log, Filepath, isAppended);
        }

        /// <summary>
        /// Gets the string representing to the specified log.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <param name="attributeNames">The attribute names to consider.</param>
        /// <returns>The string representing to the specified log.</returns>
        public virtual string ToString(
            IBdoLog log,
            List<string> attributeNames = null)
        {
            return "";
        }

        /// <summary>
        /// Gets the string representing to the specified event.
        /// </summary>
        /// <param name="logEvent">The log event to consider.</param>
        /// <param name="attributeNames">The attribute names to consider.</param>
        /// <returns>The string representing to the specified event.</returns>
        public virtual string ToString(
            IBdoLogEvent logEvent,
            List<string> attributeNames = null)
        {
            return "";
        }

        #endregion

        // ------------------------------------------
        // IDISPOSABLE METHODS
        // ------------------------------------------

        #region IDisposable_Methods

        /// <summary>
        /// Disposes this instance. 
        /// </summary>
        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            if (isDisposing)
            {
                _log?.Dispose();
            }
        }

        #endregion   
    }
}
