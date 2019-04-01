using BindOpen.Framework.Core.Application.Datasources;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Connections;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Configuration.Connectors;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Runtime.Application.Services
{
    /// <summary>
    /// This interface defines the connection service.
    /// </summary>
    public interface IConnectionService
    {
        /// <summary>
        /// Closes the connection.
        /// </summary>
        /// <param name="connector"></param>
        /// <returns>The log of the operation.</returns>
        Log Close(Connection connector);
        //Connection GetConnection(string name);
        //bool IsOpened(string name);

        /// <summary>
        /// Opens the connection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configuration"></param>
        /// <param name="log"></param>
        /// <returns>The log of the operation.</returns>
        T Open<T>(ConnectorConfiguration configuration, ILog log = null) where T : Connection, new();

        /// <summary>
        /// Opens the connection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataSource"></param>
        /// <param name="connectorDefinitionUniqueName"></param>
        /// <param name="log"></param>
        /// <returns>The log of the operation.</returns>
        T Open<T>(IDataSource dataSource, string connectorDefinitionUniqueName, ILog log = null) where T : Connection, new();

        /// <summary>
        /// Opens the connection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataSourceManager"></param>
        /// <param name="dataSourceName"></param>
        /// <param name="connectorDefinitionUniqueName"></param>
        /// <param name="log"></param>
        /// <returns>The log of the operation.</returns>
        T Open<T>(DataSourceService dataSourceManager, string dataSourceName, string connectorDefinitionUniqueName, ILog log = null) where T : Connection, new();

        /// <summary>
        /// Opens the connection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataSourceName"></param>
        /// <param name="connectorDefinitionUniqueName"></param>
        /// <param name="log"></param>
        /// <returns>The log of the operation.</returns>
        T Open<T>(string dataSourceName, string connectorDefinitionUniqueName, ILog log = null) where T : Connection, new();

        /// <summary>
        /// Updates this service.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="specificationAreas"></param>
        /// <param name="updateModes"></param>
        /// <param name="appScope"></param>
        /// <param name="scriptVariableSet"></param>
        /// <returns>The log of the operation.</returns>
        Log Update<T>(T item = default, string[] specificationAreas = null, UpdateMode[] updateModes = null, IAppScope appScope = null, ScriptVariableSet scriptVariableSet = null) where T : DataItem;
    }
}