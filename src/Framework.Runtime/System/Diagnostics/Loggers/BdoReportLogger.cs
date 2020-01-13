using BindOpen.Framework.Data.Elements;
using BindOpen.Framework.Extensions.Runtime;
using BindOpen.Framework.System.Diagnostics;
using BindOpen.Framework.System.Diagnostics.Events;
using BindOpen.Framework.System.Diagnostics.Loggers;
using System;
using System.IO;

namespace BindOpen.Framework.System.Diagnostics.Loggers
{
    /// <summary>
    /// This class represents a YAML logger.
    /// </summary>
    /// <remarks>The output format is YAML.</remarks>
    public class BdoReportLogger : BdoLogger
    {
        // ------------------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ReportLogger class.
        /// </summary>
        public BdoReportLogger()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the ReportLogger class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="mode">The mode to consider.</param>
        /// <param name="eventFilter">The function that filters events.</param>
        /// <remarks>With expiration day number equaling to -1, no files expires. Equaling to 0, all files except the current one expires.</remarks>
        public BdoReportLogger(
            string name,
            BdoLoggerMode mode,
            Predicate<IBdoLogEvent> eventFilter = null) :
            base(name, BdoDefaultLoggerFormat.Report, mode, eventFilter)
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
        public override bool WriteTask(IBdoLog log, IBdoTaskConfiguration task)
        {
            return false;
        }

        /// <summary>
        /// Logs the specified record.
        /// </summary>
        /// <param name="logEvent">The log event to consider.</param>
        public override bool WriteEvent(
            IBdoLogEvent logEvent)
        {
            if (EventFilter == null || EventFilter.Invoke(logEvent))
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
        public override bool WriteDetailElement(
            IBdoLog log,
            string elementName,
            object elementValue)
        {
            return false;
        }

        /// <summary>
        /// Logs the specified record.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <param name="childLog">The child log to log.</param>
        public override bool WriteChildLog(
            IBdoLog log,
            IBdoLog childLog)
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
        public override bool Save(IBdoLog log, String logFilePath, bool isAppended = false)
        {
            if (log == null) return false;

            log.UpdateStorageInfo();

            StreamWriter streamWriter = null;
            bool isWasSaved = false;
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
                foreach (IBdoLogEvent logEvent in log.Events)
                {
                    line = "";
                    line += textDelimiterValue + (logEvent.Kind == EventKinds.Error ? "E" : "W").Replace(textDelimiterValue, textDelimiterValue + textDelimiterValue) + textDelimiterValue;
                    line += aFieldDelimiterValue;
                    line += textDelimiterValue + logEvent.ResultCode.ToString().Replace(textDelimiterValue, textDelimiterValue + textDelimiterValue) + textDelimiterValue;

                    // we retrieve the report attributes
                    if (log.Detail != null)
                    {
                        foreach (IDataElement dataElement in log.Detail.Elements)
                        {
                            line += aFieldDelimiterValue;

                            IDataElement currentElement = null;
                            if ((logEvent.Detail != null)
                                && ((currentElement = logEvent.Detail[dataElement.Name]) != null))
                            {
                                line += textDelimiterValue + currentElement.ToString() + textDelimiterValue;
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
