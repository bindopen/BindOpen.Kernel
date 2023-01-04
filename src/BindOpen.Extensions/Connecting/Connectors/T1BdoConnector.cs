using BindOpen.Logging;
using System;

namespace BindOpen.Extensions.Connecting
{
    /// <summary>
    /// This class represents a connector.
    /// </summary>
    public abstract class TBdoConnector<TConnection> :
        BdoConnector,
        ITBdoConnector<TConnection>
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
        public new virtual ITBdoConnector<TConnection> WithConnectionString(string connectionString)
        {
            ConnectionString = connectionString;

            return this as ITBdoConnector<TConnection>;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public new ITBdoConnector<TConnection> WithConnectionTimeOut(int timeOut)
        {
            ConnectionTimeOut = timeOut;

            return this as ITBdoConnector<TConnection>;
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
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="isAutoConnected"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public ITBdoConnector<TConnection> UsingConnection(
            Action<TConnection> action,
            bool isAutoConnected = true,
            IBdoLog log = null)
            => base.UsingConnection((c, l) => action?.Invoke(c as TConnection), isAutoConnected)
                as ITBdoConnector<TConnection>;

        /// <summary>
        /// Executes the specified function.
        /// </summary>
        /// <param name="action">The action using the created connection and the current log to consider.</param>
        /// <param name="isAutoConnected">Indicates whether the connection is automatically opened.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns></returns>
        public ITBdoConnector<TConnection> UsingConnection(
            Action<TConnection, IBdoLog> action,
            bool isAutoConnected = true,
            IBdoLog log = null)
            => base.UsingConnection((c, l) => action?.Invoke(c as TConnection, l), isAutoConnected, log)
                as ITBdoConnector<TConnection>;

        #endregion
    }
}
