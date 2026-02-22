using BindOpen.Data;

namespace BindOpen.Scoping;

/// <summary>
/// Defines a BindOpen extension runtime item.
/// </summary>
/// <remarks>
/// This interface is the base for all active components (Tasks, Connectors, Entities) 
/// that are instantiated and executed within a scope.
/// </remarks>
public interface IBdoExtension : IBdoObject, IIdentified, IBdoDefinable
{
    /// <summary>
    /// Gets or sets the kind of this extension.
    /// </summary>
    /// <value>The category this extension belongs to (e.g., Task, Connector).</value>
    BdoExtensionKinds ExtensionKind { get; set; }
}