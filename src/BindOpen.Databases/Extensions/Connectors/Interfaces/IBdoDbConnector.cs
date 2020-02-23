using BindOpen.Data.Connections;
using BindOpen.Data.Elements;
using BindOpen.Data.Queries;
using BindOpen.Extensions.Runtime;
using BindOpen.System.Diagnostics;
using BindOpen.System.Scripting;

namespace BindOpen.Extensions.Connectors
{
    /// <summary>
    /// This class defines a database connector.
    /// </summary>
    public interface IBdoDbConnector : ITBdoConnector<IBdoDbConnection>
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
        string CreateCommandText(
            IDbQuery query,
            DbQueryParameterMode parameterMode = DbQueryParameterMode.ValueInjected,
            IDataElementSet parameterSet = null,
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
