using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Configuration;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Runtime.Application.Modules;
using BindOpen.Framework.Runtime.Application.Security;
using BindOpen.Framework.Runtime.System;

namespace BindOpen.Framework.Runtime.Application.Settings
{
    /// <summary>
    /// This class represents a DTO application settings.
    /// </summary>
    [XmlType("BdoAppSettings", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot("app.settings", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class BdoAppSettings : ConfigurationDto, IBdoAppSettings
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
        public List<IApplicationCredential> Credentials { get; set; } = new List<IApplicationCredential>();

        /// <summary>
        /// The data sources of this instance.
        /// </summary>
        [XmlArray("dataSources")]
        [XmlArrayItem("add")]
        public List<IDataSource> DataSources { get; set; } = new List<IDataSource>();

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

        #endregion

        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the AppSettingsDto class.
        /// </summary>
        public BdoAppSettings()
            : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the AppSettingsDto class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public BdoAppSettings(params IDataElement[] items)
            : base(items)
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
            base.UpdateStorageInfo(log);

            foreach (ApplicationCredential applicationCredential in this.Credentials)
                applicationCredential.UpdateStorageInfo(log);

            foreach (IDataSource dataSource in this.DataSources)
                dataSource.UpdateStorageInfo(log);
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(ILog log = null)
        {
            base.UpdateRuntimeInfo(log);

            foreach (ApplicationCredential applicationCredential in this.Credentials)
                applicationCredential.UpdateRuntimeInfo(log);

            foreach (IDataSource dataSource in this.DataSources)
                dataSource.UpdateRuntimeInfo(log);
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
        /// <returns>Log of the operation.</returns>
        /// <remarks>Put reference collections as null if you do not want to repair this instance.</remarks>
        public override ILog Update<T>(
            T item = default,
            string[] specificationAreas = null,
            UpdateMode[] updateModes = null)
        {
            ILog log = new Log();

            if (item is BdoAppSettings appSettings)
            {
                log.Append(base.Update(
                    appSettings,
                    specificationAreas,
                    updateModes));

                // we update the credentials

                if (appSettings.Credentials != null)
                {
                    Credentials = new List<IApplicationCredential>();
                    foreach (IApplicationCredential applicationCredential in appSettings.Credentials)
                        Credentials.Add(applicationCredential.Clone() as ApplicationCredential);
                }

                if (appSettings.DataSources != null)
                {
                    DataSources = new List<IDataSource>();
                    foreach (IDataSource dataSource in appSettings.DataSources)
                        DataSources.Add(dataSource.Clone() as IDataSource);
                }
            }

            return log;
        }

        #endregion
    }
}
