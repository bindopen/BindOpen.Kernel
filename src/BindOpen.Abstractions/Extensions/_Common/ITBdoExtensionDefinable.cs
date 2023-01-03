using BindOpen.Runtime.Definition;

namespace BindOpen.Extensions
{
    /// <summary>
    /// This interface defines a configurable data.
    /// </summary>
    public interface ITBdoExtensionDefinable<T>
        where T : IBdoExtensionItemDefinition
    {
        /// <summary>
        /// 
        /// </summary>
        T Definition { get; set; }
    }
}
