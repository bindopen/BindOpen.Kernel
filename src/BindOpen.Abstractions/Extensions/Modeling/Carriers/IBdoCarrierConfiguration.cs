using BindOpen.Meta.Elements;
using BindOpen.Runtime.Definition;

namespace BindOpen.Extensions.Modeling
{
    /// <summary>
    /// This interface defines the carrier configuration.
    /// </summary>
    public interface IBdoCarrierConfiguration : ITBdoExtensionTitledItemConfiguration<IBdoCarrierDefinition>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        new IBdoCarrierConfiguration Add(params IBdoMetaElement[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        new IBdoCarrierConfiguration WithItems(params IBdoMetaElement[] items);
    }
}