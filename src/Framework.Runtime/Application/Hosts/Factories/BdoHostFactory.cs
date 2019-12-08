using BindOpen.Framework.Core.Data.Helpers.Files;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Runtime.Application.Options.Hosts;
using BindOpen.Framework.Runtime.Application.Settings.Hosts;
using System;

namespace BindOpen.Framework.Runtime.Application.Hosts
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
        /// <returns></returns>
        public static IBdoLog InitBindOpenFolders(string rootFolderPath, bool mustRuntimeFolderBeCreated = false, string runtimeFolderPath = null)
        {
            IBdoLog log = new BdoLog();

            if (!string.IsNullOrEmpty(rootFolderPath) || mustRuntimeFolderBeCreated)
            {
                var options = new TBdoHostOptions<BdoDefaultHostSettings>();
                options.SetRootFolder(rootFolderPath);
                options.SetAppSettings(p => { if (runtimeFolderPath != null) { p.SetRuntimeFolder(runtimeFolderPath); } });
                options.Update();

                if (!string.IsNullOrEmpty(rootFolderPath))
                {
                    // we create the application settings file (bindopen.xml)
                    options.AppSettings.Configuration?.SaveXml(options.AppSettingsFilePath, log);
                }

                if (mustRuntimeFolderBeCreated)
                {
                    // we create the default folders
                    FileHelper.CreateDirectory(options.AppSettings.RuntimeFolderPath, log);
                    FileHelper.CreateDirectory(options.AppSettings.ConfigurationFolderPath, log);
                    FileHelper.CreateDirectory(options.AppSettings.LibraryFolderPath, log);
                    FileHelper.CreateDirectory(options.AppSettings.LogsFolderPath, log);
                    FileHelper.CreateDirectory(options.AppSettings.PackagesFolderPath, log);

                    // we create the configuration file (bindopen.config.xml)
                    options.Settings = new BdoDefaultHostSettings();
                    options.Settings.Configuration?.SaveXml(options.AppSettings.ConfigurationFolderPath + BdoDefaultHostPaths.__DefaultConfigurationFileName, log);
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
        public static ITBdoHost<BdoDefaultHostSettings> CreateBindOpenDefaultHost(
            Action<ITBdoHostOptions<BdoDefaultHostSettings>> setupAction = null)
            => CreateBindOpenHost<TBdoHost<BdoDefaultHostSettings>, BdoDefaultHostSettings>(setupAction);

        /// <summary>
        /// Adds a BindOpen host.
        /// </summary>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <returns></returns>
        public static ITBdoHost<S> CreateBindOpenHost<S>(Action<ITBdoHostOptions<S>> setupAction = null)
            where S : class, IBdoHostSettings, new()
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
            where S : class, IBdoHostSettings, new()
        {
            THost host = new THost();
            host.Configure(setupAction);
            host.Start();
            return host;
        }
    }
}