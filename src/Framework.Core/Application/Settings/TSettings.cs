using System.Xml.Schema;
using BindOpen.Framework.Core.Application.Configuration;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Application.Settings
{
    /// <summary>
    /// This class represents a configuration.
    /// </summary>
    public class TSettings<Q> : BaseSettings, ITSettings<Q>
        where Q : class, IBaseConfiguration, new()
    {
        // -------------------------------------------------------
        // PROPERTIES
        // -------------------------------------------------------

        #region Properties

        /// <summary>
        /// The configuration of this instance.
        /// </summary>
        public new Q Configuration { get => base.Configuration as Q; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TSettings class.
        /// </summary>
        public TSettings()
        {
            _configuration = new Q();
        }

        /// <summary>
        /// Instantiates a new instance of the TSettings class.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        public TSettings(IAppScope appScope, Q configuration)
        {
            _appScope = appScope;
            _configuration = configuration;
        }

        #endregion

        /// <summary>
        /// Loads the application settings of this instance.
        /// </summary>
        /// <param name="filePath">The file path to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="xmlSchemaSet">The XML schema set to consider for checking.</param>
        /// <returns>Returns the loading log.</returns>
        public override ILog Load(
            string filePath,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            XmlSchemaSet xmlSchemaSet = null)
        {
            ILog log = new Log();
            Q configuration = ConfigurationLoader.Load<Q>(filePath, appScope, scriptVariableSet, log, xmlSchemaSet);
            _appScope = appScope;
            _configuration = configuration;

            return log;
        }
    }
}
