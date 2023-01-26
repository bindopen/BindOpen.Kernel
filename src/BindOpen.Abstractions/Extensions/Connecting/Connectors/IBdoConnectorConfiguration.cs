using BindOpen.Runtime.Definition;

namespace BindOpen.Extensions.Connecting
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoConnectorConfiguration
        : ITBdoExtensionItemConfiguration<IBdoConnectorDefinition>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetConnectionString();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        IBdoConnectorConfiguration WithConnectionString(string connectionString = null);
    }
}