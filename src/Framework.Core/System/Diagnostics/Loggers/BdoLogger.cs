using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Runtime.Items;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public HashSet<DatasourceKind> OutputKinds { get; set; } = new HashSet<DatasourceKind>() { DatasourceKind.Repository };

        /// <summary>
        /// The default format of this instance.
        /// </summary>
        public BdoDefaultLoggerFormat DefaultFormat { get; set; } = BdoDefaultLoggerFormat.Custom;

        /// <summary>
        /// The UI culture of this instance.
        /// </summary>
        public string UICulture { get; set; } = null;

        /// <summary>
        /// The log of this instance.
        /// </summary>
        public IBdoLog Log { get; private set; } = null;

        /// <summary>
        /// Function that filters event.
        /// </summary>
        public Predicate<IBdoLogEvent> EventFilter
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
        protected BdoLogger()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the Logger class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="logFormat">The log format to consider.</param>
        /// <param name="mode">The mode to consider.</param>
        /// <param name="eventFilter">The function that filters events.</param>
        protected BdoLogger(
            string name,
            BdoDefaultLoggerFormat logFormat,
            BdoLoggerMode mode,
            Predicate<IBdoLogEvent> eventFilter = null) : this()
        {
            Name = name;
            DefaultFormat = logFormat;
            Mode = mode;
            EventFilter = eventFilter;
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
        public virtual bool WriteLog(IBdoLog log)
        {
            return false;
        }

        /// <summary>
        /// Logs the specified task.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <param name="task">The task to log.</param>
        public virtual bool WriteTask(IBdoLog log, IBdoTaskConfiguration task)
        {
            return false;
        }

        /// <summary>
        /// Logs the specified event.
        /// </summary>
        /// <param name="logEvent">The log event to consider.</param>
        public virtual bool WriteEvent(IBdoLogEvent logEvent)
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
            object elementValue)
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
            bool isGood = true;
            foreach (var outputKind in OutputKinds)
            {
                switch (outputKind)
                {
                    case DatasourceKind.Console:
                        isGood &= WriteToConsole(text);
                        break;
                    case DatasourceKind.Repository:
                        isGood &= WriteToFile(text);
                        break;
                };
            }

            return isGood;
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
                Trace.Write(text);
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

                    using (StreamWriter streamWriter = new StreamWriter(Filepath, true))
                    {
                        streamWriter.Write(text);
                    }

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
        // MUTATORS
        // ------------------------------------------------------

        #region Mutators

        /// <summary>
        /// Adds a console output.
        /// </summary>
        public IBdoLogger AddConsoleOutput()
        {
            OutputKinds.Add(DatasourceKind.Console);

            return this;
        }

        /// <summary>
        /// Adds a file output.
        /// </summary>
        /// <param name="folderPath">The folder path to consider.</param>
        /// <param name="fileName">The file name to consider.</param>
        /// <param name="isFileToBeMoved">Indicates whether the current file must be moved.</param>
        /// <param name="id">The ID to consider.</param>
        public IBdoLogger AddFileOutput(string folderPath, string fileName = null, bool isFileToBeMoved = false, string id = null)
        {
            if (!string.IsNullOrEmpty(folderPath))
            {
                string oldFilePath = Filepath;
                fileName = (string.IsNullOrEmpty(fileName) ? (oldFilePath == null ? null : Path.GetFileName(oldFilePath)) : fileName);

                _folderPath = folderPath;
                SetFileName(fileName, id);

                if (isFileToBeMoved && File.Exists(oldFilePath) && !string.IsNullOrEmpty(oldFilePath))
                {
                    // we move the old file to the new folder
                    string newFilePath = Filepath;

                    try
                    {
                        if (!Directory.Exists(folderPath))
                            Directory.CreateDirectory(folderPath);
                        File.Move(oldFilePath, newFilePath);
                    }
                    catch
                    {
                    }
                }

                OutputKinds.Add(DatasourceKind.Repository);
            }


            return this;
        }

        /// <summary>
        /// Sets the UI culture of this instance.
        /// </summary>
        /// <param name="uiCulture">The UI culture to consider.</param>
        public IBdoLogger WithUICulture(string uiCulture)
        {
            UICulture = uiCulture;

            return this;
        }

        /// <summary>
        /// Sets the specified log.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        public IBdoLogger SetLog(IBdoLog log)
        {
            Log = log;

            return this;
        }

        /// <summary>
        /// Sets the mode of this instance.
        /// </summary>
        /// <param name="mode">The logger mode to consider.</param>
        public IBdoLogger WithMode(BdoLoggerMode mode)
        {
            Mode = mode;

            return this;
        }

        /// <summary>
        /// Sets the specified event filter.
        /// </summary>
        /// <param name="eventFilter">The event filter to consider.</param>
        public IBdoLogger WithEventFilter(Predicate<IBdoLogEvent> eventFilter)
        {
            EventFilter = eventFilter;

            return this;
        }

        /// <summary>
        /// Delete the logs older than the specified day number.
        /// </summary>
        /// <param name="expirationDayNumber">The number of expiration days to consider.</param>
        /// <param name="fileFormat">The file format to consider.</param>
        /// <remarks>With expiration day number equaling to -1, no files expires. Equaling to 0, all files except the current one expires.</remarks>
        public IBdoLogger DeleteExpiredLogs(int expirationDayNumber, string fileFormat = null)
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

            return this;
        }

        /// <summary>
        /// Sets the name of the file of this instance.
        /// </summary>
        /// <param name="fileName">The name of the file to consider.</param>
        /// <param name="id">The ID to consider.</param>
        private void SetFileName(string fileName, string id = null)
        {
            _fileNameFormat = fileName;
            fileName = (string.IsNullOrEmpty(fileName) ? "log_$(timeStamp).log" : fileName);
            fileName = fileName.Replace("$(timeStamp)", DateTime.Now.ToString("yyyyMMddHHmmss"), false);
            fileName = fileName.Replace("$(id)", id ?? Guid.NewGuid().ToString(), false);
            _fileName = fileName;
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
            return Save(Log, Filepath, isAppended);
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
                Log?.Dispose();
            }
        }

        #endregion
    }
}
