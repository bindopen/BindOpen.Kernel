using cor_base_wdl.business.dictionaries;
using cor_base_wdl.business.libraries;
using cor_base_wdl.data;
using cor_base_wdl.data.references;
using cor_base_wdl.data.serialization;
using cor_base_wdl.system.tracking;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;


namespace cor_runtime_wdl.business.universe
{

    /// <summary>
    /// This class represents a runtime cenral business dictionary that defines business libraries.
    /// </summary>
    [Serializable()]
    [XmlType("RuntimeCentralBusinessDictionary", Namespace = "http://www.w3.org/2001/dkm.xsd")]
    [XmlRoot(ElementName = "centralBusinessDictionary", Namespace = "http://www.w3.org/2001/dkm.xsd", IsNullable = false)]
    public class RuntimeCentralBusinessDictionary : BusinessLibraryIndexation
    {


        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of RuntimeCentralBusinessDictionary class.
        /// </summary>
        public RuntimeCentralBusinessDictionary()
        {
        }

        #endregion


        // ------------------------------------------
        // LOADING
        // ------------------------------------------

        #region Loading

        /// <summary>
        /// Gets the dictionary of this instance from embed resource.
        /// </summary>
        /// <returns>The assembly of this instance.</returns>
        protected static RuntimeCentralBusinessDictionary GetDictionaryFromEmbedResource()
        {
            RuntimeCentralBusinessDictionary aCentralBusinessDictionary = null;

            Assembly aAssembly = Assembly.Load("cor_runtime_wdl");
            if (aAssembly != null)
            {
                Stream aStream = null;
                try
                {
                    aStream = aAssembly.GetManifestResourceStream("cor_runtime_wdl.business.universe.CentralBusinessDictionary.xml");
                    XmlSerializer aXmlSerializer = new XmlSerializer(typeof(RuntimeCentralBusinessDictionary));
                    aCentralBusinessDictionary =
                        (RuntimeCentralBusinessDictionary)aXmlSerializer.Deserialize(aStream);
                }
                finally
                {
                    if (aStream != null)
                        aStream.Close();
                }
            }

            return aCentralBusinessDictionary;
        }

        /// <summary>
        /// Gets the dictionaries of this instance from folder.
        /// </summary>
        /// <param name="aFolderPath">The path of the folder to consider.</param>
        /// <returns>The assembly of this instance.</returns>
        protected void LoadDictionariesFromFolder(String aFolderPath)
        {
            if (string.IsNullOrEmpty(aFolderPath))
                return;

            if (Directory.Exists(aFolderPath))
                foreach (String aFilePath in Directory.GetFiles(aFolderPath, "*.dll"))
                {
                    BusinessLibrary aBusinessLibrary = BusinessLibrary.Create(aFilePath);
                    if ((aBusinessLibrary != null)&&(this.GetBusinessLibraryWithName(aBusinessLibrary.Name)!=null))
                        this.BusinessLibraries.Add(aBusinessLibrary);
                }
        }

        /// <summary>
        /// Gets the dictionary of this instance from file path.
        /// </summary>
        /// <returns>The dictionary of this instance.</returns>
        protected static RuntimeCentralBusinessDictionary Load(String aFilePath)
        {
            RuntimeCentralBusinessDictionary aCentralBusinessDictionary = null;

            if (aFilePath == null)
                return null;

            if ((aFilePath != null) &&
                (System.IO.File.Exists(aFilePath)))
            {
                StreamReader aStreamReader = null;
                try
                {
                    aStreamReader = new StreamReader(aFilePath);

                    XmlSerializer aXmlSerializer =
                        new XmlSerializer(typeof(RuntimeCentralBusinessDictionary));
                    aCentralBusinessDictionary =
                        (RuntimeCentralBusinessDictionary)aXmlSerializer.Deserialize(aStreamReader);
                }
                finally
                {
                    if (aStreamReader != null)
                        aStreamReader.Close();
                }
            }

            return aCentralBusinessDictionary;
        }

