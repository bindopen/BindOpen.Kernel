using BindOpen.Application.Scopes;
using BindOpen.Data.Items;
using System;
using System.IO;

namespace BindOpen.System.Diagnostics.Loggers
{
    /// <summary>
    /// This class represents a logger builder.
    /// </summary>
    public abstract class BdoLoggerBuilder : DataItem
    {
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

        public IBdoHostOptions Options { get; set; }

        #endregion

        // ------------------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoLoggerBuilder class.
        /// </summary>
        protected BdoLoggerBuilder(IBdoHostOptions options)
        {
            Options = options;
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
                    catch (IOException)
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
        // IDISPOSABLE METHODS
        // ------------------------------------------

        #region IDisposable_Methods

        private bool _isDisposed = false;

        /// <summary>
        /// Disposes this instance. 
        /// </summary>
        /// <param name="isDisposing">Indicates whether this instance is disposing</param>
        protected override void Dispose(bool isDisposing)
        {
            if (_isDisposed)
            {
                return;
            }

            Log?.Dispose();

            _isDisposed = true;

            base.Dispose(isDisposing);
        }

        #endregion
    }
}
