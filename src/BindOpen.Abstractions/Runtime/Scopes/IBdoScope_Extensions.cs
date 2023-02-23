using BindOpen.Data.Configuration;
using BindOpen.Data.Meta;
using BindOpen.Extensions.Connecting;
using BindOpen.Extensions.Modeling;
using BindOpen.Extensions.Processing;
using BindOpen.Extensions.Scripting;
using BindOpen.Logging;
using System;

namespace BindOpen.Runtime.Scopes
{
    /// <summary>
    /// This interface defines an application 
    /// </summary>
    public partial interface IBdoScope : IDisposable
    {
        // Modeling ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="config">The config to consider.</param>
        /// <param key="log">The log to consider.</param>
        /// <param key="varSet">The variable element set to use.</param>
        /// <returns>Returns the created entity.</returns>
        IBdoEntity CreateEntity(
            IBdoConfiguration config,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);

        // Connecting ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="config">The config to consider.</param>
        /// <param key="log">The log to consider.</param>
        /// <param key="varSet">The variable element set to use.</param>
        /// <returns>Returns the created connector.</returns>
        IBdoConnector CreateConnector(
            IBdoConfiguration config,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);

        // Processing ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="config">The config to consider.</param>
        /// <param key="log">The log to consider.</param>
        /// <param key="varSet">The variable element set to use.</param>
        /// <returns>Returns the created task.</returns>
        IBdoTask CreateTask(
            IBdoConfiguration config = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);

        // Scripting ------------------------------------------------

        /// <summary>
        /// Creates a new script interpreter.
        /// </summary>
        /// <returns>Returns the new script interpreter.</returns>
        IBdoScriptInterpreter CreateInterpreter();
    }
}

