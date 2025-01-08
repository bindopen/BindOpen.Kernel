using BindOpen.Data.Meta;

namespace BindOpen.Data
{
    /// <summary>
    /// This interface defines a data expression.
    /// </summary>
    public interface IBdoReference : IBdoObject, IIdentified, IReferenced
    {
        /// <summary>
        /// The kind.
        /// </summary>
        BdoReferenceKind Kind { get; set; }

        IBdoExpression Expression { get; set; }

        IBdoMetaData MetaData { get; set; }
    }
}