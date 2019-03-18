using System;
using System.IO;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Configuration.Tasks;
using BindOpen.Framework.Core.System.Diagnostics.Events;

namespace BindOpen.Framework.Core.System.Diagnostics.Loggers
{
    /// <summary>
    /// This class represents a YAML logger.
    /// </summary>
    /// <remarks>The output format is YAML.</remarks>
    public class ReportLogger : Logger
    {

        // ------------------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ReportLogger class.
        /// </summary>
        public ReportLogger()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the ReportLogger class.
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
        public ReportLogger(
            String name,
            LoggerMode mode,
            String folderPath,
            String fileName = null,
            DataSourceKind outputKind = DataSourceKind.Repository,
            Boolean isVerbose = false,
            String uiCulture = null,
            Predicate<LogEvent> eventFinder = null,
            int expirationDayNumber = -1) : 
            base(name, LogFormat.Report, mode, outputKind, isVerbose, uiCulture, folderPath, fileName, eventFinder, expirationDayNumber)
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
        public override Boolean WriteTask(
            Log log, TaskConfiguration task)
        {
            return false;
        }

        /// <summary>
        /// Logs the specified record.
        /// </summary>
        /// <param name="logEvent">The log event to consider.</param>
        public override Boolean WriteEvent(
            LogEvent logEvent)
        {
            if (this.EventFinder == null || this.EventFinder.Invoke(logEvent))
                return false;
            else
                return false;
        }

        /// <summary>
        /// Logs the specified element.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <param name="elementName">The element name to consider.</param>
        /// <param name="elementValue">The element value to consider.</param>
        public override Boolean WriteDetailElement(
            Log log,
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
        public override Boolean WriteChildLog(
            Log log,
            Log childLog)
        {
            return false;
        }

        #endregion


        // ------------------------------------------
        // SERIALIZATION / UNSERIALIZATION
        // ------------------------------------------

        #region

        // Serialization ---------------------------------

        /// <summary>
        /// Saves this instance in the specified log file.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <param name="logFilePath">The path of the log file to save.</param>
        /// <param name="isAppended">Indicates whether the new content is appended if one alreay exists.</param>
        /// <returns>Returns the saving log.</returns>
        public override Boolean Save(Log log, String logFilePath, Boolean isAppended = false)
        {
            if (log == null) return false;

            log.UpdateStorageInfo();

            StreamWriter streamWriter = null;
            Boolean isWasSaved = false;
            try
            {
                // we create the folder if it does not exist
                if (!Directory.Exists(Path.GetDirectoryName(logFilePath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(logFilePath));

                // we initialize variables
                String aFieldDelimiterValue = "\t";
                String textDelimiterValue = "\"";
                String line = "";

                // if the file to write already exists then we append it
                if ((global::System.IO.File.Exists(logFilePath)) & (isAppended))
                    streamWriter = global::System.IO.File.AppendText(logFilePath);
                else
                {
                    streamWriter = global::System.IO.File.CreateText(logFilePath);

                    // we add the header
                    line = "";
                    line += textDelimiterValue + "Kind" + textDelimiterValue;
                    line += aFieldDelimiterValue + textDelimiterValue;
                    line += "ResultCode" + textDelimiterValue;
                    foreach (DataElement dataElement in log.Detail.Elements)
                    {
                        line += aFieldDelimiterValue;
                        line += textDelimiterValue + dataElement.Name + textDelimiterValue;
                    }
                    streamWriter.WriteLine(line);
                }

                // we add results
                foreach (LogEvent logEvent in log.Events)
                {
                    line = "";
                    line += textDelimiterValue + (logEvent.Kind == EventKind.Error ? "E" : "W").Replace(textDelimiterValue, textDelimiterValue + textDelimiterValue) + textDelimiterValue;
                    line += aFieldDelimiterValue;
                    if (logEvent is LogEvent)
                        line += textDelimiterValue + (logEvent as LogEvent).ResultCode.ToString().Replace(textDelimiterValue, textDelimiterValue + textDelimiterValue) + textDelimiterValue;

                    // we retrieve the report attributes
                    if (log.Detail != null)
                    {
                        foreach (DataElement dataElement in log.Detail.Elements)
                        {
                            line += aFieldDelimiterValue;

                            DataElement currentDataElement = null;
                            if ((logEvent.Detail != null) &&
                                ((currentDataElement = logEvent.Detail[dataElement.Name]) != null))
                            {
                                line += textDelimiterValue + currentDataElement.ToString() + textDelimiterValue;
                            }
                        }
                    }

                    streamWriter.WriteLine(line);
                }

                isWasSaved = true;
            }
            catch
            {
                isWasSaved = false;
            }
            finally
            {
                if (streamWriter != null)
                    streamWriter.Close();
            }

            return isWasSaved;
        }

        #endregion

    }
}
