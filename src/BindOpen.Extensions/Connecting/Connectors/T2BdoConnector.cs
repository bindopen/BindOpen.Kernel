using BindOpen.Runtime.Scopes;
using System;
using BindOpen.Logging;

namespace BindOpen.Extensions.Connecting
{
    /// <summary>
    /// This class represents a connector.
    /// </summary>
    public abstract class TBdoConnector<T, TConnection> :
        TBdoConnector<TConnection>,
        ITBdoConnector<T, TConnection>
        where T : class, IBdoConnector
        where TConnection : class, IBdoConnection
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

        #endregion

        // ------------------------------------------
        // IBdoConnector Implementation
        // ------------------------------------------

        #region IBdoConnector

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public new virtual T WithConnectionString(string connectionString)
        {
            ConnectionString = connectionString;

            return this as T;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public new T WithConnectionTimeOut(int timeOut)
        {
            ConnectionTimeOut = timeOut;

            return this as T;
        }

        /// <summary>
        /// Creates a new connection.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        public new virtual TConnection NewConnection(IBdoLog log = null)
        {
            return null;
        }

        /// <summary>
        /// Updates this instance considering the specified scope.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <returns>Returns the database builder.</returns>
        public new T WithScope(IBdoScope scope)
        {
            _scope = scope;

            return this as T;
        }

        /// <summary>
        /// Executes the specified function.
        /// </summary>
        /// <param name="action">The action using the created connection and the current log to consider.</param>
        /// <param name="isAutoConnected">Indicates whether the connection is automatically opened.</param>
        /// <returns></returns>
        public new T UsingConnection(
            Action<TConnection> action,
            bool isAutoConnected = true,
            IBdoLog log = null)
            => base.UsingConnection(action, isAutoConnected, log) as T;

        /// <summary>
        /// Executes the specified function.
        /// </summary>
        /// <param name="action">The action using the created connection and the current log to consider.</param>
        /// <param name="isAutoConnected">Indicates whether the connection is automatically opened.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="isAutoConnected">Indicates whether the connection is automatically opened.</param>
        /// <returns></returns>
        public new T UsingConnection(
            Action<TConnection, IBdoLog> action,
            bool isAutoConnected = true,
            IBdoLog log = null)
            => base.UsingConnection(action, isAutoConnected, log) as T;

        #endregion
    }
}
