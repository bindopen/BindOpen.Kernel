using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Helpers.Serialization;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Common;
using BindOpen.Framework.Core.Extensions.Configuration;
using BindOpen.Framework.Core.Extensions.Configuration.Carriers;
using BindOpen.Framework.Core.Extensions.Configuration.Connectors;
using BindOpen.Framework.Core.Extensions.Configuration.Entities;
using BindOpen.Framework.Core.Extensions.Configuration.Formats;
using BindOpen.Framework.Core.Extensions.Configuration.Metrics;
using BindOpen.Framework.Core.Extensions.Configuration.Routines;
using BindOpen.Framework.Core.Extensions.Configuration.Tasks;
using BindOpen.Framework.Core.Extensions.Definition;
using BindOpen.Framework.Core.Extensions.Definition.Carriers;
using BindOpen.Framework.Core.Extensions.Definition.Connectors;
using BindOpen.Framework.Core.Extensions.Definition.Entities;
using BindOpen.Framework.Core.Extensions.Definition.Formats;
using BindOpen.Framework.Core.Extensions.Definition.Handlers;
using BindOpen.Framework.Core.Extensions.Definition.Routines;
using BindOpen.Framework.Core.Extensions.Definition.Scriptwords;
using BindOpen.Framework.Core.Extensions.Definition.Tasks;
using BindOpen.Framework.Core.Extensions.Indexes;
using BindOpen.Framework.Core.Extensions.Libraries;
using BindOpen.Framework.Core.System.Assemblies;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Extensions
{

    /// <summary>
    /// This class represents an application extension.
    /// </summary>
    [Serializable()]
    [XmlType("AppExtension", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "appExtension", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class AppExtension : DataItem
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        /// <summary>
        /// Libraries of this instance.
        /// </summary>
        protected List<Library> _Libraries = new List<Library>();

        /// <summary>
        /// The application domain of this instance.
        /// </summary>
        protected AppDomain _appDomain = null;

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Libraries of this instance.
        /// </summary>
        public List<Library> Libraries
        {
            get { return this._Libraries; }
            set { this._Libraries = value; }
        }

        /// <summary>
        /// Application domain of this instance.
        /// </summary>
        public AppDomain AppDomain
        {
            get { return this._appDomain; }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the AppExtension class.
        /// </summary>
        public AppExtension()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the AppExtension class.
        /// </summary>
        /// <param name="appDomain">The application domain to consider.</param>
        public AppExtension(
            AppDomain appDomain)
        {
            this._appDomain= appDomain;
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        // Libraries --------------------------------

        /// <summary>
        /// Returns the names of the libraries.
        /// </summary>
        /// <returns>Returns the names of the libraries of this instance.</returns>
        public List<String> GetLibraryNames()
        {
            return this._Libraries.Select(p => p.Name).Distinct(StringComparer.CurrentCultureIgnoreCase).ToList();
        }

        /// <summary>
        /// Returns the specified library.
        /// </summary>
        /// <param name="name">The name of the library to consider.</param>
        /// <returns>The library with the specified name.</returns>
        public Library GetLibrary(String name)
        {
            if (name==null)
                return null;
            return this._Libraries.FirstOrDefault(p => p.KeyEquals(name));            
        }

        /// <summary>
        /// Returns the specified library definition.
        /// </summary>
        /// <param name="name">The name of the library to consider.</param>
        /// <returns>The library with the specified name.</returns>
        public LibraryDefinition GetLibraryDefinition(String name)
        {
            Library library = this.GetLibrary(name);
            return (library != null ? library.Definition : null);
        }
        
        /// <summary>
        /// Returns the specified library.
        /// </summary>
        /// <param name="names">The names of the libraries to consider.</param>
        /// <returns>The libraries with the specified names.</returns>
        public List<Library> GetLibraries(List<String> names=null)
        {
            if (names == null)
                return this._Libraries;
            return this._Libraries.Where(p => p.Name != null && names.Any(q=>q!=null && p.Name.KeyEquals(q))).ToList();
        }

        /// <summary>
        /// Returns the library definitions of this instance.
        /// </summary>
        /// <returns>Returns the library definitions of this instance.</returns>
        public List<LibraryDefinition> GetLibraryDefinitions()
        {
            return this._Libraries.Select(p => p.Definition).Where(p=>p!=null).ToList();
        }

        // Item definitions ----------------------------------

        /// <summary>
        /// Returns the item definitions of this instance.
        /// </summary>
        /// <param name="libraryNames">The names of libraries to consider.</param>
        /// <returns>The script words of specified library names.</returns>
        public List<T> GetItemDefinitions<T>(
            List<String> libraryNames = null) where T : AppExtensionItemDefinition
        {
            List<T> itemDefinitions = new List<T>();
            foreach (Library library in this.GetLibraries(libraryNames))
                itemDefinitions.AddRange(library.GetItemDefinitions<T>());

            return itemDefinitions;
        }

        /// <summary>
        /// Returns the item definitions of this instance.
        /// </summary>
        /// <param name="libraryNames">The names of libraries to consider.</param>
        /// <returns>The script words of specified library names.</returns>
        public List<string> GetItemDefinitionUniqueNames<T>(
            List<String> libraryNames = null) where T : AppExtensionItemDefinition
        {
            return GetItemDefinitions<T>(libraryNames).Select(p=>p.ToKey()).ToList();
        }

        /// <summary>
        /// Returns the item definition with the specified unique name.
        /// </summary>
        /// <param name="uniqueName">The unique name of item to return.</param>
        /// <param name="libraryNames">The names of libraries to consider.</param>
        /// <returns>The item with the specified unique name.</returns>
        public T GetItemDefinitionWithUniqueName<T>(
            String uniqueName,
            List<String> libraryNames = null) where T : AppExtensionItemDefinition
        {
            T aItemDefinition = null;
            foreach (Library library in this.GetLibraries(libraryNames))
                if ((aItemDefinition = library.GetItemDefinitionWithUniqueName<T>(uniqueName)) != null)
                    break;

            return aItemDefinition;
        }

        // Script word definitions ---------------------------

        /// <summary>
        /// Returns the possible parent definitions of the specified script word definition.
        /// </summary>
        /// <param name="definitionName">The definition name to consider.</param>
        /// <param name="libraryNames">The names of libraries to consider.</param>
        /// <returns>The parent definitions of the specified script word definition.</returns>
        public List<ScriptWordDefinition> GetParentScriptWordDefinitions(
            String definitionName,
            List<String> libraryNames = null)
        {
            List<ScriptWordDefinition> parentDefinitions = this.GetParentScriptWordDefinitions(definitionName, null, libraryNames).Distinct().ToList();

            return parentDefinitions;
        }

        private List<ScriptWordDefinition> GetParentScriptWordDefinitions(
            String definitionName,
            ScriptWordDefinition parentFeachDefinition,
            List<String> libraryNames = null)
        {
            List<ScriptWordDefinition> parentDefinitions = new List<ScriptWordDefinition>();

            if (definitionName != null)
            {
                List<ScriptWordDefinition> definitions = 
                    (parentFeachDefinition == null ?  this.GetItemDefinitions<ScriptWordDefinition>(libraryNames) : parentFeachDefinition.Children);
                foreach (ScriptWordDefinition currentScriptWordDefinition in definitions)
                {
                    if (currentScriptWordDefinition.KeyEquals(definitionName) && parentFeachDefinition != null)
                        parentDefinitions.Add(parentFeachDefinition);

                    parentDefinitions.AddRange(this.GetParentScriptWordDefinitions(definitionName, currentScriptWordDefinition, libraryNames));
                }
            }

            return parentDefinitions;
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Adds the specified library.
        /// </summary>
        /// <param name="library">The dynamic library to consider.</param>
        public void AddLibrary(Library library)
        {
            if (library != null)
            {
                library.SourceKind = DataSourceKind.Memory;
                if (!this._Libraries.Any(p => p.KeyEquals(library.Name)))
                    this._Libraries.Add(library);
            }
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            this._Libraries = new List<Library>();
        }

        #endregion

        // ------------------------------------------
        // LIBRARY ELEMENTS
        // ------------------------------------------

        #region Library Elements

        // Assembly --------------------------------------

        /// <summary>
        /// Gets the specified assembly.
        /// </summary>
        /// <param name="assemblyName">The name of the assembly to consider.</param>
        /// <returns></returns>
        protected Assembly GetAsssembly(String assemblyName)
        {
            if (this._appDomain == null) return null;

            Assembly assembly = null;
            int i = this._appDomain.GetAssemblies().Length;
            if ((this._appDomain != null) & (assemblyName != null))
            {
                assemblyName = assemblyName.Trim();
                assembly =
                    this._appDomain.GetAssemblies().FirstOrDefault(p => p.GetName().Name.KeyEquals(assemblyName));
            }
            return assembly;
        }

        /// <summary>
        /// Gets the extension item class reference of the specified object.
        /// </summary>
        /// <param name="extensionItemKind">The extension item kind to consider.</param>
        /// <param name="uniqueName">The unique name of the extension item defintion to consider.</param>
        /// <param name="extensionItemDefinition">The corresponding library item definition.</param>
        public AssemblyHelper.ClassReference GetItemClassReference(
            AppExtensionItemKind extensionItemKind,
            String uniqueName,
            out AppExtensionItemDefinition extensionItemDefinition)
        {
            AssemblyHelper.ClassReference assemblyReference = new AssemblyHelper.ClassReference();
            extensionItemDefinition = null;

            switch (extensionItemKind)
            {
                case AppExtensionItemKind.Carrier:
                    CarrierDefinition carrierDefinition = this.GetItemDefinitionWithUniqueName<CarrierDefinition>(uniqueName);
                    if (carrierDefinition != null)
                        assemblyReference = AssemblyHelper.GetClassReference(carrierDefinition.ItemClass);
                    extensionItemDefinition = carrierDefinition;
                    break;
                case AppExtensionItemKind.Task:
                    TaskDefinition taskDefinition = this.GetItemDefinitionWithUniqueName<TaskDefinition>(uniqueName);
                    if (taskDefinition != null)
                        assemblyReference = AssemblyHelper.GetClassReference(taskDefinition.ItemClass);
                    extensionItemDefinition = taskDefinition;
                    break;
                case AppExtensionItemKind.Connector:
                    ConnectorDefinition dataConnectorDefinition = this.GetItemDefinitionWithUniqueName<ConnectorDefinition>(uniqueName);
                    if (dataConnectorDefinition != null)
                        assemblyReference = AssemblyHelper.GetClassReference(dataConnectorDefinition.ItemClass);
                    extensionItemDefinition = dataConnectorDefinition;
                    break;
                case AppExtensionItemKind.Entity:
                    EntityDefinition dataEntityDefinition = this.GetItemDefinitionWithUniqueName<EntityDefinition>(uniqueName);
                    if (dataEntityDefinition != null)
                        assemblyReference = AssemblyHelper.GetClassReference(dataEntityDefinition.ItemClass);
                    extensionItemDefinition = dataEntityDefinition;
                    break;
                case AppExtensionItemKind.Format:
                    FormatDefinition dataFormatDefinition = this.GetItemDefinitionWithUniqueName<FormatDefinition>(uniqueName);
                    if (dataFormatDefinition != null)
                        assemblyReference = AssemblyHelper.GetClassReference(dataFormatDefinition.ItemClass);
                    extensionItemDefinition = dataFormatDefinition;
                    break;
                case AppExtensionItemKind.Handler:
                    HandlerDefinition dataHandlerDefinition = this.GetItemDefinitionWithUniqueName<HandlerDefinition>(uniqueName);
                    if (dataHandlerDefinition != null)
                        assemblyReference = AssemblyHelper.GetClassReference(dataHandlerDefinition.CallingClass);
                    extensionItemDefinition = dataHandlerDefinition;
                    break;
                case AppExtensionItemKind.RoutineConfiguration:
                    RoutineDefinition routineDefinition = this.GetItemDefinitionWithUniqueName<RoutineDefinition>(uniqueName);
                    if (routineDefinition != null)
                        assemblyReference = AssemblyHelper.GetClassReference(routineDefinition.ItemClass);
                    extensionItemDefinition = routineDefinition;
                    break;
                case AppExtensionItemKind.ScriptWord:
                    ScriptWordDefinition scriptWordDefinition = this.GetItemDefinitionWithUniqueName<ScriptWordDefinition>(uniqueName);
                    if (scriptWordDefinition != null)
                        assemblyReference = AssemblyHelper.GetClassReference(scriptWordDefinition.CallingClass);
                    extensionItemDefinition = scriptWordDefinition;
                    break;
            }

            return assemblyReference;
        }

        /// <summary>
        /// Gets the extension item class reference of the specified object.
        /// </summary>
        /// <param name="extensionItemKind">The extension item kind to consider.</param>
        /// <param name="uniqueName">The unique name of the extension item defintion to consider.</param>
        protected AssemblyHelper.ClassReference GetItemClassReference(
            AppExtensionItemKind extensionItemKind,
            String uniqueName)
        {
            AppExtensionItemDefinition extensionItemDefinition = null;
            return this.GetItemClassReference(extensionItemKind, uniqueName, out extensionItemDefinition);
        }

        /// <summary>
        /// Gets the class reference of the specified viewer.
        /// </summary>
        /// <param name="extensionItemKind">The extension item kind to consider.</param>
        /// <param name="uniqueName">The unique name of the library item to consider.</param>
        /// <param name="viewerKey">The viewer key to consider.</param>
        public AssemblyHelper.ClassReference GetViewerClassReference(
            AppExtensionItemKind extensionItemKind,
            String uniqueName,
            String viewerKey)
        {
            AssemblyHelper.ClassReference assemblyReference = new AssemblyHelper.ClassReference();

            switch (extensionItemKind)
            {
                case AppExtensionItemKind.Entity:
                    EntityDefinition entityDefinition = this.GetItemDefinitionWithUniqueName<EntityDefinition>(uniqueName);
                    if (entityDefinition != null)
                        assemblyReference = AssemblyHelper.GetClassReference(
                            DataElementSet.Create(entityDefinition.ViewerClass).GetElementItem(viewerKey) as String);
                    break;
                case AppExtensionItemKind.Format:
                    FormatDefinition informationFormat = this.GetItemDefinitionWithUniqueName<FormatDefinition>(uniqueName);
                    if (informationFormat != null)
                        assemblyReference = AssemblyHelper.GetClassReference(
                            DataElementSet.Create(informationFormat.ViewerClass).GetElementItem(viewerKey) as String);
                    break;
            }

            return assemblyReference;
        }

        // Configurations --------------------------------------

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="definitionName">The unique name of the definition to consider.</param>
        /// <param name="xmlString">The XML string to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="parameters">The object parameters to consider.</param>
        public TAppExtensionItemConfiguration<T> CreateConfiguration<T>(
            String definitionName,
            String xmlString = null,
            Log log = null,
            params Object[] parameters) where T : AppExtensionItemDefinition
        {
            AppExtensionItemKind extensionItemKind = typeof(T).GetExtensionItemKind();

            T definition = this.GetItemDefinitionWithUniqueName<T>(definitionName);

            TAppExtensionItemConfiguration<T> configuration = null;

            if (definition == null)
            {
                if (log != null)
                    log.AddError("Could not retrieve the extension item '" + definitionName + "' of kind '" + extensionItemKind.ToString() + "'");
            }
            else if (xmlString == null)
                switch (extensionItemKind)
                {
                    case AppExtensionItemKind.Carrier:
                        configuration = new CarrierConfiguration(null, definitionName) as TAppExtensionItemConfiguration<T>;
                        break;
                    case AppExtensionItemKind.Task:
                        configuration = new TaskConfiguration(null, definitionName) as TAppExtensionItemConfiguration<T>;
                        break;
                    case AppExtensionItemKind.Connector:
                        configuration = new ConnectorConfiguration(null, definitionName) as TAppExtensionItemConfiguration<T>;
                        break;
                    case AppExtensionItemKind.Entity:
                        configuration = new EntityConfiguration(null, definitionName) as TAppExtensionItemConfiguration<T>;
                        break;
                    case AppExtensionItemKind.Format:
                        configuration = new FormatConfiguration(null, definitionName) as TAppExtensionItemConfiguration<T>;
                        break;
                    case AppExtensionItemKind.Metrics:
                        configuration = new MetricsConfiguration(null, definitionName) as TAppExtensionItemConfiguration<T>;
                        break;
                    case AppExtensionItemKind.RoutineConfiguration:
                        configuration = new RoutineConfiguration(null, definitionName) as TAppExtensionItemConfiguration<T>;
                        break;
                }
            else
                switch (extensionItemKind)
                {
                    case AppExtensionItemKind.Carrier:
                        configuration = XmlHelper.LoadFromString<CarrierConfiguration>(xmlString, log) as TAppExtensionItemConfiguration<T>;
                        break;
                    case AppExtensionItemKind.Task:
                        configuration = XmlHelper.LoadFromString<CarrierConfiguration>(xmlString, log) as TAppExtensionItemConfiguration<T>;
                        break;
                    case AppExtensionItemKind.Connector:
                        configuration = XmlHelper.LoadFromString<ConnectorConfiguration>(xmlString, log) as TAppExtensionItemConfiguration<T>;
                        break;
                    case AppExtensionItemKind.Entity:
                        configuration = XmlHelper.LoadFromString<EntityConfiguration>(xmlString, log) as TAppExtensionItemConfiguration<T>;
                        break;
                    case AppExtensionItemKind.Format:
                        configuration = XmlHelper.LoadFromString<FormatConfiguration>(xmlString, log) as TAppExtensionItemConfiguration<T>;
                        break;
                    case AppExtensionItemKind.Metrics:
                        configuration = XmlHelper.LoadFromString<MetricsConfiguration>(xmlString, log) as TAppExtensionItemConfiguration<T>;
                        break;
                    case AppExtensionItemKind.RoutineConfiguration:
                        configuration = XmlHelper.LoadFromString<RoutineConfiguration>(xmlString, log) as TAppExtensionItemConfiguration<T>;
                        break;
                }

            if (configuration!=null)
            {
                configuration.DefinitionUniqueId = definitionName;
                configuration.SetDefinition(definition);
            }

            return configuration;
        }
        
        // Accessors --------------------------------

        /// <summary>
        /// Gets the type of the specified library item.
        /// </summary>
        /// <param name="extensionItemKind">The extension item kind to consider.</param>
        /// <param name="uniqueName">The unique name of the object to consider.</param>
        public Type GetItemType(
            AppExtensionItemKind extensionItemKind,
            String uniqueName)
        {
            AppExtensionItemDefinition extensionItemDefinition = null;
            AssemblyHelper.ClassReference assemblyReference =
                this.GetItemClassReference(extensionItemKind, uniqueName, out extensionItemDefinition);

            return this.GetTypeFromAssemblyReference(assemblyReference);
        }

        // Private --------------------------------

        /// <summary>
        /// Creates the instance of the specified extension object instance type.
        /// </summary>
        /// <param name="classReference">The class reference to consider.</param>
        /// <param name="object1">The object to consider.</param>
        /// <param name="attributes">The attributes to consider.</param>
        public Log CreateInstance(AssemblyHelper.ClassReference classReference, out Object object1, params Object[] attributes)
        {
            Log log = new Log();
            object1 = null;

            try
            {
                if (!String.IsNullOrEmpty(classReference.AssemblyName))
                {
                    Assembly assembly = this.GetAsssembly(classReference.AssemblyName);
                    if ((assembly != null) && (!String.IsNullOrEmpty(classReference.ClassName)))
                        object1 = assembly.CreateInstance(
                            classReference.ClassName,
                            true, BindingFlags.CreateInstance, null, attributes, null, null);
                }
            }
            catch (Exception ex)
            {
                log.AddException(ex);
            }

            return log;
        }

        /// <summary>
        /// Loads the specified instance.
        /// </summary>
        /// <param name="classReference">The class reference to consider.</param>
        /// <param name="xmlString">The XML string to consider.</param>
        /// <param name="object1">The object to consider.</param>
        public Log LoadDataItemInstance(AssemblyHelper.ClassReference classReference, String xmlString, out Object object1)
        {
            Log log = new Log();
            object1 = null;

            try
            {
                if (!String.IsNullOrEmpty(classReference.AssemblyName))
                {
                    Assembly assembly = this.GetAsssembly(classReference.AssemblyName);
                    if (assembly != null)
                    {
                        Type type = assembly.GetType(classReference.ClassName);
                        if (type != null)
                        {
                            MethodInfo methodInfo = typeof(XmlHelper).GetMethod("LoadFromString", BindingFlags.Public | BindingFlags.Static);
                            if (methodInfo != null)
                            {
                                MethodInfo genericMethodInfo = methodInfo.MakeGenericMethod(
                                    new Type[] { type });

                                Object[] objects = new Object[3] { xmlString, log, null };
                                object1 = genericMethodInfo.Invoke(null, objects);
                            }
                            else
                                log.AddError(
                                    title: "'LoadFromXmlString' function not found in the specified class",
                                    description: "Could not find the static function called 'LoadFromXmlString' in the specified type ('" + classReference.ClassName + "').");
                        }
                        else
                            log.AddError(
                                title: "Specified type not found",
                                description: "Could not retrieve the specified type ('" + classReference.ClassName + "') in the specified assembly ('" + assembly.FullName + "').");
                    }
                    else
                        log.AddError("Could not retrieve the specified assembly ('" + assembly.FullName + "')");
                }
                else
                    log.AddError("Assembly name '" + classReference.AssemblyName + "' missing");
            }
            catch (Exception ex)
            {
                log.AddException(ex);
            }

            return log;
        }

        /// <summary>
        /// Gets the specified type from the specified assembly reference.
        /// </summary>
        /// <param name="classReference">The class reference to consider.</param>
        private Type GetTypeFromAssemblyReference(AssemblyHelper.ClassReference classReference)
        {
            Type type = null;

            try
            {
                if (!String.IsNullOrEmpty(classReference.AssemblyName))
                {
                    Assembly assembly = this.GetAsssembly(classReference.AssemblyName);
                    if ((assembly != null) && (!String.IsNullOrEmpty(classReference.ClassName)))
                        type = assembly.GetType(classReference.ClassName);
                }
            }
            catch
            {
            }

            return type;
        }

        #endregion

        // ------------------------------------------
        // LOADING
        // ------------------------------------------
    
        #region Loading

        // From index -------------------------------

        /// <summary>
        /// Loads the specified library.
        /// </summary>
        /// <param name="extensionConfiguration">The extension loading order to consider.</param>
        /// <returns></returns>
        public Log LoadLibrary(AppExtensionConfiguration extensionConfiguration)
        {
            Log log = new Log();
            List<Library> loadedLibraries = new List<Library>();

            if (extensionConfiguration != null)
            {
                foreach (AppExtensionFilter extensionFilter in extensionConfiguration.GetFilters())
                {
                    string defaultFolderPath = string.IsNullOrEmpty(extensionFilter.FolderPath) ?
                        extensionConfiguration.DefaultFolderPath : extensionFilter.FolderPath;

                    Library library = this._appDomain.LoadLibrary(
                            extensionFilter.ToDefinition(),
                            log,
                            null,
                            extensionFilter.SourceKinds,
                            defaultFolderPath);

                    if (library != null && !log.HasErrorsOrExceptions()
                        && !this._Libraries.Any(p => p.Definition?.Id.KeyEquals(library.Definition?.Id) == true))
                    {
                        loadedLibraries.Add(library);
                    }
                }
            }

            this._Libraries.AddRange(loadedLibraries);

            return log;
        }

        /// <summary>
        /// Loads the specified library.
        /// </summary>
        /// <param name="libraryIndex">The library index to consider.</param>
        /// <param name="libraryName">The name of libraries to consider.</param>
        /// <param name="libraryFolderPath">The path of the library folder to consider.</param>
        /// <param name="extensionItemKinds">The kinds of extension items to consider.</param>
        /// <param name="dataSourceKinds">The kinds of data sources to consider.</param>
        /// <returns>The log of the load task.</returns>
        public Log LoadLibrary(
            LibraryIndex libraryIndex,
            String libraryName,
            String libraryFolderPath = null,
            List<AppExtensionItemKind> extensionItemKinds = null,
            List<DataSourceKind> dataSourceKinds = null)
        {
            List<Library> loadedLibraries = new List<Library>();
            return this.LoadLibrary(
                libraryIndex,
                out loadedLibraries,
                new List<String>() { libraryName },
                extensionItemKinds,
                dataSourceKinds,
                libraryFolderPath);
        }

        /// <summary>
        /// Loads the specified library.
        /// </summary>
        /// <param name="libraryIndex">The library index to consider.</param>
        /// <param name="libraryNames">The names of libraries to consider.</param>
        /// <param name="libraryFolderPath">The path of the library folder to consider.</param>
        /// <param name="extensionItemKinds">The kinds of extension items to consider.</param>
        /// <param name="dataSourceKinds">The kinds of data sources to consider.</param>
        /// <returns>The log of the load task.</returns>
        public Log LoadLibrary(
            LibraryIndex libraryIndex,
            List<String> libraryNames = null,
            String libraryFolderPath = null,
            List<AppExtensionItemKind> extensionItemKinds = null,
            List<DataSourceKind> dataSourceKinds = null)
        {
            List<Library> loadedLibraries = new List<Library>();
            return this.LoadLibrary(libraryIndex, out loadedLibraries,
                libraryNames, extensionItemKinds, dataSourceKinds, libraryFolderPath);
        }

        /// <summary>
        /// Loads the specified library.
        /// </summary>
        /// <param name="libraryIndex">The library index to consider.</param>
        /// <param name="loadedLibraries">The loaded libraries to consider.</param>
        /// <param name="libraryNames">The names of libraries to consider.</param>
        /// <param name="libraryFolderPath">The path of the library folder to consider.</param>
        /// <param name="extensionItemKinds">The kinds of extension items to consider.</param>
        /// <param name="dataSourceKinds">The kinds of data sources to consider.</param>
        /// <returns>The log of the load task.</returns>
        public Log LoadLibrary(
            LibraryIndex libraryIndex,
            out List<Library> loadedLibraries,
            List<String> libraryNames = null,
            List<AppExtensionItemKind> extensionItemKinds = null,
            List<DataSourceKind> dataSourceKinds = null,
            String libraryFolderPath = null)
        {
            Log log = new Log();
            loadedLibraries = new List<Library>();

            if (libraryIndex != null)
                foreach (LibraryDefinition currentLibraryDefinition in libraryIndex.GetDefinitions(libraryNames))
                    if (!this._Libraries.Any(p => p.Id.KeyEquals(currentLibraryDefinition.Id)))
                    {
                        Library library = this._appDomain.LoadLibrary(
                            currentLibraryDefinition,
                            log,
                            extensionItemKinds, dataSourceKinds,
                            libraryFolderPath);

                        if (library != null && !log.HasErrorsOrExceptions())
                            if (!this._Libraries.Any(p => p.Definition != null && p.Definition.Id.KeyEquals(library.Definition.Id)))
                                loadedLibraries.Add(library);
                    }

            this._Libraries.AddRange(loadedLibraries);

            return log;
        }

        // From file -------------------------------

        /// <summary>
        /// Loads the specifed libraries in the specified way.
        /// </summary>
        /// <param name="completeFilePath">The file path to consider.</param>
        /// <param name="extensionItemKinds">The kinds of extension items to consider.</param>
        /// <param name="folderPath">The folder path to consider.</param>
        /// <returns>The log of the load task.</returns>
        /// <remarks>If null then we load the existing library names.</remarks>
        public virtual Log LoadLibraryFromFile(
            String completeFilePath,
            List<AppExtensionItemKind> extensionItemKinds = null,
            String folderPath = "")
        {
            List<Library> loadedLibraries = new List<Library>();
            return this.LoadLibraryFromFile(completeFilePath, out loadedLibraries, extensionItemKinds, folderPath);
        }

        /// <summary>
        /// Loads the specifed libraries in the specified way.
        /// </summary>
        /// <param name="completeFilePath">The file path to consider.</param>
        /// <param name="loadedLibrary">The loaded library to consider.</param>
        /// <param name="extensionItemKinds">The kinds of extension items to consider.</param>
        /// <param name="folderPath">The folder path to consider.</param>
        /// <returns>The log of the load task.</returns>
        /// <remarks>If null then we load the existing library names.</remarks>
        public virtual Log LoadLibraryFromFile(
            String completeFilePath,
            out Library loadedLibrary,
            List<AppExtensionItemKind> extensionItemKinds = null,
            String folderPath = "")
        {
            List<Library> loadedLibraries = new List<Library>();
            Log log = this.LoadLibraryFromFile(completeFilePath, out loadedLibraries, extensionItemKinds, folderPath);

            loadedLibrary = (loadedLibraries.Count > 0 ? loadedLibraries[0] : null);

            return log;
        }

        /// <summary>
        /// Loads the specifed libraries in the specified way.
        /// </summary>
        /// <param name="completeFilePath">The file path to consider.</param>
        /// <param name="loadedLibraries">The loaded libraries to consider.</param>
        /// <param name="extensionItemKinds">The kinds of extension items to consider.</param>
        /// <param name="folderPath">The folder path to consider.</param>
        /// <returns>The log of the load task.</returns>
        /// <remarks>If null then we load the existing library names.</remarks>
        public virtual Log LoadLibraryFromFile(
            String completeFilePath,
            out List<Library> loadedLibraries,
            List<AppExtensionItemKind> extensionItemKinds = null,
            String folderPath ="")
        {
            Log log = new Log();
            loadedLibraries = new List<Library>();

            Log subLog = null;

            folderPath = folderPath.GetEndedString(@"\").ToPath();

            if (String.IsNullOrEmpty(completeFilePath))
            {
                log.AddError("Assembly file path missing");
            }
            else
            {
                foreach (String subFilePath in completeFilePath.Split('|'))
                {
                    String completeSubFilePath = (subFilePath.Contains(@"\") ? subFilePath : folderPath + subFilePath).ToPath();

                    List<String> completeSubFilePaths = new List<String>();
                    if (completeSubFilePath.Contains('*'))
                    {
                        try
                        {
                            completeSubFilePaths = Directory.GetFiles(
                                Path.GetDirectoryName(completeSubFilePath),
                                Path.GetFileName(completeSubFilePath)).ToList();
                        }
                        catch
                        {
                            log.AddError("Could not find the assembly file path '" + completeSubFilePath + "'");
                        }
                    }
                    else
                    {
                        completeSubFilePaths = new List<String>() { completeSubFilePath };
                    }

                    foreach (String filePath in completeSubFilePaths)
                    {
                        subLog = new Log();
                        Library library = this._appDomain.LoadLibrary(
                            new LibraryDefinition() { FileName = Path.GetFileName(filePath) },
                            subLog,
                            extensionItemKinds, new List<DataSourceKind>() { DataSourceKind.Repository },
                            Path.GetDirectoryName(filePath));
                        log.AddSubLog(subLog, (p => p.HasErrorsOrExceptionsOrWarnings()), title: "Loading assembly '" + filePath + "'");

                        if (library != null && !log.HasErrorsOrExceptions())
                            if (!this._Libraries.Any(p => p.Definition != null && p.Definition.Id.KeyEquals(library.Definition.Id)))
                                loadedLibraries.Add(library);
                    }
                }
            }

            this._Libraries.AddRange(loadedLibraries);

            return log;
        }

        // Direct -----------------------------------

        /// <summary>
        /// Loads the specifed libraries in the specified way.
        /// </summary>
        /// <param name="libraryAssemblyName">The library assembly name to consider.</param>
        /// <param name="extensionItemKinds">The kinds of extension items to consider.</param>
        /// <returns>The log of the load task.</returns>
        /// <remarks>If null then we load the existing library names.</remarks>
        public virtual Log LoadLibrary(
            String libraryAssemblyName,
            List<AppExtensionItemKind> extensionItemKinds = null)
        {
            Library loadedLibrary = null;
            return this.LoadLibrary(libraryAssemblyName, out loadedLibrary, extensionItemKinds);
        }

        /// <summary>
        /// Loads the specifed libraries in the specified way.
        /// </summary>
        /// <param name="libraryAssemblyName">The library assembly name to consider.</param>
        /// <param name="loadedLibrary">The loaded library to consider.</param>
        /// <param name="extensionItemKinds">The kinds of extension items to consider.</param>
        /// <returns>The log of the load task.</returns>
        /// <remarks>If null then we load the existing library names.</remarks>
        public virtual Log LoadLibrary(
            String libraryAssemblyName,
            out Library loadedLibrary,
            List<AppExtensionItemKind> extensionItemKinds = null)
        {
            List<Library> loadedLibraries = new List<Library>();
            Log log = this.LoadLibrary(new List<String>() { libraryAssemblyName }, out loadedLibraries, extensionItemKinds);

            loadedLibrary = (loadedLibraries.Count > 0 ? loadedLibraries[0] : null);

            return log;
        }

        /// <summary>
        /// Loads the specifed libraries in the specified way.
        /// </summary>
        /// <param name="libraryAssemblyNames">The library assembly names to consider.</param>
        /// <param name="extensionItemKinds">The kinds of extension items to consider.</param>
        /// <returns>The log of the load task.</returns>
        /// <remarks>If null then we load the existing library names.</remarks>
        public virtual Log LoadLibrary(
            List<String> libraryAssemblyNames,
            List<AppExtensionItemKind> extensionItemKinds = null)
        {
            List<Library> loadedLibraries = new List<Library>();
            return this.LoadLibrary(libraryAssemblyNames, out loadedLibraries, extensionItemKinds);
        }

        /// <summary>
        /// Loads the specifed libraries in the specified way.
        /// </summary>
        /// <param name="libraryAssemblyNames">The library assembly names to consider.</param>
        /// <param name="loadedLibraries">The loaded libraries to consider.</param>
        /// <param name="extensionItemKinds">The kinds of extension items to consider.</param>
        /// <returns>The log of the load task.</returns>
        /// <remarks>If null then we load the existing library names.</remarks>
        public virtual Log LoadLibrary(
            List<String> libraryAssemblyNames,
            out List<Library> loadedLibraries,
            List<AppExtensionItemKind> extensionItemKinds = null)
        {
            Log log = new Log();
            loadedLibraries = new List<Library>();

            Log subLog = null;

            foreach (String libraryAssemblyName in libraryAssemblyNames)
            {
                Assembly assembly = AppDomainPool.LoadAssembly(AppDomain.CurrentDomain, libraryAssemblyName, log);

                if (assembly == null)
                    log.AddError("Could not load assembly '" + libraryAssemblyName + "'");
                else
                {
                    LibraryDefinition libraryDefinition = LibraryDefinition.Load(assembly, null, log);

                    if (libraryDefinition == null)
                        log.AddError("Error while attempting to load the library definition in assembly '" + libraryAssemblyName + "'");
                    else
                    {
                        subLog = new Log();
                        Library library = this._appDomain.LoadLibrary(
                            new LibraryDefinition() { AssemblyName = libraryAssemblyName },
                            subLog,
                            extensionItemKinds, new List<DataSourceKind>() { DataSourceKind.Memory });

                        log.AddSubLog(subLog, (p => p.HasErrorsOrExceptionsOrWarnings()), title: "Loading assembly '" + libraryAssemblyName + "'");

                        if (library != null && !subLog.HasErrorsOrExceptions())
                            if (!this._Libraries.Any(p => p.Definition !=null && p.Definition.Id.KeyEquals(library.Definition.Id)))
                                loadedLibraries.Add(library);
                    }
                }
            }

            this._Libraries.AddRange(loadedLibraries);

            return log;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize()
        {
            foreach (Library library in this._Libraries)
                library.Initialize(this);
        }

        #endregion

        // ------------------------------------------
        // STATIC METHODS
        // ------------------------------------------

        #region Static Methods

        /// <summary>
        /// Gets the dictionary type of the specified extension item kind.
        /// </summary>
        /// <param name="libraryItemKind">The extension item kind to consider.</param>
        /// <returns>The Xml string of this instance.</returns>
        public static Type GetDictionaryType(AppExtensionItemKind libraryItemKind)
        {
            switch (libraryItemKind)
            {
                case AppExtensionItemKind.Carrier:
                    return typeof(CarrierConfigurationIndex);
                case AppExtensionItemKind.Task:
                    return typeof(TaskIndex);
                //case AppExtensionItemKind.Command:
                //    return typeof(CommandDictionary);
                case AppExtensionItemKind.Connector:
                    return typeof(ConnectorIndex);
                case AppExtensionItemKind.Entity:
                    return typeof(EntityIndex);
                case AppExtensionItemKind.Handler:
                    return typeof(DataHandlerIndex);
                case AppExtensionItemKind.Metrics:
                    return typeof(MetricsIndex);
                case AppExtensionItemKind.ScriptWord:
                    return typeof(ScriptWordIndex);
                case AppExtensionItemKind.RoutineConfiguration:
                    return typeof(RoutineConfigurationIndex);
                default:
                    return null;
            }
        }

        /// <summary>
        /// Gets the class name of the specified complete name.
        /// </summary>
        /// <param name="completeName">The complete name to consider.</param>
        /// <returns>Returns the cloned metrics definition.</returns>
        public static String GetClassName(String completeName)
        {
            String className = completeName;

            if (completeName != null)
            {
                if (completeName.Contains("."))
                    className = className.Substring(
                        completeName.IndexOf('.') + 1);

                if (completeName.Contains(","))
                    className = className.Substring(0, className.IndexOf(','));
            }

            return (className ?? "");
        }

        #endregion
    }
}
