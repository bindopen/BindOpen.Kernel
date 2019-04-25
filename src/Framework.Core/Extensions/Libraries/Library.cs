using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Common;
using BindOpen.Framework.Core.Extensions.Definitions;
using BindOpen.Framework.Core.Extensions.Definitions.Carriers;
using BindOpen.Framework.Core.Extensions.Definitions.Connectors;
using BindOpen.Framework.Core.Extensions.Definitions.Entities;
using BindOpen.Framework.Core.Extensions.Definitions.Handlers;
using BindOpen.Framework.Core.Extensions.Definitions.Libraries;
using BindOpen.Framework.Core.Extensions.Definitions.Metrics;
using BindOpen.Framework.Core.Extensions.Definitions.Routines;
using BindOpen.Framework.Core.Extensions.Definitions.Scriptwords;
using BindOpen.Framework.Core.Extensions.Definitions.Tasks;

namespace BindOpen.Framework.Core.Extensions.Libraries
{
    /// <summary>
    /// This class represents an library.
    /// </summary>
    [Serializable()]
    [XmlType("Library", Namespace = "https://bindopen.org/xsd")]
    public class Library : NamedDataItem, ILibrary
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        /// <summary>
        /// The data source kind of this instance.
        /// </summary>        
        protected DataSourceKind _sourceKind = DataSourceKind.Any;

        // Definitions ----------------------------------

        /// <summary>
        /// The carrier definitions of this instance.
        /// </summary>
        protected List<ICarrierDefinition> _carrierDefinitions = new List<ICarrierDefinition>();

        /// <summary>
        /// The connector definitions of this instance.
        /// </summary>
        protected List<IConnectorDefinition> _connectorDefinitions = new List<IConnectorDefinition>();

        /// <summary>
        /// The class definitions of this instance.
        /// </summary>
        protected List<IEntityDefinition> _entityDefinitions = new List<IEntityDefinition>();

        /// <summary>
        /// The reference definitions of this instance.
        /// </summary>
        protected List<IHandlerDefinition> _handlerDefinitions = new List<IHandlerDefinition>();

        /// <summary>
        /// The metrics definitions of this instance.
        /// </summary>
        protected List<IMetricsDefinition> _metricsDefinitions = new List<IMetricsDefinition>();

        /// <summary>
        /// The routine definitions of this instance.
        /// </summary>
        protected List<IRoutineDefinition> _routineDefinitions = new List<IRoutineDefinition>();

        /// <summary>
        /// The script word definitions of this instance.
        /// </summary>
        protected List<IScriptwordDefinition> _scriptWordDefinitions = new List<IScriptwordDefinition>();

        /// <summary>
        /// The task definitions of this instance.
        /// </summary>
        protected List<ITaskDefinition> _taskDefinitions = new List<ITaskDefinition>();

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        // Definition -------------------------------

        /// <summary>
        /// Definition unique ID of this instance.
        /// </summary>
        [XmlElement("definition")]
        public string DefinitionName
        {
            get { return Definition?.Name; }
        }

        /// <summary>
        /// The definition of this instance.
        /// </summary>
        [XmlIgnore()]
        public ILibraryDefinitionDto Definition { get; set; } = null;

        // Assemblies -------------------------------

        /// <summary>
        /// The assembly of this instance.
        /// </summary>
        [XmlIgnore()]
        public Assembly Assembly { get; set; } = null;

