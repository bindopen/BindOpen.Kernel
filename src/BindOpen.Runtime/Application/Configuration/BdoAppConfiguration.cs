using BindOpen.Application.Scopes;
using BindOpen.Application.Security;
using BindOpen.Data.Common;
using BindOpen.Data.Elements;
using BindOpen.Data.Items;
using BindOpen.System.Diagnostics;
using BindOpen.System.Scripting;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BindOpen.Application.Configuration
{
    /// <summary>
    /// This class represents a BindOpen application configuration.
    /// </summary>
    [XmlType("BdoAppConfiguration", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot("app.config", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class BdoAppConfiguration : BdoBaseConfiguration, IBdoAppConfiguration
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
        public List<Datasource> Datasources { get; set; } = new List<Datasource>();

        #endregion

        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the AppConfiguration class.
        /// </summary>
        public BdoAppConfiguration()
            : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the AppConfiguration class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public BdoAppConfiguration(params IDataElement[] items)
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
        public override void UpdateStorageInfo(IBdoLog log = null)
        {
            base.UpdateStorageInfo(log);

            foreach (ApplicationCredential applicationCredential in Credentials)
                applicationCredential.UpdateStorageInfo(log);

            foreach (Datasource dataSource in Datasources)
                dataSource.UpdateStorageInfo(log);
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of script variables to consider.</param>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IBdoScope scope = null, IBdoScriptVariableSet scriptVariableSet = null, IBdoLog log = null)
        {
            base.UpdateRuntimeInfo(scope, scriptVariableSet, log);

            foreach (ApplicationCredential applicationCredential in Credentials)
                applicationCredential.UpdateRuntimeInfo(scope, scriptVariableSet, log);

            foreach (Datasource dataSource in Datasources)
                dataSource.UpdateRuntimeInfo(scope, scriptVariableSet, log);
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
        public override IBdoLog Update<T>(
            T item = default,
            string[] specificationAreas = null,
            UpdateModes[] updateModes = null)
        {
            var log = new BdoLog();

            if (item is BdoAppConfiguration configuration)
            {
                log.AddEvents(base.Update(
                    configuration,
                    specificationAreas,
                    updateModes));

                // we update the credentials

                if (configuration.Credentials != null)
                {
                    Credentials = new List<ApplicationCredential>();
                    foreach (ApplicationCredential applicationCredential in configuration.Credentials)
                        Credentials.Add(applicationCredential.Clone() as ApplicationCredential);
                }

                if (configuration.Datasources != null)
                {
                    Datasources = new List<Datasource>();
                    foreach (Datasource dataSource in configuration.Datasources)
                        Datasources.Add(dataSource.Clone() as Datasource);
                }
            }

            return log;
        }

        #endregion

        // ------------------------------------------
        // MISCELLANEOUS
        // ------------------------------------------

        #region Miscellaneous

        /// <summary>
        /// Gets the name of the application instance.
        /// </summary>
        /// <returns>Returns the name of the application instance.</returns>
        public static string __ApplicationInstanceName => (Environment.MachineName ?? "").ToUpper();

        #endregion
    }
}
