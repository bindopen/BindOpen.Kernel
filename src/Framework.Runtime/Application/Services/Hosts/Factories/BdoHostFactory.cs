using BindOpen.Framework.Application.Options;
using BindOpen.Framework.Application.Settings;
using BindOpen.Framework.Data.Helpers.Files;
using BindOpen.Framework.System.Diagnostics;
using System;

namespace BindOpen.Framework.Application.Scopes
{
    /// <summary>
    /// This static class is a factory for BindOpen hosts.
    /// </summary>
    public static class BdoHostFactory
    {
        // Initializers

        /// <summary>
        /// Initializes the specified runtime folder.
        /// </summary>
        /// <param name="rootFolderPath">The setup action to consider.</param>
        /// <param name="mustRuntimeFolderBeCreated">Indicates whether the runtime folder must be created.</param>
        /// <param name="runtimeFolderPath">The setup action to consider.</param>
        /// <returns>Returns the log.</returns>
        public static IBdoLog InitBindOpenFolders(string rootFolderPath, bool mustRuntimeFolderBeCreated = false, string runtimeFolderPath = null)
        {
            var log = new BdoLog();

            if (!string.IsNullOrEmpty(rootFolderPath) || mustRuntimeFolderBeCreated)
            {
                var options = new TBdoHostOptions<BdoDefaultAppSettings>();
                options.SetRootFolder(rootFolderPath);
                options.SetHostSettings(p => { if (runtimeFolderPath != null) { p.WithRuntimeFolder(runtimeFolderPath); } });
                options.Update();

                if (!string.IsNullOrEmpty(rootFolderPath))
                {
                    // we create the application settings file (bindopen.xml)
                    options.HostSettings.Configuration?.SaveXml(options.HostConfigFilePath, log);
                }

                if (mustRuntimeFolderBeCreated)
                {
                    // we create the default folders
                    FileHelper.CreateDirectory(options.HostSettings.RuntimeFolderPath, log);
                    FileHelper.CreateDirectory(options.HostSettings.AppConfigurationFolderPath, log);
                    FileHelper.CreateDirectory(options.HostSettings.LibraryFolderPath, log);
                    FileHelper.CreateDirectory(options.HostSettings.LogsFolderPath, log);
                    FileHelper.CreateDirectory(options.HostSettings.PackagesFolderPath, log);
                    FileHelper.CreateDirectory(options.HostSettings.ProjectsFolderPath, log);

                    // we create the configuration file (bindopen.config.xml)
                    options.Settings = new BdoDefaultAppSettings();
                    options.Settings.Configuration?.SaveXml(options.HostSettings.AppConfigurationFolderPath + BdoDefaultHostPaths.__DefaultAppConfigFileName, log);
                }
            }

            return log;
        }

        // Factories --------------------------

        /// <summary>
        /// Adds a BindOpen host with default settings.
        /// </summary>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <returns></returns>
        public static BdoDefaultHost CreateBindOpenDefaultHost(
            Action<ITBdoHostOptions<BdoDefaultAppSettings>> setupAction = null)
            => CreateBindOpenHost<BdoDefaultHost, BdoDefaultAppSettings>(setupAction);

        /// <summary>
        /// Adds a BindOpen host.
        /// </summary>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <typeparam name="S"></typeparam>
        /// <returns></returns>
        public static ITBdoHost<S> CreateBindOpenHost<S>(Action<ITBdoHostOptions<S>> setupAction = null)
            where S : class, IBdoAppSettings, new()
            => CreateBindOpenHost<TBdoHost<S>, S>(setupAction);

        /// <summary>
        /// Adds a BindOpen host.
        /// </summary>
        /// <typeparam name="THost">The class of host to consider.</typeparam>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <typeparam name="S"></typeparam>
        /// <returns></returns>
        public static THost CreateBindOpenHost<THost, S>(
            Action<ITBdoHostOptions<S>> setupAction = null)
            where THost : class, ITBdoHost<S>, new()
            where S : class, IBdoAppSettings, new()
        {
            THost host = new THost();
            host.Configure(setupAction);
            host.Start();
            return host;
        }
    }
}