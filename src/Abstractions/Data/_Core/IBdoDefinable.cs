using BindOpen.System.Scoping;

namespace BindOpen.System.Data
{
    /// <summary>
    /// This interface defines a configurable data.
    /// </summary>
    public interface IBdoDefinable
    {
        /// <summary>
        /// 
        /// </summary>
        BdoExtensionKind DefinitionExtensionKind { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string DefinitionUniqueName { get; set; }
    }
}
