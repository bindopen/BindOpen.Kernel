using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Configuration;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;
using BindOpen.Framework.Runtime.Application.Modules;
using BindOpen.Framework.Runtime.Application.Security;
using BindOpen.Framework.Runtime.System;

namespace BindOpen.Framework.Runtime.Application.Settings
{
    /// <summary>
    /// This class represents an application settings.
    /// </summary>
    [XmlType("AppSettings", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot("app.settings", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class AppSettings : UsableConfiguration
    {
        // -------------------------------------------------------
        // PROPERTIES
        // -------------------------------------------------------

        #region Properties

        /// <summary>
        /// The credentials of this instance.
        /// </summary>
        [XmlArray("credentials")]
        [XmlArrayItem("credential")]
        public List<ApplicationCredential> Credentials { get; set; } = new List<ApplicationCredential>();

        /// <summary>
        /// The data sources of this instance.
        /// </summary>
        [XmlArray("dataSources")]
        [XmlArrayItem("add")]
        public List<DataSource> DataSources { get; set; } = new List<DataSource>();

        // Settings ----------------------------------------------

        /// <summary>
        /// The application instance kind of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name="applicationInstanceKind")]
        public ApplicationInstanceKind ApplicationInstanceKind
        {
            get { return this.Get<ApplicationInstanceKind>(ApplicationInstanceKind.Autonomous); }
            set { this.Set(value); }
        }

        /// <summary>
        /// Application instance ID of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name="applicationInstanceId")]
        public String ApplicationInstanceId
        {
            get { return this.Get<String>(); }
            set { this.Set(value); }
        }

        /// <summary>
        /// Default current UI culture name of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name="defaultUICultureName")]
        public String DefaultUICultureName
        {
            get { return this.Get<String>(); }
            set { this.Set(value); }
        }

        /// <summary>
        /// Default theme of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name="defaultTheme")]
        public String DefaultTheme
        {
            get { return this.Get<String>(); }
            set { this.Set(value); }
        }

        /// <summary>
        /// Cookie domain name of this instance.
        /// </summary>
        /// <example>toto.com, titi.com...</example>
        [XmlIgnore()]
        [DetailProperty(Name="cookieDomain")]
        public String CookieDomain
        {
            get { return this.Get<String>(); }
            set { this.Set(value); }
        }

        // Execution ----------------------

        /// <summary>
        /// Runtime mode of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name="currentRuntimeMode")]
        public RuntimeMode CurrentRuntimeMode
        {
            get { return this.Get<RuntimeMode>(RuntimeMode.Normal); }
            set { this.Set(value); }
        }

        /// <summary>
        /// Execution level of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name="executionLevel")]
        public ApplicationExecutionLevel ExecutionLevel
        {
            get { return this.Get<ApplicationExecutionLevel>(ApplicationExecutionLevel.PROD); }
            set { this.Set(value); }
        }

        // Platform data ----------------------

        /// <summary>
        /// Name of the platform server instance of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name="serverInstanceName")]
        public String ServerInstanceName
        {
            get { return this.Get<String>(); }
            set { this.Set(value); }
        }

        /// <summary>
        /// Name of the application instance of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name="applicationInstanceName")]
        public String ApplicationInstanceName
        {
            get { return this.Get<String>(); }
            set { this.Set(value); }
        }

        // Tracking ----------------------

        /// <summary>
        /// Indicates whether user tracking is enabled.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name="isUserTrackingEnabled")]
        public Boolean? IsUserTrackingEnabled
        {
            get { return this.Get<Boolean?>(); }
            set { this.Set(value); }
        }

        // Folders ---------------

        /// <summary>
        /// The extensions folder path of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "extensions.folderPath")]
        public String ExtensionsFolderPath
        {
            get { return this.Get<String>().GetEndedString(@"\").ToPath(); }
            set { this.Set(value); }
        }

        /// <summary>
        /// The path of the log folder.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name="log.folderPath")]
        public String LogFolderPath
        {
            get { return this.Get<String>().GetEndedString(@"\").ToPath(); }
            set { this.Set(value); }
        }

        /// <summary>
        /// The path of the runtime folder.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name="runtime.folderPath")]
        public String RuntimeFolderPath
        {
            get { return this.Get<String>().GetEndedString(@"\").ToPath(); }
            set { this.Set(value); }
        }

        #endregion

        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the AppSettings class.
        /// </summary>
        public AppSettings()
            : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the AppSettings class.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        public AppSettings(IAppScope appScope)
            : base(appScope)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the AppSettings class.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="usingFilePaths">The paths of the using files to consider.</param>
        public AppSettings(IAppScope appScope, params String[] usingFilePaths)
            : base(appScope, usingFilePaths)
        {
        }

        #endregion

        // -------------------------------------------------------------
        // ACCESSORS
        // -------------------------------------------------------------

        #region Accessors


        #endregion

        // -------------------------------------------------------------
        // MUTATORS
        // -------------------------------------------------------------

        #region Mutators

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            this.Elements.Clear();
            this.DataSources = new List<DataSource>();
            this.Credentials = new List<ApplicationCredential>();
            this.UsingFilePaths = new List<String>();
            this.UsingConfiguration = new Core.Application.Configuration.Configuration();
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
        public override void UpdateStorageInfo(Log log = null)
        {
            base.UpdateStorageInfo(log);

            foreach (ApplicationCredential applicationCredential in this.Credentials)
                applicationCredential.UpdateStorageInfo(log);

            foreach (DataSource dataSource in this.DataSources)
                dataSource.UpdateStorageInfo(log);
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IAppScope appScope = null, Log log = null)
        {
            base.UpdateRuntimeInfo(appScope, log);

            foreach (ApplicationCredential applicationCredential in this.Credentials)
                applicationCredential.UpdateRuntimeInfo(appScope, log);

            foreach (DataSource dataSource in this.DataSources)
                dataSource.UpdateRuntimeInfo(appScope, log);
        }

        #endregion

        // --------------------------------------------------
        // UPDATE, CHECK, REPAIR
        // --------------------------------------------------

        #region Update_Check_Repair

        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <typeparam name="T">The application configuration class to consider.</typeparam>
        /// <param name="item">The item to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="updateModes">The update modes to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>Log of the operation.</returns>
        /// <remarks>Put reference collections as null if you do not want to repair this instance.</remarks>
        public override Log Update<T>(
            T item = null,
            List<String> specificationAreas = null,
            List<UpdateMode> updateModes = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null)
        {
            Log log = new Log();

            if (item is AppSettings)
            {
                AppSettings referenceItem = item as AppSettings;

                log.Append(base.Update(
                    referenceItem,
                    specificationAreas,
                    updateModes,
                    appScope,
                    scriptVariableSet));

                // we update the credentials

                if (referenceItem.Credentials != null)
                {
                    this.Credentials = new List<ApplicationCredential>();
                    foreach (ApplicationCredential applicationCredential in referenceItem.Credentials)
                        this.Credentials.Add(applicationCredential.Clone() as ApplicationCredential);
                }

                if (referenceItem.DataSources != null)
                {
                    this.DataSources = new List<DataSource>();
                    foreach (DataSource dataSource in referenceItem.DataSources)
                        this.DataSources.Add(dataSource.Clone() as DataSource);
                }
            }

            return log;
        }

        #endregion
    }
}
