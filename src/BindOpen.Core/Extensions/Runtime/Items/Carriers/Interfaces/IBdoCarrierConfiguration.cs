using BindOpen.Data.Elements;
using BindOpen.Extensions.Definition;

namespace BindOpen.Extensions.Runtime
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
        new IBdoCarrierConfiguration Add(params IDataElement[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        new IBdoCarrierConfiguration WithItems(params IDataElement[] items);
    }
}