using BindOpen.Data;
using BindOpen.Hosting.Services;
using BindOpen.Scopes.Scopes;
using System;

namespace BindOpen.Hosting.Hosts
{
    /// <summary>
    /// The interface defines the base bot.
    /// </summary>
    public interface IBdoHost : ITBdoJob<IBdoHost>, IBdoScope
    {
        /// <summary>
        /// Get the specified known path.
        /// </summary>
        /// <param key="pathKind">The kind of known path.</param>
        /// <returns>Returns the specified path.</returns>
        string GetKnownPath(BdoHostPathKind pathKind);

        // Options ---------------------------------

        /// <summary>
        /// The options.
        /// </summary>
        IBdoHostOptions Options { get; }

        /// <summary>
        /// Configures the bot.
        /// </summary>
        /// <param key="setupOptions">The action to setup the bot.</param>
        /// <returns>Returns the bot.</returns>
        IBdoHost Configure(Action<IBdoHostOptions> setupOptions);

        /// <summary>
        /// Sets the specfied options
        /// </summary>
        /// <param key="options"></param>
        /// <returns>Returns this instance.</returns>
        IBdoHost WithOptions(IBdoHostOptions options);

        // Process ----------------------------------

        /// <summary>
        /// Starts the process.
        /// </summary>
        /// <returns>Returns the bot to consider.</returns>
        new IBdoHost Start();

        /// <summary>
        /// Ends the process specifying the status.
        /// </summary>
        /// <param key="executionStatus">The execution status to apply.</param>
        /// <returns>Returns the bot to consider.</returns>
        new IBdoHost End(ProcessExecutionStatus executionStatus = ProcessExecutionStatus.Stopped);

        // Run ----------------------------------

        /// <summary>
        /// Runs the specified action.
        /// </summary>
        /// <param key="action">The action to consider.</param>
        /// <returns>Returns this instance.</returns>
        IBdoHost Run(Action<IBdoHost> action);
    }
}