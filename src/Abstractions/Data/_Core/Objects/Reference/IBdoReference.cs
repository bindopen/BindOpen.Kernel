using BindOpen.System.Data.Meta;
using BindOpen.System.Scoping.Script;

namespace BindOpen.System.Data
{
    /// <summary>
    /// This interface defines a data expression.
    /// </summary>
    public interface IBdoReference : IBdoObject
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