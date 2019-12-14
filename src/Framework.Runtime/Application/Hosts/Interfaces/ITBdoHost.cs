using BindOpen.Framework.Core.System.Processing;
using BindOpen.Framework.Runtime.Application.Options;
using BindOpen.Framework.Runtime.Application.Services;
using BindOpen.Framework.Runtime.Application.Settings;
using System;

namespace BindOpen.Framework.Runtime.Application.Hosts
{
    /// <summary>
    /// The interface defines the bot.
    /// </summary>
    public interface ITBdoHost<S> : ITBdoService<S>, IBdoHost
        where S : class, IBdoAppSettings, new()
    {
        // Options ---------------------------------

        /// <summary>
        /// The options.
        /// </summary>
        ITBdoHostOptions<S> Options { get; set; }

        /// <summary>
        /// Configures the bot.
        /// </summary>
        /// <param name="setupOptions">The action to setup the bot.</param>
        /// <returns>Returns the bot.</returns>
        ITBdoHost<S> Configure(Action<ITBdoHostOptions<S>> setupOptions);

        // Process ----------------------------------

        /// <summary>
        /// Starts the process.
        /// </summary>
        /// <returns>Returns the bot to consider.</returns>
        new ITBdoHost<S> Start();

        /// <summary>
        /// Ends the process specifying the status.
        /// </summary>
        /// <param name="executionStatus">The execution status to apply.</param>
        /// <returns>Returns the bot to consider.</returns>
        new ITBdoHost<S> End(ProcessExecutionStatus executionStatus = ProcessExecutionStatus.Stopped);
    }
}