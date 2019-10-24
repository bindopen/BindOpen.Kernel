using System.ComponentModel;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;
using BindOpen.Framework.Runtime.Application.Configuration;
using BindOpen.Framework.Runtime.Application.Modules;
using BindOpen.Framework.Runtime.System;

namespace BindOpen.Framework.Runtime.Application.Settings
{
    /// <summary>
    /// This class represents a BDO application settings.
    /// </summary>
    public class TAppSettings<Q> : TSettings<Q>, ITAppSettings<Q>
        where Q : class, IAppConfiguration, new()
    {
        // -------------------------------------------------------
        // PROPERTIES
        // -------------------------------------------------------

        #region Properties

        // Elements ---------------------------

        /// <summary>
        /// The application instance kind of this instance.
        /// </summary>
        [XmlIgnore()]
        [DefaultValue(ApplicationInstanceKind.Autonomous)]
        [DetailProperty(Name = "applicationInstanceKind")]
        public ApplicationInstanceKind ApplicationInstanceKind { get; set; }

        /// <summary>
        /// Application instance ID of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "applicationInstanceId")]
        public string ApplicationInstanceId { get; set; }

        /// <summary>
        /// Default current UI culture name of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "defaultUICultureName")]
        public string DefaultUICultureName { get; set; }

        /// <summary>
        /// Default theme of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "defaultTheme")]
        public string DefaultTheme { get; set; }

        /// <summary>
        /// Cookie domain name of this instance.
        /// </summary>
        /// <example>toto.com, titi.com...</example>
        [XmlIgnore()]
        [DetailProperty(Name = "cookieDomain")]
        public string CookieDomain { get; set; }

        // Execution ----------------------

        /// <summary>
        /// Runtime mode of this instance.
        /// </summary>
        [XmlIgnore()]
        [DefaultValue(RuntimeMode.Normal)]
        [DetailProperty(Name = "currentRuntimeMode")]
        public RuntimeMode CurrentRuntimeMode { get; set; }

        /// <summary>
        /// Execution level of this instance.
        /// </summary>
        [XmlIgnore()]
        [DefaultValue(ApplicationExecutionLevel.PROD)]
        [DetailProperty(Name = "executionLevel")]
        public ApplicationExecutionLevel ExecutionLevel { get; set; }

        // Platform data ----------------------

        /// <summary>
        /// Name of the platform server instance of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "serverInstanceName")]
        public string ServerInstanceName { get; set; }

        /// <summary>
        /// Name of the application instance of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "applicationInstanceName")]
        public string ApplicationInstanceName { get; set; }

        // Tracking ----------------------

        /// <summary>
        /// Indicates whether user tracking is enabled.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "isUserTrackingEnabled")]
        public bool? IsUserTrackingEnabled { get; set; }

        // Folders ---------------

        /// <summary>
        /// The extensions folder path of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "extensions.folderPath")]
        public string ExtensionsFolderPath { get; set; }

        /// <summary>
        /// The path of the log folder.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "log.folderPath")]
        public string LogFolderPath { get; set; }

        /// <summary>
        /// The path of the runtime folder.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "runtime.folderPath")]
        public string RuntimeFolderPath { get; set; }

        /// <summary>
        /// The application configuration of this instance.
        /// </summary>
        public IAppConfiguration AppConfiguration => Configuration as IAppConfiguration;

        #endregion

        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TAppSettings class.
        /// </summary>
        public TAppSettings()
            : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the TAppSettings class.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        public TAppSettings(IAppScope appScope, Q configuration)
            : base(appScope, configuration)
        {
        }

        #endregion

        // --------------------------------------------------
        // SERIALIZATION
        // --------------------------------------------------

        #region Serialization

        /// <summary>
        /// Updates information for storage.
        /// </summary>
        /// <param name="log">The log to update.</param>
        public override void UpdateStorageInfo(ILog log = null)
        {
            Configuration?.UpdateFromObject<DetailPropertyAttribute>(this);
            Configuration?.UpdateStorageInfo(log);
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The set of script variables to consider.</param>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IAppScope appScope = null, IScriptVariableSet scriptVariableSet = null, ILog log = null)
        {
            if (Configuration != null)
            {
                Configuration.UpdateRuntimeInfo(appScope, scriptVariableSet, log);
                this.UpdateFromElementSet<DetailPropertyAttribute>(Configuration, appScope, scriptVariableSet);
            }
        }

        /// <summary>
        /// Sets the specified application scope.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        public void SetAppScope(IAppHostScope appScope)
        {
            _appScope = appScope;
        }

        #endregion
    }
}
