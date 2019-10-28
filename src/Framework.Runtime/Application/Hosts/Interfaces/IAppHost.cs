using System;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Processing;
using BindOpen.Framework.Runtime.Application.Options;
using BindOpen.Framework.Runtime.Application.Services;
using BindOpen.Framework.Runtime.Application.Settings;

namespace BindOpen.Framework.Runtime.Application.Hosts
{
    /// <summary>
    /// The interface defines the base application host.
    /// </summary>
    public interface IAppHost : IAppService
    {
        /// <summary>
        /// The application settings.
        /// </summary>
        new IAppHostScope Scope { get; }

        /// <summary>
        /// The application options.
        /// </summary>
        IAppHostOptions Options { get; set; }

        /// <summary>
        /// The application settings.
        /// </summary>
        new IAppSettings Settings { get; }

        /// <summary>
        /// Get the specified known path.
        /// </summary>
        /// <param name="pathKind">The kind of known path.</param>
        /// <returns>Returns the specified path.</returns>
        string GetKnownPath(ApplicationPathKind pathKind);

        /// <summary>
        /// Configures the application host.
        /// </summary>
        /// <param name="setupOptions">The action to setup the application host.</param>
        /// <returns>Returns the application host.</returns>
        IAppHost Configure(Action<IAppHostOptions> setupOptions);

        // Process ----------------------------------

        /// <summary>
        /// Starts the process.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the application host to consider.</returns>
        new IAppHost Start(ILog log = null);

        /// <summary>
        /// Ends the process specifying the status.
        /// </summary>
        /// <param name="executionStatus">The execution status to apply.</param>
        /// <returns>Returns the application host to consider.</returns>
        new IAppHost End(ProcessExecutionStatus executionStatus = ProcessExecutionStatus.Stopped);

    }
}