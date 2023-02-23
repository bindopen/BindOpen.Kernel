using BindOpen.Data.Configuration;
using BindOpen.Data.Meta;
using BindOpen.Extensions.Connecting;
using BindOpen.Extensions.Modeling;
using BindOpen.Extensions.Processing;
using BindOpen.Extensions.Scripting;
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
        /// <param key="scope">The scope to consider.</param>
        /// <param key="meta">The meta to consider.</param>
        /// <param key="log">The log to consider.</param>
        /// <param key="varSet">The variable element set to use.</param>
        /// <returns>Returns the created entity.</returns>
        public IBdoEntity CreateEntity(
            IBdoConfiguration meta,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
            => Scope?.CreateEntity(meta, varSet, log);

        // Connectors ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="meta">The meta to consider.</param>
        /// <param key="log">The log to consider.</param>
        /// <param key="varSet">The variable element set to use.</param>
        /// <returns>Returns the created connector.</returns>
        public IBdoConnector CreateConnector(
            IBdoConfiguration meta,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
            => Scope?.CreateConnector(meta, varSet, log);

        // Tasks ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="meta">The meta to consider.</param>
        /// <param key="log">The log to consider.</param>
        /// <param key="varSet">The variable element set to use.</param>
        /// <returns>Returns the created task.</returns>
        public IBdoTask CreateTask(
            IBdoConfiguration meta,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
            => Scope?.CreateTask(meta, varSet, log);

        // Scripting ------------------------------------------------

        /// <summary>
        /// Creates a new script interpreter.
        /// </summary>
        /// <returns>Returns the new script interpreter.</returns>
        public IBdoScriptInterpreter CreateInterpreter()
            => Scope?.CreateInterpreter();
    }
}