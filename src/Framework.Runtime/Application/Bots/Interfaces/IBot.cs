using System;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Processing;
using BindOpen.Framework.Runtime.Application.Options;
using BindOpen.Framework.Runtime.Application.Services;
using BindOpen.Framework.Runtime.Application.Settings;

namespace BindOpen.Framework.Runtime.Application.Bots
{
    /// <summary>
    /// The interface defines the base bot.
    /// </summary>
    public interface IBot : IBotService
    {
        /// <summary>
        /// The application settings.
        /// </summary>
        new IBotScope Scope { get; }

        /// <summary>
        /// The application options.
        /// </summary>
        IBotOptions Options { get; set; }

        /// <summary>
        /// The application settings.
        /// </summary>
        new IBotSettings Settings { get; }

        /// <summary>
        /// Get the specified known path.
        /// </summary>
        /// <param name="pathKind">The kind of known path.</param>
        /// <returns>Returns the specified path.</returns>
        string GetKnownPath(ApplicationPathKind pathKind);

        /// <summary>
        /// Configures the bot.
        /// </summary>
        /// <param name="setupOptions">The action to setup the bot.</param>
        /// <returns>Returns the bot.</returns>
        IBot Configure(Action<IBotOptions> setupOptions);

        // Process ----------------------------------

        /// <summary>
        /// Starts the process.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the bot to consider.</returns>
        new IBot Start(ILog log = null);

        /// <summary>
        /// Ends the process specifying the status.
        /// </summary>
        /// <param name="executionStatus">The execution status to apply.</param>
        /// <returns>Returns the bot to consider.</returns>
        new IBot End(ProcessExecutionStatus executionStatus = ProcessExecutionStatus.Stopped);

    }
}