using BindOpen.Framework.Core.Application.Depots.Datasources;
using BindOpen.Framework.Core.Application.Scopes;
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
        protected IAppHostScope _appScope = null;

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        // Extensions ----------------------

        /// <summary>
        /// The application extension of this instance.
        /// </summary>
        public IAppScope Scope
        {
            get { return _appScope; }
        }

        /// <summary>
        /// The data context of this instance.
        /// </summary>
        public IDataContext Context
        {
            get { return _appScope?.Context; }
        }

        /// <summary>
        /// Script interpreter of this instance.
        /// </summary>
        public IScriptInterpreter Interpreter
        {
            get { return _appScope?.Interpreter; }
        }

        /// <summary>
        /// Data source service of this instance.
        /// </summary>
        public IDataSourceDepot DataSourceDepot
        {
            get { return _appScope?.DataSourceDepot; }
        }

        /// <summary>
        /// Connection manager of this instance.
        /// </summary>
        public IConnectionService ConnectionService
        {
            get { return _appScope?.ConnectionService; }
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
        public ScopedService(IAppHostScope appScope) : base("")
        {
            _appScope = appScope;
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
            return Initialize<AppHostScope>();
        }

        /// <summary>
        /// Initializes information.
        /// </summary>
        /// <typeparam name="T">The runtime application scope to consider.</typeparam>
        /// <returns>Returns the log of the task.</returns>
        protected virtual ILog Initialize<T>() where T : IAppHostScope, new()
        {
            return new Log();
        }

        #endregion
    }
}