﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Items;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.Helpers.Serialization
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class XmlHelper
    {
        // ------------------------------------------
        // STATIC
        // ------------------------------------------

        #region Static

        // Serialization ----------------------------

        /// <summary>
        /// Saves the xml string of this instance.
        /// </summary>
        /// <param name="object1">The object1 to save.</param>
        /// <param name="log">The saving log to consider.</param>
        /// <returns>The Xml string of this instance.</returns>
        public static string ToXml(this Object object1, ILog log = null)
        {
            if (object1==null) return "";

            String st = "";
            StringWriter streamWriter = null;
            try
            {
                // if the object is a data item then we update the storage info
                if (object1 is DataItem dataItem)
                {
                    dataItem?.UpdateStorageInfo(log);
                }

                XmlSerializer xmlSerializer = new XmlSerializer(object1.GetType());
                streamWriter = new StringWriter();
                xmlSerializer.Serialize(streamWriter, object1);
                st = streamWriter.ToString();
            }
            catch (Exception ex)
            {
                log?.AddException(ex);
            }
            finally
            {
                streamWriter?.Close();
            }

            return st;
        }

        /// <summary>
        /// Saves this instance to the specified file path.
        /// </summary>
        /// <param name="object1">The object1 to save.</param>
        /// <param name="filePath">Path of the file to save.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>True if the saving operation has been done. False otherwise.</returns>
        public static bool SaveXml(this Object object1, String filePath, ILog log = null)
        {
            if (object1==null) return false;

            bool isWasSaved = false;
            StreamWriter streamWriter = null;
            try
            {
                if (!string.IsNullOrEmpty(filePath))
                {
                    // we create the folder if it does not exist
                    if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                        Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                    // if the object is a data item then we update the storage info
                    if (object1 is DataItem dataItem)
                    {
                        dataItem?.UpdateStorageInfo(log);
                    }

                    if (object1 is IAppExtensionItem appExtensionItem)
                    {
                        object1 = appExtensionItem.Configuration;
                    }

                    // we save the xml file
                    XmlSerializer xmlSerializer = new XmlSerializer(object1.GetType());
                    streamWriter = new StreamWriter(filePath, false, Encoding.UTF8);
                    xmlSerializer.Serialize(streamWriter, object1);

                    isWasSaved = true;
                }
            }
            catch(Exception exception)
            {
                log?.AddException(exception);

                isWasSaved = exception==null;
            }
            finally
            {
                streamWriter?.Close();
            }

            return isWasSaved;
        }

        // Deserialiaze ----------------------------

        /// <summary>
        /// Loads a data item from the specified file path.
        /// </summary>
        /// <param name="filePath">The path of the Xml file to load.</param>
        /// <param name="log">The output log of the method.</param>
        /// <param name="xmlSchemaSet">The XML schema set to consider for checking.</param>
        /// <param name="mustFileExist">Indicates whether the file must exist.</param>
        /// <returns>The loaded log.</returns>
        /// <remarks>If the XML schema set is null then the schema is not checked.</remarks>
        public static T Load<T>(
            String filePath,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null,
            XmlSchemaSet xmlSchemaSet = null,
            bool mustFileExist = true) where T : DataItem
        {
            T dataItem = null;
            log = (log?? new Log());

            StreamReader streamReader = null;
            if (!File.Exists(filePath))
            {
                if (mustFileExist)
                    log.AddError("File not found ('" + filePath + "'). Could not load '" + typeof(T).Name.ToString() + "' object");
            }
            else
            {
                try
                {
                    Log checkLog = new Log();
                    if (xmlSchemaSet != null)
                    {
                        XDocument xDocument = XDocument.Load(filePath);
                        xDocument.Validate(xmlSchemaSet, (o, e) =>
                        {
                            checkLog.AddError("File not valid ('" + filePath + "'). Could not load '" + typeof(T).Name.ToString() + "' object");
                        });
                        log.Append(checkLog);
                    }

                    if (!checkLog.HasErrorsOrExceptions())
                    {
                        // then we load
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                        streamReader = new StreamReader(filePath);
                        dataItem = xmlSerializer.Deserialize(XmlReader.Create(streamReader)) as T;

                        dataItem?.UpdateRuntimeInfo(appScope, scriptVariableSet, log);
                    }
                }
                catch (Exception ex)
                {
                    log.AddException(ex);
                }
                finally
                {
                    streamReader?.Close();
                }
            }

            return dataItem;
        }

        /// <summary>
        /// Loads the data item from the specified file path.
        /// </summary>
        /// <typeparam name="T">The data item class to consider.</typeparam>
        /// <param name="xmlString">The Xml string to load.</param>
        /// <param name="log">The output log of the load method.</param>
        /// <param name="xmlSchemaSet">The XML schema set to consider for checking.</param>
        /// <returns>The loaded log.</returns>
        /// <remarks>If the XML schema set is null then the schema is not checked.</remarks>
        public static T LoadFromString<T>(
            String xmlString,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null,
            XmlSchemaSet xmlSchemaSet = null) where T : DataItem
        {
            T dataItem = null;

            if (xmlString!=null)
            {
                StreamReader streamReader = null;
                try
                {
                    Log checkLog = new Log();
                    if (xmlSchemaSet != null)
                    {
                        XDocument xDocument = XDocument.Parse(xmlString);
                        xDocument.Validate(xmlSchemaSet, (o, e) =>
                        {
                            if (log != null)
                            {
                                log.AddError(
                                   title: "Xml string not valid",
                                   description: e.Message);
                            }
                        });
                        log?.Append(checkLog);
                    }

                    if (!checkLog.HasErrorsOrExceptions())
                    {
                        // then we load
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                        StringReader stringReader = new StringReader(xmlString);
                        dataItem = xmlSerializer.Deserialize(XmlReader.Create(stringReader)) as T;

                        dataItem?.UpdateRuntimeInfo(appScope, scriptVariableSet, log);
                    }
                }
                catch (Exception ex)
                {
                    log?.AddException(ex);
                }
                finally
                {
                    streamReader?.Close();
                }
            }

            return dataItem;
        }

        /// <summary>
        /// Loads the specified XML schema set.
        /// </summary>
        /// <param name="xmlSchemaSet">The XML schema set to consider.</param>
        /// <param name="assembly">The assembly to consider.</param>
        /// <param name="xsdResources">The XSD resources to consider.</param>
        /// <returns>The XML schema set.</returns>
        public static XmlSchemaSet LoadXmlSchemaSet(
            XmlSchemaSet xmlSchemaSet,
            Assembly assembly,
            List<string> xsdResources)
        {
            if (xmlSchemaSet==null)
                xmlSchemaSet = new XmlSchemaSet();
            Stream stream;
            foreach (String currentXsdResource in xsdResources)
            {
                stream = assembly.GetManifestResourceStream(currentXsdResource);
                xmlSchemaSet.Add("https://bindopen.org/xsd", XmlReader.Create(new StreamReader(stream)));
            }
            return xmlSchemaSet;
        }

        #endregion

    }
}
