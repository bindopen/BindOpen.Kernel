using BindOpen.Framework.Extensions.Runtime;
using BindOpen.Framework.System.Diagnostics;
using BindOpen.Framework.System.Diagnostics.Loggers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;

namespace BindOpen.Framework.System.Diagnostics.Loggers
{
    /// <summary>
    /// This class represents a JSON logger.
    /// </summary>
    /// <remarks>The output format is JSON.</remarks>
    public class BdoJsonLogger : BdoLogger
    {
        // ------------------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the JsonLogger class.
        /// </summary>
        public BdoJsonLogger()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the JsonLogger class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="mode">The mode to consider.</param>
        /// <param name="eventFilter">The function that filters events.</param>
        /// <remarks>With expiration day number equaling to -1, no files expires. Equaling to 0, all files except the current one expires.</remarks>
        public BdoJsonLogger(
            string name,
            BdoLoggerMode mode,
            Predicate<IBdoLogEvent> eventFilter = null)
            : base(name, BdoDefaultLoggerFormat.Json, mode, eventFilter)
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
            return Save(log.Root, Filepath);
        }

        /// <summary>
        /// Logs the specified record.
        /// </summary>
        /// <param name="logEvent">The log event to consider.</param>
        public override bool WriteEvent(
            IBdoLogEvent logEvent)
        {
            if (EventFilter?.Invoke(logEvent) != false)
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
            IBdoLog log,
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

        // Unserialization ---------------------------------

        /// <summary>
        /// Instantiates a new instance of Log class from a xml file.
        /// </summary>
        /// <param name="filePath">The path of the Xml file to load.</param>
        /// <param name="loadLog">The output log of the load task.</param>
        /// <param name="mustFileExist">Indicates whether the file must exist.</param>
        /// <returns>The load log.</returns>
        public override IBdoLog LoadLog(
            string filePath,
            IBdoLog loadLog = null,
            bool mustFileExist = true)
        {
            BdoLog log = null;
            StreamReader streamReader = null;
            try
            {
                DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(BdoLog));
                streamReader = new StreamReader(filePath);
                log = (BdoLog)
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
            log?.UpdateRuntimeInfo(null, null, loadLog);

            return log;
        }

        /// <summary>
        /// Instantiates a new instance of Log class from a xml string.
        /// </summary>
        /// <param name="xmlString">The Xml string to load.</param>
        /// <param name="loadLog">The output log of the load task.</param>
        /// <returns>The log defined in the Xml file.</returns>
        public override IBdoLog LoadLogFromString(
            string xmlString,
            IBdoLog loadLog = null)
        {
            BdoLog log = null;

            BdoLog childLoadLog = new BdoLog();

            try
            {
                DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(BdoLog));
                StringReader aStringReader = new StringReader(xmlString);
                log = (BdoLog)
                    dataContractJsonSerializer.ReadObject(XmlReader.Create(aStringReader));
            }
            catch (Exception ex)
            {
                childLoadLog.AddException(ex);
            }
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
        public override bool Save(IBdoLog log, string logFilePath, bool isAppended = false)
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
                memoryStream?.Close();

                streamWriter?.Close();
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
            IBdoLog log,
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

