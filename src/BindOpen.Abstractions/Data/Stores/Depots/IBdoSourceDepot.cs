using BindOpen.Data.Configuration;
using BindOpen.Data.Items;

namespace BindOpen.Data.Stores
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoSourceDepot : ITBdoDepot<IBdoDatasource>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceName"></param>
        /// <param name="connectorDefinitionUniqueId"></param>
        /// <returns></returns>
        IBdoConfiguration GetConnectorConfig(string sourceName, string connectorDefinitionUniqueId = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceName"></param>
        /// <returns></returns>
        string GetInstanceName(string sourceName = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceName"></param>
        /// <returns></returns>
        string GetInstanceOtherwiseModuleName(string sourceName = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceName"></param>
        /// <returns></returns>
        string GetModuleName(string sourceName = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceName"></param>
        /// <param name="connectorDefinitionUniqueId"></param>
        /// <returns></returns>
        string GetConnectionString(string sourceName = null, string connectorDefinitionUniqueId = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceName"></param>
        /// <param name="connectorDefinitionUniqueId"></param>
        /// <returns></returns>
        bool HasConnectorConfig(string sourceName = null, string connectorDefinitionUniqueId = null);
    }
}