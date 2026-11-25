using BindOpen.Data.Meta;
using BindOpen.Data.Objects;

namespace BindOpen.Data;

/// <summary>
/// Defines a BindOpen reference.
/// A reference is a specialized object used to point to data via an identifier,
/// a dynamic expression, or a direct metadata object.
/// </summary>
public interface IBdoReference : IBdoObject, IIdentified, IReferenced
{
    /// <summary>
    /// Gets or sets the kind of this reference.
    /// This determines which property (Expression or MetaData) is used to resolve the value.
    /// </summary>
    BdoReferenceKind Kind { get; set; }

    /// <summary>
    /// Gets or sets the expression associated with this reference.
    /// Used when Kind is set to Expression.
    /// </summary>
    IBdoExpression Expression { get; set; }

    /// <summary>
    /// Gets or sets the metadata object associated with this reference.
    /// Used when Kind is set to MetaData.
    /// </summary>
    IBdoMetaData MetaData { get; set; }
}