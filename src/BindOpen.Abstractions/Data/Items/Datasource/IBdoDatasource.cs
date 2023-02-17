using BindOpen.Data.Configuration;
using System.Collections.Generic;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoDatasource :
        IBdoHandledItem, IIdentified, INamed, IReferenced
    {
        /// <summary>
        /// 
        /// </summary>
        string InstanceName { get; set; }

        /// <summary>
        /// Sets the specified module name of this instance. 
        /// </summary>
        /// <param name="instanceName">The instance name to consider.</param>
        IBdoDatasource WithInstanceName(string instanceName);

        /// <summary>
        /// 
        /// </summary>
        bool IsDefault { get; set; }

        /// <summary>
        /// Specifies that this instance is the default. 
        /// </summary>
        IBdoDatasource AsDefault(bool isDefault = true);

        /// <summary>
        /// 
        /// </summary>
        DatasourceKind Kind { get; set; }

        /// <summary>
        /// Sets the specified kind of this instance. 
        /// </summary>
        /// <param name="kind">The kind to consider.</param>
        IBdoDatasource WithKind(DatasourceKind kind);

        /// <summary>
        /// 
        /// </summary>
        string ModuleName { get; set; }

        /// <summary>
        /// Sets the specified module name of this instance. 
        /// </summary>
        /// <param name="moduleName">The module name to consider.</param>
        IBdoDatasource WithModuleName(string moduleName);

        /// <summary>
        /// 
        /// </summary>
        IBdoConfiguration Config();

        /// <summary>
        /// 
        /// </summary>
        List<IBdoConfiguration> ConfigList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IBdoConfiguration GetConfig(string name = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool HasConfig(string name = null);

        /// <summary>
        /// Removes the specified connector config.
        /// </summary>
        /// <param name="name">The name of the connector config to consider.</param>
        IBdoDatasource RemoveConfig(string name);

        /// <summary>
        /// Sets the specified configs.
        /// </summary>
        /// <param name="configs">The configs to consider.</param>
        IBdoDatasource WithConfig(params IBdoConfiguration[] configs);

        /// <summary>
        /// Adds the specified connector config.
        /// </summary>
        /// <param name="config">The connector to add.</param>
        IBdoDatasource AddConfig(IBdoConfiguration config);
    }
}