using BindOpen.MetaData;
using BindOpen.MetaData.Items;
using BindOpen.MetaData.Stores;
using BindOpen.Logging;
using BindOpen.Runtime.Hosting.Exceptions;
using BindOpen.Runtime.References;
using BindOpen.Runtime.Scopes;
using BindOpen.Runtime.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Runtime.Hosts
{
    /// <summary>
    /// This class represents a host options.
    /// </summary>
    public class BdoHostOptions : BdoItem, IBdoHostOptions
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        // Settings ----------------------

        /// <summary>
        /// The host settings of this instance.
        /// </summary>
        public IBdoHostSettings Settings { get; internal set; } = new BdoHostSettings();

        /// <summary>
        /// The host configuration file path.
        /// </summary>
        public string SettingsFilePath { get; internal set; } = (@".\" + BdoDefaultHostPaths.__DefaultHostConfigFileName).ToPath();

        /// <summary>
        /// Indicates whether the host settings file must exist.
        /// </summary>
        /// <remarks>If it does not exist then an exception is thrown.</remarks>
        public bool? IsSettingsFileRequired { get; internal set; }

        // Paths ----------------------

        /// <summary>
        /// The root folder path.
        /// </summary>
        public string RootFolderPath { get; internal set; }

        /// <summary>
        /// The root folder path.
        /// </summary>
        public List<(Predicate<IBdoHostOptions> Predicate, string RootFolderPath)> RootFolderPathDefinitions { get; internal set; }

        // Extensions ----------------------

        /// <summary>
        /// The extension loading options.
        /// </summary>
        public IBdoAssemblyReferenceCollection ExtensionReferences { get; internal set; } = new BdoAssemblyReferenceCollection();

        /// <summary>
        /// The extension loading options.
        /// </summary>
        public IExtensionLoadOptions ExtensionLoadOptions { get; internal set; }

        // Loggers -------------------------

        /// <summary>
        /// The logger initialization function of this instance.
        /// </summary>
        public Func<IBdoHost, ILogger> LoggerInit { get; internal set; }

        // Trigger actions ----------------------

        /// <summary>
        /// The action that the start of this instance completes.
        /// </summary>
        public Action<IBdoHost> Action_OnStartSuccess { get; internal set; }

        /// <summary>
        /// The action that the start of this instance fails.
        /// </summary>
        public Action<IBdoHost> Action_OnStartFailure { get; internal set; }

        /// <summary>
        /// The action that this instance completes.
        /// </summary>
        public Action<IBdoHost> Action_OnExecutionSucess { get; internal set; }

        /// <summary>
        /// The action that is executed when the instance fails.
        /// </summary>
        public Action<IBdoHost> Action_OnExecutionFailure { get; internal set; }

        // Depot initialization actions ----------------------

        /// <summary>
        /// The data store of this instance.
        /// </summary>
        public IBdoDataStore DataStore { get; internal set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoHostOptions class.
        /// </summary>
        public BdoHostOptions() : base()
        {
            ExtensionLoadOptions = new ExtensionLoadOptions()
                .WithLibraryFolderPath((@".\" + BdoDefaultHostPaths.__DefaultLibraryFolderPath).ToPath())
                .WithSourceKinds(DatasourceKind.Memory, DatasourceKind.Repository);
        }

        #endregion


        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="updateModes">The update modes to consider.</param>
        /// <returns>The log of the operation.</returns>
        /// <remarks>Put reference collections as null if you do not want to repair this instance.</remarks>
        public void Update<T>(
            T item = default,
            string[] specificationAreas = null,
            UpdateModes[] updateModes = null,
            IBdoLog log = null)
        {
            if (specificationAreas == null || specificationAreas.Contains(BdoHostPathKind.RootFolder.ToString()))
            {
                if (string.IsNullOrEmpty(RootFolderPath))
                {
                    RootFolderPath = FileHelper.GetAppRootFolderPath();
                }
                else
                {
                    RootFolderPath = RootFolderPath.GetConcatenatedPath(FileHelper.GetAppRootFolderPath()).EndingWith(@"\").ToPath();
                }
            }

            if (specificationAreas == null)
            {
                SettingsFilePath = SettingsFilePath.GetConcatenatedPath(RootFolderPath).ToPath();

                //Settings?.Update(null, null, log);
                Settings?.WithLibraryFolder(Settings?.LibraryFolderPath.GetConcatenatedPath(RootFolderPath).EndingWith(@"\").ToPath());

                ExtensionLoadOptions?.WithLibraryFolderPath(Settings?.LibraryFolderPath);
            }
        }

        // Paths -------------------------------------------

        /// <summary>
        /// Set the root folder.
        /// </summary>
        /// <param name="predicate">The condition that must be satisfied.</param>
        /// <param name="rootFolderPath">The root folder path.</param>
        /// <returns>Returns the host option.</returns>
        public IBdoHostOptions SetRootFolder(Predicate<IBdoHostOptions> predicate, string rootFolderPath)
        {
            RootFolderPathDefinitions ??= new List<(Predicate<IBdoHostOptions> Predicate, string RootFolderPath)>();
            RootFolderPathDefinitions.Add((predicate, rootFolderPath));

            return this;
        }

        /// <summary>
        /// Sets the specified root folder path.
        /// </summary>
        /// <param name="path">The path to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IBdoHostOptions SetRootFolder(string path)
        {
            RootFolderPath = path?.EndingWith(@"\").ToPath();

            return this;
        }

        // Extensions -------------------------------------------

        /// <summary>
        /// Adds the extensions.
        /// </summary>
        /// <param name="action">The action for adding extensions.</param>
        /// <param name="loadOptionsAction">The action for loading options.</param>
        /// <returns>Returns the host option.</returns>
        public IBdoHostOptions AddExtensions(Action<IBdoAssemblyReferenceCollection> action = null, Action<IExtensionLoadOptions> loadOptionsAction = null)
        {
            if (action != null)
            {
                action?.Invoke(ExtensionReferences);
            }
            else
            {
                ExtensionReferences.AddAllAssemblies();
            }
            loadOptionsAction?.Invoke(ExtensionLoadOptions);

            return this;
        }

        // Settings -------------------------------------------

        /// <summary>
        /// Sets the host settings applying action on it.
        /// </summary>
        /// <param name="hostSettings">The host settings to consider.</param>
        /// <param name="action">The action to apply on the settings.</param>
        public IBdoHostOptions SetSettings(Action<IBdoHostSettings> action = null)
        {
            Settings = new BdoHostSettings();
            action?.Invoke(Settings);

            return this;
        }


        /// <summary>
        /// Set the specified host settings file path.
        /// </summary>
        /// <param name="path">The settings file path.</param>
        /// <param name="isRequired">Indicates whether the host settings file is required.</param>
        /// <returns>Returns the host option.</returns>
        public IBdoHostOptions SetSettingsFile(string path, bool? isRequired = false)
        {
            SettingsFilePath = path?.ToPath();
            return SetSettingsFile(isRequired);
        }

        /// <summary>
        /// Set the specified host settings file path.
        /// </summary>
        /// <param name="path">The settings file path.</param>
        /// <param name="isRequired">Indicates whether the host settings file is required.</param>
        /// <returns>Returns the host option.</returns>
        public IBdoHostOptions SetSettingsFile(bool? isRequired)
        {
            IsSettingsFileRequired = isRequired;

            return this;
        }

        // Logs -------------------------------------------

        /// <summary>
        /// Adds the specified logger.
        /// </summary>
        /// <param name="factory">The logger factory to consider.</param>
        /// <returns>Returns the host option.</returns>
        public IBdoHostOptions SetLogger(ILoggerFactory factory)
        {
            LoggerInit = _ => factory.CreateLogger<IBdoHost>();

            return this;
        }

        /// <summary>
        /// Adds the specified logger.
        /// </summary>
        /// <param name="initLogger">The logger initialization to consider.</param>
        /// <returns>Returns the host option.</returns>
        public IBdoHostOptions SetLogger(Func<IBdoHost, ILogger> initLogger)
        {
            LoggerInit = initLogger;

            return this;
        }

        // Trigger actions -------------------------------------------

        /// <summary>  
        /// The action that is executed when the start of this instance succedes.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        public IBdoHostOptions ExecuteOnStartSuccess(Action<IBdoHost> action)
        {
            Action_OnStartSuccess = new Action<IBdoJob>(p => action?.Invoke(p as IBdoHost));

            return this;
        }

        /// <summary>
        /// The action that is executed when the start of this instance fails.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        public IBdoHostOptions ExecuteOnStartFailure(Action<IBdoHost> action)
        {
            Action_OnStartFailure = new Action<IBdoJob>(p => action?.Invoke(p as IBdoHost));

            return this;
        }

        /// <summary>
        /// The action that is executed when this instance is successfully completed.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        public IBdoHostOptions ExecuteOnExecutionSuccess(Action<IBdoHost> action)
        {
            Action_OnExecutionSucess = new Action<IBdoJob>(p => action?.Invoke(p as IBdoHost));

            return this;
        }

        /// <summary>
        /// The action that is executed when this instance execution fails.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        public IBdoHostOptions ExecuteOnExecutionFailure(Action<IBdoHost> action)
        {
            Action_OnExecutionFailure = new Action<IBdoJob>(p => action?.Invoke(p as IBdoHost));

            return this;
        }

        /// <summary>
        /// Throws an exception when start fails.
        /// </summary>
        public IBdoHostOptions ThrowExceptionOnStartFailure(bool throwException = false)
        {
            Action_OnStartFailure = throwException ?
                (_ => throw new BdoHostLoadException("BindOpen host failed while loading"))
                : null;

            return this;
        }

        /// <summary>
        /// Throws an exception when start fails.
        /// </summary>
        public IBdoHostOptions ThrowExceptionOnExecutionFailure(bool throwException = false)
        {
            Action_OnExecutionFailure = throwException ?
                (_ => throw new BdoHostLoadException("BindOpen host failed while loading"))
                : null;

            return this;
        }

        // Depots -------------------------------------------

        /// <summary>
        /// Adds the data store executing the specified action.
        /// </summary>
        /// <param name="action">The action to execute on the created data store.</param>
        public IBdoHostOptions AddDataStore(Action<IBdoDataStore> action = null)
        {
            DataStore ??= new BdoDataStore();
            action?.Invoke(DataStore);

            return this;
        }

        #endregion

        // ------------------------------------------
        // IDISPOSABLE METHODS
        // ------------------------------------------

        #region IDisposable_Methods

        private bool _isDisposed = false;

        /// <summary>
        /// Disposes this instance. 
        /// </summary>
        /// <param name="isDisposing">Indicates whether this instance is disposing</param>
        protected override void Dispose(bool isDisposing)
        {
            if (_isDisposed)
            {
                return;
            }

            ExtensionLoadOptions?.Dispose();
            DataStore?.Dispose();

            _isDisposed = true;

            base.Dispose(isDisposing);
        }

        #endregion
    }
}