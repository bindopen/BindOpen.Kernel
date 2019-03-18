using bdo.core.data.items;
using bdo.core.system.diagnostics;
using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace bdo.runtime.data.io
{

    /// <summary>
    /// This class represents an import/export manager.
    /// </summary>
    public static class ImportExportManager
    {

        // ------------------------------------------
        // IMPORT
        // ------------------------------------------

        #region Import

        /// <summary>
        /// Imports the specified file.
        /// </summary>
        /// <param name="filePath">The file path to consider.</param>
        /// <param name="object1">The object that is imported to.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The refered settings object of the specified information configuration.</returns>
        public static Object Import(
            String filePath,
            Object object1,
            Log log)
        {
            Object importObject = null;
            StreamReader streamReader = null;
            try
            {
                streamReader = new StreamReader(filePath);
                String xmlString = streamReader.ReadToEnd();

                // we parse the xml string
                XDocument xDocument = XDocument.Parse(xmlString);
                
                // then we load
                XmlSerializer xmlSerializer = new XmlSerializer(object1.GetType());
                StringReader aStringReader = new StringReader(xmlString);
                importObject = (DataItem)xmlSerializer.Deserialize(XmlReader.Create(aStringReader));

                if (importObject != null)
                {
                    System.Reflection.MethodInfo methodInfo = object1.GetType().GetMethod(
                        "Import",
                        BindingFlags.Public | BindingFlags.FlattenHierarchy,
                        null, new Type[1] { typeof(Object) }, null);
                    if (methodInfo != null)
                        log.AddEvents(methodInfo.Invoke(object1, new Object[1] { importObject }) as Log);
                    else
                        log.AddError(
                            "Import function missing in object type ('" + object1.GetType().ToString() + "')");
                }
            }
            catch(Exception ex)
            {
                if (log != null)
                    log.AddException(ex);
            }
            finally
            {
                if (streamReader != null)
                    streamReader.Close();
            }

            return object1;
        }

        #endregion


        // ------------------------------------------
        // EXPORT
        // ------------------------------------------

        #region Export

        /// <summary>
        /// Imports the specified file.
        /// </summary>
        /// <param name="filePath">The file path to consider.</param>
        /// <param name="object1">The object type to export.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The refered settings object of the specified information configuration.</returns>
        public static Boolean Export(
            String filePath,
            Object object1,
            Log log)
        {
            Boolean isExported = false;
            StreamWriter streamWriter = null;
            try
            {
                // we create the folder if it does not exist
                if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                // we save the xml file
                XmlSerializer xmlSerializer = new XmlSerializer(object1.GetType());
                streamWriter = new StreamWriter(filePath, false, System.Text.Encoding.UTF8);
                xmlSerializer.Serialize(streamWriter, object1);
                isExported = true;
            }
            catch (Exception ex)
            {
                if (log != null)
                    log.AddException(ex);
            }
            finally
            {
                if (streamWriter != null)
                    streamWriter.Close();
            }

            return isExported;
        }

        #endregion

    }
}