using BindOpen.Extensions.Runtime;
using System.Collections.Generic;

namespace BindOpen.Data.Items
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
        bool IsDefault { get; set; }

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
        IBdoConnectorConfiguration GetConfiguration(string definitionName = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="definitionName"></param>
        /// <returns></returns>
        bool HasConfiguration(string definitionName = null);

        // Mutators ---------------------------

        /// <summary>
        /// Sets the specified kind of this instance. 
        /// </summary>
        /// <param name="kind">The kind to consider.</param>
        IDatasource WithKind(DatasourceKind kind);

        /// <summary>
        /// Sets the specified module name of this instance. 
        /// </summary>
        /// <param name="moduleName">The module name to consider.</param>
        IDatasource WithModuleName(string moduleName);

        /// <summary>
        /// Sets the specified module name of this instance. 
        /// </summary>
        /// <param name="instanceName">The instance name to consider.</param>
        IDatasource WithInstanceName(string instanceName);

        /// <summary>
        /// Specifies that this instance is the default. 
        /// </summary>
        IDatasource AsDefault();

        /// <summary>
        /// Adds the specified connector configuration.
        /// </summary>
        /// <param name="config">The connector to add.</param>
        IDatasource AddConfiguration(IBdoConnectorConfiguration config);

        /// <summary>
        /// Removes the specified connector configuration.
        /// </summary>
        /// <param name="definitionName">The unique ID of the connector definition to consider.</param>
        IDatasource RemoveConfiguration(string definitionName);

        /// <summary>
        /// Sets the specified configurations.
        /// </summary>
        /// <param name="configs">The configurations to consider.</param>
        IDatasource WithConfiguration(params IBdoConnectorConfiguration[] configs);
    }
}