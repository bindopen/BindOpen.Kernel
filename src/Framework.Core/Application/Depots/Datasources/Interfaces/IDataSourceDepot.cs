using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Items.Connectors;
using System.Collections.Generic;

namespace BindOpen.Framework.Core.Application.Depots.Datasources
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataSourceDepot : ITDepot<DataSource>
    {
        /// <summary>
        /// 
        /// </summary>
        List<DataSource> Sources { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceName"></param>
        /// <param name="connectorDefinitionUniqueId"></param>
        /// <returns></returns>
        IConnectorConfiguration GetConnectorConfiguration(string sourceName, string connectorDefinitionUniqueId);

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