        /// <summary>
        /// The ExtensionDataContext kind of this instance.
        /// </summary>
        [XmlElement("dataSourceKind")]
        public DataSourceKind SourceKind
        {
            get { return _sourceKind; }
            set { _sourceKind = value; }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of Library class.
        /// </summary>
        public Library()
        {
        }

        /// <summary>
        /// Instantiates a new instance of Library class from a library instance.
        /// </summary>
        /// <param name="definition">Business library definition to consider.</param>
        public Library(ILibraryDefinitionDto definition)
        {
            Definition = definition;
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            _carrierDefinitions = new List<ICarrierDefinition>();
            _connectorDefinitions = new List<IConnectorDefinition>();
            _entityDefinitions = new List<IEntityDefinition>();
            _handlerDefinitions = new List<IHandlerDefinition>();
            _metricsDefinitions = new List<IMetricsDefinition>();
            _routineDefinitions = new List<IRoutineDefinition>();
            _scriptWordDefinitions = new List<IScriptwordDefinition>();
            _taskDefinitions = new List<ITaskDefinition>();
        }

        /// <summary>
        /// Adds the specified definition.
        /// </summary>
        /// <typeparam name="T">The application extension item definition class to consider.</typeparam>
        /// <param name="definition">The definition to add.</param>
        public void Add<T>(T definition) where T : IAppExtensionItemDefinition
        {
            if (definition is ICarrierDefinition carier)
            {
                _carrierDefinitions.Add(carier);
            }
            else if (definition is IConnectorDefinition connector)
            {
                _connectorDefinitions.Add(connector);
            }
            else if (definition is IEntityDefinition entity)
            {
                _entityDefinitions.Add(entity);
            }
            else if (definition is IHandlerDefinition handler)
            {
                _handlerDefinitions.Add(handler);
            }
            else if (definition is IMetricsDefinition metrics)
            {
                _metricsDefinitions.Add(metrics);
            }
            else if (definition is IRoutineDefinition routine)
            {
                _routineDefinitions.Add(routine);
            }
            else if (definition is IScriptwordDefinition scriptWord)
            {
                _scriptWordDefinitions.Add(scriptWord);
            }
            else if (definition is ITaskDefinition task)
            {
                _taskDefinitions.Add(task);
            }
        }

        /// <summary>
        /// Adds the specified definition.
        /// </summary>
        /// <typeparam name="T">The application extension item definition class to consider.</typeparam>
        /// <param name="key">The key to consider.</param>
        public void Delete<T>(string key) where T : IAppExtensionItemDefinition
        {
            if (typeof(T) is ICarrierDefinition)
            {
                _carrierDefinitions.RemoveAll(p=>p.KeyEquals(key));
            }
            else if (typeof(T) is IConnectorDefinition)
            {
                _connectorDefinitions.RemoveAll(p=>p.KeyEquals(key));
            }
            else if (typeof(T) is IEntityDefinition)
            {
                _entityDefinitions.RemoveAll(p=>p.KeyEquals(key));
            }
            else if (typeof(T) is IHandlerDefinition)
            {
                _handlerDefinitions.RemoveAll(p=>p.KeyEquals(key));
            }
            else if (typeof(T) is IMetricsDefinition)
            {
                _metricsDefinitions.RemoveAll(p=>p.KeyEquals(key));
            }
            else if (typeof(T) is IRoutineDefinition)
            {
                _routineDefinitions.RemoveAll(p=>p.KeyEquals(key));
            }
            else if (typeof(T) is IScriptwordDefinition)
            {
                _scriptWordDefinitions.RemoveAll(p=>p.KeyEquals(key));
            }
            else if (typeof(T) is ITaskDefinition)
            {
                _taskDefinitions.RemoveAll(p=>p.KeyEquals(key));
            }
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Gets the key of this instance.
        /// </summary>
        /// <returns>Returns the key of this instance.</returns>
        public override string Key()
        {
            return DefinitionName ?? Name;
        }

        /// <summary>
        /// Gets the normalized name of this instance.
        /// </summary>
        /// <returns>Returns the normalized name of this instance.</returns>
        /// <remarks>The normalized name is the name in which only the alphanumeric characters and _ are allowed.</remarks>
        public string GetNormalizedName()
        {
            return DefinitionName.GetNormalizedName();
        }

        // Item definitions ----------------------------------

        /// <summary>
        /// Returns the item definitions of this instance.
        /// </summary>
        /// <returns>The item words of specified library names.</returns>
        public List<T> GetItemDefinitions<T>() where T : IAppExtensionItemDefinition
        {
            List<T> itemDefinitions = new List<T>();

            switch (typeof(T).GetExtensionItemKind())
            {
                case AppExtensionItemKind.Carrier:
                    return _carrierDefinitions.ToList() as List<T>;
                case AppExtensionItemKind.Task:
                    return _taskDefinitions.ToList() as List<T>;
                case AppExtensionItemKind.Connector:
                    return _connectorDefinitions.ToList() as List<T>;
                case AppExtensionItemKind.Entity:
                    return _entityDefinitions.ToList() as List<T>;
                case AppExtensionItemKind.Format:
                    return _entityDefinitions.SelectMany(p => p.Dto?.FormatDefinitions).Distinct().ToList() as List<T>;
                case AppExtensionItemKind.Metrics:
                    return _metricsDefinitions.ToList() as List<T>;
                case AppExtensionItemKind.Handler:
                    return _handlerDefinitions.ToList() as List<T>;
                case AppExtensionItemKind.Routine:
                    return _routineDefinitions.ToList() as List<T>;
                case AppExtensionItemKind.Scriptword:
                    return _scriptWordDefinitions.ToList() as List<T>;
            }

            return itemDefinitions;
        }

        /// <summary>
        /// Returns the item definition with the specified unique name.
        /// </summary>
        /// <param name="uniqueName">The unique ID of item to return.</param>
        /// <returns>The item with the specified unique name.</returns>
        public T GetItemDefinitionWithUniqueId<T>(string uniqueName) where T : IAppExtensionItemDefinition
        {
            T aItemDefinition = default;

            if (uniqueName != null)
            {
                switch (typeof(T).GetExtensionItemKind())
                {
                    case AppExtensionItemKind.Carrier:
                        return (T)_carrierDefinitions.Find(p => p.KeyEquals(uniqueName));
                    case AppExtensionItemKind.Task:
                        return (T)_taskDefinitions.Find(p => p.KeyEquals(uniqueName));
                    case AppExtensionItemKind.Connector:
                        return (T)_connectorDefinitions.Find(p => p.KeyEquals(uniqueName));
                    case AppExtensionItemKind.Entity:
                        return (T)_entityDefinitions.Find(p => p.KeyEquals(uniqueName));
                    case AppExtensionItemKind.Metrics:
                        return (T)_metricsDefinitions.Find(p => p.KeyEquals(uniqueName));
                    case AppExtensionItemKind.Handler:
                        return (T)_handlerDefinitions.Find(p => p.KeyEquals(uniqueName));
                    case AppExtensionItemKind.Routine:
                        return (T)_routineDefinitions.Find(p => p.KeyEquals(uniqueName));
                    case AppExtensionItemKind.Scriptword:
                        return (T)GetScriptwordDefinitionWithUniqueName(uniqueName);
                    default:
                        break;
                }
            }

            return aItemDefinition;
        }

        // Script words ------------------------------------------------

        /// <summary>
        /// Returns the script word definition with the specified unique name.
        /// </summary>
        /// <param name="uniqueName">The unique ID of script word to return.</param>
        /// <param name="parentDefinition">The parent definition to return.</param>
        /// <returns>The script word with the specified unique name.</returns>
        public IScriptwordDefinition GetScriptwordDefinitionWithUniqueName(
            string uniqueName, IScriptwordDefinition parentDefinition = null)
        {
            if (uniqueName == null)
                return null;

            List<IScriptwordDefinition> definitions =
                parentDefinition == null ? _scriptWordDefinitions : new List<IScriptwordDefinition>(parentDefinition.Children);

            IScriptwordDefinition scriptWordDefinition = null;
            foreach (IScriptwordDefinition currentDefinition in definitions)
            {
                if (currentDefinition.KeyEquals(uniqueName))
                {
                    return currentDefinition;
                }
                else
                   if ((scriptWordDefinition = GetScriptwordDefinitionWithUniqueName(uniqueName, currentDefinition)) != null)
                {
                    break;
                }
            }

            return scriptWordDefinition;
        }

        #endregion

        // ------------------------------------------
        // INITIALIZATION
        // ------------------------------------------

        #region Initialization

        /// <summary>
        /// Inializes the specified library.
        /// </summary>
          /// <param name="appExtension">The application extension to consider.</param>
        public void Initialize(IAppExtension appExtension)
        {
            if (appExtension == null) return;

            InitializeScriptTree(appExtension);
        }

        private void InitializeScriptTree(IAppExtension appExtension, IScriptwordDefinition parentScriptwordDefinition = null)
        {
            if (appExtension == null) return;

            List<IScriptwordDefinition> scriptWordDefinitions = new List<IScriptwordDefinition>();

            if (parentScriptwordDefinition == null)
                scriptWordDefinitions = _scriptWordDefinitions;
            else
                scriptWordDefinitions = new List<IScriptwordDefinition>(parentScriptwordDefinition.Children);

            // we recursively retrieve the sub script words
            int i = 0;
            while (i < scriptWordDefinitions.Count)
            {
                IScriptwordDefinition currentScriptwordDefinition = scriptWordDefinitions[i];

                // if the current script word is a reference then
                if (!string.IsNullOrEmpty(currentScriptwordDefinition?.Dto.ReferenceUniqueName))
                {
                    // we retrieve the reference script word
                    IScriptwordDefinition referenceScriptwordDefinition = GetScriptwordDefinitionWithUniqueName(currentScriptwordDefinition?.Dto.ReferenceUniqueName);
                    if (referenceScriptwordDefinition == null)
                    {
                        referenceScriptwordDefinition = appExtension.GetItemDefinitionWithUniqueId<IScriptwordDefinition>(
                           currentScriptwordDefinition?.Dto.ReferenceUniqueName,
                           appExtension.GetLibraryNames().Excluding(Name).ToArray());
                    }

                    if (referenceScriptwordDefinition != null)
                        scriptWordDefinitions[i] = referenceScriptwordDefinition;
                }
                else
                {
                    InitializeScriptTree(appExtension, currentScriptwordDefinition);
                }

                i++;
            }
        }

        #endregion
    }
}
