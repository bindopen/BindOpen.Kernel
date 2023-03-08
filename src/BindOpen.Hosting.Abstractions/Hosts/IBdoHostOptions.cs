using BindOpen.Data;
using BindOpen.Data.Stores;
using BindOpen.Scoping.Scopes;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace BindOpen.Hosting.Hosts
{
    /// <summary>
    /// The interface defines the base BDO host options.
    /// </summary>
    public interface IBdoHostOptions : IBdoItem
    {
        // Paths ----------------------

        /// <summary>
        /// The root folder path.
        /// </summary>
        string RootFolderPath { get; }

        // -------

        /// <summary>
        /// The root folder path.
        /// </summary>
        List<(Predicate<IBdoHostOptions> Predicate, string RootFolderPath)> RootFolderPathDefinitions { get; }

        /// <summary>
        /// Set the root folder.
        /// </summary>
        /// <param key="predicate">The condition that muse be satistfied.</param>
        /// <param key="rootFolderPath">The root folder path to consider.</param>
        /// <returns>Returns the host option.</returns>
        IBdoHostOptions SetRootFolder(Predicate<IBdoHostOptions> predicate, string rootFolderPath);

        /// <summary>
        /// Set the root folder.
        /// </summary>
        /// <param key="rootFolderPath">The root folder path to consider.</param>
        /// <returns>Returns the host option.</returns>
        IBdoHostOptions SetRootFolder(string rootFolderPath);

        // Extensions ----------------------

        /// <summary>
        /// The extension load options.
        /// </summary>
        IExtensionLoadOptions ExtensionLoadOptions { get; }

        // Depots ----------------------

        /// <summary>
        /// The depot sets of this instance.
        /// </summary>
        IBdoDataStore DataStore { get; }

        // -------

        /// <summary>
        /// Adds the data store executing the specified action.
        /// </summary>
        IBdoHostOptions AddDataStore(Action<IBdoDataStore> action = null);

        // Settings ----------------------

        /// <summary>
        /// The settings.
        /// </summary>
        IBdoHostSettings Settings { get; }

        /// <summary>
        /// The settings file path of this instance.
        /// </summary>
        string SettingsFilePath { get; }

        /// <summary>
        /// Indicates whether the host settings file must exist.
        /// </summary>
        /// <remarks>If it does not exist then an exception is thrown.</remarks>
        bool? IsSettingsFileRequired { get; }

        // -------

        /// <summary>
        /// Set the host settings file.
        /// </summary>
        /// <param key="filePath">The host settings file path.</param>
        /// <param key="isRequired">Indicates whether the host settings file is required.</param>
        /// <returns>Returns the host option.</returns>
        IBdoHostOptions SetSettingsFile(string filePath, bool? isRequired = false);

        /// <summary>
        /// Set the host settings file.
        /// </summary>
        /// <param key="isRequired">Indicates whether the host settings file is required.</param>
        /// <returns>Returns the host option.</returns>
        IBdoHostOptions SetSettingsFile(bool? isRequired);

        /// <summary>
        /// Sets the host settings applying action on it.
        /// </summary>
        /// <param key="action">The action to apply on the settings.</param>
        IBdoHostOptions SetSettings(Action<IBdoHostSettings> action = null);

        // Loggers ----------------------

        /// <summary>
        /// The logger of this instance.
        /// </summary>
        public Func<IBdoHost, ILogger> LoggerInit { get; }

        // -------

        /// <summary>
        /// Adds the specified logger.
        /// </summary>
        /// <param key="loggerFactory">The logger factory to consider.</param>
        /// <returns>Returns the host option.</returns>
        IBdoHostOptions SetLogger(ILoggerFactory loggerFactory);

        /// <summary>
        /// Adds the specified logger.
        /// </summary>
        /// <param key="initLogger">The logger initialization to consider.</param>
        /// <returns>Returns the host option.</returns>
        IBdoHostOptions SetLogger(Func<IBdoHost, ILogger> initLogger);

        // Trigger actions -------------------------------------------

        /// <summary>
        /// The action that the start of this instance completes.
        /// </summary>
        public Action<IBdoHost> Action_OnStartSuccess { get; }

        /// <summary>
        /// The action that the start of this instance fails.
        /// </summary>
        public Action<IBdoHost> Action_OnStartFailure { get; }

        /// <summary>
        /// The action that this instance completes.
        /// </summary>
        public Action<IBdoHost> Action_OnExecutionSucess { get; }

        /// <summary>
        /// The action that is executed when the instance fails.
        /// </summary>
        public Action<IBdoHost> Action_OnExecutionFailure { get; }

        // -------

        /// <summary>  
        /// The action that is executed when start succeeds.
        /// </summary>
        /// <param key="action">The action to execute.</param>
        IBdoHostOptions ExecuteOnStartSuccess(Action<IBdoHost> action);

        /// <summary>
        /// The action that is executed when start fails.
        /// </summary>
        /// <param key="action">The action to execute.</param>
        IBdoHostOptions ExecuteOnStartFailure(Action<IBdoHost> action);

        /// <summary>
        /// The action that is executed when execution succeeds.
        /// </summary>
        /// <param key="action">The action to execute.</param>
        IBdoHostOptions ExecuteOnExecutionSuccess(Action<IBdoHost> action);

        /// <summary>
        /// The action that is executed when execution fails.
        /// </summary>
        /// <param key="action">The action to execute.</param>
        IBdoHostOptions ExecuteOnExecutionFailure(Action<IBdoHost> action);

        /// <summary>
        /// Throws an exception when start fails.
        /// </summary>
        IBdoHostOptions ThrowExceptionOnStartFailure(bool throwException = false);

        /// <summary>
        /// Throws an exception when execution fails.
        /// </summary>
        IBdoHostOptions ThrowExceptionOnExecutionFailure(bool throwException = false);
    }
}