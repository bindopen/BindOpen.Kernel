using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Definitions;
using BindOpen.Framework.Core.Extensions.Definitions.Libraries;
using BindOpen.Framework.Core.Extensions.Libraries;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Extensions
{
    /// <summary>
    /// This class represents an application extension.
    /// </summary>
    public class AppExtension : DataItem, IAppExtension
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        /// <summary>
        /// Libraries of this instance.
        /// </summary>
        protected List<ILibrary> _libraries = new List<ILibrary>();

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Application domain of this instance.
        /// </summary>
        public AppDomain AppDomain { get; } = null;

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
            AppDomain = appDomain;
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        // Libraries --------------------------------

        /// <summary>
        /// Returns the specified library.
        /// </summary>
        /// <param name="names">The names of the libraries to consider.</param>
        /// <returns>The libraries with the specified names.</returns>
        public List<ILibrary> GetLibraries(string[] names = null)
        {
            if (names == null)
                return _libraries;
            return _libraries.Where(p => p.Name != null && names.Any(q => q != null && p.Name.KeyEquals(q))).ToList();
        }

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
        public ILibrary GetLibrary(string name)
        {
            if (name==null) return null;
            return _libraries.Find(p => p.KeyEquals(name));
        }

        // Library definitions --------------------------------

        /// <summary>
        /// Returns the library definitions of this instance.
        /// </summary>
        /// <param name="names">The names of the libraries to consider.</param>
        /// <returns>Returns the library definitions of this instance.</returns>
        public List<ILibraryDefinitionDto> GetLibraryDefinitions(string[] names = null)
        {
            return GetLibraries(names).Select(p => p.Definition).Where(p => p != null).ToList();
        }

        /// <summary>
        /// Returns the specified library definition.
        /// </summary>
        /// <param name="name">The name of the library to consider.</param>
        /// <returns>The library with the specified name.</returns>
        public ILibraryDefinitionDto GetLibraryDefinition(string name)
        {
            ILibrary library = GetLibrary(name);
            return library?.Definition;
        }

        // Item definitions ----------------------------------

        /// <summary>
        /// Returns the item definitions of this instance.
        /// </summary>
        /// <param name="libraryNames">The names of libraries to consider.</param>
        /// <returns>The script words of specified library names.</returns>
        public List<T> GetItemDefinitions<T>(
            string[] libraryNames = null) where T : IAppExtensionItemDefinition
        {
            List<T> itemDefinitions = new List<T>();
            foreach (ILibrary library in GetLibraries(libraryNames))
                itemDefinitions.AddRange(library.GetItemDefinitions<T>());

            return itemDefinitions;
        }

        /// <summary>
        /// Returns the item definitions of this instance.
        /// </summary>
        /// <param name="libraryNames">The names of libraries to consider.</param>
        /// <returns>The script words of specified library names.</returns>
        public List<string> GetItemDefinitionUniqueIds<T>(
            string[] libraryNames = null) where T : IAppExtensionItemDefinition
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
            string[] libraryNames = null) where T : IAppExtensionItemDefinition
        {
            T itemDefinition = default;
            foreach (Library library in GetLibraries(libraryNames))
            {
                if ((itemDefinition = library.GetItemDefinitionWithUniqueId<T>(uniqueId)) != null)
                    break;
            }

            return itemDefinition;
        }

        //// Assemblies -------------------------

        ///// <summary>
        ///// Gets the extension item class reference of the specified object.
        ///// </summary>
        ///// <param name="extensionItemKind">The extension item kind to consider.</param>
        ///// <param name="uniqueId">The unique ID of the extension item defintion to consider.</param>
        ///// <param name="extensionItemDefinition">The corresponding library item definition.</param>
        //public AssemblyHelper.ClassReference GetItemClassReference(
        //    AppExtensionItemKind extensionItemKind,
        //    string uniqueId,
        //    out AppExtensionItemDefinitionDto extensionItemDefinition)
        //{
        //    AssemblyHelper.ClassReference assemblyReference = new AssemblyHelper.ClassReference();
        //    extensionItemDefinition = null;

        //    switch (extensionItemKind)
        //    {
        //        case AppExtensionItemKind.Carrier:
        //            CarrierDefinition carrierDefinition = GetItemDefinitionWithUniqueId<CarrierDefinition>(uniqueId);
        //            if (carrierDefinition != null)
        //                assemblyReference = AssemblyHelper.GetClassReference(carrierDefinition.ItemClass);
        //            extensionItemDefinition = carrierDefinition;
        //            break;
        //        case AppExtensionItemKind.Task:
        //            TaskDefinitionDto taskDefinition = GetItemDefinitionWithUniqueId<TaskDefinitionDto>(uniqueId);
        //            if (taskDefinition != null)
        //                assemblyReference = AssemblyHelper.GetClassReference(taskDefinition.ItemClass);
        //            extensionItemDefinition = taskDefinition;
        //            break;
        //        case AppExtensionItemKind.Connector:
        //            ConnectorDefinitionDto dataConnectorDefinition = GetItemDefinitionWithUniqueId<ConnectorDefinitionDto>(uniqueId);
        //            if (dataConnectorDefinition != null)
        //                assemblyReference = AssemblyHelper.GetClassReference(dataConnectorDefinition.ItemClass);
        //            extensionItemDefinition = dataConnectorDefinition;
        //            break;
        //        case AppExtensionItemKind.Entity:
        //            EntityDefinitionDto dataEntityDefinition = GetItemDefinitionWithUniqueId<EntityDefinitionDto>(uniqueId);
        //            if (dataEntityDefinition != null)
        //                assemblyReference = AssemblyHelper.GetClassReference(dataEntityDefinition.ItemClass);
        //            extensionItemDefinition = dataEntityDefinition;
        //            break;
        //        case AppExtensionItemKind.Format:
        //            FormatDefinitionDto dataFormatDefinition = GetItemDefinitionWithUniqueId<FormatDefinitionDto>(uniqueId);
        //            if (dataFormatDefinition != null)
        //                assemblyReference = AssemblyHelper.GetClassReference(dataFormatDefinition.ItemClass);
        //            extensionItemDefinition = dataFormatDefinition;
        //            break;
        //        case AppExtensionItemKind.Handler:
        //            HandlerDefinitionDto dataHandlerDefinition = GetItemDefinitionWithUniqueId<HandlerDefinitionDto>(uniqueId);
        //            if (dataHandlerDefinition != null)
        //                assemblyReference = AssemblyHelper.GetClassReference(dataHandlerDefinition.CallingClass);
        //            extensionItemDefinition = dataHandlerDefinition;
        //            break;
        //        case AppExtensionItemKind.Routine:
        //            RoutineDefinitionDto routineDefinition = GetItemDefinitionWithUniqueId<RoutineDefinitionDto>(uniqueId);
        //            if (routineDefinition != null)
        //                assemblyReference = AssemblyHelper.GetClassReference(routineDefinition.ItemClass);
        //            extensionItemDefinition = routineDefinition;
        //            break;
        //        case AppExtensionItemKind.Scriptword:
        //            ScriptwordDefinitionDto scriptWordDefinition = GetItemDefinitionWithUniqueId<ScriptwordDefinitionDto>(uniqueId);
        //            if (scriptWordDefinition != null)
        //                assemblyReference = AssemblyHelper.GetClassReference(scriptWordDefinition.CallingClass);
        //            extensionItemDefinition = scriptWordDefinition;
        //            break;
        //    }

        //    return assemblyReference;
        //}

        ///// <summary>
        ///// Gets the extension item class reference of the specified object.
        ///// </summary>
        ///// <param name="extensionItemKind">The extension item kind to consider.</param>
        ///// <param name="uniqueId">The unique ID of the extension item defintion to consider.</param>
        //protected AssemblyHelper.ClassReference GetItemClassReference(
        //    AppExtensionItemKind extensionItemKind,
        //    string uniqueId)
        //{
        //    AppExtensionItemDefinitionDto extensionItemDefinition = null;
        //    return GetItemClassReference(extensionItemKind, uniqueId, out extensionItemDefinition);
        //}

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Adds the specified library.
        /// </summary>
        /// <param name="library">The dynamic library to consider.</param>
        public void AddLibrary(ILibrary library)
        {
            if (library != null)
            {
                library.SourceKind = DataSourceKind.Memory;
                if (!_libraries.Any(p => p.KeyEquals(library.Name)))
                    _libraries.Add(library);
            }
        }

        /// <summary>
        /// Adds the specified libraries.
        /// </summary>
        /// <param name="libraries">The dynamic libraries to consider.</param>
        public void AddLibraries(ILibrary[] libraries)
        {
            foreach(ILibrary library in libraries)
            {
                AddLibrary(library);
            }
        }

        // From config -------------------------------

        /// <summary>
        /// Adds the specified libraries.
        /// </summary>
        /// <param name="config">The application extension configuration to consider.</param>
        /// <param name="folderPath">The folder path to consider.</param>
        /// <returns>Reurns the opeartion log.</returns>
        public ILog AddLibraries(
            IAppExtensionConfiguration config,
            string folderPath = null)
        {
            ILog log = new Log();

            if (config != null)
            {
                foreach (IAppExtensionFilter extensionFilter in config.GetFilters())
                {
                    Log subLog = new Log();

                    string defaultFolderPath = string.IsNullOrEmpty(extensionFilter.FolderPath) ?
                        config.DefaultFolderPath : extensionFilter.FolderPath;
                    if (string.IsNullOrEmpty(defaultFolderPath))
                        defaultFolderPath = folderPath;

                    ILibrary library = LibraryLoader.Load(AppDomain, extensionFilter, subLog);

                    if (library != null && !log.HasErrorsOrExceptions()
                        && !_libraries.Any(p => p.Definition?.KeyEquals(library.Definition) == true))
                    {
                        AddLibrary(library);
                    }

                    if (subLog.HasErrorsOrExceptionsOrWarnings())
                        log.AddSubLog(subLog, title: "Loading library '" + (extensionFilter?.Name ?? "?") + "'");
                    else
                        log.AddMessage("Library '" + (extensionFilter?.Name ?? "?") + "' loaded");
                }
            }

            return log;
        }

        // From file -------------------------------

        /// <summary>
        /// Adds the specifed libraries in the specified way.
        /// </summary>
        /// <param name="filePaths">The file paths to consider.</param>
        /// <param name="folderPath">The folder path to consider.</param>
        /// <returns>The log of the load task.</returns>
        /// <remarks>If null then we load the existing library names.</remarks>
        public virtual ILog AddLibrariesFromFile(
            string filePaths,
            string folderPath)
        {
            ILog log = new Log();

            Log subLog = null;

            folderPath = folderPath.GetEndedString(@"\").ToPath();

            if (string.IsNullOrEmpty(filePaths))
            {
                log.AddError("Assembly file path missing");
            }
            else
            {
                foreach (string subFilePath in filePaths.Split('|'))
                {
                    string completeSubFilePath = (folderPath + subFilePath).ToPath();

                    List<string> completeSubFilePaths = new List<string>();
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
                        completeSubFilePaths = new List<string>() { completeSubFilePath };
                    }

                    foreach (string filePath in completeSubFilePaths)
                    {
                        subLog = new Log();

                        ILibrary library = LibraryLoader.Load(
                            AppDomain,
                            new AppExtensionFilter(
                                null,
                                Path.GetFileName(filePath),
                                new[] { DataSourceKind.Repository },
                                Path.GetDirectoryName(filePath)),
                            subLog);

                        log.AddSubLog(subLog, p => p.HasErrorsOrExceptionsOrWarnings(), title: "Loading assembly '" + filePath + "'");

                        if (library != null && !log.HasErrorsOrExceptions())
                        {
                            if (!_libraries.Any(p => p.Definition?.Id.KeyEquals(library.Definition.Id) == true))
                            {
                                AddLibrary(library);
                            }
                        }
                    }
                }
            }

            return log;
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            _libraries = new List<ILibrary>();
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize()
        {
            foreach (ILibrary library in _libraries)
                library.Initialize(this);
        }

        #endregion
    }
}
