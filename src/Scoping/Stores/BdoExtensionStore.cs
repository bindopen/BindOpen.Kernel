using BindOpen.Kernel.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Kernel.Scoping.Stores
{
    /// <summary>
    /// This class represents a BindOpen extension item store.
    /// </summary>
    public class BdoExtensionStore : BdoObject, IBdoExtensionStore
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
        public IEnumerable<IBdoExtensionDefinition> GetDefinitions(BdoExtensionKinds kind = BdoExtensionKinds.Any)
        {
            if (kind == BdoExtensionKinds.Connector || kind == BdoExtensionKinds.Any)
            {
                return _connectorDictionary.Select(q => q.Value);
            }
            if (kind == BdoExtensionKinds.Entity || kind == BdoExtensionKinds.Any)
            {
                return _entityDictionary.Select(q => q.Value);
            }
            if (kind == BdoExtensionKinds.Function || kind == BdoExtensionKinds.Any)
            {
                return _functionDictionary.Select(q => q.Value);
            }
            if (kind == BdoExtensionKinds.Task || kind == BdoExtensionKinds.Any)
            {
                return _taskDictionary.Select(q => q.Value);
            }

            return Enumerable.Empty<IBdoExtensionDefinition>();
        }

        /// <summary>
        /// Returns the item definition with the specified unique name.
        /// </summary>
        /// <param key="uniqueName">The unique ID of item to return.</param>
        /// <returns>The item with the specified unique name.</returns>
        public IBdoExtensionDefinition GetDefinition(
            BdoExtensionKinds kind,
            string uniqueName)
        {
            string upperUniqueName = uniqueName?.ToUpper();

            if (uniqueName != null)
            {
                switch (kind)
                {
                    case BdoExtensionKinds.Connector:
                        {
                            _connectorDictionary.TryGetValue(upperUniqueName, out IBdoConnectorDefinition connectorDefinition);
                            return connectorDefinition;
                        }
                    case BdoExtensionKinds.Entity:
                        {
                            _entityDictionary.TryGetValue(upperUniqueName, out IBdoEntityDefinition entityDefinition);
                            return entityDefinition;
                        }
                    case BdoExtensionKinds.Function:
                        {
                            _functionDictionary.TryGetValue(upperUniqueName, out IBdoFunctionDefinition functionDefinition);
                            return functionDefinition;
                        }
                    case BdoExtensionKinds.Task:
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

        public IBdoExtensionDefinition GetDefinitionFromType(Type type)
        {
            var extensionKind = type?.GetExtensionKind();

            return extensionKind switch
            {
                BdoExtensionKinds.Connector =>
                    _connectorDictionary.FirstOrDefault(q => q.Value?.RuntimeType == type).Value,
                BdoExtensionKinds.Entity =>
                    _entityDictionary.FirstOrDefault(q => q.Value?.RuntimeType == type).Value,
                BdoExtensionKinds.Task =>
                    _taskDictionary.FirstOrDefault(q => q.Value?.RuntimeType == type).Value,
                _ => null,
            };
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
