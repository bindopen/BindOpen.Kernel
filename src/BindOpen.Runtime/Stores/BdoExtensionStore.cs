using BindOpen.Data.Items;
using BindOpen.Extensions;
using BindOpen.Runtime.Definitions;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Runtime.Stores
{
    /// <summary>
    /// This class represents a BindOpen extension item store.
    /// </summary>
    public class BdoExtensionStore : BdoItem,
        IBdoExtensionStore
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private readonly Dictionary<string, IBdoConnectorDefinition> _connectorDefinitions = new();
        private readonly Dictionary<string, IBdoEntityDefinition> _entityDefinitions = new();
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
        public ITBdoSet<T> GetDefinitions<T>() where T : IBdoExtensionDefinition
        {
            return (typeof(T).GetExtensionKind()) switch
            {
                BdoExtensionKind.Connector => _connectorDefinitions as ITBdoSet<T>,
                BdoExtensionKind.Entity => _entityDefinitions as ITBdoSet<T>,
                BdoExtensionKind.Format => _entityDefinitions.SelectMany(p => p.Value?.FormatDefinitions).Distinct().ToList() as ITBdoSet<T>,
                BdoExtensionKind.Metrics => _metricsDefinitions as ITBdoSet<T>,
                BdoExtensionKind.Routine => _routineDefinitions as ITBdoSet<T>,
                BdoExtensionKind.Scriptword => _scriptWordDefinitions as ITBdoSet<T>,
                BdoExtensionKind.Task => _taskDefinitions as ITBdoSet<T>,
                _ => new TBdoSet<T>(),
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="uniqueName"></param>
        /// <returns></returns>
        public T GetDefinition<T>(
            string uniqueName)
            where T : IBdoExtensionDefinition
        {
            var definition = GetDefinition(
                typeof(T).GetExtensionKind(),
                uniqueName);
            return (T)definition;
        }

        /// <summary>
        /// Returns the item definition with the specified unique name.
        /// </summary>
        /// <param key="uniqueName">The unique ID of item to return.</param>
        /// <returns>The item with the specified unique name.</returns>
        public IBdoExtensionDefinition GetDefinition(
            BdoExtensionKind kind,
            string uniqueName)
        {
            string upperUniqueName = uniqueName?.ToUpper();

            if (uniqueName != null)
            {
                switch (kind)
                {
                    case BdoExtensionKind.Connector:
                        {
                            _connectorDefinitions.TryGetValue(upperUniqueName, out IBdoConnectorDefinition connectorDefinition);
                            return connectorDefinition;
                        }
                    case BdoExtensionKind.Entity:
                        {
                            _entityDefinitions.TryGetValue(upperUniqueName, out IBdoEntityDefinition entityDefinition);
                            return entityDefinition;
                        }
                    case BdoExtensionKind.Metrics:
                        {
                            _metricsDefinitions.TryGetValue(upperUniqueName, out IBdoMetricsDefinition metricsDefinition);
                            return metricsDefinition;
                        }
                    case BdoExtensionKind.Routine:
                        {
                            _routineDefinitions.TryGetValue(upperUniqueName, out IBdoRoutineDefinition routineDefinition);
                            return routineDefinition;
                        }
                    case BdoExtensionKind.Scriptword:
                        {
                            return GetScriptwordDefinitionWithUniqueName(uniqueName);
                        }
                    case BdoExtensionKind.Task:
                        {
                            _taskDefinitions.TryGetValue(upperUniqueName, out IBdoTaskDefinition taskDefinition);
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
        /// <param key="uniqueName">The unique ID of script word to return.</param>
        /// <param key="parentDefinition"></param>
        /// <returns>The script word with the specified unique name.</returns>
        public IBdoScriptwordDefinition GetScriptwordDefinitionWithUniqueName(string uniqueName, IBdoScriptwordDefinition parentDefinition = null)
        {
            if (_scriptWordDefinitions != null || string.IsNullOrEmpty(uniqueName))
            {
                return null;
            }

            IBdoScriptwordDefinition scriptWordDefinition = null;

            foreach (var pair in _scriptWordDefinitions)
            {
                if (string.Compare(pair.Key, uniqueName) == 0)
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
        /// <param key="definition">The definition to add.</param>
        public IBdoExtensionStore Add<T>(T definition) where T : IBdoExtensionDefinition
        {
            var uniqueName = definition?.UniqueName?.ToUpper();

            if (definition is IBdoEntityDefinition carier)
            {
                if (!_entityDefinitions.ContainsKey(uniqueName))
                {
                    _entityDefinitions.Add(uniqueName, carier);
                }
            }
            else if (definition is IBdoConnectorDefinition connector)
            {
                if (!_connectorDefinitions.ContainsKey(uniqueName))
                {
                    _connectorDefinitions.Add(uniqueName, connector);
                }
            }
            else if (definition is IBdoEntityDefinition entity)
            {
                if (!_entityDefinitions.ContainsKey(uniqueName))
                {
                    _entityDefinitions.Add(uniqueName, entity);
                }
            }
            else if (definition is IBdoMetricsDefinition metrics)
            {
                if (!_metricsDefinitions.ContainsKey(uniqueName))
                {
                    _metricsDefinitions.Add(uniqueName, metrics);
                }
            }
            else if (definition is IBdoRoutineDefinition routine)
            {
                if (!_routineDefinitions.ContainsKey(uniqueName))
                {
                    _routineDefinitions.Add(uniqueName, routine);
                }
            }
            else if (definition is IBdoScriptwordDefinition scriptWord)
            {
                if (!_scriptWordDefinitions.ContainsKey(uniqueName))
                {
                    _scriptWordDefinitions.Add(uniqueName, scriptWord);
                }
            }
            else if (definition is IBdoTaskDefinition task)
            {
                if (!_taskDefinitions.ContainsKey(uniqueName))
                {
                    _taskDefinitions.Add(uniqueName, task);
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
