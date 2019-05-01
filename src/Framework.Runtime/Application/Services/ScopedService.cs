using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Application.Services.Data.Datasources;
using BindOpen.Framework.Core.Data.Context;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Runtime.Application.Services
{
    /// <summary>
    /// This class represents a scoped service.
    /// </summary>
    public class ScopedService : IdentifiedDataItem, IScopedService
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        // Extensions ----------------------

        /// <summary>
        /// The scope of this instance.
        /// </summary>
        protected IRuntimeAppScope _appScope = null;

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        // Extensions ----------------------

        /// <summary>
        /// The application scope of this instance.
        /// </summary>
        public IRuntimeAppScope AppScope
        {
            get { return this._appScope; }
        }

        /// <summary>
        /// The application extension of this instance.
        /// </summary>
        public IAppExtension AppExtension
        {
            get { return this._appScope?.AppExtension; }
        }

        /// <summary>
        /// The data context of this instance.
        /// </summary>
        public IDataContext DataContext
        {
            get { return this._appScope?.DataContext; }
        }

        /// <summary>
        /// Script interpreter of this instance.
        /// </summary>
        public IScriptInterpreter ScriptInterpreter
        {
            get { return this._appScope?.ScriptInterpreter; }
        }

        /// <summary>
        /// Data source service of this instance.
        /// </summary>
        public IDataSourceService DataSourceService
        {
            get { return this._appScope?.DataSourceService; }
        }

        /// <summary>
        /// Connection manager of this instance.
        /// </summary>
        public IConnectionService ConnectionService
        {
            get { return this._appScope?.ConnectionService; }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ScopedService class.
        /// </summary>
        public ScopedService() : base("")
        {
        }

        /// <summary>
        /// Instantiates a new instance of the ScopedService class.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        public ScopedService(IRuntimeAppScope appScope) : base("")
        {
            this._appScope = appScope;
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Initializes information.
        /// </summary>
        /// <returns>Returns the log of the task.</returns>
        protected virtual ILog Initialize()
        {
            return this.Initialize<RuntimeAppScope>();
        }

        /// <summary>
        /// Initializes information.
        /// </summary>
        /// <typeparam name="T">The runtime application scope to consider.</typeparam>
        /// <returns>Returns the log of the task.</returns>
        protected virtual ILog Initialize<T>() where T : IRuntimeAppScope, new()
        {
            return new Log();
        }

        #endregion
    }
}