        /// <summary>
        /// Loads the central business dictionary.
        /// </summary>
        /// <param name="aTaskLog">The log of the task of loading.</param>
        /// <param name="someDataSourceKinds">The data loading sources to consider.</param>
        /// <param name="aLibFolderPath">The path of the app_lib folder.</param>
        /// <returns>Returns the central business dictionary.</returns>
        public static RuntimeCentralBusinessDictionary Load(
            TaskLog aTaskLog,
            List<InformationReferenceSourceKind> someDataSourceKinds = null,
            String aLibFolderPath = null)
        {
            RuntimeCentralBusinessDictionary aCentralBusinessDictionary = new RuntimeCentralBusinessDictionary();
            
            if (someDataSourceKinds == null)
                someDataSourceKinds = new List<InformationReferenceSourceKind>() 
                { 
                    InformationReferenceSourceKind.EmbedResource,
                    InformationReferenceSourceKind.XmlFile
                };

            TaskLog aChildTaskLog = new TaskLog(TaskLog.LogMode.Manual);
            try
            {
                if (someDataSourceKinds.Contains(InformationReferenceSourceKind.EmbedResource))
                    aCentralBusinessDictionary = RuntimeCentralBusinessDictionary.GetDictionaryFromEmbedResource();
                if ((aCentralBusinessDictionary != null) & (someDataSourceKinds.Contains(InformationReferenceSourceKind.XmlFile)))
                    aCentralBusinessDictionary.LoadDictionariesFromFolder(aLibFolderPath);
            }
            catch (Exception aException)
            {
                aChildTaskLog.AddTaskException(
                    aException,
                    TaskResult.TaskResultCriticality.High,
                    ""
                    );
            }
            if (aChildTaskLog.HasTaskErrorsOrExceptionsOrWarnings())
                aTaskLog.AddTaskResults(aChildTaskLog);

            return aCentralBusinessDictionary;
        }

        /// <summary>
        /// Initializes a new instance of RuntimeCentralBusinessDictionary class from a xml string.
        /// </summary>
        /// <param name="aXmlString">The Xml string to load.</param>
        /// <param name="aIsCheckXml">Indicates whether the file must be checked.</param>
        /// <param name="aLoadTaskLog">The output log of the load task.</param>
        /// <returns>The task log defined in the Xml file.</returns>
        public static RuntimeCentralBusinessDictionary LoadFromXmlString(
            String aXmlString,
            Boolean aIsCheckXml,
            TaskLog aLoadTaskLog)
        {
            RuntimeCentralBusinessDictionary aRuntimeCentralBusinessDictionary = null;

            TaskLog aChildLoadTaskLog = new TaskLog(TaskLog.LogMode.Manual);

            StringReader aStringReader = null;
            try
            {
                // we check it
                if (aIsCheckXml)
                {
                    // we parse the xml string
                    XDocument aXDocument = XDocument.Parse(aXmlString);

                    XmlSchemaSet aXmlSchemaSet = null;
                    XsdLoader.LoadXmlSchemaSet(
                        aXmlSchemaSet,
                        Assembly.Load("cor_base_wdl"),
                        new List<String>()
                        {
                            "cor_base_wdl.data.xsd.TaskLog.xsd",
                            "cor_base_wdl.data.xsd.TaskResult.xsd",
                            "cor_base_wdl.data.xsd.Task.xsd",
                            "cor_base_wdl.data.xsd.GlobalObjectAttribute.xsd",
                            "cor_base_wdl.data.xsd.GlobalObjectAttributeValue.xsd",
                            "cor_base_wdl.data.xsd.ObjectDetail.xsd",
                            "cor_base_wdl.data.xsd.ObjectAttribute.xsd",
                            "cor_base_wdl.data.xsd.ObjectAttributeValueType.xsd"
                        });
                    aXDocument.Validate(aXmlSchemaSet, (o, e) =>
                    {
                        aChildLoadTaskLog.AddTaskError(
                            "Xml String not valid.",
                            TaskResult.TaskResultCriticality.High,
                            "",
                            e.Message
                            );
                    });
                }


                if (!aChildLoadTaskLog.HasTaskErrorsOrExceptionsOrWarnings())
                {
                    // then we load
                    XmlSerializer aXmlSerializer = new XmlSerializer(typeof(TaskLog));
                    aStringReader = new StringReader(aXmlString);
                    aRuntimeCentralBusinessDictionary = (RuntimeCentralBusinessDictionary)
                        aXmlSerializer.Deserialize(XmlReader.Create(aStringReader));
                }
            }
            catch (Exception ex)
            {
                aChildLoadTaskLog.AddTaskException(
                    ex,
                    TaskResult.TaskResultCriticality.High,
                    ""
                    );
            }
            finally
            {
                if (aStringReader != null)
                    aStringReader.Close();
            }
            if (aLoadTaskLog != null)
                aLoadTaskLog.AddTaskResults(aChildLoadTaskLog);

            return aRuntimeCentralBusinessDictionary;
        }

        #endregion

    }
}
