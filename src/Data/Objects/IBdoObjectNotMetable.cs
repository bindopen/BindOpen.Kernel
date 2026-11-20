namespace BindOpen.Data.Objects;

/// <summary>
/// Represents a BindOpen object that cannot be converted into or treated as metadata.
/// Use this interface for internal components or logic-heavy objects that should
/// not be exposed within a metadata tree.
/// </summary>
public interface IBdoObjectNotMetable : IBdoObject
{
}