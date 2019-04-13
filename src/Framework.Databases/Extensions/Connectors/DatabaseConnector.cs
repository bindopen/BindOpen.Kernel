using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Items.Dictionary;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.Extensions.Runtime.Connectors;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;
using BindOpen.Framework.Databases.Data.Queries;
using BindOpen.Framework.Databases.Data.Queries.Builders;

namespace BindOpen.Framework.Databases.Extensions.Connectors
{
    /// <summary>
    /// This class defines a database connector.
    /// </summary>
    public abstract class DatabaseConnector : Connector
    {
        // -----------------------------------------------
        // PROPERTIES
        // -----------------------------------------------

        #region Properties

        /// <summary>
        /// The provider of this instance.
        /// </summary>
        [XmlIgnore()]
        public DbQueryBuilder QueryBuilder { get; set; } = null;

        /// <summary>
        /// The provider of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name="provider")]
        public string Provider
        {
            get;
            set;
        }

        /// <summary>
        /// The server address of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name="serverAddress")]
        public string ServerAddress
        {
            get;
            set;
        }

        /// <summary>
        /// The initial catalog of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name="initialCatalog")]
        public string InitialCatalog
        {
            get;
            set;
        }

        /// <summary>
        /// The integrated security of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name="integratedSecurity")]
        public string IntegratedSecurity
        {
            get;
            set;
        }

        /// <summary>
        /// The user name of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name="userName")]
        public string UserName
        {
            get;
            set;
        }

        /// <summary>
        /// The password of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name="password")]
        public string Password
        {
            get;
            set;
        }

        /// <summary>
        /// The database kind of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "databaseKind")]
        public DatabaseConnectorKind DatabaseConnectorKind
        {
            get;
            set;
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DatabaseConnector class.
        /// </summary>
        protected DatabaseConnector() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the DatabaseConnector class.
        /// </summary>
        /// <param name="name">The name of this instance.</param>
        /// <param name="namePreffix">The preffix of the name of this instance.</param>
        protected DatabaseConnector(
            string name,
            string namePreffix = null) : base(name, namePreffix)
        {
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Gets the SQL text of the specified query.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the SQL text of the specified query.</returns>
        public string GetSqlText(
            IDbDataQuery query,
            IScriptVariableSet scriptVariableSet,
            ILog log)
        {
            string sqlText = "";

            if (QueryBuilder == null)
                log.AddError("Data builder missing");
            else
                log.Append(QueryBuilder.BuildQuery(query, scriptVariableSet, out sqlText));

            return sqlText;
        }

        /// <summary>
        /// Sets the query builder of this instance.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <returns>Returns the database builder.</returns>
        public abstract void SetQueryBuilder(AppScope appScope);

        /// <summary>
        /// Updates the connection string.
        /// </summary>
        /// <param name="connectionString">The connection string to consider.</param>
        /// <returns>Returns a clone of this instance.</returns>
        public override void UpdateConnectionString(string connectionString = null)
        {
            base.UpdateConnectionString(connectionString);
            DictionaryDataItem item = DictionaryDataItem.Create(connectionString);

            Provider = item.GetContent("Provider").Trim().ToLower();
            DatabaseConnectorKind = GuessDatabaseConnectorKind();
            ServerAddress = item.GetContent("Data Source");
            InitialCatalog = item.GetContent("Initial Catalog");
            UserName = item.GetContent("User Id");
            Password = item.GetContent("Password");
        }

        #endregion

        // ------------------------------------------
        // DATABASE MANAGEMENT
        // ------------------------------------------

        #region Database Management

        /// <summary>
        /// Gets the database connection of this instance.
        /// </summary>
        /// <returns>Returns the connection of this instance.</returns>
        public virtual IDbConnection GetDbConnection()
        {
            return null;
        }

        // ------------------------------------------------

        /// <summary>
        /// Gets the database kind from the specified connection string.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>The database provider  of the specified connection string.</returns>
        public static DatabaseConnectorKind GuessDatabaseConnectorKind(string connectionString)
        {
            connectionString = connectionString.Trim();

            if (connectionString.IndexOf("SQLOLEDB", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return DatabaseConnectorKind.MSSqlServer;
            }
            else if (connectionString.IndexOf("MSDASQL", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return DatabaseConnectorKind.MySQL;
            }
            else if (connectionString.IndexOf("MSDAORA", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return DatabaseConnectorKind.Oracle;
            }
            //else if ((connectionString.ToUpper().Contains("MICROSOFT.JET.OLEDB.4.0")) &
            //    (connectionString.Contains("EXTENDED PROPERTIES=\"EXCEL")))
            //    databaseKind = ConnectorKind_database.Excel;
            //else if ((connectionString.ToUpper().Contains("MICROSOFT.JET.OLEDB.4.0")) &
            //    (connectionString.Contains("EXTENDED PROPERTIES=\"TEXT")))
            //    databaseKind = ConnectorKind_database.TextFiles;
            else if (connectionString.IndexOf("POSTGRESQL", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return DatabaseConnectorKind.PostgreSQL;
            }

            return DatabaseConnectorKind.Any;
        }

        /// <summary>
        /// Guesses the database kind of this instance.
        /// </summary>
        /// <returns>The database kind of this instance.</returns>
        public DatabaseConnectorKind GuessDatabaseConnectorKind()
        {
            return DatabaseConnector.GuessDatabaseConnectorKind(ConnectionString);
        }

        // Execution non query  ---------------------------------------

        /// <summary>
        /// Executes a database data query that returns nothing.
        /// </summary>
        /// <param name="queryText">The text to execute.</param>
        /// <param name="scriptVariableSet">The interpretation variables to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the data query execution task.</returns>
        public virtual void ExecuteNonQuery(
            string queryText,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null)
        {
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
        public virtual void ExecuteQuery(
            string queryText,
            ref DbDataReader dataReader,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null)
        {
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
        public virtual void ExecuteQuery(
            string queryText,
            ref DataSet dataSet,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null)
        {
        }

        // Table ---------------------------------------

        /// <summary>
        /// Gets the identity of the last inserted item
        /// </summary>
        /// <param name="id">The long identifier to populate.</param>
        /// <param name="log">The log to consider.</param>
        public virtual void GetIdentity(
            ref long id,
            ILog log = null)
        {
        }

        /// <summary>
        /// Executes the specified data query and updates the specified data table.
        /// </summary>
        /// <param name="queryText">The text to execute.</param>
        /// <param name="dataTable">The data table to update.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the task.</returns>
        public virtual void UpdateDataTable(
            string queryText,
            DataTable dataTable,
            ILog log = null)
        {
        }

        /// <summary>
        /// Executes the specified data query and updates the specified data table.
        /// </summary>
        /// <param name="queryText">The text to execute.</param>
        /// <param name="dataSet">The data set to update.</param>
        /// <param name="tableNames">The table names to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The log of the task.</returns>
        public virtual void UpdateDataSet(
            string queryText,
            DataSet dataSet,
            List<string> tableNames,
            ILog log = null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connector"></param>
        public void SetConnector(IConnector connector)
        {
        }

        #endregion
    }
}
