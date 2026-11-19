using BindOpen.Data;
using BindOpen.Scoping.Entities;

namespace BindOpen.Scoping.Connectors;

/// <summary>
/// Defines the metadata and structural requirements for a BindOpen connector.
/// </summary>
/// <remarks>
/// As a specialized entity definition, this interface specifies the nature of 
/// the data source it interacts with, allowing the Scoping engine to correctly 
/// index and resolve connection factories.
/// </remarks>
public interface IBdoConnectorDefinition : IBdoEntityDefinition
{
    /// <summary>
    /// Gets or sets the kind of data source associated with this connector definition.
    /// </summary>
    /// <value>The <see cref="DatasourceKind"/> (e.g., Database, Rest, Email, Folder).</value>
    DatasourceKind DatasourceKind { get; set; }
}