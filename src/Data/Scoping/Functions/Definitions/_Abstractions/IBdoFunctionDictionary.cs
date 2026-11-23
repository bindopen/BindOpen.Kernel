namespace BindOpen.Scoping.Functions;

/// <summary>
/// Defines a specialized dictionary for the registration and discovery of function definitions.
/// </summary>
/// <remarks>
/// This dictionary serves as the primary repository for the scripting engine, allowing it
/// to look up and execute discrete logic units (functions) by their unique identifiers.
/// </remarks>
public interface IBdoFunctionDictionary : ITBdoExtensionDictionary<IBdoFunctionDefinition>
{
}