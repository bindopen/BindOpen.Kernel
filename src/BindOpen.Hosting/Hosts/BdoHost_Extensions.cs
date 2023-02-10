using BindOpen.Data.Configuration;
using BindOpen.Data.Meta;
using BindOpen.Extensions.Connecting;
using BindOpen.Extensions.Modeling;
using BindOpen.Extensions.Processing;
using BindOpen.Hosting.Services;
using BindOpen.Logging;

namespace BindOpen.Hosting.Hosts
{
    /// <summary>
    /// This class represents a host.
    /// </summary>
    public partial class BdoHost : BdoJob, IBdoHost
    {
        // Entities ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="config">The config to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <returns>Returns the created entity.</returns>
        public IBdoEntity CreateEntity(
            IBdoConfiguration config,
            IBdoMetaList varSet = null,
            IBdoLog log = null)
            => Scope?.CreateEntity(config, varSet, log);

        // Connectors ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="config">The config to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <returns>Returns the created connector.</returns>
        public IBdoConnector CreateConnector(
            IBdoConfiguration config,
            IBdoMetaList varSet = null,
            IBdoLog log = null)
            => Scope?.CreateConnector(config, varSet, log);

        // Tasks ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="config">The config to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <returns>Returns the created task.</returns>
        public IBdoTask CreateTask(
            IBdoConfiguration config = null,
            IBdoMetaList varSet = null,
            IBdoLog log = null)
            => Scope?.CreateTask(config, varSet, log);
    }
}