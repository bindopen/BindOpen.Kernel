using cor_base_wdl.business.libraries.dictionaries;
using cor_base_wdl.business.libraries;
using cor_base_wdl.business.metrics;
using cor_base_wdl.business.tasks;
using cor_base_wdl.data.connectors;
using cor_base_wdl.data.information;
using cor_base_wdl.data.documents.format;
using cor_base_wdl.data.information._class;
using cor_base_wdl.data.references;
using cor_base_wdl.system.assembly;
using cor_base_wdl.system.script;
using cor_base_wdl.system.logging;
using cor_runtime_wdl.business.universe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace cor_runtime_wdl.business.libraries
{

    /// <summary>
    /// This class represents a runtime dynamic library.
    /// </summary>
    [Serializable()]
    public class RuntimeDynamicBusinessLibrary : BusinessLibrary
    {

        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private RuntimeExtensionHandler myExtensionHandler = null;

        #endregion


        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of RuntimeDynamicBusinessLibrary class.
        /// </summary>
        public RuntimeDynamicBusinessLibrary()
        {
        }

        /// <summary>
        /// Instantiates a new instance of RuntimeDynamicBusinessLibrary class.
        /// </summary>
        /// <param name="aRuntimeExtensionHandler">The runtime extension handler to consider.</param>
        /// <param name="aId">The ID to consider.</param>
        public RuntimeDynamicBusinessLibrary(RuntimeExtensionHandler aRuntimeExtensionHandler, String aId)
        {
            this.myExtensionHandler = aRuntimeExtensionHandler;
            this.Id = aId;
        }

        /// <summary>
        /// Instantiates a new instance of RuntimeDynamicBusinessLibrary class.
        /// </summary>
        /// <param name="aRuntimeExtensionHandler">The runtime extension handler to consider.</param>
        /// <param name="aId">The ID to consider.</param>
        /// <param name="aAssembly">The assembly to consider.</param>
        public RuntimeDynamicBusinessLibrary(RuntimeExtensionHandler aRuntimeExtensionHandler, String aId, String aAssembly)
        {
            this.myExtensionHandler = aRuntimeExtensionHandler;
            this.Id = aId;
            this.AssemblyName = aAssembly;
        }

        /// <summary>
        /// Instantiates a new instance of RuntimeDynamicBusinessLibrary class from a library instance.
        /// </summary>
        /// <param name="aRuntimeExtensionHandler">The runtime extension handler to consider.</param>
        /// <param name="aBusinessLibrary">The library to consider.</param>
        /// <param name="aLibFolderPath">The path of the app_lib folder.</param>
        public RuntimeDynamicBusinessLibrary(
            RuntimeExtensionHandler aRuntimeExtensionHandler,
            BusinessLibraryDefinition aBusinessLibrary, String aLibFolderPath = null):
            base(aBusinessLibrary,aLibFolderPath)
        {
            this.myExtensionHandler = aRuntimeExtensionHandler;
        }

        #endregion


        // ------------------------------------------
        // LOADING
        // ------------------------------------------

        #region Loading

        /// <summary>
        /// Loads the business tasks and script words defined in the central extension handler.
        /// </summary>
        /// <param name="someBusinessObjectKinds">The business object kinds to consider.</param>
        /// <param name="someDataSourceKinds">The data loading sources to consider.</param>
        /// <returns>The log of the load task.</returns>
        public Log Load(
            List<BusinessLibraryElementKind> someBusinessObjectKinds,
            List<DataBindingKind> someDataSourceKinds = null)
        {
            Log aLog = new Log();

            if (this.myExtensionHandler == null)
                return aLog;

            if (someBusinessObjectKinds == null)
                someBusinessObjectKinds = new List<BusinessLibraryElementKind>() { BusinessLibraryElementKind.Any };

            if (someDataSourceKinds == null)
                someDataSourceKinds = new List<DataBindingKind>() 
                { 
                    DataBindingKind.EmbedResource,
                    DataBindingKind.XmlFile
                };

            // we initialize
            this.myScriptWords = new Dictionary<DynamicScriptWordKey, ScriptWord>();
            this.myTaskDefinitions = new List<TaskDefinition>();
            this.myClassDefinitions = new List<ClassDefinition>();
            this.myConnectorDefinitions = new List<ConnectorDefinition>();
            this.myBusinessMetrics = new List<MetricsDefinition>();

            try
            {
                // we load the central extension handler libraries
                System.Reflection.Assembly aAssembly = null;
                Stream aStream = null;
                XmlSerializer aXmlSerializer = null;

                // we feach the libraries
                this.mySourceKind = DataBindingKind.None;

                aAssembly = null;

                // first we load the using assemblies
                foreach (String aUsingAssemblyFileName in this.myUsingAssemblyFileNames)
                    System.Reflection.Assembly.LoadFrom(this.myLibraryFolderPath + aUsingAssemblyFileName);

                // we determine the location of this instance.
                foreach (DataBindingKind aCurrentDataSourceKind in someDataSourceKinds)
                {
                    try
                    {
                        ScriptDictionary aScriptDictionary = null;
                        TaskDictionary aTaskDictionary = null;
                        ClassDictionary aClassDictionary = null;
                        DataConnectorDictionary aConnectorDictionary = null;
                        MetricsDictionary aMetricsDictionary = null;

                        switch (aCurrentDataSourceKind)
                        {
                            case DataBindingKind.EmbedResource:
                            case DataBindingKind.XmlFile:
                                if (aCurrentDataSourceKind == DataBindingKind.EmbedResource)
                                    aAssembly = AppDomainPool.LoadAssembly(this.myExtensionHandler.AppDomain, this.AssemblyName);
                                else if (aCurrentDataSourceKind == DataBindingKind.XmlFile)
                                    aAssembly = System.Reflection.Assembly.LoadFrom(this.myLibraryFolderPath + this.FileName);

                                if (aAssembly != null)
                                {
                                    // we load the script words
                                    if ((someBusinessObjectKinds.Contains(BusinessLibraryElementKind.Any)) |
                                        (someBusinessObjectKinds.Contains(BusinessLibraryElementKind.ScriptWord)))
                                    {
                                        aStream = aAssembly.GetManifestResourceStream(
                                                 this.AssemblyName +
                                                 ".definition.script.dico_" + this.Name.ToLower() + ".xml"
                                                 );
                                        if (aStream != null)
                                        {
                                            aXmlSerializer = new XmlSerializer(typeof(ScriptDictionary));
                                            aScriptDictionary = (ScriptDictionary)aXmlSerializer.Deserialize(aStream);
                                            if ((String.IsNullOrEmpty(aScriptDictionary.BusinessLibraryId) || (String.IsNullOrEmpty(this.Id)) || (aScriptDictionary.BusinessLibraryId.ToLower() != this.Id.ToLower())) &&
                                                (String.IsNullOrEmpty(aScriptDictionary.BusinessLibraryName) || (String.IsNullOrEmpty(this.Name)) || (aScriptDictionary.BusinessLibraryName.ToLower() != this.Name.ToLower())))
                                                aScriptDictionary = null;
                                        }
                                    }

                                    // we load the business tasks
                                    if ((someBusinessObjectKinds.Contains(BusinessLibraryElementKind.Any)) |
                                        (someBusinessObjectKinds.Contains(BusinessLibraryElementKind.Task)))
                                    {
                                        aStream = aAssembly.GetManifestResourceStream(
                                            this.AssemblyName +
                                            ".definition.tasks.dico_" + this.Name.ToLower() + ".xml"
                                            );
                                        if (aStream != null)
                                        {
                                            aXmlSerializer = new XmlSerializer(typeof(TaskDictionary));
                                            aTaskDictionary = (TaskDictionary)aXmlSerializer.Deserialize(aStream);
                                            if ((String.IsNullOrEmpty(aTaskDictionary.BusinessLibraryId) || (String.IsNullOrEmpty(this.Id)) || (aTaskDictionary.BusinessLibraryId.ToLower() != this.Id.ToLower())) &&
                                                (String.IsNullOrEmpty(aTaskDictionary.BusinessLibraryName) || (String.IsNullOrEmpty(this.Name)) || (aTaskDictionary.BusinessLibraryName.ToLower() != this.Name.ToLower())))
                                                aTaskDictionary = null;
                                        }
                                    }

                                    // we load the information kinds
                                    if ((someBusinessObjectKinds.Contains(BusinessLibraryElementKind.Any)) |
                                        (someBusinessObjectKinds.Contains(BusinessLibraryElementKind.DataClass)))
                                    {
                                        aStream = aAssembly.GetManifestResourceStream(
                                            this.AssemblyName +
                                            ".definition.classes.dico_" + this.Name.ToLower() + ".xml"
                                            );
                                        if (aStream != null)
                                        {
                                            aXmlSerializer = new XmlSerializer(typeof(ClassDictionary));
                                            aClassDictionary = (ClassDictionary)aXmlSerializer.Deserialize(aStream);
                                            if ((String.IsNullOrEmpty(aClassDictionary.BusinessLibraryId) || (String.IsNullOrEmpty(this.Id)) || (aClassDictionary.BusinessLibraryId.ToLower() != this.Id.ToLower())) &&
                                                (String.IsNullOrEmpty(aClassDictionary.BusinessLibrary) || (String.IsNullOrEmpty(this.Name)) || (aClassDictionary.BusinessLibrary.ToLower() != this.Name.ToLower())))
                                                aClassDictionary = null;
                                        }
                                    }

                                    // we load the connectors
                                    if ((someBusinessObjectKinds.Contains(BusinessLibraryElementKind.Any)) |
                                        (someBusinessObjectKinds.Contains(BusinessLibraryElementKind.Connector)))
                                    {
                                        aStream = aAssembly.GetManifestResourceStream(
                                            this.AssemblyName +
                                            ".definition.connections.dico_" + this.Name.ToLower() + ".xml"
                                            );
                                        if (aStream != null)
                                        {
                                            aXmlSerializer = new XmlSerializer(typeof(DataConnectorDictionary));
                                            aConnectorDictionary = (DataConnectorDictionary)aXmlSerializer.Deserialize(aStream);
                                            if ((String.IsNullOrEmpty(aConnectorDictionary.BusinessLibraryId) || (String.IsNullOrEmpty(this.Id)) || (aConnectorDictionary.BusinessLibraryId.ToLower() != this.Id.ToLower())) &&
                                                (String.IsNullOrEmpty(aConnectorDictionary.BusinessLibraryName) || (String.IsNullOrEmpty(this.Name)) || (aConnectorDictionary.BusinessLibraryName.ToLower() != this.Name.ToLower())))
                                                aConnectorDictionary = null;
                                        }
                                    }

                                    // we load the business metrics
                                    if ((someBusinessObjectKinds.Contains(BusinessLibraryElementKind.Any)) |
                                        (someBusinessObjectKinds.Contains(BusinessLibraryElementKind.Metrics)))
                                    {
                                        aStream = aAssembly.GetManifestResourceStream(
                                            this.AssemblyName +
                                            ".definition.metrics.dico_" + this.Name.ToLower() + ".xml"
                                            );
                                        if (aStream != null)
                                        {
                                            aXmlSerializer = new XmlSerializer(typeof(MetricsDictionary));
                                            aMetricsDictionary = (MetricsDictionary)aXmlSerializer.Deserialize(aStream);
                                            if ((String.IsNullOrEmpty(aMetricsDictionary.BusinessLibraryId) || (String.IsNullOrEmpty(this.Id)) || (aMetricsDictionary.BusinessLibraryId.ToLower() != this.Id.ToLower())) &&
                                                (String.IsNullOrEmpty(aMetricsDictionary.BusinessLibraryName) || (String.IsNullOrEmpty(this.Name)) || (aMetricsDictionary.BusinessLibraryName.ToLower() != this.Name.ToLower())))
                                                aMetricsDictionary = null;
                                        }
                                    }

                                    // we fills the information
                                    this.Fill(
                                        aScriptDictionary, 
                                        aTaskDictionary, 
                                        aClassDictionary,
                                        aConnectorDictionary,
                                        aMetricsDictionary);

                                    this.mySourceKind = aCurrentDataSourceKind;
                                    this.Assembly = aAssembly;
                                    break;
                                }

                                break;
                            case DataBindingKind.WebService:

                                this.Load_WebService(aLog);
                                this.mySourceKind = DataBindingKind.WebService;

                                break;
                        }
                    }
                    catch (Exception aException)
                    {
                        aLog.AddCheckpoint(
                            "Loading the definition of the library called '" + this.Name + "'.",
                            EventCriticality.None,
                            ""
                            );
                        aLog.AddException(
                            aException,
                            EventCriticality.High,
                            ""
                            );
                    }
                    finally
                    {
                        if (aStream != null)
                            aStream.Close();
                    }

                    if (this.mySourceKind != DataBindingKind.None)
                        break;
                }

                // if we have no assembly
                if (this.mySourceKind == DataBindingKind.None)
                    aLog.AddWarning(
                        "Business library (ID='" + this.Id + "') could not be retrieved.",
                        EventCriticality.High,
                        "");
            }
            catch (Exception aException)
            {
                aLog.AddException(
                    aException,
                    EventCriticality.High,
                    ""
                    );
            }

            return aLog;
        }


        /// <summary>
        /// Loads the script dictionary from the web service.
        /// </summary>
        /// <param name="aLog">The log to consider.</param>
        protected virtual void Load_WebService(
            Log aLog)
        {
        }

        /// <summary>
        /// Fills the scripts, entities and tasks using dictionaries.
        /// </summary>
        /// <param name="aScriptDictionary">The script dictionary to consider.</param>
        /// <param name="aTaskDictionary">The business task dictionary to consider.</param>
        /// <param name="aClassDictionary">The class definition dictionary to consider.</param>
        /// <param name="aConnectorDictionary">The connector dictionary to consider.</param>
        /// <param name="aMetricsDictionary">The business metrics dictionary to consider.</param>
        /// <returns>The log of the load task.</returns>
        public void Fill(
            ScriptDictionary aScriptDictionary,
            TaskDictionary aTaskDictionary,
            ClassDictionary aClassDictionary,
            DataConnectorDictionary aConnectorDictionary,
            MetricsDictionary aMetricsDictionary)
        {
            // we rebuild the script tree
            this.myScriptWords.Clear();
            if (aScriptDictionary != null)
                this.ReBuildScriptTree(aScriptDictionary);

            // we add the business tasks
            this.myTaskDefinitions.Clear();
            if (aTaskDictionary != null)
                foreach (TaskDefinition aCurrentBusinessTask in aTaskDictionary.Definitions)
                {
                    aCurrentBusinessTask.BusinessLibraryName = this.Name;
                    aCurrentBusinessTask.UniqueName = aCurrentBusinessTask.BusinessLibraryName + "$" + aCurrentBusinessTask.Name;
                    if (!aCurrentBusinessTask.Class.Contains(".tasks."))
                        aCurrentBusinessTask.Class = this.AssemblyName +
                            ".tasks." +
                            aCurrentBusinessTask.Class;
                    if (!aCurrentBusinessTask.Class.Contains(","))
                        aCurrentBusinessTask.Class += "," + this.AssemblyName;
                    this.myTaskDefinitions.Add(aCurrentBusinessTask);
                }

            // we add the information kinds
            this.myClassDefinitions.Clear();
            if (aClassDictionary != null)
                foreach (ClassDefinition aCurrentClassDefinition in aClassDictionary.Definitions)
                {
                    aCurrentClassDefinition.BusinessLibraryName = this.Name;
                    aCurrentClassDefinition.UniqueName = aCurrentClassDefinition.BusinessLibraryName + "$" + aCurrentClassDefinition.Name;
                    if (aCurrentClassDefinition.ClassName != null)
                    {
                        if (!aCurrentClassDefinition.ClassName.Contains(".informationKinds."))
                            aCurrentClassDefinition.ClassName = this.AssemblyName +
                                ".informationKinds.schemas." +
                                aCurrentClassDefinition.ClassName;
                        if (!aCurrentClassDefinition.ClassName.Contains(","))
                            aCurrentClassDefinition.ClassName += "," + this.AssemblyName;
                    }
                    this.myClassDefinitions.Add(aCurrentClassDefinition);

                    foreach (FormatIndexation aCurrentInformationFormat in aCurrentClassDefinition.Formats)
                    {
                        aCurrentInformationFormat.BusinessLibraryName = this.Name;
                        aCurrentInformationFormat.UniqueName = aCurrentClassDefinition.UniqueName + "$" + aCurrentInformationFormat.Name;
                        if (aCurrentInformationFormat.ClassName != null)
                        {
                            if (!aCurrentInformationFormat.ClassName.Contains(".informationKinds."))
                                aCurrentInformationFormat.ClassName = this.AssemblyName +
                                    ".informationKinds.settings." +
                                    aCurrentInformationFormat.ClassName;
                            if (!aCurrentInformationFormat.ClassName.Contains(","))
                                aCurrentInformationFormat.ClassName += "," + this.AssemblyName;
                        }
                    }
                }

            // we add the connectors
            this.myConnectorDefinitions.Clear();
            if (aConnectorDictionary != null)
                foreach (ConnectorDefinition aCurrentConnectorDefinition in aConnectorDictionary.Definitions)
                {
                    aCurrentConnectorDefinition.BusinessLibraryName = this.Name;
                    aCurrentConnectorDefinition.UniqueName = aCurrentConnectorDefinition.BusinessLibraryName + "$" + aCurrentConnectorDefinition.Name;
                    if (aCurrentConnectorDefinition.ParameterStatementName!= null)
                    {
                        if (!aCurrentConnectorDefinition.ParameterStatementName.Contains(".connectorDefinitions."))
                            aCurrentConnectorDefinition.ParameterStatementName= this.AssemblyName +
                                ".connectorDefinitions." +
                                aCurrentConnectorDefinition.ParameterStatementName;
                        if (!aCurrentConnectorDefinition.ParameterStatementName.Contains(","))
                            aCurrentConnectorDefinition.ParameterStatementName+= "," + this.AssemblyName;
                    }
                    this.myConnectorDefinitions.Add(aCurrentConnectorDefinition);
                }

            // we add the business metricss
            this.myBusinessMetrics.Clear();
            if (aMetricsDictionary != null)
                foreach (MetricsDefinition aCurrentBusinessMetrics in aMetricsDictionary.Definitions)
                {
                    aCurrentBusinessMetrics.BusinessLibraryName = this.Name;
                    aCurrentBusinessMetrics.UniqueName = aCurrentBusinessMetrics.BusinessLibraryName + "$" + aCurrentBusinessMetrics.Name;
                    this.myBusinessMetrics.Add(aCurrentBusinessMetrics);
                }
        }

        /// <summary>
        /// Loads the business tasks and script words from the Xml files located in the specified folder.
        /// Scripts are located in the 'script' sub folder.
        /// Business tasks are located in the 'tasks' sub folder.
        /// </summary>
        /// <remarks>The specified folder is the </remarks>
        /// <param name="someBusinessObjectKinds">The business object kinds to consider.</param>
        /// <param name="aFolderPath">The path of the folder containing scripts and business tasks.</param>
        /// <returns>The log of the load task.</returns>
        public Log LoadFromXmlFiles(
            List<BusinessLibraryElementKind> someBusinessObjectKinds,
            String aFolderPath)
        {
            Log aLog = new Log();

            if (someBusinessObjectKinds == null)
                someBusinessObjectKinds = new List<BusinessLibraryElementKind>() { BusinessLibraryElementKind.Any };

            if (aFolderPath==null)
                return aLog;

            // if we could not load the library then we try to load it from local xml files
            StreamReader aStream = null;
            try
            {
                aFolderPath = (aFolderPath.EndsWith("\\") ? aFolderPath : aFolderPath + "\\");

                // we try to load the script dictionary
                if (someBusinessObjectKinds.Contains(BusinessLibraryElementKind.ScriptWord))
                {
                    aStream = new StreamReader(
                         aFolderPath +
                         "script\\dico_" + this.Name.ToLower() + ".xml"
                         );
                    if (aStream != null)
                    {
                        XmlSerializer aXmlSerializer = new XmlSerializer(typeof(ScriptDictionary));
                        ScriptDictionary aScriptDictionary = (ScriptDictionary)
                            aXmlSerializer.Deserialize(aStream);

                        // we rebuild the script tree
                        this.ReBuildScriptTree(aScriptDictionary);
                    }
                }

                // we try to load the business task dictionary
                if (someBusinessObjectKinds.Contains(BusinessLibraryElementKind.Task))
                {
                    aStream = new StreamReader(
                         aFolderPath +
                         "tasks\\dico_" + this.Name.ToLower() + ".xml"
                         );
                    if (aStream != null)
                    {
                        XmlSerializer aXmlSerializer = new XmlSerializer(typeof(TaskDictionary));
                        TaskDictionary aTaskDictionary = (TaskDictionary)
                            aXmlSerializer.Deserialize(aStream);

                        // we add the business tasks
                        foreach (TaskDefinition aCurrentBusinessTask in aTaskDictionary.Definitions)
                        {
                            aCurrentBusinessTask.BusinessLibraryName = this.Name;
                            aCurrentBusinessTask.UniqueName = aCurrentBusinessTask.BusinessLibraryName + "$" + aCurrentBusinessTask.Name;
                            this.myTaskDefinitions.Add(aCurrentBusinessTask);
                        }
                    }
                }

                // we try to load the information kind dictionary
                if (someBusinessObjectKinds.Contains(BusinessLibraryElementKind.DataClass))
                {
                    aStream = new StreamReader(
                         aFolderPath +
                         "classes\\dico_" + this.Name.ToLower() + ".xml"
                         );
                    if (aStream != null)
                    {
                        XmlSerializer aXmlSerializer = new XmlSerializer(typeof(ClassDictionary));
                        ClassDictionary aClassDictionary = (ClassDictionary)
                            aXmlSerializer.Deserialize(aStream);

                        // we add the information kinds
                        foreach (ClassDefinition aCurrentClassDefinition in aClassDictionary.Definitions)
                        {
                            aCurrentClassDefinition.BusinessLibraryName = this.Name;
                            aCurrentClassDefinition.UniqueName = aCurrentClassDefinition.BusinessLibraryName + "$" + aCurrentClassDefinition.Name;
                            foreach (FormatIndexation aCurrentInformationFormat in aCurrentClassDefinition.Formats)
                            {
                                aCurrentInformationFormat.BusinessLibraryName = this.Name;
                                aCurrentInformationFormat.UniqueName = aCurrentClassDefinition.UniqueName + "$" + aCurrentClassDefinition.Name;
                            }
                            this.myClassDefinitions.Add(aCurrentClassDefinition);
                        }
                    }
                }
            }
            catch (Exception aException)
            {
                aLog.AddException(
                    aException,
                    EventCriticality.High,
                    ""
                    );
            }
            finally
            {
                if (aStream != null)
                    aStream.Close();
            }

            return aLog;
        }

        #endregion


        // ------------------------------------------
        // REBUILDING SCRIPT TREE
        // ------------------------------------------

        #region ReBuildingScriptTree

        /// <summary>
        /// Rebuilds this instance.
        /// </summary>
        /// <param name="aScriptDictionary">The script dictionnary to consider.</param>
        /// <param name="aParentDictionaryScriptWord">The parent dictionnary script word to consider.</param>
        /// <param name="aParentDynamicScriptWord">The parent dynamic script word to consider.</param>
        private void ReBuildScriptTree(
            ScriptDictionary aScriptDictionary,
            ScriptWord aParentDictionaryScriptWord =null,
            ScriptWord aParentDynamicScriptWord = null)
        {
            List<ScriptWord> someDictionaryScriptWords = new List<ScriptWord>();

            if (aParentDictionaryScriptWord == null)
                someDictionaryScriptWords = aScriptDictionary.Definitions;
            else
                someDictionaryScriptWords = aParentDictionaryScriptWord.SubScriptWords;

            // we recursively retrieve the sub script words
            foreach (ScriptWord aDictionaryScriptWord in someDictionaryScriptWords)
                if (aDictionaryScriptWord.IsDynamic)
                {
                    ScriptWord aDynamicScriptWord = null;

                    // if the current script word is a reference then
                    if ((aDictionaryScriptWord.ReferenceUniqueName != null) & (aDictionaryScriptWord.ReferenceUniqueName != ""))
                    {
                        if (aParentDictionaryScriptWord != null)
                        {
                            // we retrieve the reference script word
                            ScriptWord aReferenceScriptWord = this.GetScriptWordWithUniqueName(aDictionaryScriptWord.ReferenceUniqueName);
                            if (aReferenceScriptWord ==null)
                                aReferenceScriptWord = this.myExtensionHandler.GetScriptWordWithUniqueName(aDictionaryScriptWord.ReferenceUniqueName);
                            if (aReferenceScriptWord != null)
                            {
                                // if found we clone it
                                aDynamicScriptWord = new ScriptWord(aReferenceScriptWord);
                                aDynamicScriptWord.Definition = aReferenceScriptWord.Definition;
                                aDynamicScriptWord.BusinessLibraryName = this.Name;
                                aDynamicScriptWord.UniqueName = this.Name + "$" + aDynamicScriptWord.MethodName;
                                aDynamicScriptWord.SubScriptWords = aReferenceScriptWord.SubScriptWords;

                                // and we insert the clone in the script tree
                                DynamicScriptWordKey aDynamicScriptWordKey = new DynamicScriptWordKey();
                                aDynamicScriptWordKey.Id = Guid.NewGuid().ToString();
                                aDynamicScriptWordKey.Name = aDynamicScriptWord.Name;

                                if (aParentDynamicScriptWord == null)
                                    this.myScriptWords.Add(aDynamicScriptWordKey, aDynamicScriptWord);
                                else
                                {
                                    aDynamicScriptWord.Parent = aParentDynamicScriptWord;
                                    aParentDynamicScriptWord.SubScriptWords.Add(aDynamicScriptWordKey, aDynamicScriptWord);
                                }
                            }
                        }
                        else
                        {
                            List<ScriptWord> someDynamicScriptWords = this.GetScriptWordsWithUniqueName(aDictionaryScriptWord.ReferenceUniqueName);
                            someDynamicScriptWords.AddRange(this.myExtensionHandler.GetScriptWordsWithUniqueName(aDictionaryScriptWord.ReferenceUniqueName));
                            foreach(ScriptWord aReferenceScriptWord in someDynamicScriptWords)
                                foreach (ScriptWord aChildDictionaryScriptWord in aDictionaryScriptWord.SubScriptWords)
                                {
                                    aDynamicScriptWord = new ScriptWord(aChildDictionaryScriptWord);
                                    aDynamicScriptWord.Definition = aChildDictionaryScriptWord;
                                    aDynamicScriptWord.BusinessLibraryName = this.Name;
                                    aDynamicScriptWord.UniqueName = this.Name + "$" + aDynamicScriptWord.MethodName;

                                    aDynamicScriptWord.SubScriptWords.Clear();

                                    // we insert the clone in the script tree
                                    DynamicScriptWordKey aDynamicScriptWordKey = new DynamicScriptWordKey();
                                    aDynamicScriptWordKey.Id = Guid.NewGuid().ToString();
                                    aDynamicScriptWordKey.Name = aDynamicScriptWord.Name;

                                    aDynamicScriptWord.Parent = aReferenceScriptWord;
                                    aReferenceScriptWord.SubScriptWords.Add(aDynamicScriptWordKey, aDynamicScriptWord);

                                    this.ReBuildScriptTree(aScriptDictionary, aChildDictionaryScriptWord, aDynamicScriptWord);
                                }
                        }
                    }
                    else
                    {
                        aDynamicScriptWord = new ScriptWord(aDictionaryScriptWord);
                        aDynamicScriptWord.Definition = aDictionaryScriptWord;
                        aDynamicScriptWord.BusinessLibraryName = this.Name;
                        aDynamicScriptWord.UniqueName = this.Name + "$" + aDynamicScriptWord.MethodName;

                        aDynamicScriptWord.SubScriptWords.Clear();

                        // we insert the clone in the script tree
                        DynamicScriptWordKey aDynamicScriptWordKey = new DynamicScriptWordKey();
                        aDynamicScriptWordKey.Id = Guid.NewGuid().ToString();
                        aDynamicScriptWordKey.Name = aDynamicScriptWord.Name;

                        if (aParentDynamicScriptWord == null)
                            this.myScriptWords.Add(aDynamicScriptWordKey, aDynamicScriptWord);
                        else
                        {
                            aDynamicScriptWord.Parent = aParentDynamicScriptWord;
                            aParentDynamicScriptWord.SubScriptWords.Add(aDynamicScriptWordKey, aDynamicScriptWord);
                        }
                        this.ReBuildScriptTree(aScriptDictionary, aDictionaryScriptWord, aDynamicScriptWord);
                    }
                }

        }

        #endregion
    }
}
