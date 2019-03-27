using System;
using System.Collections.Generic;
using BindOpen.Framework.Core.Application.Datasources;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Connections;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Configuration.Connectors;
using BindOpen.Framework.Core.Extensions.Definition.Connectors;
using BindOpen.Framework.Core.Extensions.Runtime.Connectors;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

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
        protected readonly IRuntimeAppScope _appScope = null;

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ConnectionManager class.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        public ConnectionService(IRuntimeAppScope appScope)
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
        /// <param name="connectorDefinitionUniqueName">The connector definition name to consider.</param>
        /// <param name="log">The log of execution to consider.</param>
        /// <returns>Returns True if the connector has been opened. False otherwise.</returns>
        public T Open<T>(
            String dataSourceName,
            String connectorDefinitionUniqueName,
            Log log = null) where T : Connection, new()
        {
            return this.Open<T>(null, dataSourceName, connectorDefinitionUniqueName, log) as T;
        }

        /// <summary>
        /// Creates a connector.
        /// </summary>
        /// <param name="dataSourceManager">The source manager to consider.</param>
        /// <param name="dataSourceName">The data source name to consider.</param>
        /// <param name="connectorDefinitionUniqueName">The connector definition name to consider.</param>
        /// <param name="log">The log of execution to consider.</param>
        /// <returns>Returns True if the connector has been opened. False otherwise.</returns>
        public T Open<T>(
            DataSourceService dataSourceManager,
            String dataSourceName,
            String connectorDefinitionUniqueName,
            Log log = null) where T : Connection, new()
        {
            if (log == null) log = new Log();

            this.Update<ConnectionService>();

            if (dataSourceManager == null)
                dataSourceManager = this._appScope?.DataSourceService;

            if (dataSourceManager == null)
                log.AddError("Source manager missing");
            else if (!dataSourceManager.HasSource(dataSourceName))
                log.AddError("Data source '" + dataSourceName + "' missing in manager");
            else
                return this.Open<T>(dataSourceManager.GetSource(dataSourceName), connectorDefinitionUniqueName, log) as T;

            return null;
        }

        /// <summary>
        /// Creates a connector.
        /// </summary>
        /// <param name="dataSource">The data source to consider.</param>
        /// <param name="connectorDefinitionUniqueName">The connector definition name to consider.</param>
        /// <param name="log">The log of execution to consider.</param>
        /// <returns>Returns True if the connector has been opened. False otherwise.</returns>
        public T Open<T>(
            DataSource dataSource, String connectorDefinitionUniqueName,
            Log log = null) where T : Connection, new()
        {
            if (log == null) log = new Log();

            if (dataSource == null)
                log.AddError("Data source missing");
            //else if (String.IsNullOrEmpty(connectorDefinitionUniqueName))
            //    log.AddError("Connection definition missing");
            else if (!String.IsNullOrEmpty(connectorDefinitionUniqueName) && dataSource.HasConfiguration(connectorDefinitionUniqueName))
                log.AddError("Connection not defined in data source", description: "No connector is defined in the specified data source.");
            else if (!String.IsNullOrEmpty(connectorDefinitionUniqueName))
                return this.Open<T>(dataSource.GetConfiguration(connectorDefinitionUniqueName), log) as T;
            else if (dataSource.Configurations.Count>0)
                return this.Open<T>(dataSource.Configurations[0], log);

            return null;
        }

        /// <summary>
        /// Creates a connector using the specified data module and connector unique name.
        /// </summary>
        /// <param name="configuration">The connector configuration to consider.</param>
        /// <param name="log">The log of execution to consider.</param>
        /// <returns>Returns True if the connector has been opened. False otherwise.</returns>
        public T Open<T>(ConnectorConfiguration configuration, Log log = null) where T : Connection, new()
        {
            Log subLog = new Log();

            this.Update<ConnectionService>();

            T connection = null;
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
                connection = new T()
                {
                    Connector = this._appScope.CreateItem<ConnectorDefinition>(
                            null, configuration, null, subLog) as Connector
                };

                if (connection.Connector == null)
                {
                    connection = null;
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
        public Log Close(Connection connector)
        {
            Log log = new Log();

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
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>Log of the operation.</returns>
        /// <remarks>Put reference collections as null if you do not want to repair this instance.</remarks>
        public override Log Update<T>(
            T item = null,
            List<String> specificationAreas = null,
            List<UpdateMode> updateModes = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null)
        {
            return new Log();
        }

        #endregion
    }
}
