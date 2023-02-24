using BindOpen.Data.Configuration;
using BindOpen.Data.Meta;
using BindOpen.Extensions.Connecting;
using BindOpen.Extensions.Modeling;
using BindOpen.Extensions.Processing;
using BindOpen.Logging;

namespace BindOpen.Runtime.Scopes
{
    /// <summary>
    /// This static class provides methods to create extension items.
    /// </summary>
    public static partial class BdoScopeExtensions
    {
        // Entities ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="config">The config to consider.</param>
        /// <param key="log">The log to consider.</param>
        /// <param key="varSet">The variable element set to use.</param>
        /// <typeparam name="T">The entity class to return.</typeparam>
        /// <returns>Returns the created entity.</returns>
        public static T CreateEntity<T>(
            this IBdoScope scope,
            IBdoConfiguration config = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null) where T : BdoEntity
        {
            return scope.CreateEntity(config, varSet, log) as T;
        }

        // Connectors ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="config">The config to consider.</param>
        /// <param key="log">The log to consider.</param>
        /// <param key="varSet">The variable element set to use.</param>
        /// <typeparam name="T">The connector class to return.</typeparam>
        /// <returns>Returns the created connector.</returns>
        public static T CreateConnector<T>(
            this IBdoScope scope,
            IBdoConfiguration config,
            IBdoMetaSet varSet = null,
            IBdoLog log = null) where T : class, IBdoConnector, new()
        {
            return scope.CreateConnector(config, varSet, log) as T;
        }

        // Tasks ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="config">The config to consider.</param>
        /// <param key="log">The log to consider.</param>
        /// <param key="varSet">The variable element set to use.</param>
        /// <typeparam name="T">The entity class to return.</typeparam>
        /// <returns>Returns the created entity.</returns>
        public static T CreateTask<T>(
            this IBdoScope scope,
            IBdoConfiguration config = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null) where T : BdoTask
        {
            return scope.CreateTask(config, varSet, log) as T;
        }
    }
}
