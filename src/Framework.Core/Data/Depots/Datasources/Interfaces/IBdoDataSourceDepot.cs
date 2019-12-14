using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Runtime.Items;
using System.Collections.Generic;

namespace BindOpen.Framework.Core.Data.Depots.Datasources
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoDatasourceDepot : ITBdoDepot<Datasource>
    {
        /// <summary>
        /// 
        /// </summary>
        List<Datasource> Sources { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceName"></param>
        /// <param name="connectorDefinitionUniqueId"></param>
        /// <returns></returns>
        IBdoConnectorConfiguration GetConnectorConfiguration(string sourceName, string connectorDefinitionUniqueId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceName"></param>
        /// <returns></returns>
        string GetInstanceName(string sourceName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceName"></param>
        /// <returns></returns>
        string GetInstanceOtherwiseModuleName(string sourceName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceName"></param>
        /// <returns></returns>
        string GetModuleName(string sourceName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceName"></param>
        /// <param name="connectorDefinitionUniqueId"></param>
        /// <returns></returns>
        string GetStringConnection(string sourceName, string connectorDefinitionUniqueId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceName"></param>
        /// <param name="connectorDefinitionUniqueId"></param>
        /// <returns></returns>
        bool HasConnectorConfiguration(string sourceName, string connectorDefinitionUniqueId);
    }
}