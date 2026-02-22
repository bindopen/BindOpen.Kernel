namespace BindOpen.Scoping.Connectors;

/// <summary>
/// Defines a specialized dictionary for managing and discovering connector definitions.
/// </summary>
/// <remarks>
/// This dictionary provides a central registry for all <see cref="IBdoConnectorDefinition"/> 
/// instances, facilitating the lookup of connection blueprints based on datasource kinds or unique names.
/// </remarks>
public interface IBdoConnectorDictionary : ITBdoExtensionDictionary<IBdoConnectorDefinition>
{
}