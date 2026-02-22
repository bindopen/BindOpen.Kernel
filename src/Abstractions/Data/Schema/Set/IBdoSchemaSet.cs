namespace BindOpen.Data.Schema;

/// <summary>
/// Defines a BindOpen schema set.
/// A schema set is a collection of schema elements that can be managed 
/// as a single unit to define complex validation and structural rules.
/// </summary>
public partial interface IBdoSchemaSet : ITBdoSet<IBdoSchema>, INamed
{
    // This interface serves as the base for building libraries of schemas
    // that can be referenced by metadata elements or other definitions.
}