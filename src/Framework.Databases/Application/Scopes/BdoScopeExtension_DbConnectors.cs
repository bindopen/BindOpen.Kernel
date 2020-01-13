using BindOpen.Framework.Extensions.Runtime;
using BindOpen.Framework.System.Diagnostics;
using BindOpen.Framework.System.Scripting;

namespace BindOpen.Framework.Application.Scopes
{
    /// <summary>
    /// This class represents a BindOpen scope extension.
    /// </summary>
    public static class BdoScopeExtension_DbConnectors
    {
        // Connectors ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="name">The name to consider.</param>
        /// <typeparam name="T">The connector class to return.</typeparam>
        /// <returns>Returns the created connector.</returns>
        public static T CreateDbConnector<T>(
            this IBdoScope scope,
            string name = null) where T : class, IBdoDbConnector, new()
            => scope.CreateDbConnector<T>(name);

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        /// <param name="name">The name to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <typeparam name="T">The connector class to return.</typeparam>
        /// <returns>Returns the created connector.</returns>
        public static T CreateDbConnector<T>(
            this IBdoScope scope,
            IBdoConnectorConfiguration configuration,
            string name = null,
            IBdoLog log = null,
            IBdoScriptVariableSet scriptVariableSet = null) where T : class, IBdoDbConnector, new()
            => scope.CreateDbConnector<T>(configuration, name, log, scriptVariableSet);

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        /// <param name="name">The name to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>Returns the created connector.</returns>
        public static BdoDbConnector CreateDbConnector(
            this IBdoScope scope,
            IBdoConnectorConfiguration configuration = null,
            string name = null,
            IBdoLog log = null,
            IBdoScriptVariableSet scriptVariableSet = null)
            => scope.CreateDbConnector(configuration, name, log, scriptVariableSet);
    }
}
