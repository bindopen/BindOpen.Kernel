using System;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Application.Depots.Datasources;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Connections;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Items.Connectors;
using BindOpen.Framework.Core.Extensions.Items;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Runtime.Application.Services
{
    /// <summary>
    /// This class represents a connection service.
    /// </summary>
    public class ConnectionService : DataItem, IConnectionService
    {
        /// <summary>
        /// The application scope of this instance.
        /// </summary>
        protected readonly IAppHostScope _appScope = null;

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ConnectionManager class.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        public ConnectionService(IAppHostScope appScope)
        {
            this._appScope = appScope;
        }

        #endregion

        // ------------------------------------------
        // CONNECTION MANAGEMENT
        // ------------------------------------------

        #region Connection Management

        // Create -------------------------------------

        /// <summary>
        /// Creates a connector.
        /// </summary>
        /// <param name="dataSourceName">The data source name to consider.</param>
        /// <param name="connectorDefinitionUniqueId">The connector definition name to consider.</param>
        /// <param name="log">The log of execution to consider.</param>
        /// <returns>Returns True if the connector has been opened. False otherwise.</returns>
        public T Open<T>(
            string dataSourceName,
            string connectorDefinitionUniqueId,
            ILog log = null) where T : IConnection, new()
        {
            return this.Open<T>(null, dataSourceName, connectorDefinitionUniqueId, log);
        }

        /// <summary>
        /// Creates a connector.
        /// </summary>
        /// <param name="dataSourceService">The source service to consider.</param>
        /// <param name="dataSourceName">The data source name to consider.</param>
        /// <param name="connectorDefinitionUniqueId">The connector definition name to consider.</param>
        /// <param name="log">The log of execution to consider.</param>
        /// <returns>Returns True if the connector has been opened. False otherwise.</returns>
        public T Open<T>(
            IDataSourceDepot dataSourceService,
            string dataSourceName,
            string connectorDefinitionUniqueId,
            ILog log = null) where T : IConnection, new()
        {
            if (log == null) log = new Log();

            this.Update<ConnectionService>();

            if (dataSourceService == null)
                dataSourceService = this._appScope?.DataSourceDepot;

            if (dataSourceService == null)
                log.AddError("Source manager missing");
            else if (!dataSourceService.HasSource(dataSourceName))
                log.AddError("Data source '" + dataSourceName + "' missing in manager");
            else
                return this.Open<T>(dataSourceService.GetSource(dataSourceName), connectorDefinitionUniqueId, log);

            return default;
        }

        /// <summary>
        /// Creates a connector.
        /// </summary>
        /// <param name="dataSource">The data source to consider.</param>
        /// <param name="connectorDefinitionUniqueId">The connector definition name to consider.</param>
        /// <param name="log">The log of execution to consider.</param>
        /// <returns>Returns True if the connector has been opened. False otherwise.</returns>
        public T Open<T>(
            IDataSource dataSource, String connectorDefinitionUniqueId,
            ILog log = null) where T : IConnection, new()
        {
            if (log == null) log = new Log();

            if (dataSource == null)
                log.AddError("Data source missing");
            //else if (string.IsNullOrEmpty(connectorDefinitionUniqueId))
            //    log.AddError("Connection definition missing");
            else if (!string.IsNullOrEmpty(connectorDefinitionUniqueId) && dataSource.HasConfiguration(connectorDefinitionUniqueId))
                log.AddError("Connection not defined in data source", description: "No connector is defined in the specified data source.");
            else if (!string.IsNullOrEmpty(connectorDefinitionUniqueId))
                return this.Open<T>(dataSource.GetConfiguration(connectorDefinitionUniqueId), log);
            else if (dataSource.Configurations.Count>0)
                return this.Open<T>(dataSource.Configurations[0], log);

            return default;
        }

        /// <summary>
        /// Creates a connector using the specified data module and connector unique name.
        /// </summary>
        /// <param name="configuration">The connector configuration to consider.</param>
        /// <param name="log">The log of execution to consider.</param>
        /// <returns>Returns True if the connector has been opened. False otherwise.</returns>
        public T Open<T>(IConnectorConfiguration configuration, ILog log = null) where T : IConnection, new()
        {
            ILog subLog = new Log();

            this.Update<ConnectionService>();

            T connection = default;
            if (configuration == null)
            {
                subLog.AddError("Connection missing");
            }
            else if (this._appScope == null)
            {
                subLog.AddError("Application scope missing", description: "No application scope was defined for this connection service.");
            }
            else if (!subLog.Append(this._appScope.Check(true)).HasErrorsOrExceptions())
            {
                connection = new T();
                connection.SetConnector(this._appScope.CreateConnector(configuration, null, subLog));

                if (connection.Connector == null)
                {
                    connection = default;
                }
                else
                {
                    subLog.Append(connection.Open());
                }
            }

            (log ?? (log = new Log())).Append(subLog);

            return connection;
        }

        // Close -------------------------------------

        /// <summary>
        /// Closes the specified connector.
        /// </summary>
        /// <param name="connector">The connector to consider.</param>
        /// <returns>Returns the log of execution.</returns>
        public ILog Close(IConnection connector)
        {
            ILog log = new Log();

            if (connector == null)
            {
                this.Update<ConnectionService>();
            }
            else
            {
                connector.Close();
            }

            return log;
        }

        #endregion

        // --------------------------------------------------
        // UPDATE, CHECK, REPAIR
        // --------------------------------------------------

        #region Update_Check_Repair

        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="updateModes">The update modes to consider.</param>
        /// <returns>Log of the operation.</returns>
        /// <remarks>Put reference collections as null if you do not want to repair this instance.</remarks>
        public override ILog Update<T>(
            T item = default,
            string[] specificationAreas = null,
            UpdateMode[] updateModes = null)
        {
            return new Log();
        }

        #endregion
    }
}
