using BindOpen.Data.Connections;
using BindOpen.System.Diagnostics;
using System;

namespace BindOpen.Extensions.Runtime
{
    /// <summary>
    /// This class represents a connector.
    /// </summary>
    public abstract class TBdoConnector<T> : BdoConnector, ITBdoConnector<T>
        where T : class, IBdoConnection
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TBdoConnector class.
        /// </summary>
        protected TBdoConnector() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the TBdoConnector class.
        /// </summary>
        /// <param name="config">The configuration of this instance.</param>
        protected TBdoConnector(IBdoConnectorConfiguration config)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the TBdoConnector class.
        /// </summary>
        /// <param name="name">The name of this instance.</param>
        /// <param name="connectionString">The connection string to consider.</param>
        protected TBdoConnector(string name, string connectionString = null) : base()
        {
            Name = name;
            ConnectionString = connectionString;
        }

        #endregion

        // ------------------------------------------
        // MANAGEMENT
        // ------------------------------------------

        #region Management

        /// <summary>
        /// Creates a new connection.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        public new abstract T CreateConnection(IBdoLog log = null);

        /// <summary>
        /// Executes the specified function.
        /// </summary>
        /// <param name="action">The action using the created connection and the current log to consider.</param>
        /// <param name="isAutoConnected">Indicates whether the connection is automatically opened.</param>
        /// <returns></returns>
        public void UsingConnection(
            Action<T> action,
            bool isAutoConnected = true)
            => UsingConnection((c, l) => action?.Invoke(c), null, isAutoConnected);

        /// <summary>
        /// Executes the specified function.
        /// </summary>
        /// <param name="action">The action using the created connection and the current log to consider.</param>
        /// <param name="isAutoConnected">Indicates whether the connection is automatically opened.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="isAutoConnected">Indicates whether the connection is automatically opened.</param>
        /// <returns></returns>
        public void UsingConnection(
            Action<T, IBdoLog> action,
            IBdoLog log,
            bool isAutoConnected = true)
            => base.UsingConnection((conn, l) => action?.Invoke(conn as T, l), log, isAutoConnected);

        #endregion
    }
}
