using BindOpen.Framework.Core.System.Assemblies;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Processing;

namespace BindOpen.Framework.Runtime.Application.Services
{
    /// <summary>
    /// The interface defines the application service.
    /// </summary>
    public interface IAppService : IScopedService
    {
        // Execution ---------------------------------

        /// <summary>
        /// The application domain pool.
        /// </summary>
        AppDomainPool AppDomainPool { get; }

        /// <summary>
        /// The log.
        /// </summary>
        ILog Log { get; }

        // Load --------------------------------------

        /// <summary>
        /// Indicates whether the load is completed.
        /// </summary>
        bool IsLoadCompleted { get; }

        /// <summary>
        /// Fires the 'LoadComplete' event.
        /// </summary>
        void LoadComplete();

        /// <summary>
        /// This event is triggered when the application is successfully initialized.
        /// </summary>
        event AppService.OnLoadCompletedEventHandler OnLoadCompleted;

        // Process -----------------------------------

        /// <summary>
        /// The state of the current execution.
        /// </summary>
        ProcessExecutionState CurrentExecutionState { get; set; }

        /// <summary>
        /// The status of the current execution.
        /// </summary>
        ProcessExecutionStatus CurrentExecutionStatus { get; set; }
    }
}