using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Items.Tasks;

namespace BindOpen.Framework.Core.System.Diagnostics.Loggers
{
    /// <summary>
    /// This class represents a JSON logger.
    /// </summary>
    /// <remarks>The output format is JSON.</remarks>
    public class JsonLogger : Logger
    {
        // ------------------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the JsonLogger class.
        /// </summary>
        public JsonLogger()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the JsonLogger class.
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
        public JsonLogger(
            String name,
            LoggerMode mode,
            String folderPath,
            String fileName = null,
            DataSourceKind outputKind = DataSourceKind.Repository,
            bool isVerbose = false,
            String uiCulture = null,
            Predicate<ILogEvent> eventFinder = null,
            int expirationDayNumber = -1)
            : base(name, LogFormat.Json, mode, outputKind, isVerbose, uiCulture, folderPath, fileName, eventFinder, expirationDayNumber)
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
            ILog log, ITaskDto task)
        {
            return this.Save(log.Root, this.Filepath);
        }

        /// <summary>
        /// Logs the specified record.
        /// </summary>
        /// <param name="logEvent">The log event to consider.</param>
        public override bool WriteEvent(
            ILogEvent logEvent)
        {
            if (this.EventFinder == null || this.EventFinder.Invoke(logEvent))
                return this.Save(logEvent?.Root, this.Filepath);
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
            string elementName,
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
            Log log = null;
            StreamReader streamReader = null;
            try
            {
                DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(Log));
                streamReader = new StreamReader(filePath);
                log = (Log)
                    dataContractJsonSerializer.ReadObject(XmlReader.Create(streamReader));
            }
            catch (Exception ex)
            {
                loadLog.AddException(ex);
            }
            finally
            {
                streamReader?.Close();
            }
            log?.UpdateRuntimeInfo(loadLog);

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
            Log log = null;

            Log childLoadLog = new Log();

            try
            {
                DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(Log));
                StringReader aStringReader = new StringReader(xmlString);
                log = (Log)
                    dataContractJsonSerializer.ReadObject(XmlReader.Create(aStringReader));
            }
            catch (Exception ex)
            {
                childLoadLog.AddException(ex);
            }
            if (log != null) log.UpdateRuntimeInfo(loadLog);

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
            if (log == null) return false;

            log.UpdateStorageInfo();

            bool isWasSaved = false;
            MemoryStream memoryStream = null;
            StreamWriter streamWriter = null;
            try
            {
                // we create the folder if it does not exist
                if (!Directory.Exists(Path.GetDirectoryName(logFilePath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(logFilePath));

                // we save the xml file
                DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(log.GetType());                
                streamWriter = new StreamWriter(logFilePath, false, Encoding.UTF8);

                memoryStream = new MemoryStream();
                dataContractJsonSerializer.WriteObject(memoryStream, log);

                streamWriter.Write(Encoding.UTF8.GetString(memoryStream.ToArray()));
                isWasSaved = true;
            }
            catch (Exception ex)
            {
                String st = ex.ToString();
                isWasSaved = false;
            }
            finally
            {
                if (memoryStream != null)
                    memoryStream.Close();

                if (streamWriter != null)
                    streamWriter.Close();
            }

            return isWasSaved;
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
            if (log == null) return "";

            String st = "";
            MemoryStream memoryStream = null;
            try
            {
                DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(log.GetType());

                memoryStream = new MemoryStream();
                dataContractJsonSerializer.WriteObject(memoryStream, log);

                st = Encoding.UTF8.GetString(memoryStream.ToArray());
            }
            catch
            {
            }
            finally
            {
                if (memoryStream != null)
                    memoryStream.Close();
            }

            return st;
        }

        #endregion
    }
}

