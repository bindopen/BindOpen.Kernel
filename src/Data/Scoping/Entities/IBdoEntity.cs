namespace BindOpen.Scoping.Entities;

/// <summary>
/// Defines a BindOpen entity instance.
/// </summary>
/// <remarks>
/// An entity is a structured object that represents business data. 
/// Inheriting from <see cref="IBdoExtension"/> ensures it can be tracked, 
/// configured, and managed by the scoping engine.
/// </remarks>
public interface IBdoEntity : IBdoExtension
{
}