using BindOpen.Framework.Application.Scopes;
using BindOpen.Framework.Data.Connections;
using BindOpen.Framework.Data.Elements;
using BindOpen.Framework.Data.Queries;
using BindOpen.Framework.System.Diagnostics;
using BindOpen.Framework.System.Scripting;
using System.Data;

namespace BindOpen.Framework.Extensions.Runtime
{
    /// <summary>
    /// This class defines a database connector.
    /// </summary>
    public interface IBdoDbConnector : IBdoConnector
    {
        // -----------------------------------------------
        // PROPERTIES
        // -----------------------------------------------

        #region Properties

        /// <summary>
        /// The provider of this instance.
        /// </summary>
        string Provider { get; set; }

        /// <summary>
        /// The server address of this instance.
        /// </summary>
        string ServerAddress { get; set; }

        /// <summary>
        /// The initial catalog of this instance.
        /// </summary>
        string InitialCatalog { get; set; }

        /// <summary>
        /// The integrated security of this instance.
        /// </summary>
        string IntegratedSecurity { get; set; }

        /// <summary>
        /// The user name of this instance.
        /// </summary>
        string UserName { get; set; }

        /// <summary>
        /// The password of this instance.
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// The database kind of this instance.
        /// </summary>
        BdoDbConnectorKind DatabaseConnectorKind { get; set; }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Updates the connection string with the specified string.
        /// </summary>
        /// <param name="connectionString">The connection string to consider.</param>
        new IBdoConnector WithConnectionString(string connectionString = null);

        /// <summary>
        /// Updates the instance considering the specified scope.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        new IBdoConnector WithScope(IBdoScope scope);

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Creates a new database connection.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        new IBdoDbConnection CreateConnection(IBdoLog log = null);

        // SQL commands

        /// <summary>
        /// Gets the SQL text of the specified query.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the SQL text of the specified query.</returns>
        string CreateCommandText(
            IDbQuery query,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null);

        /// <summary>
        /// Gets the SQL text of the specified query.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="isParametersInjected">Indicates whether parameters are replaced.</param>
        /// <returns>Returns the SQL text of the specified query.</returns>
        string CreateCommandText(
            IDbQuery query,
            bool isParametersInjected = false,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null);

        /// <summary>
        /// Gets the SQL text of the specified query.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <param name="parameterSet">The parameter elements to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="isParametersInjected">Indicates whether parameters are replaced.</param>
        /// <returns>Returns the SQL text of the specified query.</returns>
        string CreateCommandText(
            IDbQuery query,
            IDataElementSet parameterSet,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null);

        /// <summary>
        /// Gets the SQL text of the specified query.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <param name="parameterSet">The parameter set to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="isParametersInjected">Indicates whether parameters are replaced.</param>
        /// <returns>Returns the SQL text of the specified query.</returns>
        string CreateCommandText(
            IDbQuery query,
            IDataElementSet parameterSet,
            bool isParametersInjected = false,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null);

        /// <summary>
        /// Creates a command from the specified query.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the database command.</returns>
        IDbCommand CreateCommand(
            IDbQuery query,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null);

        /// <summary>
        /// Creates a command from the specified query.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <param name="parameterSet">The parameter elements to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the database command.</returns>
        IDbCommand CreateCommand(
            IDbQuery query,
            IDataElementSet parameterSet,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null);

        #endregion

        // ------------------------------------------
        // DATABASE MANAGEMENT
        // ------------------------------------------

        #region Database Management

        /// <summary>
        /// Estimates the kind of the database connector of this instance.
        /// </summary>
        /// <returns>The database connector kind of this instance.</returns>
        BdoDbConnectorKind EstimateDbConnectorKind();

        #endregion
    }
}
