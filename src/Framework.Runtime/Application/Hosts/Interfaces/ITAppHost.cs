using System;
using System.Xml.Schema;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Processing;
using BindOpen.Framework.Core.System.Scripting;
using BindOpen.Framework.Runtime.Application.Configuration;
using BindOpen.Framework.Runtime.Application.Options;
using BindOpen.Framework.Runtime.Application.Security;
using BindOpen.Framework.Runtime.Application.Services;
using BindOpen.Framework.Runtime.Application.Settings;

namespace BindOpen.Framework.Runtime.Application.Hosts
{
    /// <summary>
    /// The interface defines the application host.
    /// </summary>
    public interface ITAppHost<Q> : ITAppService<Q>, IBaseAppHost
        where Q : IAppConfiguration, new()
    {
        // Execution ---------------------------------

        /// <summary>
        /// The application settings.
        /// </summary>
        ITAppSettings<Q> Settings { get; }

        /// <summary>
        /// The application settings.
        /// </summary>
        T GetSettings<T>() where T : TAppSettings<Q>;

        /// <summary>
        /// Configures the application host.
        /// </summary>
        /// <param name="setupOptions">The action to setup the application host.</param>
        /// <returns>Returns the application host.</returns>
        ITAppHost<Q> Configure(Action<ITAppHostOptions<Q>> setupOptions);

        // Process ----------------------------------

        /// <summary>
        /// Starts the process.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the application host to consider.</returns>
        new ITAppHost<Q> Start(ILog log = null);

        /// <summary>
        /// Ends the process specifying the status.
        /// </summary>
        /// <param name="executionStatus">The execution status to apply.</param>
        /// <returns>Returns the application host to consider.</returns>
        new ITAppHost<Q> End(ProcessExecutionStatus executionStatus = ProcessExecutionStatus.Stopped);

        // Settings ----------------------------------

        /// <summary>
        /// The set of user settings.
        /// </summary>
        IDataElementSet UserSettingsSet { get; set; }

        /// <summary>
        /// Gets the specified credential.
        /// </summary>
        /// <param name="name">The name of the credential to consider.</param>
        /// <returns>Returns the specified credential.</returns>
        IApplicationCredential GetCredential(string name);

        /// <summary>
        /// Loads the settings.
        /// </summary>
        /// <param name="filePath">The path of the file.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="log">The log of the option.</param>
        /// <param name="xmlSchemaSet">The XML schema set to consider.</param>
        /// <returns>Returns the application settings.</returns>
        ITAppSettings<Q> LoadSettings(
            string filePath,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null,
            XmlSchemaSet xmlSchemaSet = null);

        /// <summary>
        /// Saves settings.
        /// </summary>
        void SaveSettings();
    }
}