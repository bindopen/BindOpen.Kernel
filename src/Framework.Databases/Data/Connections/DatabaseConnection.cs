using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Connections;
using BindOpen.Framework.Core.Extensions.Runtime.Connectors;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;
using BindOpen.Framework.Databases.Data.Queries;
using BindOpen.Framework.Databases.Extensions.Runtime.Connectors;

namespace BindOpen.Framework.Databases.Data.Connections
{

    /// <summary>
    /// This class represents a database connection.
    /// </summary>
    [XmlType("DatabaseConnection", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "databaseConnection", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class DatabaseConnection : Connection, IDatabaseConnection
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
                this.SetConnector(value);
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
            this.Connector = connector;
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
        protected override void SetConnector(Connector connector)
        {
            base.SetConnector(connector);
        }

        /// <summary>
        /// Gets the database connection of this instance.
        /// </summary>
        /// <returns>Returns the connection of this instance.</returns>
        public IDbConnection GetIDbConnection()
        {
            return this.Connector?.GetDbConnection();
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
            String queryText,
            ScriptVariableSet scriptVariableSet = null,
            Log log = null)
        {
            this.Connector?.ExecuteNonQuery(queryText, scriptVariableSet, log);
        }

        /// <summary>
        /// Executes a database data query putting the result into a data reader.
        /// </summary>
        /// <param name="query">The data query to execute.</param>
        /// <param name="scriptVariableSet">The interpretation variables to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the data query execution task.</returns>
        public void ExecuteQuery(
            DbDataQuery query,
            ScriptVariableSet scriptVariableSet = null,
            Log log = null)
        {
            log = log ?? new Log();

            if (query != null)
            {
                // retrieve all the alias platform users
                String queryText = this.Connector?.GetSqlText(query, scriptVariableSet, log);
                if (!log.HasErrorsOrExceptions())
                    this.ExecuteNonQuery(queryText, scriptVariableSet, log);
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
            String queryText,
            ref DbDataReader dataReader,
            ScriptVariableSet scriptVariableSet = null,
            Log log = null)
        {
            if (this.Connector != null)
            {
                this.Connector.ExecuteQuery(queryText, ref dataReader, scriptVariableSet, log);
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
            DbDataQuery query,
            ref DbDataReader dataReader,
            ScriptVariableSet scriptVariableSet = null,
            Log log = null)
        {
            log = log ?? new Log();

            if (query != null)
            {
                // retrieve all the alias platform users
                String queryText = this.Connector?.GetSqlText(query, scriptVariableSet, log);
                if (!log.HasErrorsOrExceptions())
                    this.ExecuteQuery(queryText, ref dataReader, scriptVariableSet, log);
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
            String queryText,
            ref DataSet dataSet,
            ScriptVariableSet scriptVariableSet = null,
            Log log = null)
        {
            this.Connector?.ExecuteQuery(queryText, ref dataSet, scriptVariableSet, log);
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
            DbDataQuery query,
            ref DataSet dataSet,
            ScriptVariableSet scriptVariableSet = null,
            Log log = null)
        {
            log = log ?? new Log();

            if (query != null)
            {
                // retrieve all the alias platform users
                String queryText = this.Connector?.GetSqlText(query, scriptVariableSet, log);
                if (!log.HasErrorsOrExceptions())
                    this.ExecuteQuery(queryText, ref dataSet, scriptVariableSet, log);
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
            Log log = null)
        {
            this.Connector?.GetIdentity(ref id, log);
        }

        /// <summary>
        /// Executes the specified data query and updates the specified data table.
        /// </summary>
        /// <param name="queryText">The text to execute.</param>
        /// <param name="dataTable">The data table to update.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the task.</returns>
        public void UpdateDataTable(
            String queryText,
            DataTable dataTable,
            Log log = null)
        {
            this.Connector?.UpdateDataTable(queryText, dataTable, log);
        }

        /// <summary>
        /// Executes the specified data query and updates the specified data table.
        /// </summary>
        /// <param name="query">The data query to execute.</param>
        /// <param name="dataTable">The data table to update.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the task.</returns>
        public void UpdateDataTable(
            DbDataQuery query,
            DataTable dataTable,
            Log log = null)
        {
            log = log ?? new Log();

            if (query != null)
            {
                // retrieve all the alias platform users
                String queryText = this.Connector?.GetSqlText(query, null, log);
                if (!log.HasErrorsOrExceptions())
                    this.UpdateDataTable(queryText, dataTable, log);
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
            String queryText,
            DataSet dataSet,
            List<String> tableNames,
            Log log = null)
        {
            this.Connector?.UpdateDataSet(queryText, dataSet, tableNames, log);
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
            DbDataQuery query,
            DataSet dataSet,
            List<String> tableNames,
            Log log = null)
        {
            log = log ?? new Log();

            if (query != null)
            {
                // retrieve all the alias platform users
                String queryText = this.Connector?.GetSqlText(query, null, log);
                if (!log.HasErrorsOrExceptions())
                    this.UpdateDataSet(queryText, dataSet, tableNames, log);
            }

        }

        #endregion
    }
}
