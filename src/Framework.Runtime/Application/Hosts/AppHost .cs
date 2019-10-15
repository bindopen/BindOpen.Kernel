using System;
using System.IO;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Helpers.Serialization;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Extensions;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Processing;
using BindOpen.Framework.Runtime.Application.Options;
using BindOpen.Framework.Runtime.Application.Security;
using BindOpen.Framework.Runtime.Application.Services;
using BindOpen.Framework.Runtime.Application.Settings;

namespace BindOpen.Framework.Runtime.Application.Hosts
{
    /// <summary>
    /// This class represents an application host.
    /// </summary>
    public abstract class AppHost : AppService, IAppHost
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The application settings.
        /// </summary>
        public new IAppHostScope Scope => base.Scope as IAppHostScope;

        /// <summary>
        /// The options of this instance.
        /// </summary>
        public IAppHostOptions Options { get; set; } = null;

        /// <summary>
        /// The settings of this instance.
        /// </summary>
        public new IAppSettings Settings => (base.Settings = Options.Settings) as IAppSettings;

        /// <summary>
        /// The set of user settings of this intance.
        /// </summary>
        public IDataElementSet UserSettingsSet { get; set; } = new DataElementSet();

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the AppHost class.
        /// </summary>
        protected AppHost() : this(null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the AppHost class.
        /// </summary>
        protected AppHost(
            IAppHostScope appScope = null,
            IAppHostOptions options = null,
            IDataElementSet userSettingsSet = null) : base(appScope, options?.Settings)
        {
            Options = options;

            // we initiate the options
            Options?.SetAppFolder(Directory.GetCurrentDirectory());

            Options?.SetExtensions(
                new AppExtensionConfiguration(
                    new AppExtensionFilter("BindOpen.Framework.Runtime"),
                    new AppExtensionFilter("BindOpen.Framework.Runtime")));

            UserSettingsSet = userSettingsSet;
        }

        #endregion

        // ------------------------------------------
        // PROCESSING
        // ------------------------------------------

        #region Processing

        /// <summary>
        /// Configures the application host.
        /// </summary>
        /// <param name="setupOptions">The action to setup the application host.</param>
        /// <returns>Returns the application host.</returns>
        public virtual IAppHost Configure(Action<IAppHostOptions> setupOptions)
        {
            setupOptions?.Invoke(Options);

            return this;
        }

        /// <summary>
        /// Saves settings.
        /// </summary>
        public virtual void SaveSettings()
        {
            String filePath = GetKnownPath(ApplicationPathKind.SettingsFolder) + "appconfig.xml";
            if ((UserSettingsSet != null) && (!string.IsNullOrEmpty(filePath)))
                UserSettingsSet.SaveXml(filePath);
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        // Credentials -----------------------------

        /// <summary>
        /// Get the credential with the specified name.
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns></returns>
        public virtual IApplicationCredential GetCredential(string name)
        {
            IApplicationCredential credential = new ApplicationCredential
            {
                Name = "[unkwnon]"
            };
            return (Options?.Settings)?.AppConfiguration?.Credentials.Find(p => p.KeyEquals(name));
        }

        // Paths --------------------------------------

        /// <summary>
        /// Returns the path of the application temporary folder.
        /// </summary>
        /// <param name="pathKind">The kind of paths.</param>
        /// <returns>The path of the application temporary folder.</returns>
        public virtual String GetKnownPath(ApplicationPathKind pathKind)
        {
            String path = null;
            switch (pathKind)
            {
                case ApplicationPathKind.AppFolder:
                    path = Options?.AppFolderPath;
                    break;
                case ApplicationPathKind.DefaultLogFolder:
                    path = GetKnownPath(ApplicationPathKind.RuntimeFolder) + @"logs\";
                    break;
                case ApplicationPathKind.ExtensionsFolder:
                    path = Options?.Settings?.ExtensionsFolderPath;
                    if (string.IsNullOrEmpty(path))
                        path = GetKnownPath(ApplicationPathKind.RuntimeFolder) + @"extensions\";
                    break;
                case ApplicationPathKind.LibraryFolder:
                    path = Options?.LibraryFolderPath;
                    if (string.IsNullOrEmpty(path))
                        path = GetKnownPath(ApplicationPathKind.AppFolder);
                    break;
                case ApplicationPathKind.LogFolder:
                    path = Options?.Settings?.LogFolderPath;
                    if (string.IsNullOrEmpty(path))
                        path = GetKnownPath(ApplicationPathKind.DefaultLogFolder);
                    break;
                case ApplicationPathKind.RoamingFolder:
                    path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).GetEndedString(@"\");
                    break;
                case ApplicationPathKind.RuntimeFolder:
                    path = Options?.RuntimeFolderPath;
                    if (string.IsNullOrEmpty(path))
                        path = Options?.Settings?.RuntimeFolderPath;
                    break;
                case ApplicationPathKind.SettingsFile:
                    path = Options?.SettingsFilePath;
                    if (string.IsNullOrEmpty(path))
                        path = GetDefaultSettingsFilePath();
                    break;
                case ApplicationPathKind.SettingsFolder:
                    path = GetKnownPath(ApplicationPathKind.RuntimeFolder) + @"settings\";
                    break;
                case ApplicationPathKind.TemporaryFolder:
                    path = GetKnownPath(ApplicationPathKind.RuntimeFolder) + @"temp\";
                    break;
                case ApplicationPathKind.UsersFolder:
                    path = GetKnownPath(ApplicationPathKind.RuntimeFolder) + @"users\";
                    break;
            }

            return (string.IsNullOrEmpty(path) ? StringHelper.__NoneString : path).ToPath();
        }

        private string GetDefaultSettingsFilePath()
        {
            // by default, settings file is {{runtime folder}}\settings\appconfig.xml
            string defaultSettingsFilePath = string.IsNullOrEmpty(Options?.RuntimeFolderPath) ? null : Options?.RuntimeFolderPath + @"settings\".ToPath() + AppHostOptions.__DefaultSettingsFileName;

            if (!File.Exists(defaultSettingsFilePath))
            {
                // by default, settings file is {{runtime folder}}\appconfig.xml
                defaultSettingsFilePath = string.IsNullOrEmpty(Options?.RuntimeFolderPath) ? null : Options?.RuntimeFolderPath + AppHostOptions.__DefaultSettingsFileName;
                if (!File.Exists(defaultSettingsFilePath))
                {
                    // then {{application folder}}\app_data\appconfig.xml
                    defaultSettingsFilePath = string.IsNullOrEmpty(Options?.AppFolderPath) ? null : Options?.AppFolderPath + @"app_data\".ToPath() + AppHostOptions.__DefaultSettingsFileName;
                    if (!File.Exists(defaultSettingsFilePath))
                    {
                        // then {{application folder}}\appconfig.xml
                        defaultSettingsFilePath = Options?.AppFolderPath + AppHostOptions.__DefaultSettingsFileName;
                    }
                }
            }

            return defaultSettingsFilePath;
        }

        #endregion

        // ------------------------------------------
        // PROCESSING
        // ------------------------------------------

        #region Processing

        /// <summary>
        /// Starts the application.
        /// </summary>
        /// <returns>Returns true if this instance is started.</returns>
        public new virtual IAppHost Start(ILog log = null)
        {
            return base.Start(log) as IAppHost;
        }

        /// <summary>
        /// Indicates the application ends.
        /// </summary>
        /// <param name="executionStatus">The execution status to consider.</param>
        public new virtual IAppHost End(ProcessExecutionStatus executionStatus = ProcessExecutionStatus.Stopped)
        {
            return base.End(executionStatus) as IAppHost;
        }

        #endregion
    }
}