using BindOpen.Data;
using BindOpen.Scoping.Connectors;
using BindOpen.Scoping.Entities;
using BindOpen.Scoping.Tasks;

namespace BindOpen.Scoping;

/// <summary>
/// This static class provides factory methods to create scoping objects and definitions.
/// </summary>
public static partial class BdoScoping
{
    // Entities ------------------------------------------------

    /// <summary>
    /// Creates a new instance of an entity definition.
    /// </summary>
    /// <param name="packageDefinition">The package definition to associate with this entity.</param>
    /// <param name="name">The name of the entity definition.</param>
    /// <returns>A new instance of <see cref="BdoEntityDefinition"/>.</returns>
    public static BdoEntityDefinition NewEntityDefinition(
        this IBdoPackageDefinition packageDefinition,
        string name = null)
    {
        var definition = new BdoEntityDefinition(name, packageDefinition);
        if (!string.IsNullOrEmpty(name))
        {
            definition.WithName(name);
        }

        return definition;
    }

    // Connectors ------------------------------------------------

    /// <summary>
    /// Creates a new instance of a connector definition.
    /// </summary>
    /// <param name="packageDefinition">The package definition to associate with this connector.</param>
    /// <param name="name">The name of the connector definition.</param>
    /// <returns>A new instance of <see cref="BdoConnectorDefinition"/>.</returns>
    public static BdoConnectorDefinition NewConnectorDefinition(
        this IBdoPackageDefinition packageDefinition,
        string name = null)
    {
        var definition = new BdoConnectorDefinition(name, packageDefinition);
        if (!string.IsNullOrEmpty(name))
        {
            definition.WithName(name);
        }

        return definition;
    }

    // Tasks ------------------------------------------------

    /// <summary>
    /// Creates a new instance of a task definition.
    /// </summary>
    /// <param name="packageDefinition">The package definition to associate with this task.</param>
    /// <param name="name">The name of the task definition.</param>
    /// <returns>A new instance of <see cref="BdoTaskDefinition"/>.</returns>
    public static BdoTaskDefinition NewTaskDefinition(
        this IBdoPackageDefinition packageDefinition,
        string name = null)
    {
        var definition = new BdoTaskDefinition(name, packageDefinition);
        if (!string.IsNullOrEmpty(name))
        {
            definition.WithName(name);
        }

        return definition;
    }
}