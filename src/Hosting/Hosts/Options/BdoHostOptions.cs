﻿using BindOpen.System.Data;
using BindOpen.System.Data.Helpers;
using BindOpen.System.Data.Stores;
using BindOpen.System.Scoping;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace BindOpen.System.Hosting.Hosts
{
    /// <summary>
    /// This class represents a host options.
    /// </summary>
    public class BdoHostOptions : BdoObject, IBdoHostOptions
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        // Settings ----------------------

        /// <summary>
        /// The host settings of this instance.
        /// </summary>
        public IBdoHostSettings Settings { get; set; } = new BdoHostSettings();

        /// <summary>
        /// The host config file path.
        /// </summary>
        public string SettingsFilePath { get; set; } = (@".\" + BdoDefaultHostPaths.__DefaultHostConfigFileName).ToPath();

        /// <summary>
        /// Indicates whether the host settings file must exist.
        /// </summary>
        /// <remarks>If it does not exist then an exception is thrown.</remarks>
        public bool? IsSettingsFileRequired { get; set; }

        // Paths ----------------------

        /// <summary>
        /// The root folder path.
        /// </summary>
        public string RootFolderPath { get; set; }

        /// <summary>
        /// The root folder path.
        /// </summary>
        public List<(Predicate<IBdoHostOptions> Predicate, string RootFolderPath)> RootFolderPathDefinitions { get; set; }

        // Extensions ----------------------

        /// <summary>
        /// The extension loading options.
        /// </summary>
        public IExtensionLoadOptions ExtensionLoadOptions { get; set; }

        // Loggers -------------------------

        /// <summary>
        /// The logger initialization function of this instance.
        /// </summary>
        public Func<IBdoHost, ILogger> LoggerInit { get; set; }

        // Trigger actions ----------------------

        /// <summary>
        /// The action that the start of this instance completes.
        /// </summary>
        public Action<IBdoHost> Action_OnStartSuccess { get; set; }

        /// <summary>
        /// The action that the start of this instance fails.
        /// </summary>
        public Action<IBdoHost> Action_OnStartFailure { get; set; }

        /// <summary>
        /// The action that this instance completes.
        /// </summary>
        public Action<IBdoHost> Action_OnExecutionSucess { get; set; }

        /// <summary>
        /// The action that is executed when the instance fails.
        /// </summary>
        public Action<IBdoHost> Action_OnExecutionFailure { get; set; }

        // Depot initialization actions ----------------------

        /// <summary>
        /// The data store of this instance.
        /// </summary>
        public IBdoDataStore DataStore { get; set; }

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
                .AddSource(DatasourceKind.Memory)
                .AddSource(DatasourceKind.Repository, (@".\" + BdoDefaultHostPaths.__DefaultLibraryFolderPath).ToPath());
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
        /// <param key="isDisposing">Indicates whether this instance is disposing</param>
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