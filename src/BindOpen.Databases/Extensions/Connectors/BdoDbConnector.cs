using BindOpen.Application.Scopes;
using BindOpen.Data.Connections;
using BindOpen.Data.Elements;
using BindOpen.Data.Items;
using BindOpen.Data.Queries;
using BindOpen.Extensions.Runtime;
using BindOpen.System.Diagnostics;
using BindOpen.System.Scripting;
using System;
using System.Data;
using System.Xml.Serialization;

namespace BindOpen.Extensions.Connectors
{
    /// <summary>
    /// This class defines a database connector.
    /// </summary>
    public abstract class BdoDbConnector : TBdoConnector<IBdoDbConnection>, IBdoDbConnector
    {
        /// <summary>
        /// The query builder of this instance.
        /// </summary>
        protected DbQueryBuilder _queryBuilder;

        // -----------------------------------------------
        // PROPERTIES
        // -----------------------------------------------

        #region Properties

        /// <summary>
        /// The provider of this instance.
        /// </summary>
        [XmlIgnore()]
        protected DbQueryBuilder QueryBuilder => _queryBuilder;

        /// <summary>
        /// The provider of this instance.
        /// </summary>
        [DetailProperty(Name = "provider")]
        public string Provider
        {
            get;
            set;
        }

        /// <summary>
        /// The server address of this instance.
        /// </summary>
        [DetailProperty(Name = "serverAddress")]
        public string ServerAddress
        {
            get;
            set;
        }

        /// <summary>
        /// The initial catalog of this instance.
        /// </summary>
        [DetailProperty(Name = "initialCatalog")]
        public string InitialCatalog
        {
            get;
            set;
        }

        /// <summary>
        /// The integrated security of this instance.
        /// </summary>
        [DetailProperty(Name = "integratedSecurity")]
        public string IntegratedSecurity
        {
            get;
            set;
        }

        /// <summary>
        /// The user name of this instance.
        /// </summary>
        [DetailProperty(Name = "userName")]
        public string UserName
        {
            get;
            set;
        }

        /// <summary>
        /// The password of this instance.
        /// </summary>
        [DetailProperty(Name = "password")]
        public string Password
        {
            get;
            set;
        }

        /// <summary>
        /// The database kind of this instance.
        /// </summary>
        [DetailProperty(Name = "databaseKind")]
        public BdoDbConnectorKind DatabaseConnectorKind
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
        protected BdoDbConnector() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the DatabaseConnector class.
        /// </summary>
        /// <param name="name">The name of this instance.</param>
        /// <param name="connectionString">The connection string to consider.</param>
        protected BdoDbConnector(
            string name, string connectionString = null) : base(name, connectionString)
        {
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Updates the connection string with the specified string.
        /// </summary>
        /// <param name="connectionString">The connection string to consider.</param>
        public override IBdoConnector WithConnectionString(string connectionString = null)
        {
            base.WithConnectionString(connectionString);
            DictionaryDataItem item = DictionaryDataItem.Create(connectionString);

            Provider = item.GetContent("Provider").Trim().ToLower();
            DatabaseConnectorKind = EstimateDbConnectorKind();
            ServerAddress = item.GetContent("Data Source");
            InitialCatalog = item.GetContent("Initial Catalog");
            UserName = item.GetContent("User Id");
            Password = item.GetContent("Password");

            return this;
        }

        /// <summary>
        /// Updates the instance considering the specified scope.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        public override IBdoConnector WithScope(IBdoScope scope)
        {
            base.WithScope(scope);

            return this;
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        // SQL commands

        /// <summary>
        /// Gets the SQL text of the specified query.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <param name="parameterMode">Indicates whether parameters are replaced.</param>
        /// <param name="parameterSet">The parameter set to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the SQL text of the specified query.</returns>
        public string CreateCommandText(
            IDbQuery query,
            DbQueryParameterMode parameterMode = DbQueryParameterMode.ValueInjected,
            IDataElementSet parameterSet = null,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            string sqlText = "";

            if (QueryBuilder == null)
                log?.AddError("Data builder missing");
            else
                sqlText = QueryBuilder.BuildQuery(query, parameterMode, parameterSet, scriptVariableSet, log);

            return sqlText;
        }

        /// <summary>
        /// Creates a command from the specified query.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <param name="parameterMode">Indicates whether parameters are replaced.</param>
        /// <param name="parameterSet">The parameter elements to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the database command.</returns>
        public abstract IDbCommand CreateCommand(
            IDbQuery query,
            DbQueryParameterMode parameterMode,
            IDataElementSet parameterSet = null,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null);

        #endregion

        // ------------------------------------------
        // DATABASE MANAGEMENT
        // ------------------------------------------

        #region Database Management

        // ------------------------------------------------

        /// <summary>
        /// Estimates the database connector kind from the specified connection string.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>The database provider  of the specified connection string.</returns>
        public static BdoDbConnectorKind EstimateDbConnectorKind(string connectionString)
        {
            if (connectionString != null)
            {
                connectionString = connectionString.Trim();

                if (connectionString.IndexOf("SQLOLEDB", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return BdoDbConnectorKind.MSSqlServer;
                }
                else if (connectionString.IndexOf("MSDASQL", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return BdoDbConnectorKind.MySQL;
                }
                else if (connectionString.IndexOf("MSDAORA", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return BdoDbConnectorKind.Oracle;
                }
                else if (connectionString.IndexOf("POSTGRESQL", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return BdoDbConnectorKind.PostgreSql;
                }
            }

            return BdoDbConnectorKind.Any;
        }

        /// <summary>
        /// Estimates the database connector kind of this instance.
        /// </summary>
        /// <returns>The database connector kind of this instance.</returns>
        public BdoDbConnectorKind EstimateDbConnectorKind()
        {
            return BdoDbConnector.EstimateDbConnectorKind(ConnectionString);
        }

        #endregion
    }
}
