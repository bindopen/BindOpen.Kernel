using BindOpen.Runtime.Definition;

namespace BindOpen.Extensions
{
    /// <summary>
    /// This interface defines a configurable data.
    /// </summary>
    public interface ITBdoExtensionConfigurable<D, C>
        where D : IBdoExtensionItemDefinition
        where C : ITBdoExtensionItemConfiguration<D>
    {
        /// <summary>
        /// 
        /// </summary>
        C Configuration { get; set; }
    }
}
