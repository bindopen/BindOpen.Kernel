using BindOpen.Application.Scopes;
using BindOpen.Data.Connections;
using BindOpen.Data.Elements;
using BindOpen.Data.Queries;
using BindOpen.Extensions.Runtime;
using BindOpen.System.Diagnostics;
using BindOpen.System.Scripting;
using System.Data;
using System.Data.SqlClient;

namespace BindOpen.Extensions.Connectors
{
    /// <summary>
    /// This class represents a OleDb database connector.
    /// </summary>
    [BdoConnector(Name = "database.mssqlserver$client")]
    public class BdoDbConnector_MSSqlServer : BdoDbConnector
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoDbConnector_MSSqlServer class.
        /// </summary>
        public BdoDbConnector_MSSqlServer() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the BdoDbConnector_MSSqlServer class.
        /// </summary>
        /// <param name="name">The name of this instance.</param>
        /// <param name="connectionString">The connection string to consider.</param>
        public BdoDbConnector_MSSqlServer(
            string name, string connectionString = null) : base(name, connectionString)
        {
        }

        #endregion

        // ------------------------------------------
        // MANAGEMENT
        // ------------------------------------------

        #region Management

        /// <summary>
        /// Updates this instance considering the specified scope.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <returns>Returns the database builder.</returns>
        public override IBdoConnector WithScope(IBdoScope scope)
        {
            _queryBuilder = new DbQueryBuilder_MSSqlServer(scope);

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public override IBdoDbConnection CreateConnection(IBdoLog log = null)
        {
            IBdoDbConnection connection = null;

            if (!Check<BdoDbConnector_MSSqlServer>().AddEventsTo(log, p => p.HasErrorsOrExceptions()).HasErrorsOrExceptions())
            {
                var dbConnection = new SqlConnection(ConnectionString);
                if (dbConnection != null)
                {
                    connection = new BdoDbConnection(this, dbConnection);
                }
            }

            return connection;
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
        public override IDbCommand CreateCommand(
            IDbQuery query,
            DbQueryParameterMode parameterMode,
            IDataElementSet parameterSet = null,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            var command = new SqlCommand(CreateCommandText(query, parameterMode, parameterSet, scriptVariableSet, log));

            if (query.ParameterSet != null)
            {
                foreach (var parameter in query.ParameterSet.Elements)
                {
                    command.Parameters.AddWithValue(parameter.Name, parameter.GetObject());
                }
            }

            return command;
        }

        #endregion

    }
}
