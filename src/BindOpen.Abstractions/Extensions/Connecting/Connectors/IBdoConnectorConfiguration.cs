using BindOpen.Data.Meta;
using BindOpen.Runtime.Definition;

namespace BindOpen.Extensions.Connecting
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoConnectorConfiguration
        : ITBdoExtensionTitledItemConfiguration<IBdoConnectorDefinition>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetConnectionString();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        new IBdoConnectorConfiguration Add(params IBdoMetaData[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        new IBdoConnectorConfiguration WithItems(params IBdoMetaData[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        IBdoConnectorConfiguration WithConnectionString(string connectionString = null);
    }
}