using BindOpen.Extensions.Connecting;
using System.Collections.Generic;

namespace BindOpen.MetaData.Items
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoDataSource : ITIdentifiedPoco<IBdoDataSource>, ITNamedPoco<IBdoDataSource>, IReferenced
    {
        /// <summary>
        /// 
        /// </summary>
        string InstanceName { get; set; }

        /// <summary>
        /// Sets the specified module name of this instance. 
        /// </summary>
        /// <param name="instanceName">The instance name to consider.</param>
        IBdoDataSource WithInstanceName(string instanceName);

        /// <summary>
        /// 
        /// </summary>
        bool IsDefault { get; set; }

        /// <summary>
        /// Specifies that this instance is the default. 
        /// </summary>
        IBdoDataSource AsDefault(bool isDefault = true);

        /// <summary>
        /// 
        /// </summary>
        DatasourceKind Kind { get; set; }

        /// <summary>
        /// Sets the specified kind of this instance. 
        /// </summary>
        /// <param name="kind">The kind to consider.</param>
        IBdoDataSource WithKind(DatasourceKind kind);

        /// <summary>
        /// 
        /// </summary>
        string ModuleName { get; set; }

        /// <summary>
        /// Sets the specified module name of this instance. 
        /// </summary>
        /// <param name="moduleName">The module name to consider.</param>
        IBdoDataSource WithModuleName(string moduleName);

        /// <summary>
        /// 
        /// </summary>
        IBdoConnectorConfiguration Config();

        /// <summary>
        /// 
        /// </summary>
        List<IBdoConnectorConfiguration> ConfigList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IBdoConnectorConfiguration GetConfig(string name = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool HasConfig(string name = null);

        /// <summary>
        /// Removes the specified connector configuration.
        /// </summary>
        /// <param name="name">The name of the connector configuration to consider.</param>
        IBdoDataSource RemoveConfig(string name);

        /// <summary>
        /// Sets the specified configurations.
        /// </summary>
        /// <param name="configs">The configurations to consider.</param>
        IBdoDataSource WithConfig(params IBdoConnectorConfiguration[] configs);

        /// <summary>
        /// Adds the specified connector configuration.
        /// </summary>
        /// <param name="config">The connector to add.</param>
        IBdoDataSource AddConfig(IBdoConnectorConfiguration config);
    }
}