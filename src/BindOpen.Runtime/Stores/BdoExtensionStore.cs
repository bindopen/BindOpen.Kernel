using BindOpen.Extensions.Scripting;
using BindOpen.Data.Items;
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

        private readonly Dictionary<string, IBdoCarrierDefinition> _carrierDefinitions = new Dictionary<string, IBdoCarrierDefinition>();
        private readonly Dictionary<string, IBdoConnectorDefinition> _connectorDefinitions = new Dictionary<string, IBdoConnectorDefinition>();
        private readonly Dictionary<string, IBdoEntityDefinition> _entityDefinitions = new Dictionary<string, IBdoEntityDefinition>();
        private readonly Dictionary<string, IBdoHandlerDefinition> _handlerDefinitions = new Dictionary<string, IBdoHandlerDefinition>();
        private readonly Dictionary<string, IBdoMetricsDefinition> _metricsDefinitions = new Dictionary<string, IBdoMetricsDefinition>();
        private readonly Dictionary<string, IBdoRoutineDefinition> _routineDefinitions = new Dictionary<string, IBdoRoutineDefinition>();
        private readonly Dictionary<string, IBdoScriptwordDefinition> _scriptWordDefinitions = new Dictionary<string, IBdoScriptwordDefinition>();
        private readonly Dictionary<string, IBdoTaskDefinition> _taskDefinitions = new Dictionary<string, IBdoTaskDefinition>();

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
                BdoExtensionItemKind.Carrier => _carrierDefinitions as Dictionary<string, T>,
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
            _carrierDefinitions.Clear();
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

            if (definition is IBdoCarrierDefinition carier)
            {
                if (!_carrierDefinitions.ContainsKey(uniqueId))
                {
                    _carrierDefinitions.Add(uniqueId, carier);
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
        // IIdentifiedPoco Implementation
        // ------------------------------------------

        #region IIdentifiedPoco

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IBdoExtensionStore WithId(string id)
        {
            Id = id;
            return this;
        }

        #endregion
    }
}
