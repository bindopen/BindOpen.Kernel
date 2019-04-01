using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Helpers.Serialization;
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
using BindOpen.Framework.Core.System.Assemblies;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Extensions.Runtime
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
        protected List<Library> _libraries = new List<Library>();

        /// <summary>
        /// The application domain of this instance.
        /// </summary>
        private AppDomain _appDomain = null;

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Application domain of this instance.
        /// </summary>
        public AppDomain AppDomain
        {
            get { return _appDomain; }
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
            _appDomain= appDomain;
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
        public List<string> GetLibraryNames()
        {
            return _libraries.Select(p => p.Name).Distinct(StringComparer.CurrentCultureIgnoreCase).ToList();
        }

        /// <summary>
        /// Returns the specified library.
        /// </summary>
        /// <param name="name">The name of the library to consider.</param>
        /// <returns>The library with the specified name.</returns>
        public Library GetLibrary(string name)
        {
            if (name==null) return null;
            return _libraries.Find(p => p.KeyEquals(name));
        }

        /// <summary>
        /// Returns the specified library definition.
        /// </summary>
        /// <param name="name">The name of the library to consider.</param>
        /// <returns>The library with the specified name.</returns>
        public LibraryDefinition GetLibraryDefinition(string name)
        {
            Library library = GetLibrary(name);
            return library?.Definition;
        }

        /// <summary>
        /// Returns the specified library.
        /// </summary>
        /// <param name="names">The names of the libraries to consider.</param>
        /// <returns>The libraries with the specified names.</returns>
        public List<Library> GetLibraries(string[] names=null)
        {
            if (names == null)
                return _libraries;
            return _libraries.Where(p => p.Name != null && names.Any(q=>q!=null && p.Name.KeyEquals(q))).ToList();
        }

        /// <summary>
        /// Returns the library definitions of this instance.
        /// </summary>
        /// <param name="names">The names of the libraries to consider.</param>
        /// <returns>Returns the library definitions of this instance.</returns>
        public List<LibraryDefinition> GetLibraryDefinitions(string[] names = null)
        {
            return GetLibraries(names).Select(p => p.Definition).Where(p=>p!=null).ToList();
        }

        // Item definitions ----------------------------------

        /// <summary>
        /// Returns the item definitions of this instance.
        /// </summary>
        /// <param name="libraryNames">The names of libraries to consider.</param>
        /// <returns>The script words of specified library names.</returns>
        public List<T> GetItemDefinitions<T>(string[] libraryNames = null) where T : AppExtensionItemDefinition
        {
            List<T> itemDefinitions = new List<T>();
            foreach (Library library in GetLibraries(libraryNames))
                itemDefinitions.AddRange(library.GetItemDefinitions<T>());

            return itemDefinitions;
        }

        /// <summary>
        /// Returns the item definitions of this instance.
        /// </summary>
        /// <param name="libraryNames">The names of libraries to consider.</param>
        /// <returns>The script words of specified library names.</returns>
        public List<string> GetItemDefinitionUniqueIds<T>(
            string[] libraryNames = null) where T : AppExtensionItemDefinition
        {
            return GetItemDefinitions<T>(libraryNames).Select(p=>p.Key()).ToList();
        }

        /// <summary>
        /// Returns the item definition with the specified unique name.
        /// </summary>
        /// <param name="uniqueId">The unique ID of item to return.</param>
        /// <param name="libraryNames">The names of libraries to consider.</param>
        /// <returns>The item with the specified unique name.</returns>
        public T GetItemDefinitionWithUniqueId<T>(
            string uniqueId,
            string[] libraryNames = null) where T : AppExtensionItemDefinition
        {
            T itemDefinition = null;
            foreach (Library library in GetLibraries(libraryNames))
            {
                if ((itemDefinition = library.GetItemDefinitionWithUniqueId<T>(uniqueId)) != null)
                    break;
            }

            return itemDefinition;
        }

        // Script word definitions ---------------------------

        /// <summary>
        /// Returns the possible parent definitions of the specified script word definition.
        /// </summary>
        /// <param name="definitionName">The definition name to consider.</param>
        /// <param name="libraryNames">The names of libraries to consider.</param>
        /// <returns>The parent definitions of the specified script word definition.</returns>
        public List<ScriptWordDefinition> GetParentScriptWordDefinitions(
            string definitionName,
            string[] libraryNames = null)
        {
            List<ScriptWordDefinition> parentDefinitions = GetParentScriptWordDefinitions(definitionName, null, libraryNames).Distinct().ToList();

            return parentDefinitions;
        }

        private List<ScriptWordDefinition> GetParentScriptWordDefinitions(
            string definitionName,
            ScriptWordDefinition parentFeachDefinition,
            string[] libraryNames = null)
        {
            List<ScriptWordDefinition> parentDefinitions = new List<ScriptWordDefinition>();

            if (definitionName != null)
            {
                List<ScriptWordDefinition> definitions = 
                    (parentFeachDefinition == null ?  GetItemDefinitions<ScriptWordDefinition>(libraryNames) : parentFeachDefinition.Children);
                foreach (ScriptWordDefinition currentScriptWordDefinition in definitions)
                {
                    if (currentScriptWordDefinition.KeyEquals(definitionName) && parentFeachDefinition != null)
                        parentDefinitions.Add(parentFeachDefinition);

                    parentDefinitions.AddRange(GetParentScriptWordDefinitions(definitionName, currentScriptWordDefinition, libraryNames));
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
                if (!_libraries.Any(p => p.KeyEquals(library.Name)))
                    _libraries.Add(library);
            }
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            _libraries = new List<Library>();
        }

        #endregion

        // ------------------------------------------
        // LIBRARY ELEMENTS
        // ------------------------------------------

        #region Library Elements

        // Assembly --------------------------------------

        /// <summary>
        /// Creates the instance of the specified extension item.
        /// </summary>
        /// <param name="definitionName">The unique name of the definition to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        /// <param name="name">The name to consider.</param>
        /// <param name="log">The log to consider.</param>
        public ITAppExtensionRuntimeItem<T> CreateItem<T>(
            String definitionName,
            TAppExtensionItemConfiguration<T> configuration,
            String name = null,
            ILog log = null) where T : AppExtensionItemDefinition
        {
            if (this.Check(true).HasErrorsOrExceptions())
                return null;

            if (definitionName == null && configuration != null)
                definitionName = configuration.DefinitionUniqueId;

            AppExtensionItemKind extensionItemKind = typeof(T).GetExtensionItemKind();
            AssemblyHelper.ClassReference assemblyReference =
                this.GetItemClassReference(extensionItemKind, definitionName, out AppExtensionItemDefinition definitionObject);
            T definition = definitionObject as T;

            if (definition == null)
            {
                log?.AddError("Could not retrieve the extension item '" + definitionName + "' of kind '" + extensionItemKind.ToString() + "'");
            }

            Object extensionObject = null;
            log?.AddEvents(this.AppDomain.CreateInstance(assemblyReference, out extensionObject, name, configuration, this));

            ITAppExtensionRuntimeItem<T> extensionItem = extensionObject as ITAppExtensionRuntimeItem<T>;

            if (extensionItem == null)
            {
                log?.AddError("Could not create '" + extensionItemKind.ToString() + "' object with unique name '" + definitionName + "'");
            }
            else
            {
                if (configuration == null)
                {
                    configuration = this.CreateConfiguration<T>(definitionName, null, log);
                }
                else
                {
                    configuration.DefinitionUniqueId = definitionName;
                    configuration.SetDefinition(definition);
                }

                extensionItem.SetConfiguration(configuration);
            }

            return extensionItem;
        }


        /// <summary>
        /// Gets the extension item class reference of the specified object.
        /// </summary>
        /// <param name="extensionItemKind">The extension item kind to consider.</param>
        /// <param name="uniqueName">The unique name of the extension item defintion to consider.</param>
        /// <param name="extensionItemDefinition">The corresponding library item definition.</param>
        public AssemblyHelper.ClassReference GetItemClassReference(
            AppExtensionItemKind extensionItemKind,
            string uniqueName,
            out AppExtensionItemDefinition extensionItemDefinition)
        {
            AssemblyHelper.ClassReference assemblyReference = new AssemblyHelper.ClassReference();
            extensionItemDefinition = null;

            switch (extensionItemKind)
            {
                case AppExtensionItemKind.Carrier:
                    CarrierDefinition carrierDefinition = GetItemDefinitionWithUniqueId<CarrierDefinition>(uniqueName);
                    if (carrierDefinition != null)
                        assemblyReference = AssemblyHelper.GetClassReference(carrierDefinition.ItemClass);
                    extensionItemDefinition = carrierDefinition;
                    break;
                case AppExtensionItemKind.Task:
                    TaskDefinition taskDefinition = GetItemDefinitionWithUniqueId<TaskDefinition>(uniqueName);
                    if (taskDefinition != null)
                        assemblyReference = AssemblyHelper.GetClassReference(taskDefinition.ItemClass);
                    extensionItemDefinition = taskDefinition;
                    break;
                case AppExtensionItemKind.Connector:
                    ConnectorDefinition dataConnectorDefinition = GetItemDefinitionWithUniqueId<ConnectorDefinition>(uniqueName);
                    if (dataConnectorDefinition != null)
                        assemblyReference = AssemblyHelper.GetClassReference(dataConnectorDefinition.ItemClass);
                    extensionItemDefinition = dataConnectorDefinition;
                    break;
                case AppExtensionItemKind.Entity:
                    EntityDefinition dataEntityDefinition = GetItemDefinitionWithUniqueId<EntityDefinition>(uniqueName);
                    if (dataEntityDefinition != null)
                        assemblyReference = AssemblyHelper.GetClassReference(dataEntityDefinition.ItemClass);
                    extensionItemDefinition = dataEntityDefinition;
                    break;
                case AppExtensionItemKind.Format:
                    FormatDefinition dataFormatDefinition = GetItemDefinitionWithUniqueId<FormatDefinition>(uniqueName);
                    if (dataFormatDefinition != null)
                        assemblyReference = AssemblyHelper.GetClassReference(dataFormatDefinition.ItemClass);
                    extensionItemDefinition = dataFormatDefinition;
                    break;
                case AppExtensionItemKind.Handler:
                    HandlerDefinition dataHandlerDefinition = GetItemDefinitionWithUniqueId<HandlerDefinition>(uniqueName);
                    if (dataHandlerDefinition != null)
                        assemblyReference = AssemblyHelper.GetClassReference(dataHandlerDefinition.CallingClass);
                    extensionItemDefinition = dataHandlerDefinition;
                    break;
                case AppExtensionItemKind.RoutineConfiguration:
                    RoutineDefinition routineDefinition = GetItemDefinitionWithUniqueId<RoutineDefinition>(uniqueName);
                    if (routineDefinition != null)
                        assemblyReference = AssemblyHelper.GetClassReference(routineDefinition.ItemClass);
                    extensionItemDefinition = routineDefinition;
                    break;
                case AppExtensionItemKind.ScriptWord:
                    ScriptWordDefinition scriptWordDefinition = GetItemDefinitionWithUniqueId<ScriptWordDefinition>(uniqueName);
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
            string uniqueName)
        {
            AppExtensionItemDefinition extensionItemDefinition = null;
            return GetItemClassReference(extensionItemKind, uniqueName, out extensionItemDefinition);
        }

        // Configurations --------------------------------------

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="definitionUniqueId">The unique ID of the definition to consider.</param>
        /// <param name="xmlstring">The XML string to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="parameters">The object parameters to consider.</param>
        public TAppExtensionItemConfiguration<T> CreateConfiguration<T>(
            string definitionUniqueId,
            string xmlstring = null,
            ILog log = null,
            params object[] parameters) where T : AppExtensionItemDefinition
        {
            AppExtensionItemKind extensionItemKind = typeof(T).GetExtensionItemKind();

            T definition = GetItemDefinitionWithUniqueId<T>(definitionUniqueId);

            TAppExtensionItemConfiguration<T> configuration = null;

            if (definition == null)
            {
                if (log != null)
                    log.AddError("Could not retrieve the extension item '" + definitionUniqueId + "' of kind '" + extensionItemKind.Tostring() + "'");
            }
            else if (xmlstring == null)
                switch (extensionItemKind)
                {
                    case AppExtensionItemKind.Carrier:
                        configuration = new CarrierConfiguration(null, definitionUniqueId) as TAppExtensionItemConfiguration<T>;
                        break;
                    case AppExtensionItemKind.Task:
                        configuration = new TaskConfiguration(null, definitionUniqueId) as TAppExtensionItemConfiguration<T>;
                        break;
                    case AppExtensionItemKind.Connector:
                        configuration = new ConnectorConfiguration(null, definitionUniqueId) as TAppExtensionItemConfiguration<T>;
                        break;
                    case AppExtensionItemKind.Entity:
                        configuration = new EntityConfiguration(null, definitionUniqueId) as TAppExtensionItemConfiguration<T>;
                        break;
                    case AppExtensionItemKind.Format:
                        configuration = new FormatConfiguration(null, definitionUniqueId) as TAppExtensionItemConfiguration<T>;
                        break;
                    case AppExtensionItemKind.Metrics:
                        configuration = new MetricsConfiguration(null, definitionUniqueId) as TAppExtensionItemConfiguration<T>;
                        break;
                    case AppExtensionItemKind.RoutineConfiguration:
                        configuration = new RoutineConfiguration(null, definitionUniqueId) as TAppExtensionItemConfiguration<T>;
                        break;
                }
            else
                switch (extensionItemKind)
                {
                    case AppExtensionItemKind.Carrier:
                        configuration = XmlHelper.LoadFromstring<CarrierConfiguration>(xmlstring, log) as TAppExtensionItemConfiguration<T>;
                        break;
                    case AppExtensionItemKind.Task:
                        configuration = XmlHelper.LoadFromstring<CarrierConfiguration>(xmlstring, log) as TAppExtensionItemConfiguration<T>;
                        break;
                    case AppExtensionItemKind.Connector:
                        configuration = XmlHelper.LoadFromstring<ConnectorConfiguration>(xmlstring, log) as TAppExtensionItemConfiguration<T>;
                        break;
                    case AppExtensionItemKind.Entity:
                        configuration = XmlHelper.LoadFromstring<EntityConfiguration>(xmlstring, log) as TAppExtensionItemConfiguration<T>;
                        break;
                    case AppExtensionItemKind.Format:
                        configuration = XmlHelper.LoadFromstring<FormatConfiguration>(xmlstring, log) as TAppExtensionItemConfiguration<T>;
                        break;
                    case AppExtensionItemKind.Metrics:
                        configuration = XmlHelper.LoadFromstring<MetricsConfiguration>(xmlstring, log) as TAppExtensionItemConfiguration<T>;
                        break;
                    case AppExtensionItemKind.RoutineConfiguration:
                        configuration = XmlHelper.LoadFromstring<RoutineConfiguration>(xmlstring, log) as TAppExtensionItemConfiguration<T>;
                        break;
                }

            if (configuration!=null)
            {
                configuration.DefinitionUniqueId = definitionUniqueId;
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
            string uniqueName)
        {
            AppExtensionItemDefinition extensionItemDefinition = null;
            AssemblyHelper.ClassReference assemblyReference =
                GetItemClassReference(extensionItemKind, uniqueName, out extensionItemDefinition);

            return GetTypeFromAssemblyReference(assemblyReference);
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
                    return typeof(CarrierIndex);
                case AppExtensionItemKind.Task:
                    return typeof(TaskIndex);
                //case AppExtensionItemKind.Command:
                //    return typeof(CommandDictionary);
                case AppExtensionItemKind.Connector:
                    return typeof(ConnectorIndex);
                case AppExtensionItemKind.Entity:
                    return typeof(EntityIndex);
                case AppExtensionItemKind.Handler:
                    return typeof(HandlerIndex);
                case AppExtensionItemKind.Metrics:
                    return typeof(MetricsIndex);
                case AppExtensionItemKind.ScriptWord:
                    return typeof(ScriptWordIndex);
                case AppExtensionItemKind.RoutineConfiguration:
                    return typeof(RoutineIndex);
                default:
                    return null;
            }
        }

        /// <summary>
        /// Gets the class name of the specified complete name.
        /// </summary>
        /// <param name="completeName">The complete name to consider.</param>
        /// <returns>Returns the cloned metrics definition.</returns>
        public static string GetClassName(string completeName)
        {
            string className = completeName;

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
