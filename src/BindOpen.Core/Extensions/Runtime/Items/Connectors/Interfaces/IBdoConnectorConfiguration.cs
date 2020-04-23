using BindOpen.Data.Elements;
using BindOpen.Extensions.Definition;

namespace BindOpen.Extensions.Runtime
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoConnectorConfiguration : ITBdoExtensionTitledItemConfiguration<IBdoConnectorDefinition>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        new IBdoConnectorConfiguration Add(params IDataElement[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        new IBdoConnectorConfiguration WithItems(params IDataElement[] items);

    }
}