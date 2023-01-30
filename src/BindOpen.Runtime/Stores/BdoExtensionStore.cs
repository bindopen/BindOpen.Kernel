using BindOpen.Data.Items;
using BindOpen.Extensions.Scripting;
using BindOpen.Runtime.Definition;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Runtime.Stores
{
    /// <summary>
    /// This class represents a BindOpen extension item store.
    /// </summary>
    public class BdoExtensionStore : BdoItem, IBdoExtensionStore
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private readonly Dictionary<string, IBdoConnectorDefinition> _connectorDefinitions = new();
        private readonly Dictionary<string, IBdoEntityDefinition> _entityDefinitions = new();
        private readonly Dictionary<string, IBdoHandlerDefinition> _handlerDefinitions = new();
        private readonly Dictionary<string, IBdoMetricsDefinition> _metricsDefinitions = new();
        private readonly Dictionary<string, IBdoRoutineDefinition> _routineDefinitions = new();
        private readonly Dictionary<string, IBdoScriptwordDefinition> _scriptWordDefinitions = new();
        private readonly Dictionary<string, IBdoTaskDefinition> _taskDefinitions = new();

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
        // IBdoExtensionStore Implementation
        // ------------------------------------------

        #region IBdoExtensionStore

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
            return (typeof(T).GetExtensionItemKind()) switch
            {
                BdoExtensionItemKind.Connector => _connectorDefinitions as Dictionary<string, T>,
                BdoExtensionItemKind.Entity => _entityDefinitions as Dictionary<string, T>,
                BdoExtensionItemKind.Format => _entityDefinitions.SelectMany(p => p.Value?.FormatDefinitions).Distinct().ToList() as Dictionary<string, T>,
                BdoExtensionItemKind.Handler => _handlerDefinitions as Dictionary<string, T>,
                BdoExtensionItemKind.Metrics => _metricsDefinitions as Dictionary<string, T>,
                BdoExtensionItemKind.Routine => _routineDefinitions as Dictionary<string, T>,
                BdoExtensionItemKind.Scriptword => _scriptWordDefinitions as Dictionary<string, T>,
                BdoExtensionItemKind.Task => _taskDefinitions as Dictionary<string, T>,
                _ => new Dictionary<string, T>(),
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uniqueId"></param>
        /// <returns></returns>
        public T GetItemDefinitionWithUniqueId<T>(
            string uniqueId)
            where T : IBdoExtensionItemDefinition
        {
            var definition = GetItemDefinitionWithUniqueId(
                typeof(T).GetExtensionItemKind(),
                uniqueId);
            return (T)definition;
        }

        /// <summary>
        /// Returns the item definition with the specified unique name.
        /// </summary>
        /// <param name="uniqueId">The unique ID of item to return.</param>
        /// <returns>The item with the specified unique name.</returns>
        public IBdoExtensionItemDefinition GetItemDefinitionWithUniqueId(
            BdoExtensionItemKind kind,
            string uniqueId)
        {
            string upperUniqueId = uniqueId?.ToUpper();

            if (uniqueId != null)
            {
                switch (kind)
                {
                    case BdoExtensionItemKind.Connector:
                        {
                            _connectorDefinitions.TryGetValue(upperUniqueId, out IBdoConnectorDefinition connectorDefinition);
                            return connectorDefinition;
                        }
                    case BdoExtensionItemKind.Entity:
                        {
                            _entityDefinitions.TryGetValue(upperUniqueId, out IBdoEntityDefinition entityDefinition);
                            return entityDefinition;
                        }
                    case BdoExtensionItemKind.Handler:
                        {
                            _handlerDefinitions.TryGetValue(upperUniqueId, out IBdoHandlerDefinition handlerDefinition);
                            return handlerDefinition;
                        }
                    case BdoExtensionItemKind.Metrics:
                        {
                            _metricsDefinitions.TryGetValue(upperUniqueId, out IBdoMetricsDefinition metricsDefinition);
                            return metricsDefinition;
                        }
                    case BdoExtensionItemKind.Routine:
                        {
                            _routineDefinitions.TryGetValue(upperUniqueId, out IBdoRoutineDefinition routineDefinition);
                            return routineDefinition;
                        }
                    case BdoExtensionItemKind.Scriptword:
                        {
                            return GetScriptwordDefinitionWithUniqueName(uniqueId);
                        }
                    case BdoExtensionItemKind.Task:
                        {
                            _taskDefinitions.TryGetValue(upperUniqueId, out IBdoTaskDefinition taskDefinition);
                            return taskDefinition;
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
        /// <param name="uniqueId">The unique ID of script word to return.</param>
        /// <param name="parentDefinition"></param>
        /// <returns>The script word with the specified unique name.</returns>
        public IBdoScriptwordDefinition GetScriptwordDefinitionWithUniqueName(string uniqueId, IBdoScriptwordDefinition parentDefinition = null)
        {
            if (_scriptWordDefinitions != null || string.IsNullOrEmpty(uniqueId))
            {
                return null;
            }

            IBdoScriptwordDefinition scriptWordDefinition = null;

            foreach (var pair in _scriptWordDefinitions)
            {
                if (string.Compare(pair.Key, uniqueId) == 0)
                {
                    scriptWordDefinition = pair.Value;
                    break;
                }
            }

            return scriptWordDefinition;
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public IBdoExtensionStore Clear()
        {
            _entityDefinitions.Clear();
            _connectorDefinitions.Clear();
            _entityDefinitions.Clear();
            _handlerDefinitions.Clear();
            _metricsDefinitions.Clear();
            _routineDefinitions.Clear();
            _scriptWordDefinitions.Clear();
            _taskDefinitions.Clear();

            return this;
        }

        /// <summary>
        /// Adds the specified definition.
        /// </summary>
        /// <typeparam name="T">The BindOpen extension item definition class to consider.</typeparam>
        /// <param name="definition">The definition to add.</param>
        public IBdoExtensionStore Add<T>(T definition) where T : IBdoExtensionItemDefinition
        {
            var uniqueId = definition?.UniqueId?.ToUpper();

            if (definition is IBdoEntityDefinition carier)
            {
                if (!_entityDefinitions.ContainsKey(uniqueId))
                {
                    _entityDefinitions.Add(uniqueId, carier);
                }
            }
            else if (definition is IBdoConnectorDefinition connector)
            {
                if (!_connectorDefinitions.ContainsKey(uniqueId))
                {
                    _connectorDefinitions.Add(uniqueId, connector);
                }
            }
            else if (definition is IBdoEntityDefinition entity)
            {
                if (!_entityDefinitions.ContainsKey(uniqueId))
                {
                    _entityDefinitions.Add(uniqueId, entity);
                }
            }
            else if (definition is IBdoHandlerDefinition handler)
            {
                if (!_handlerDefinitions.ContainsKey(uniqueId))
                {
                    _handlerDefinitions.Add(uniqueId, handler);
                }
            }
            else if (definition is IBdoMetricsDefinition metrics)
            {
                if (!_metricsDefinitions.ContainsKey(uniqueId))
                {
                    _metricsDefinitions.Add(uniqueId, metrics);
                }
            }
            else if (definition is IBdoRoutineDefinition routine)
            {
                if (!_routineDefinitions.ContainsKey(uniqueId))
                {
                    _routineDefinitions.Add(uniqueId, routine);
                }
            }
            else if (definition is IBdoScriptwordDefinition scriptWord)
            {
                if (!_scriptWordDefinitions.ContainsKey(uniqueId))
                {
                    _scriptWordDefinitions.Add(uniqueId, scriptWord);
                }
            }
            else if (definition is IBdoTaskDefinition task)
            {
                if (!_taskDefinitions.ContainsKey(uniqueId))
                {
                    _taskDefinitions.Add(uniqueId, task);
                }
            }

            return this;
        }

        #endregion


        // ------------------------------------------
        // IIdentified Implementation
        // ------------------------------------------

        #region IIdentified

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        #endregion
    }
}
