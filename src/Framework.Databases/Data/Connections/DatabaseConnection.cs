using BindOpen.Framework.Core.Data.Connections;
using BindOpen.Framework.Core.Extensions.Runtime.Items;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;
using BindOpen.Framework.Databases.Data.Queries;
using BindOpen.Framework.Databases.Extensions.Connectors;
using System.Collections.Generic;
using System.Data;
using System.Xml.Serialization;

namespace BindOpen.Framework.Databases.Data.Connections
{
    /// <summary>
    /// This class represents a database connection.
    /// </summary>
    [XmlType("DatabaseConnection", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "databaseConnection", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class DatabaseConnection : BdoConnection, IDatabaseConnection
    {
        // -----------------------------------------------
        // PROPERTIES
        // -----------------------------------------------

        #region Properties

        /// <summary>
        /// The connector of this instance.
        /// </summary>
        [XmlIgnore()]
        public new DatabaseConnector Connector
        {
            get
            {
                return base.Connector as DatabaseConnector;
            }
            set
            {
                SetConnector(value);
            }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DatabaseConnection class.
        /// </summary>
        public DatabaseConnection() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the DatabaseConnection class.
        /// </summary>
        /// <param name="connector">The connector to consider.</param>
        public DatabaseConnection(DatabaseConnector connector)
        {
            Connector = connector;
        }

        #endregion

        // ------------------------------------------
        // MANAGEMENT
        // ------------------------------------------

        #region Management

        /// <summary>
        /// Sets the connector of this instance.
        /// </summary>
        /// <param name="connector">The database connector to consider.</param>
        public override void SetConnector(IBdoConnector connector)
        {
            base.SetConnector(connector);
        }

        /// <summary>
        /// Gets the database connection of this instance.
        /// </summary>
        /// <returns>Returns the connection of this instance.</returns>
        public IDbConnection GetIDbConnection()
        {
            return Connector?.GetDbConnection();
        }

        // Execution non query  ---------------------------------------

        /// <summary>
        /// Executes a database data query that returns nothing.
        /// </summary>
        /// <param name="queryText">The text to execute.</param>
        /// <param name="scriptVariableSet">The interpretation variables to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the data query execution task.</returns>
        public void ExecuteNonQuery(
            string queryText,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            Connector?.ExecuteNonQuery(queryText, scriptVariableSet, log);
        }

        /// <summary>
        /// Executes a database data query putting the result into a data reader.
        /// </summary>
        /// <param name="query">The data query to execute.</param>
        /// <param name="scriptVariableSet">The interpretation variables to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the data query execution task.</returns>
        public void ExecuteQuery(
            IDbQuery query,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            if (query != null)
            {
                // retrieve all the alias platform users
                string queryText = Connector?.GetSqlText(query, scriptVariableSet, log);
                if (!log.HasErrorsOrExceptions())
                    ExecuteNonQuery(queryText, scriptVariableSet, log);
            }
        }

        // Execution query data reader  ---------------------------------------

        /// <summary>
        /// Executes a database data query putting the result into a data reader.
        /// </summary>
        /// <param name="queryText">The text to execute.</param>
        /// <param name="dataReader">The output data reader.</param>
        /// <param name="scriptVariableSet">The interpretation variables to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the data query execution task.</returns>
        public void ExecuteQuery(
            string queryText,
            ref IDataReader dataReader,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            if (Connector != null)
            {
                Connector.ExecuteQuery(queryText, ref dataReader, scriptVariableSet, log);
            }
        }

        /// <summary>
        /// Executes a database data query putting the result into a dataset.
        /// </summary>
        /// <param name="query">The database data query to execute.</param>
        /// <param name="dataReader">The output data reader.</param>
        /// <param name="scriptVariableSet">The interpretation variables to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the data query execution task.</returns>
        public void ExecuteQuery(
            IDbQuery query,
            ref IDataReader dataReader,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            if (query != null)
            {
                // retrieve all the alias platform users
                string queryText = Connector?.GetSqlText(query, scriptVariableSet, log);
                if (!log.HasErrorsOrExceptions())
                    ExecuteQuery(queryText, ref dataReader, scriptVariableSet, log);
            }
        }

        // Execution query dataset  ---------------------------------------

        /// <summary>
        /// Executes a database data query putting the result into a dataset.
        /// </summary>
        /// <param name="queryText">The text to execute.</param>
        /// <param name="dataSet">The output dataset.</param>
        /// <param name="scriptVariableSet">The interpretation variables to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the data query execution task.</returns>
        public void ExecuteQuery(
            string queryText,
            ref DataSet dataSet,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            Connector?.ExecuteQuery(queryText, ref dataSet, scriptVariableSet, log);
        }

        /// <summary>
        /// Executes a database data query putting the result into a dataset.
        /// </summary>
        /// <param name="query">The database data query to execute.</param>
        /// <param name="dataSet">The output dataset.</param>
        /// <param name="scriptVariableSet">The interpretation variables to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the data query execution task.</returns>
        public void ExecuteQuery(
            IDbQuery query,
            ref DataSet dataSet,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            if (query != null)
            {
                // retrieve all the alias platform users
                string queryText = Connector?.GetSqlText(query, scriptVariableSet, log);
                if (!log.HasErrorsOrExceptions())
                    ExecuteQuery(queryText, ref dataSet, scriptVariableSet, log);
            }
        }

        // Table ---------------------------------------

        /// <summary>
        /// Gets the identity of the last inserted item
        /// </summary>
        /// <param name="id">The long identifier to populate.</param>
        /// <param name="log">The log to consider.</param>
        public void GetIdentity(
            ref long id,
            IBdoLog log = null)
        {
            Connector?.GetIdentity(ref id, log);
        }

        /// <summary>
        /// Executes the specified data query and updates the specified data table.
        /// </summary>
        /// <param name="queryText">The text to execute.</param>
        /// <param name="dataTable">The data table to update.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the task.</returns>
        public void UpdateDataTable(
            string queryText,
            DataTable dataTable,
            IBdoLog log = null)
        {
            Connector?.UpdateDataTable(queryText, dataTable, log);
        }

        /// <summary>
        /// Executes the specified data query and updates the specified data table.
        /// </summary>
        /// <param name="query">The data query to execute.</param>
        /// <param name="dataTable">The data table to update.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the task.</returns>
        public void UpdateDataTable(
            IDbQuery query,
            DataTable dataTable,
            IBdoLog log = null)
        {
            if (query != null)
            {
                // retrieve all the alias platform users
                string queryText = Connector?.GetSqlText(query, null, log);
                if (!log.HasErrorsOrExceptions())
                    UpdateDataTable(queryText, dataTable, log);
            }
        }

        /// <summary>
        /// Executes the specified data query and updates the specified data table.
        /// </summary>
        /// <param name="queryText">The text to execute.</param>
        /// <param name="dataSet">The data set to update.</param>
        /// <param name="tableNames">The table names to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the task.</returns>
        public void UpdateDataSet(
            string queryText,
            DataSet dataSet,
            List<string> tableNames,
            IBdoLog log = null)
        {
            Connector?.UpdateDataSet(queryText, dataSet, tableNames, log);
        }

        /// <summary>
        /// Executes the specified data query and updates the specified data table.
        /// </summary>
        /// <param name="query">The data query to execute.</param>
        /// <param name="dataSet">The data set to update.</param>
        /// <param name="tableNames">The table names to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the task.</returns>
        public void UpdateDataSet(
            IDbQuery query,
            DataSet dataSet,
            List<string> tableNames,
            IBdoLog log = null)
        {
            if (query != null)
            {
                // retrieve all the alias platform users
                string queryText = Connector?.GetSqlText(query, null, log);
                if (!log.HasErrorsOrExceptions())
                    UpdateDataSet(queryText, dataSet, tableNames, log);
            }
        }

        #endregion
    }
}
