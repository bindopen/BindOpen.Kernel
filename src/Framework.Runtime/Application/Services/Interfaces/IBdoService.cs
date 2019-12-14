using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.System.Assemblies;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Diagnostics.Loggers;
using BindOpen.Framework.Core.System.Processing;
using BindOpen.Framework.Runtime.Application.Settings;
using System;
using System.Collections.Generic;

namespace BindOpen.Framework.Runtime.Application.Services
{
    /// <summary>
    /// The interface defines the BindOpen service.
    /// </summary>
    public interface IBdoService : IIdentifiedDataItem
    {
        /// <summary>
        /// The loggers.
        /// </summary>
        List<IBdoLogger> Loggers { get; set; }

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
    }
}