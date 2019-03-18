using cor_base_wdl.business.context;
using cor_base_wdl.business.libraries.dictionaries;
using cor_base_wdl.business.libraries;
using cor_base_wdl.business.tasks;
using cor_base_wdl.business.universe;
using cor_base_wdl.data.context;
using cor_base_wdl.data.references;
using cor_base_wdl.system.assembly;
using cor_base_wdl.system.logging;
using cor_runtime_wdl.business.libraries;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using dkm.core.system.assembly;

namespace cor_runtime_wdl.business.universe
{

    /// <summary>
    /// This class represents a extension handler.
    /// </summary>
    [Serializable()]
    [XmlType("RuntimeExtensionHandler", Namespace = "http://www.w3.org/2001/dkm.xsd")]
    [XmlRoot(ElementName = "runtimeExtensionHandler", Namespace = "http://www.w3.org/2001/dkm.xsd", IsNullable = false)]
    public class RuntimeExtensionHandler : ExtensionHandler
    {

        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        /// <summary>
        /// Central library indexation of this instance.
        /// </summary>
        protected BusinessLibraryDefinitionIndexation myCentralBusinessLibraryDefinitionIndexation = null;

        #endregion


        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The application domain of this instance.
        /// </summary>
        public AppDomain AppDomain
        {
            get
            {
                return this.myAppDomain;
            }
            set
            {
                this.myAppDomain = value;
                this.myCentralBusinessLibraryDefinitionIndexation = BusinessLibraryDefinitionIndexation.GetCentralIndexation(this.myAppDomain);
            }
        }

        #endregion


        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of RuntimeExtensionHandler class.
        /// </summary>
        public RuntimeExtensionHandler()
        {
        }

        #endregion


        // ------------------------------------------
        // LOADING
        // ------------------------------------------

        #region Loading

        /// <summary>
        /// Loads the specifed central libraries.
        /// </summary>
        /// <param name="someBusinessLibraryNames">The names of libraries to consider.</param>
        /// <param name="aLibFolderPath">The path of the library folder to consider.</param>
        /// <param name="someBusinessObjectKinds">The kinds of library elements to consider.</param>
        /// <returns>The log of the load task.</returns>
        public virtual Log LoadCentral(
            List<String> someBusinessLibraryNames = null,
            String aLibFolderPath=null,
            List<BusinessLibraryElementKind> someBusinessObjectKinds= null)
        {
            Log aLog = new Log();

            if (this.myCentralBusinessLibraryDefinitionIndexation != null)
                foreach (BusinessLibraryDefinition aBusinessLibrary in this.myCentralBusinessLibraryDefinitionIndexation.GetBusinessLibraries(someBusinessLibraryNames))
                    if (!this.myLibraries.Any(p => p.Id == aBusinessLibrary.Id))
                    {
                        RuntimeDynamicBusinessLibrary aLoadedBusinessLibrary = new RuntimeDynamicBusinessLibrary(
                            this, aBusinessLibrary, aLibFolderPath);

                        Log aLoadLog = aLoadedBusinessLibrary.Load(
                            someBusinessObjectKinds,
                            new List<DataBindingKind>() { 
                            DataBindingKind.EmbedResource,
                            DataBindingKind.XmlFile});

                        if (aLoadLog.HasErrorOrException())
                            aLog.AddEvents(aLoadLog);
                        else
                            this.myLibraries.Add(aLoadedBusinessLibrary);
                    }

            return aLog;
        }

        /// <summary>
        /// Loads the specifed libraries in the specified way.
        /// </summary>
        /// <param name="aFilePath">The file path to consider.</param>
        /// <param name="someBusinessObjectKinds">The kinds of library elements to consider.</param>
        /// <returns>The log of the load task.</returns>
        /// <remarks>If null then we load the existing library names.</remarks>
        public virtual Log LoadFrom(
            String aFilePath,
            List<BusinessLibraryElementKind> someBusinessObjectKinds = null)
        {
            Log aLog = new Log();

            BusinessLibraryDefinition aBusinessLibrary = RuntimeExtensionHandler.CreateBusinessLibrary(this.myAppDomain, aFilePath);
            if ((aBusinessLibrary != null) && (!this.myLibraries.Any(p => p.Id == aBusinessLibrary.Id)))
            {
                RuntimeDynamicBusinessLibrary aLoadedBusinessLibrary = new RuntimeDynamicBusinessLibrary(
                    this, aBusinessLibrary, Path.GetDirectoryName(aFilePath));
                if (!aLoadedBusinessLibrary.Load(
                    someBusinessObjectKinds,
                    new List<DataBindingKind>() { DataBindingKind.XmlFile }).HasWarning())
                    this.myLibraries.Add(aLoadedBusinessLibrary);
            }

            return aLog;
        }

