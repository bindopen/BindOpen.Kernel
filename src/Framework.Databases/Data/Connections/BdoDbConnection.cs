using BindOpen.Framework.Core.Data.Connections;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Databases.Extensions.Connectors;
using System;
using System.Data;
using System.Xml.Serialization;

namespace BindOpen.Framework.Databases.Data.Connections
{
    /// <summary>
    /// This class represents a database connection.
    /// </summary>
    [XmlType("DatabaseConnection", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "databaseConnection", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class BdoDbConnection : BdoConnection, IBdoDbConnection
    {
        // -----------------------------------------------
        // VARIABLES
        // -----------------------------------------------

        #region Variables

        private readonly string _databaseName = null;
        private readonly IDbConnection _nativeDbConnection = null;

        #endregion

        // -----------------------------------------------
        // PROPERTIES
        // -----------------------------------------------

        #region Properties

        /// <summary>
        /// The connector of this instance.
        /// </summary>
        [XmlIgnore()]
        public new BdoDbConnector Connector => base.Connector as BdoDbConnector;

        /// <summary>
        /// Gets the .NET database connection of this instance.
        /// </summary>
        /// <returns>Returns the connection of this instance.</returns>
        [XmlIgnore()]
        public IDbConnection NativeConnection => _nativeDbConnection;

        /// <summary>
        /// The name of the database of this instance.
        /// </summary>
        public string Database => _databaseName;

        string IDbConnection.ConnectionString { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoDbConnection class.
        /// </summary>
        /// <param name="connector">The connector to consider.</param>
        /// <param name="nativeConnection">The native connection to consider.</param>
        public BdoDbConnection(BdoDbConnector connector, IDbConnection nativeConnection)
        {
            _connector = connector;
            _nativeDbConnection = nativeConnection;
        }

        #endregion

        // ------------------------------------------
        // MANAGEMENT
        // ------------------------------------------

        #region Management

        /// <summary>
        /// Changes the current database .
        /// </summary>
        /// <param name="databaseName">The name of the database to consider.</param>
        /// <returns>Returns the log of process.</returns>
        public IBdoLog ChangeDatabase(string databaseName)
        {
            IBdoLog log = new BdoLog();
            try
            {
                _nativeDbConnection?.ChangeDatabase(databaseName);
            }
            catch (Exception ex)
            {
                log.AddException(ex);
            }

            return log;
        }

        #endregion

        // ------------------------------------------
        // IDISPOSABLE METHODS
        // ------------------------------------------

        #region IDisposable_Methods

        /// <summary>
        /// Disposes this instance. 
        /// </summary>
        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            if (isDisposing)
            {
                _nativeDbConnection?.Dispose();
            }
        }

        #endregion

        // ------------------------------------------
        // IBDOCONNECTION METHODS
        // ------------------------------------------

        #region IBdoConnection_Methods

        /// <summary>
        /// Opens this instance.
        /// </summary>
        /// <returns>Returns the log of process.</returns>
        public override IBdoLog Open()
        {
            IBdoLog log = new BdoLog();
            try
            {
                _nativeDbConnection?.Open();
            }
            catch (Exception ex)
            {
                log.AddException(ex);
            }

            return log;
        }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        /// <returns>Returns the log of process.</returns>
        public override IBdoLog Close()
        {
            IBdoLog log = new BdoLog();
            try
            {
                _nativeDbConnection?.Close();
            }
            catch (Exception ex)
            {
                log.AddException(ex);
            }

            return log;
        }

        /// <summary>
        /// Begins transaction.
        /// </summary>
        /// <returns>Returns the created transaction.</returns>
        public IDbTransaction BeginTransaction()
        {
            return _nativeDbConnection?.BeginTransaction();
        }

        /// <summary>
        /// Begins transaction.
        /// </summary>
        /// <param name="il">The isolation level to cosnider.</param>
        /// <returns>Returns the created transaction.</returns>
        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            return _nativeDbConnection?.BeginTransaction(il);
        }

        /// <summary>
        /// Changes database.
        /// </summary>
        /// <param name="databaseName">The name of the database to consider.</param>
        void IDbConnection.ChangeDatabase(string databaseName)
        {
            _nativeDbConnection?.ChangeDatabase(databaseName);
        }

        void IDbConnection.Close()
        {
            _nativeDbConnection?.Close();
        }

        public IDbCommand CreateCommand()
        {
            return _nativeDbConnection?.CreateCommand();
        }

        void IDbConnection.Open()
        {
            _nativeDbConnection?.Open();
        }

        #endregion

    }
}
