using BindOpen.Framework.System.Processing;
using BindOpen.Framework.Application.Settings;
using System;

namespace BindOpen.Framework.Application.Services
{
    /// <summary>
    /// The interface defines the BindOpen service.
    /// </summary>
    public interface ITBdoService<S> : IBdoService
        where S : IBdoSettings, new()
    {
        /// <summary>
        /// The settings.
        /// </summary>
        S Settings { get; set; }

        // Trigger actions --------------------------------------

        /// <summary>
        /// The action that the start of this instance completes.
        /// </summary>
        Action<ITBdoService<S>> Action_OnStartSuccess { get; set; }

        /// <summary>
        /// The action that the start of this instance fails.
        /// </summary>
        Action<ITBdoService<S>> Action_OnStartFailure { get; set; }

        /// <summary>
        /// The action that this instance completes.
        /// </summary>
        Action<ITBdoService<S>> Action_OnExecutionSucess { get; set; }

        /// <summary>
        /// The action that is executed when the instance fails.
        /// </summary>
        Action<ITBdoService<S>> Action_OnExecutionFailure { get; set; }

        // Process -----------------------------------

        /// <summary>
        /// Starts the service.
        /// </summary>
        /// <returns>Returns the service to consider.</returns>
        ITBdoService<S> Start();

        /// <summary>
        /// Ends the process specifying the status.
        /// </summary>
        /// <param name="executionStatus">The execution status to apply.</param>
        /// <returns>Returns the service to consider.</returns>
        ITBdoService<S> End(ProcessExecutionStatus executionStatus = ProcessExecutionStatus.Stopped);
    }
}