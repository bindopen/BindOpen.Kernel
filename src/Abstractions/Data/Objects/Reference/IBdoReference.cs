using BindOpen.Kernel.Data.Meta;

namespace BindOpen.Kernel.Data
{
    /// <summary>
    /// This interface defines a data expression.
    /// </summary>
    public interface IBdoReference : IBdoObject, IReferenced
    {
        /// <summary>
        /// The kind.
        /// </summary>
        BdoReferenceKind Kind { get; set; }

        IBdoExpression Expression { get; set; }

        string Identifier { get; set; }

        IBdoMetaData MetaData { get; set; }
    }
}