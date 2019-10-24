using System;
using System.Collections.Generic;
using System.Linq;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Items;
using BindOpen.Framework.Core.Extensions.Items.Scriptwords.Definition;
using BindOpen.Framework.Core.Extensions.Libraries;
using BindOpen.Framework.Core.Extensions.Libraries.Definition;

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

        /// <summary>
        /// Definitions of script words.
        /// </summary>
        protected List<IScriptwordDefinition> _scriptwordDefinitions = new List<IScriptwordDefinition>();

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Application domain of this instance.
        /// </summary>
        public AppDomain AppDomain { get; } = null;

        /// <summary>
        /// Script word definitions of this instance.
        /// </summary>
        public List<IScriptwordDefinition> ScriptwordDefinitions => _scriptwordDefinitions;

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
        public AppExtension(AppDomain appDomain)
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
        public List<ILibraryDefinition> GetLibraryDefinitions(string[] names = null)
        {
            return GetLibraries(names).Select(p => p.Definition).Where(p => p != null).ToList();
        }

        /// <summary>
        /// Returns the specified library definition.
        /// </summary>
        /// <param name="name">The name of the library to consider.</param>
        /// <returns>The library with the specified name.</returns>
        public ILibraryDefinition GetLibraryDefinition(string name)
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

        // Script word definitions ---------------------------

        /// <summary>
        /// Returns the possible parent definitions of the specified script word definition.
        /// </summary>
        /// <param name="definitionName">The definition name to consider.</param>
        /// <param name="libraryNames">The names of libraries to consider.</param>
        /// <returns>The parent definitions of the specified script word definition.</returns>
        public List<IScriptwordDefinition> GetParentScriptwordDefinitions(
            string definitionName,
            string[] libraryNames = null)
        {
            return GetParentScriptwordDefinitions(definitionName, null, libraryNames).Distinct().ToList();
        }

        private List<IScriptwordDefinition> GetParentScriptwordDefinitions(
            string definitionName,
            IScriptwordDefinition parentFeachDefinition,
            string[] libraryNames = null)
        {
            List<IScriptwordDefinition> parentDefinitions = new List<IScriptwordDefinition>();

            if (definitionName != null)
            {
                List<IScriptwordDefinition> definitions =
                    (parentFeachDefinition == null ? _scriptwordDefinitions :
                    new List<IScriptwordDefinition>(parentFeachDefinition.Children));
                foreach (IScriptwordDefinition currentScriptwordDefinition in definitions)
                {
                    if (currentScriptwordDefinition.KeyEquals(definitionName) && parentFeachDefinition != null)
                        parentDefinitions.Add(parentFeachDefinition);

                    parentDefinitions.AddRange(GetParentScriptwordDefinitions(definitionName, currentScriptwordDefinition, libraryNames));
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

            // we update the script word definitions
            _scriptwordDefinitions.AddRange(GetItemDefinitions<IScriptwordDefinition>());
        }

        #endregion
    }
}
