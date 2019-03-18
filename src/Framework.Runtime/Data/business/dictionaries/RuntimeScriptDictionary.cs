using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using cor_base_wdl.system.script;
using cor_base_wdl.business.dictionaries;
using cor_base_wdl.system.tracking;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using System.Reflection;
using cor_base_wdl.data.serialization;
using System.Xml.Schema;
using cor_runtime_wdl.business.libraries;

namespace cor_runtime_wdl.business.dictionaries
{
    /// <summary>
    /// This class represents a dictionary of scripts.
    /// </summary>
    [Serializable()]
    [XmlType("RuntimeScriptDictionary", Namespace = "http://www.w3.org/2001/dkm.xsd")]
    [XmlRoot(ElementName = "scriptDictionary", Namespace = "http://www.w3.org/2001/dkm.xsd", IsNullable = false)]
    public class RuntimeScriptDictionary : ScriptDictionary
    {

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the RuntimeScriptDictionary class.
        /// </summary>
        public RuntimeScriptDictionary()
        {
        }

        /// <summary>
        /// Initializes a new instance of the RuntimeScriptDictionary class from the specified business library.
        /// </summary>
        /// <param name="aRuntimeDynamicBusinessDictionary">The dynamic business dictionary to consider.</param>
        /// <param name="someBusinessLibraryIds">The business library IDs to consider.</param>
        /// <param name="aIsConsiderUnbrowsableWords">Indicates whether we consider the unbrowsable words.</param>
        public RuntimeScriptDictionary(
            RuntimeDynamicBusinessDictionary aRuntimeDynamicBusinessDictionary,
            List<String> someBusinessLibraryIds,
            Boolean aIsConsiderUnbrowsableWords)
        {
            if (aRuntimeDynamicBusinessDictionary != null)
            {
                this.myScriptWords = new List<ScriptWord>();
                foreach (ScriptWord aScriptWord in aRuntimeDynamicBusinessDictionary.GetScriptWords(someBusinessLibraryIds,aIsConsiderUnbrowsableWords).Values)
                    this.myScriptWords.Add(aScriptWord.Clone());
            }
        }

        /// <summary>
        /// Initializes a new instance of the RuntimeScriptDictionary class from the specified business library.
        /// </summary>
        /// <param name="aRuntimeDynamicBusinessLibrary">The dynamic business library to consider.</param>
        public RuntimeScriptDictionary(
            RuntimeDynamicBusinessLibrary aRuntimeDynamicBusinessLibrary)
        {
            if (aRuntimeDynamicBusinessLibrary != null)
            {
                this.myScriptWords = new List<ScriptWord>();
                foreach (ScriptWord aScriptWord in aRuntimeDynamicBusinessLibrary.GetScriptWords().Values)
                    this.myScriptWords.Add(aScriptWord.Clone());
            }
        }

        #endregion


        // ------------------------------------------
        // LOADING
        // ------------------------------------------

        #region Loading

        /// <summary>
        /// Initializes a new instance of RuntimeScriptDictionary class from a xml string.
        /// </summary>
        /// <param name="aXmlString">The Xml string to load.</param>
        /// <param name="aIsCheckXml">Indicates whether the file must be checked.</param>
        /// <param name="aLoadTaskLog">The output log of the load task.</param>
        /// <returns>The task log defined in the Xml file.</returns>
        public static RuntimeScriptDictionary LoadFromXmlString(
            String aXmlString,
            Boolean aIsCheckXml,
            TaskLog aLoadTaskLog)
        {
            RuntimeScriptDictionary aRuntimeScriptDictionary = null;

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
                    XmlSerializer aXmlSerializer = new XmlSerializer(typeof(RuntimeScriptDictionary));
                    aStringReader = new StringReader(aXmlString);
                    aRuntimeScriptDictionary = (RuntimeScriptDictionary) aXmlSerializer.Deserialize(XmlReader.Create(aStringReader));
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

            return aRuntimeScriptDictionary;
        }

        #endregion

    }
}
