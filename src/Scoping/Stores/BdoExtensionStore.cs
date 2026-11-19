using BindOpen.Data;
using BindOpen.Scoping.Connectors;
using BindOpen.Scoping.Entities;
using BindOpen.Scoping.Functions;
using BindOpen.Scoping.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Scoping;

/// <summary>
/// This class represents a BindOpen extension store, managing specialized dictionaries 
/// for connectors, entities, functions, and tasks.
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
    /// Initializes a new instance of the <see cref="BdoExtensionStore"/> class.
    /// </summary>
    public BdoExtensionStore() : base()
    {
    }

    #endregion

    // ------------------------------------------
    // IBdoExtensionStore Implementation
    // ------------------------------------------

    #region IBdoExtensionStore

    /// <summary>
    /// Gets the definitions of the specified kind.
    /// </summary>
    /// <param name="kind">The kind of extensions to retrieve.</param>
    /// <returns>An enumeration of extension definitions.</returns>
    public IEnumerable<IBdoExtensionDefinition> GetDefinitions(BdoExtensionKinds kind = BdoExtensionKinds.Any)
    {
        var list = new List<IBdoExtensionDefinition>();

        if (kind.HasFlag(BdoExtensionKinds.Connector) || kind == BdoExtensionKinds.Any)
            list.AddRange(_connectorDictionary.Values);

        if (kind.HasFlag(BdoExtensionKinds.Entity) || kind == BdoExtensionKinds.Any)
            list.AddRange(_entityDictionary.Values);

        if (kind.HasFlag(BdoExtensionKinds.Function) || kind == BdoExtensionKinds.Any)
            list.AddRange(_functionDictionary.Values);

        if (kind.HasFlag(BdoExtensionKinds.Task) || kind == BdoExtensionKinds.Any)
            list.AddRange(_taskDictionary.Values);

        return list;
    }

    /// <summary>
    /// Gets the extension definition with the specified unique name.
    /// </summary>
    /// <param name="kind">The kind of extension.</param>
    /// <param name="uniqueName">The unique name/ID of the definition.</param>
    /// <returns>The matching definition if found; otherwise, null.</returns>
    public IBdoExtensionDefinition GetDefinition(BdoExtensionKinds kind, string uniqueName)
    {
        if (string.IsNullOrEmpty(uniqueName)) return null;

        string key = uniqueName.ToUpper();

        return kind switch
        {
            BdoExtensionKinds.Connector => _connectorDictionary.TryGetValue(key, out var val) ? val : null,
            BdoExtensionKinds.Entity => _entityDictionary.TryGetValue(key, out var val) ? val : null,
            BdoExtensionKinds.Function => _functionDictionary.TryGetValue(key, out var val) ? val : null,
            BdoExtensionKinds.Task => _taskDictionary.TryGetValue(key, out var val) ? val : null,
            _ => null
        };
    }

    /// <summary>
    /// Gets the extension definition associated with the specified runtime type.
    /// </summary>
    /// <param name="type">The runtime type to look for.</param>
    /// <returns>The matching definition; otherwise, null.</returns>
    public IBdoExtensionDefinition GetDefinitionFromType(Type type)
    {
        if (type == null) return null;

        var kind = type.GetExtensionKind();

        return kind switch
        {
            BdoExtensionKinds.Connector => _connectorDictionary.Values.FirstOrDefault(q => q.RuntimeType == type),
            BdoExtensionKinds.Entity => _entityDictionary.Values.FirstOrDefault(q => q.RuntimeType == type),
            BdoExtensionKinds.Task => _taskDictionary.Values.FirstOrDefault(q => q.RuntimeType == type),
            _ => null
        };
    }

    /// <summary>
    /// Clears all stored definitions in this instance.
    /// </summary>
    /// <returns>Returns this instance.</returns>
    public IBdoExtensionStore Clear()
    {
        _connectorDictionary.Clear();
        _entityDictionary.Clear();
        _functionDictionary.Clear();
        _taskDictionary.Clear();
        return this;
    }

    /// <summary>
    /// Adds a definition to the store based on its type.
    /// </summary>
    /// <param name="definition">The definition to add.</param>
    /// <returns>Returns this instance.</returns>
    public IBdoExtensionStore Add(IBdoExtensionDefinition definition)
    {
        if (definition == null) return this;

        string key = definition.UniqueName?.ToUpper();
        if (string.IsNullOrEmpty(key)) return this;

        switch (definition)
        {
            case IBdoConnectorDefinition connector:
                _connectorDictionary.TryAdd(key, connector);
                break;
            case IBdoEntityDefinition entity:
                _entityDictionary.TryAdd(key, entity);
                break;
            case IBdoFunctionDefinition function:
                _functionDictionary.TryAdd(key, function);
                break;
            case IBdoTaskDefinition task:
                _taskDictionary.TryAdd(key, task);
                break;
        }

        return this;
    }

    #endregion

    // ------------------------------------------
    // IIdentified Implementation
    // ------------------------------------------

    #region IIdentified

    /// <summary>
    /// Gets or sets the identifier of this store.
    /// </summary>
    public string Identifier { get; set; }

    #endregion
}