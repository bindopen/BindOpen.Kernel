using BindOpen.Data.Meta;
using BindOpen.Script;

namespace BindOpen.Data
{
    /// <summary>
    /// This interface defines a data expression.
    /// </summary>
    public interface IBdoReference : IBdoItem
    {
        /// <summary>
        /// The kind.
        /// </summary>
        BdoReferenceKind Kind { get; set; }

        IBdoExpression Expression { get; set; }

        /// <summary>
        /// The script word.
        /// </summary>
        IBdoScriptword Word { get; set; }

        string Identifier { get; set; }

        IBdoMetaData MetaData { get; set; }
    }
}