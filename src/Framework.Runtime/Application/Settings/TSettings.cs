using System.Linq;
using System.Xml.Schema;
using BindOpen.Framework.Core.Application.Configuration;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Runtime.Application.Settings
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
        public override ILog UpdateFromFile(
            string filePath,
            SpecificationLevels[] specificationLevels = null,
            IDataElementSpecSet specificationSet = null,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            XmlSchemaSet xmlSchemaSet = null)
        {
            ILog log = new Log();

            Q configuration = ConfigurationLoader.Load<Q>(filePath, appScope, scriptVariableSet, log, xmlSchemaSet, false, false);

            if (!log.HasErrorsOrExceptions() && configuration != null)
            {
                _appScope = appScope;
                Configuration?.Update(configuration);
                Configuration?.Update(
                    new DataElementSpecSet(
                        specificationSet?.Items?
                            .Where(p => p.SpecificationLevels?.ToArray().Has(specificationLevels) == true).ToArray()),
                    null, new[] { UpdateModes.Incremental_UpdateCommonItems });

                UpdateRuntimeInfo(_appScope, null, log);
            }

            return log;
        }
    }
}
