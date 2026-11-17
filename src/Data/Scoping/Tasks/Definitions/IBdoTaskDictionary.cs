namespace BindOpen.Scoping.Tasks;

/// <summary>
/// Defines a specialized dictionary for the registration and discovery of task definitions.
/// </summary>
/// <remarks>
/// This dictionary serves as the central repository for <see cref="IBdoTaskDefinition"/> objects,
/// enabling the system to manage, categorize, and resolve the blueprints of executable units.
/// </remarks>
public interface IBdoTaskDictionary : ITBdoExtensionDictionary<IBdoTaskDefinition>
{
}