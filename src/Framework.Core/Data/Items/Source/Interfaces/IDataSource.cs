using System.Collections.Generic;
using BindOpen.Framework.Core.Extensions.Items.Connectors;

namespace BindOpen.Framework.Core.Data.Items.Source
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataSource : INamedDataItem
    {
        /// <summary>
        /// 
        /// </summary>
        List<ConnectorConfiguration> Configurations { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string InstanceName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DataSourceKind Kind { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ModuleName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="definitionName"></param>
        /// <returns></returns>
        IConnectorConfiguration GetConfiguration(string definitionName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="definitionName"></param>
        /// <returns></returns>
        bool HasConfiguration(string definitionName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        void AddConfiguration(IConnectorConfiguration config);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="definitionName"></param>
        void RemoveConfiguration(string definitionName);
    }
}