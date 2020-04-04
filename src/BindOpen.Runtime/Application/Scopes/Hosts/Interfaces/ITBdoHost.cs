using BindOpen.Application.Services;
using BindOpen.Application.Settings;
using BindOpen.System.Processing;
using System;

namespace BindOpen.Application.Scopes
{
    /// <summary>
    /// The interface defines the bot.
    /// </summary>
    public interface ITBdoHost<S> : ITBdoJob<S>, IBdoHost
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

        // Run ----------------------------------

        /// <summary>
        /// Runs the specified action.
        /// </summary>
        /// <param name="action">The action to consider.</param>
        /// <returns>Returns this instance.</returns>
        ITBdoHost<S> Run(Action<ITBdoHost<S>> action);
    }
}