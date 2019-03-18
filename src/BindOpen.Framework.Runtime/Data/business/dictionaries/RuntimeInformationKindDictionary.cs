using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using cor_base_wdl.business.dictionaries;
using cor_base_wdl.data.serialization;
using cor_base_wdl.system.tracking;
using cor_runtime_wdl.business.libraries;

namespace cor_runtime_wdl.business.dictionaries
{
    /// <summary>
    /// This class represents a dictionary of information kinds.
    /// </summary>
    [Serializable()]
    [XmlType("RuntimeInformationKindDictionary", Namespace = "http://www.w3.org/2001/dkm.xsd")]
    [XmlRoot(ElementName = "informationKindDictionary", Namespace = "http://www.w3.org/2001/dkm.xsd", IsNullable = false)]
    public class RuntimeInformationKindDictionary : InformationKindDictionary
    {


        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the RuntimeInformationKindDictionary class.
        /// </summary>
        public RuntimeInformationKindDictionary()
        {
        }

        /// <summary>
        /// Initializes a new instance of the RuntimeInformationKindDictionary class from the specified business library.
        /// </summary>
        /// <param name="aRuntimeDynamicBusinessDictionary">The dynamic business dictionary to consider.</param>
        /// <param name="someBusinessLibraryIds">The business library IDs to consider.</param>
        public RuntimeInformationKindDictionary(
            RuntimeDynamicBusinessDictionary aRuntimeDynamicBusinessDictionary,
            List<String> someBusinessLibraryIds)
        {
            if (aRuntimeDynamicBusinessDictionary != null)
                this.myInformationKinds = aRuntimeDynamicBusinessDictionary.GetInformationKinds(someBusinessLibraryIds);
        }

        /// <summary>
        /// Initializes a new instance of the RuntimeInformationKindDictionary class from the specified business library.
        /// </summary>
        /// <param name="aRuntimeDynamicBusinessLibrary">The dynamic business library to consider.</param>
        public RuntimeInformationKindDictionary(
            RuntimeDynamicBusinessLibrary aRuntimeDynamicBusinessLibrary)
        {
            if (aRuntimeDynamicBusinessLibrary != null)
                this.myInformationKinds = aRuntimeDynamicBusinessLibrary.GetInformationKinds();
        }
        #endregion


        // ------------------------------------------
        // LOADING
        // ------------------------------------------

        #region Loading

        /// <summary>
        /// Initializes a new instance of RuntimeInformationKindDictionary class from a xml string.
        /// </summary>
        /// <param name="aXmlString">The Xml string to load.</param>
        /// <param name="aIsCheckXml">Indicates whether the file must be checked.</param>
        /// <param name="aLoadTaskLog">The output log of the load entity.</param>
        /// <returns>The entity log defined in the Xml file.</returns>
        public static RuntimeInformationKindDictionary LoadFromXmlString(
            String aXmlString,
            Boolean aIsCheckXml,
            TaskLog aLoadTaskLog)
        {
            RuntimeInformationKindDictionary aRuntimeInformationKindDictionary = null;

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
                        Assembly.Load("cor_runtime_wdl"),
                        new List<String>()
                        {
                            "cor_base_wdl.data.xsd.TaskLog.xsd",
                            "cor_base_wdl.data.xsd.TaskResult.xsd",
                            "cor_base_wdl.data.xsd.Entity.xsd",
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
                    XmlSerializer aXmlSerializer = new XmlSerializer(typeof(RuntimeInformationKindDictionary));
                    aStringReader = new StringReader(aXmlString);
                    aRuntimeInformationKindDictionary = (RuntimeInformationKindDictionary) aXmlSerializer.Deserialize(XmlReader.Create(aStringReader));
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

            return aRuntimeInformationKindDictionary;
        }

        #endregion

    }
}
