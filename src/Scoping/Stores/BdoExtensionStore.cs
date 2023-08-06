using BindOpen.System.Data;
using BindOpen.System.Data.Assemblies;
using BindOpen.System.Data.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.System.Scoping.Stores
{
    /// <summary>
    /// This class represents a BindOpen extension item store.
    /// </summary>
    public class BdoExtensionStore : BdoObject,
        IBdoExtensionStore
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private readonly Dictionary<string, IBdoConnectorDefinition> _connectorDictionary = new();
        private readonly Dictionary<string, IBdoEntityDefinition> _entityDictionary = new();
        private readonly Dictionary<string, IBdoFunctionDefinition> _functionDictionary = new();
        private readonly Dictionary<string, IBdoTaskDefinition> _taskDictionary = new();

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
        public IEnumerable<T> GetDefinitions<T>() where T : IBdoExtensionDefinition
        {
            return (typeof(T).GetExtensionKind()) switch
            {
                BdoExtensionKind.Connector => _connectorDictionary.Select(q => (T)q.Value),
                BdoExtensionKind.Entity => _entityDictionary.Select(q => (T)q.Value),
                BdoExtensionKind.Function => _functionDictionary.Select(q => (T)q.Value),
                BdoExtensionKind.Task => _taskDictionary.Select(q => (T)q.Value),
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
                            _connectorDictionary.TryGetValue(upperUniqueName, out IBdoConnectorDefinition connectorDefinition);
                            return connectorDefinition;
                        }
                    case BdoExtensionKind.Entity:
                        {
                            _entityDictionary.TryGetValue(upperUniqueName, out IBdoEntityDefinition entityDefinition);
                            return entityDefinition;
                        }
                    case BdoExtensionKind.Function:
                        {
                            _functionDictionary.TryGetValue(upperUniqueName, out IBdoFunctionDefinition functionDefinition);
                            return functionDefinition;
                        }
                    case BdoExtensionKind.Task:
                        {
                            _taskDictionary.TryGetValue(upperUniqueName, out IBdoTaskDefinition taskDefinition);
                            return taskDefinition;
                        }
                    default:
                        break;
                }
            }

            return default;
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public IBdoExtensionStore Clear()
        {
            _entityDictionary.Clear();
            _connectorDictionary.Clear();
            _entityDictionary.Clear();
            _functionDictionary.Clear();
            _taskDictionary.Clear();

            return this;
        }

        /// <summary>
        /// Adds the specified definition.
        /// </summary>
        /// <typeparam name="T">The BindOpen extension item definition class to consider.</typeparam>
        /// <param key="definition">The definition to add.</param>
        public IBdoExtensionStore Add(IBdoExtensionDefinition definition)
        {
            var uniqueName = definition?.UniqueName?.ToUpper();

            if (definition is IBdoConnectorDefinition connector)
            {
                if (!_connectorDictionary.ContainsKey(uniqueName))
                {
                    _connectorDictionary.Add(uniqueName, connector);
                }
            }
            else if (definition is IBdoEntityDefinition entity)
            {
                if (!_entityDictionary.ContainsKey(uniqueName))
                {
                    _entityDictionary.Add(uniqueName, entity);
                }
            }
            else if (definition is IBdoFunctionDefinition function)
            {
                if (!_functionDictionary.ContainsKey(uniqueName))
                {
                    _functionDictionary.Add(uniqueName, function);
                }
            }
            else if (definition is IBdoTaskDefinition task)
            {
                if (!_taskDictionary.ContainsKey(uniqueName))
                {
                    _taskDictionary.Add(uniqueName, task);
                }
            }

            return this;
        }

        public T GetDefinitionFromType<T>(IBdoClassReference reference) where T : IBdoExtensionDefinition
        {
            return GetDefinitionFromType(typeof(T).GetExtensionKind(), reference).As<T>();
        }

        public IBdoExtensionDefinition GetDefinitionFromType(
            BdoExtensionKind kind,
            IBdoClassReference reference)
        {
            return kind switch
            {
                BdoExtensionKind.Connector =>
                    _connectorDictionary.FirstOrDefault(q =>
                        BdoData.Class(q.Value?.RuntimeType).Equals(reference)).Value,
                BdoExtensionKind.Entity =>
                    _entityDictionary.FirstOrDefault(q =>
                        BdoData.Class(q.Value?.RuntimeType).Equals(reference)).Value,
                BdoExtensionKind.Task =>
                    _taskDictionary.FirstOrDefault(q =>
                        BdoData.Class(q.Value?.RuntimeType).Equals(reference)).Value,
                _ => null,
            };
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
