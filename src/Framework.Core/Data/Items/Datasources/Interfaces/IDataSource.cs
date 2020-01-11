using BindOpen.Framework.Extensions.Runtime;
using System.Collections.Generic;

namespace BindOpen.Framework.Data.Items
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDatasource : INamedDataItem
    {
        /// <summary>
        /// 
        /// </summary>
        List<BdoConnectorConfiguration> Configurations { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string InstanceName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DatasourceKind Kind { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ModuleName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="definitionName"></param>
        /// <returns></returns>
        IBdoConnectorConfiguration GetConfiguration(string definitionName);

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
        void AddConfiguration(IBdoConnectorConfiguration config);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="definitionName"></param>
        void RemoveConfiguration(string definitionName);
    }
}