        /// <summary>
        /// Creates a new instance of BusinessLibrary from the specified file.
        /// </summary>
        /// <param name="aAppDomain">The application domain to consider.</param>
        /// <param name="aFilePath">The file path of the folder to consider.</param>
        /// <returns>The created library.</returns>
        public static BusinessLibraryDefinition CreateBusinessLibrary(AppDomain aAppDomain, String aFilePath)
        {
            BusinessLibraryDefinition aBusinessLibrary = null;

            if (string.IsNullOrEmpty(aFilePath))
                return aBusinessLibrary;

            if ((aAppDomain!=null) && (File.Exists(aFilePath)))
            {
                Assembly aAssembly = Assembly.LoadFrom(aFilePath);
                if (aAssembly != null)
                {
                    Stream aStream = null;
                    try
                    {
                        String aFileName = Path.GetFileNameWithoutExtension(aFilePath);

                        aStream = aAssembly.GetManifestResourceStream(aFileName + ".info.xml");
                        XmlSerializer aXmlSerializer = new XmlSerializer(typeof(BusinessLibraryDefinition));
                        aBusinessLibrary = (BusinessLibraryDefinition)aXmlSerializer.Deserialize(aStream);
                    }
                    catch (Exception ex)
                    {
                        String st = ex.ToString();
                    }
                    finally
                    {
                        if (aStream != null)
                            aStream.Close();
                    }
                }
            }

            return aBusinessLibrary;
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            this.myLibraries = new List<BusinessLibrary>();
        }

        #endregion


        // ------------------------------------------
        // BUSINESS OBJECTS
        // ------------------------------------------

        #region Business Objects

        /// <summary>
        /// Creates the instance of the specified business object class.
        /// </summary>
        /// <param name="aObjectKind">The object kind to consider.</param>
        /// <param name="aUniqueName">The unique name of the business object to consider.</param>
        public Object CreateObjectInstance(
            BusinessLibraryElementKind aObjectKind,
            String aUniqueName)
        {
            Object aDictionaryObject = null;
            AssemblyHelper.ClassReference aAssemblyReference =
                this.GetAssemblyReference(aObjectKind, aUniqueName, out aDictionaryObject);

            Object aBusinessObject = this.CreateInstance(aAssemblyReference);

            if (aBusinessObject is TaskDefinition)
                ((TaskDefinition)aBusinessObject).Initialize(aDictionaryObject as TaskDefinition);

            return aBusinessObject;
        }

        /// <summary>
        /// Creates the instance of the viewer of the specified business object class.
        /// </summary>
        /// <param name="aObjectKind">The object kind to consider.</param>
        /// <param name="aBusinessObjectUniqueName">The unique name of the business object to consider.</param>
        /// <param name="aViewerKey">The viewer key to consider.</param>
        public Object CreateObjectViewerInstance(
            BusinessLibraryElementKind aObjectKind,
            String aBusinessObjectUniqueName,
            String aViewerKey)
        {
            AssemblyHelper.ClassReference aAssemblyReference =
                this.GetAssemblyReference(aObjectKind, aBusinessObjectUniqueName, aViewerKey);

            return this.CreateInstance(aAssemblyReference);
        }

        /// <summary>
        /// Creates a data context of the specified library.
        /// </summary>
        /// <param name="aBusinessLibraryName">The business entity to consider.</param>
        /// <param name="aDataContext">The base data context to consider.</param>
        public DataContext CreateDataContext(String aBusinessLibraryName, DataContext aDataContext)
        {
            BusinessDataContext aBusinessObject = null;
            BusinessLibraryDefinition aBusinessLibrary = this.GetLibrary(aBusinessLibraryName);
            if (aBusinessLibrary != null)
            {
                AssemblyHelper.ClassReference aAssemblyReference;
                aAssemblyReference.AssemblyName = aBusinessLibrary.AssemblyName;
                aAssemblyReference.ClassName = aBusinessLibrary.AssemblyName + ".definition.context.DataContext_" + aBusinessLibraryName;
                aBusinessObject = this.CreateInstance(aAssemblyReference, aDataContext) as BusinessDataContext; 
            }

            return aBusinessObject;
        }
        
        // Private --------------------------------

        /// <summary>
        /// Creates the instance of the specified business object instance type.
        /// </summary>
        /// <param name="aAssemblyReference">The assembly reference to consider.</param>
        /// <param name="someAttributes">The attributes to consider.</param>
        private Object CreateInstance(AssemblyHelper.ClassReference aAssemblyReference, params Object[] someAttributes)
        {
            Object aObject = null;

            try
            {
                if (!String.IsNullOrEmpty(aAssemblyReference.AssemblyName))
                {
                    Assembly aAssembly = this.GetAsssembly(aAssemblyReference.AssemblyName);
                    if ((aAssembly != null) && (!String.IsNullOrEmpty(aAssemblyReference.ClassName)))
                        aObject = aAssembly.CreateInstance(
                            aAssemblyReference.ClassName,
                            true, BindingFlags.CreateInstance, null, someAttributes, null, null);
                }
            }
            catch(Exception ex)
            {
                String st = ex.ToString();
            }

            return aObject;
        }

        #endregion

    }
}
