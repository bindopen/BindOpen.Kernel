using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Definition.Items;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Framework.Core.Extensions.Runtime.Stores
{
    /// <summary>
    /// This class represents a BindOpen extension item store.
    /// </summary>
    public class BdoExtensionStore : IdentifiedDataItem, IBdoExtensionStore
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private Dictionary<string, IBdoCarrierDefinition> _carrierDefinitions = new Dictionary<string, IBdoCarrierDefinition>();
        private Dictionary<string, IBdoConnectorDefinition> _connectorDefinitions = new Dictionary<string, IBdoConnectorDefinition>();
        private Dictionary<string, IBdoEntityDefinition> _entityDefinitions = new Dictionary<string, IBdoEntityDefinition>();
        private Dictionary<string, IBdoFormatDefinition> _formatDefinitions = new Dictionary<string, IBdoFormatDefinition>();
        private Dictionary<string, IBdoHandlerDefinition> _handlerDefinitions = new Dictionary<string, IBdoHandlerDefinition>();
        private Dictionary<string, IBdoMetricsDefinition> _metricsDefinitions = new Dictionary<string, IBdoMetricsDefinition>();
        private Dictionary<string, IBdoRoutineDefinition> _routineDefinitions = new Dictionary<string, IBdoRoutineDefinition>();
        private Dictionary<string, IBdoScriptwordDefinition> _scriptWordDefinitions = new Dictionary<string, IBdoScriptwordDefinition>();
        private Dictionary<string, IBdoTaskDefinition> _taskDefinitions = new Dictionary<string, IBdoTaskDefinition>();

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoExtensionStore class.
        /// </summary>
        public BdoExtensionStore()
        {
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        // Item definitions ----------------------------------

        /// <summary>
        /// Returns the item definitions of this instance.
        /// </summary>
        /// <returns>The item words of specified library names.</returns>
        public IEnumerable<T> GetItemDefinitionEnumerables<T>() where T : IBdoExtensionItemDefinition
            => GetItemDefinitions<T>().Values.ToList();

        /// <summary>
        /// Returns the item definitions of this instance.
        /// </summary>
        /// <returns>The item words of specified library names.</returns>
        public Dictionary<string, T> GetItemDefinitions<T>() where T : IBdoExtensionItemDefinition
        {
            switch (typeof(T).GetExtensionItemKind())
            {
                case BdoExtensionItemKind.Carrier:
                    return _carrierDefinitions as Dictionary<string, T>;
                case BdoExtensionItemKind.Connector:
                    return _connectorDefinitions as Dictionary<string, T>;
                case BdoExtensionItemKind.Entity:
                    return _entityDefinitions as Dictionary<string, T>;
                case BdoExtensionItemKind.Format:
                    return _entityDefinitions.SelectMany(p => p.Value?.Dto?.FormatDefinitions).Distinct().ToList() as Dictionary<string, T>;
                case BdoExtensionItemKind.Handler:
                    return _handlerDefinitions as Dictionary<string, T>;
                case BdoExtensionItemKind.Metrics:
                    return _metricsDefinitions as Dictionary<string, T>;
                case BdoExtensionItemKind.Routine:
                    return _routineDefinitions as Dictionary<string, T>;
                case BdoExtensionItemKind.Scriptword:
                    return _scriptWordDefinitions as Dictionary<string, T>;
                case BdoExtensionItemKind.Task:
                    return _taskDefinitions as Dictionary<string, T>;
            }

            return new Dictionary<string, T>();
        }

        /// <summary>
        /// Returns the item definition with the specified unique name.
        /// </summary>
        /// <param name="uniqueName">The unique ID of item to return.</param>
        /// <returns>The item with the specified unique name.</returns>
        public T GetItemDefinitionWithUniqueId<T>(string uniqueName) where T : IBdoExtensionItemDefinition
        {
            string upperUniqueId = uniqueName?.ToUpper();

            if (uniqueName != null)
            {
                switch (typeof(T).GetExtensionItemKind())
                {
                    case BdoExtensionItemKind.Carrier:
                        {
                            _carrierDefinitions.TryGetValue(upperUniqueId, out IBdoCarrierDefinition carrierDefinition);
                            return (T)carrierDefinition;

                        }
                    case BdoExtensionItemKind.Connector:
                        {
                            _connectorDefinitions.TryGetValue(upperUniqueId, out IBdoConnectorDefinition connectorDefinition);
                            return (T)connectorDefinition;
                        }
                    case BdoExtensionItemKind.Entity:
                        {
                            _entityDefinitions.TryGetValue(upperUniqueId, out IBdoEntityDefinition entityDefinition);
                            return (T)entityDefinition;
                        }
                    case BdoExtensionItemKind.Handler:
                        {
                            _handlerDefinitions.TryGetValue(upperUniqueId, out IBdoHandlerDefinition handlerDefinition);
                            return (T)handlerDefinition;
                        }
                    case BdoExtensionItemKind.Metrics:
                        {
                            _metricsDefinitions.TryGetValue(upperUniqueId, out IBdoMetricsDefinition metricsDefinition);
                            return (T)metricsDefinition;
                        }
                    case BdoExtensionItemKind.Routine:
                        {
                            _routineDefinitions.TryGetValue(upperUniqueId, out IBdoRoutineDefinition routineDefinition);
                            return (T)routineDefinition;
                        }
                    case BdoExtensionItemKind.Scriptword:
                        {
                            return (T)GetScriptwordDefinitionWithUniqueName(uniqueName);
                        }
                    case BdoExtensionItemKind.Task:
                        {
                            _taskDefinitions.TryGetValue(upperUniqueId, out IBdoTaskDefinition taskDefinition);
                            return (T)taskDefinition;
                        }
                    default:
                        break;
                }
            }

            return default;
        }

        // Script word definitions ---------------------------

        /// <summary>
        /// Returns the script word definition with the specified unique name.
        /// </summary>
        /// <param name="uniqueName">The unique ID of script word to return.</param>
        /// <param name="parentDefinition"></param>
        /// <returns>The script word with the specified unique name.</returns>
        public IBdoScriptwordDefinition GetScriptwordDefinitionWithUniqueName(string uniqueName, IBdoScriptwordDefinition parentDefinition = null)
        {
            if (uniqueName == null)
                return null;

            return GetRecursivelyScriptwordDefinitionWithUniqueName(uniqueName?.ToUpper(), null);
        }

        /// <summary>
        /// Returns the script word definition with the specified unique name.
        /// </summary>
        /// <param name="uniqueName">The unique ID of script word to return.</param>
        /// <param name="parentDefinition">The parent definition to return.</param>
        /// <returns>The script word with the specified unique name.</returns>
        private IBdoScriptwordDefinition GetRecursivelyScriptwordDefinitionWithUniqueName(string uniqueName, IBdoScriptwordDefinition parentDefinition)
        {
            if (uniqueName == null)
                return null;

            Dictionary<string, IBdoScriptwordDefinition> definitions = parentDefinition == null ? _scriptWordDefinitions : parentDefinition.Children;

            IBdoScriptwordDefinition scriptWordDefinition = null;
            foreach (var pair in definitions)
            {
                if (string.Compare(pair.Key, uniqueName) == 0)
                {
                    return pair.Value;
                }
                else
                {
                    if ((scriptWordDefinition = GetScriptwordDefinitionWithUniqueName(uniqueName, pair.Value)) != null)
                    {
                        break;
                    }
                }
            }

            return scriptWordDefinition;
        }

        private List<IBdoScriptwordDefinition> GetParentScriptwordDefinitions(
            string definitionName,
            IBdoScriptwordDefinition parentFeachDefinition,
            string[] libraryNames = null)
        {
            List<IBdoScriptwordDefinition> parentDefinitions = new List<IBdoScriptwordDefinition>();

            //if (definitionName != null)
            //{
            //    List<IBdoScriptwordDefinition> definitions =
            //        (parentFeachDefinition == null ? _scriptwordDefinitions :
            //        new List<IBdoScriptwordDefinition>(parentFeachDefinition.Children));
            //    foreach (IBdoScriptwordDefinition currentScriptwordDefinition in definitions)
            //    {
            //        if (currentScriptwordDefinition.KeyEquals(definitionName) && parentFeachDefinition != null)
            //            parentDefinitions.Add(parentFeachDefinition);

            //        parentDefinitions.AddRange(GetParentScriptwordDefinitions(definitionName, currentScriptwordDefinition, libraryNames));
            //    }
            //}

            return parentDefinitions;
        }


        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Clears this instance.
        /// </summary>
        internal void Clear()
        {
            _carrierDefinitions.Clear();
            _connectorDefinitions.Clear();
            _entityDefinitions.Clear();
            _handlerDefinitions.Clear();
            _metricsDefinitions.Clear();
            _routineDefinitions.Clear();
            _scriptWordDefinitions.Clear();
            _taskDefinitions.Clear();
        }

        /// <summary>
        /// Adds the specified definition.
        /// </summary>
        /// <typeparam name="T">The BindOpen extension item definition class to consider.</typeparam>
        /// <param name="definition">The definition to add.</param>
        public void Add<T>(T definition) where T : IBdoExtensionItemDefinition
        {
            var uniqueId = definition?.UniqueId?.ToUpper();

            if (definition is IBdoCarrierDefinition carier)
            {
                if (!_carrierDefinitions.ContainsKey(uniqueId))
                {
                    _carrierDefinitions.Add(carier.UniqueId?.ToUpper(), carier);
                }
            }
            else if (definition is IBdoConnectorDefinition connector)
            {
                if (!_connectorDefinitions.ContainsKey(uniqueId))
                {
                    _connectorDefinitions.Add(connector.UniqueId?.ToUpper(), connector);
                }
            }
            else if (definition is IBdoEntityDefinition entity)
            {
                if (!_entityDefinitions.ContainsKey(uniqueId))
                {
                    _entityDefinitions.Add(entity.UniqueId?.ToUpper(), entity);
                }
            }
            else if (definition is IBdoHandlerDefinition handler)
            {
                if (!_handlerDefinitions.ContainsKey(uniqueId))
                {
                    _handlerDefinitions.Add(handler.UniqueId?.ToUpper(), handler);
                }
            }
            else if (definition is IBdoMetricsDefinition metrics)
            {
                if (!_metricsDefinitions.ContainsKey(uniqueId))
                {
                    _metricsDefinitions.Add(metrics.UniqueId?.ToUpper(), metrics);
                }
            }
            else if (definition is IBdoRoutineDefinition routine)
            {
                if (!_routineDefinitions.ContainsKey(uniqueId))
                {
                    _routineDefinitions.Add(routine.UniqueId?.ToUpper(), routine);
                }
            }
            else if (definition is IBdoScriptwordDefinition scriptWord)
            {
                if (!_scriptWordDefinitions.ContainsKey(uniqueId))
                {
                    _scriptWordDefinitions.Add(scriptWord.UniqueId?.ToUpper(), scriptWord);
                }
            }
            else if (definition is IBdoTaskDefinition task)
            {
                if (!_taskDefinitions.ContainsKey(uniqueId))
                {
                    _taskDefinitions.Add(task.UniqueId?.ToUpper(), task);
                }
            }
        }

        #endregion
    }
}
