using BindOpen.Meta.Elements;
using BindOpen.Logging;
using BindOpen.Runtime.Definition;
using BindOpen.Runtime.Scopes;
using System;
using System.Runtime.InteropServices;

namespace BindOpen.Extensions.Connecting
{
    /// <summary>
    /// This class represents a connector.
    /// </summary>
    public abstract class BdoConnector :
        TBdoExtensionItem<IBdoConnectorDefinition, IBdoConnectorConfiguration, IBdoConnector>,
        IBdoConnector
    {
        // ------------------------------------------
        // Variables
        // ------------------------------------------

        #region Variables

        /// <summary>
        /// The scope of this instance.
        /// </summary>
        protected IBdoScope _scope;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoConnector class.
        /// </summary>
        protected BdoConnector() : base()
        {
        }

        #endregion

        // ------------------------------------------
        // IBdoConnector Implementation
        // ------------------------------------------

        #region IBdoConnector

        /// <summary>
        /// The connection string of this instance.
        /// </summary>
        [BdoMeta("connectionString")]
        public string ConnectionString { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public virtual IBdoConnector WithConnectionString(string connectionString)
        {
            ConnectionString = connectionString;

            return this;
        }

        /// <summary>
        /// The connection string of this instance.
        /// </summary>
        [BdoMeta("connectionTimeOut")]
        public int ConnectionTimeOut { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public IBdoConnector WithConnectionTimeOut(int timeOut)
        {
            ConnectionTimeOut = timeOut;

            return this;
        }

        /// <summary>
        /// Creates a new connection.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        public virtual IBdoConnection NewConnection(IBdoLog log = null)
        {
            return null;
        }

        /// <summary>
        /// Updates this instance considering the specified scope.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <returns>Returns the database builder.</returns>
        public virtual IBdoConnector WithScope(IBdoScope scope)
        {
            _scope = scope;

            return this;
        }

        /// <summary>
        /// Executes the specified function.
        /// </summary>
        /// <param name="action">The action using the created connection and the current log to consider.</param>
        /// <param name="isAutoConnected">Indicates whether the connection is automatically opened.</param>
        /// <returns></returns>
        public IBdoConnector UsingConnection(
            Action<IBdoConnection> action,
            bool isAutoConnected = true,
            IBdoLog log = null)
            => UsingConnection((c, l) => action?.Invoke(c), isAutoConnected, log);

        /// <summary>
        /// Executes the specified function.
        /// </summary>
        /// <param name="action">The action using the created connection and the current log to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="isAutoConnected">Indicates whether the connection is automatically opened.</param>
        /// <returns></returns>
        public IBdoConnector UsingConnection(
            Action<IBdoConnection, IBdoLog> action,
            bool isAutoConnected = true,
            IBdoLog log = null)
        {
            using var connection = NewConnection(log);
            if (connection != null)
            {
                if (isAutoConnected)
                {
                    try
                    {
                        connection.Connect(log);
                    }
                    catch (ExternalException ex)
                    {
                        log?.AddException("An exception occured while trying to open connection",
                            description: ex.ToString());
                    }
                }

                action?.Invoke(connection, log);
            }

            return this;
        }

        #endregion
    }
}
