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
using BindOpen.Framework.Core.Extensions.Definition;
using BindOpen.Framework.Core.Extensions.Definition.Carriers;
using BindOpen.Framework.Core.Extensions.Definition.Connectors;
using BindOpen.Framework.Core.Extensions.Definition.Entities;
using BindOpen.Framework.Core.Extensions.Definition.Handlers;
using BindOpen.Framework.Core.Extensions.Definition.Libraries;
using BindOpen.Framework.Core.Extensions.Definition.Metrics;
using BindOpen.Framework.Core.Extensions.Definition.Routines;
using BindOpen.Framework.Core.Extensions.Definition.Scriptwords;
using BindOpen.Framework.Core.Extensions.Definition.Tasks;

namespace BindOpen.Framework.Core.Extensions.Runtime.Libraries
{
    /// <summary>
    /// This class represents an library.
    /// </summary>
    [Serializable()]
    [XmlType("Library", Namespace = "http://meltingsoft.com/bindopen/xsd")]
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
        protected List<IScriptWordDefinition> _scriptWordDefinitions = new List<IScriptWordDefinition>();

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
        /// Definition unique name of this instance.
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
        public ILibraryDefinition Definition { get; set; } = null;

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
        public Library(ILibraryDefinition definition)
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
            _scriptWordDefinitions = new List<IScriptWordDefinition>();
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
            else if (definition is IScriptWordDefinition scriptWord)
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
            else if (typeof(T) is IScriptWordDefinition)
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
                    return _entityDefinitions.SelectMany(p => p.FormatDefinitions).Distinct().ToList() as List<T>;
                case AppExtensionItemKind.Metrics:
                    return _metricsDefinitions.ToList() as List<T>;
                case AppExtensionItemKind.Handler:
                    return _handlerDefinitions.ToList() as List<T>;
                case AppExtensionItemKind.RoutineConfiguration:
                    return _routineDefinitions.ToList() as List<T>;
                case AppExtensionItemKind.ScriptWord:
                    return _scriptWordDefinitions.ToList() as List<T>;
            }

            return itemDefinitions;
        }

        /// <summary>
        /// Returns the item definition with the specified unique name.
        /// </summary>
        /// <param name="uniqueName">The unique name of item to return.</param>
        /// <returns>The item with the specified unique name.</returns>
        public T GetItemDefinitionWithUniqueId<T>(String uniqueName) where T : IAppExtensionItemDefinition
        {
            T aItemDefinition = default;

            if (uniqueName != null)
            {
                switch (typeof(T).GetExtensionItemKind())
                {
                    case AppExtensionItemKind.Carrier:
                        return (T)_carrierDefinitions.Find(p => p.KeyEquals(uniqueName));
                    case AppExtensionItemKind.Task:
                        return (T)_taskDefinitions.FirstOrDefault(p => p.KeyEquals(uniqueName));
                    case AppExtensionItemKind.Connector:
                        return (T)_connectorDefinitions.FirstOrDefault(p => p.KeyEquals(uniqueName));
                    case AppExtensionItemKind.Entity:
                        return (T)_entityDefinitions.FirstOrDefault(p => p.KeyEquals(uniqueName));
                    case AppExtensionItemKind.Format:
                        return (T)_entityDefinitions.SelectMany(p => p.FormatDefinitions).FirstOrDefault(p => p.KeyEquals(uniqueName));
                    case AppExtensionItemKind.Metrics:
                        return (T)_metricsDefinitions.FirstOrDefault(p => p.KeyEquals(uniqueName));
                    case AppExtensionItemKind.Handler:
                        return (T)_handlerDefinitions.FirstOrDefault(p => p.KeyEquals(uniqueName));
                    case AppExtensionItemKind.RoutineConfiguration:
                        return (T)_routineDefinitions.FirstOrDefault(p => p.KeyEquals(uniqueName));
                    case AppExtensionItemKind.ScriptWord:
                        return (T)GetScriptWordDefinitionWithUniqueName(uniqueName);
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
        /// <param name="uniqueName">The unique name of script word to return.</param>
        /// <param name="parentDefinition">The parent definition to return.</param>
        /// <returns>The script word with the specified unique name.</returns>
        public IScriptWordDefinition GetScriptWordDefinitionWithUniqueName(
            string uniqueName, IScriptWordDefinition parentDefinition = null)
        {
            if (uniqueName == null)
                return null;

            List<IScriptWordDefinition> definitions =
                (parentDefinition == null ? _scriptWordDefinitions : parentDefinition.Children);

            IScriptWordDefinition scriptWordDefinition = null;
            foreach (IScriptWordDefinition currentDefinition in definitions)
            {
                if (currentDefinition.KeyEquals(uniqueName))
                {
                    return currentDefinition;
                }
                else
                   if ((scriptWordDefinition = GetScriptWordDefinitionWithUniqueName(uniqueName, currentDefinition)) != null)
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
        public void Initialize(AppExtension appExtension)
        {
            if (appExtension == null) return;

            InitializeScriptTree(appExtension);
        }

        private void InitializeScriptTree(AppExtension appExtension, ScriptWordDefinition parentScriptWordDefinition = null)
        {
            if (appExtension == null) return;

            List<ScriptWordDefinition> scriptWordDefinitions = new List<ScriptWordDefinition>();

            if (parentScriptWordDefinition == null)
                scriptWordDefinitions = _scriptWordDefinitions;
            else
                scriptWordDefinitions = parentScriptWordDefinition.Children;

            // we recursively retrieve the sub script words
            int i = 0;
            while (i < scriptWordDefinitions.Count)
            {
                ScriptWordDefinition currentScriptWordDefinition = scriptWordDefinitions[i];

                // if the current script word is a reference then
                if (!string.IsNullOrEmpty(currentScriptWordDefinition.ReferenceUniqueName))
                {
                    // we retrieve the reference script word
                    ScriptWordDefinition referenceScriptWordDefinition = GetScriptWordDefinitionWithUniqueName(currentScriptWordDefinition.ReferenceUniqueName);
                    if (referenceScriptWordDefinition == null)
                    {
                        referenceScriptWordDefinition = appExtension.GetItemDefinitionWithUniqueId<ScriptWordDefinition>(
                           currentScriptWordDefinition.ReferenceUniqueName,
                           appExtension.GetLibraryNames().Excluding(Name).ToArray());
                    }

                    if (referenceScriptWordDefinition != null)
                        scriptWordDefinitions[i] = referenceScriptWordDefinition;
                }
                else
                {
                    InitializeScriptTree(appExtension, currentScriptWordDefinition);
                }

                i++;
            }
        }

        //private String GetScriptWordUniqueName(ScriptWordDefinition scriptWordDefinition, String uniqueName)
        //{
        //    if (scriptWordDefinition == null) return "";

        //    switch (scriptWordDefinition.Kind)
        //    {
        //        case ScriptItemKind.Function:
        //            uniqueName = (uniqueName ?? "").GetStartedString("Fun_");
        //            break;
        //        case ScriptItemKind.Variable:
        //            uniqueName = (uniqueName ?? "").GetStartedString("Var_");
        //            break;
        //    }
        //    return (uniqueName.Contains("$") ? uniqueName : scriptWordDefinition.LibraryName + "$" + uniqueName);
        //}

        #endregion
    }
}
