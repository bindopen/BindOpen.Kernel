namespace BindOpen.Scoping.Entities;

/// <summary>
/// Defines a specialized dictionary for the registration and discovery of entity definitions.
/// </summary>
/// <remarks>
/// This dictionary acts as the central repository for <see cref="IBdoEntityDefinition"/> objects,
/// enabling the system to manage business data schemas and their associated runtime types.
/// </remarks>
public interface IBdoEntityDictionary : ITBdoExtensionDictionary<IBdoEntityDefinition>
{
}