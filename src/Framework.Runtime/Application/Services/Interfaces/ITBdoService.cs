﻿using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.System.Assemblies;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Diagnostics.Loggers;
using BindOpen.Framework.Core.System.Processing;
using BindOpen.Framework.Runtime.Application.Settings;
using System;

namespace BindOpen.Framework.Runtime.Application.Services
{
    /// <summary>
    /// The interface defines the BindOpen service.
    /// </summary>
    public interface ITBdoService<S> : IIdentifiedDataItem
        where S : IBdoSettings, new()
    {
        /// <summary>
        /// The settings.
        /// </summary>
        S Settings { get; set; }

        /// <summary>
        /// The loggers.
        /// </summary>
        IBdoLogger[] Loggers { get; set; }

        // Execution ---------------------------------

        /// <summary>
        /// The application domain pool.
        /// </summary>
        AppDomainPool AppDomainPool { get; }

        /// <summary>
        /// The log.
        /// </summary>
        IBdoLog Log { get; }

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

        /// <summary>
        /// Indicates whether this is successfully loaded.
        /// </summary>
        bool IsLoaded { get; }

        /// <summary>
        /// Indicates that the start of this instance completes.
        /// </summary>
        void StartSucceeds();

        /// <summary>
        /// Indicates that the start of this instance fails.
        /// </summary>
        void StartFails();

        /// <summary>
        /// Indicates that this instance execution succeeds.
        /// </summary>
        void ExecutionSucceedes();

        /// <summary>
        /// Indicates that this instance execution fails.
        /// </summary>
        void ExecutionFails();

        // Process -----------------------------------

        /// <summary>
        /// The state of the current execution.
        /// </summary>
        ProcessExecutionState ExecutionState { get; set; }

        /// <summary>
        /// The status of the current execution.
        /// </summary>
        ProcessExecutionStatus ExecutionStatus { get; set; }

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