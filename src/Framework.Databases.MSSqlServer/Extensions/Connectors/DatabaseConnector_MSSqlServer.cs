using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.Extensions.Runtime.Items;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;
using BindOpen.Framework.Databases.Data.Connections;
using BindOpen.Framework.Databases.Data.Queries;
using BindOpen.Framework.Databases.Extensions.Connectors;
using BindOpen.Framework.Databases.MSSqlServer.Data.Queries.Builders;
using System.Data;
using System.Data.SqlClient;

namespace BindOpen.Framework.Databases.MSSqlServer.Extensions.Connectors
{
    /// <summary>
    /// This class represents a OleDb database connector.
    /// </summary>
    [BdoConnector(Name = "database.mssqlserver$client")]
    public class DatabaseConnector_MSSqlServer : BdoDbConnector
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DatabaseConnector_MSSqlServer class.
        /// </summary>
        public DatabaseConnector_MSSqlServer() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the DatabaseConnector_MSSqlServer class.
        /// </summary>
        /// <param name="name">The name of this instance.</param>
        /// <param name="connectionString">The connection string to consider.</param>
        public DatabaseConnector_MSSqlServer(
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
            QueryBuilder = new DbQueryBuilder_MSSqlServer(scope);

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

            if (!log.Append(Check<DatabaseConnector_MSSqlServer>(), p => p.HasErrorsOrExceptions()).HasErrorsOrExceptions())
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
        /// <param name="parameterSet">The parameter elements to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the database command.</returns>
        public override IDbCommand CreateCommand(
            IDbQuery query,
            IDataElementSet parameterSet,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            var command = new SqlCommand();

            if (query.ParameterSet != null)
            {
                foreach (var parameter in query.ParameterSet.Elements)
                {
                    command.Parameters.AddWithValue(parameter.Name, parameter.GetObject());
                }
            }

            return new SqlCommand();
        }

        #endregion

    }
}
