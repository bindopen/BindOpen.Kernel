using BindOpen.Framework.Core.Application.Depots.Datasources;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Context;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Runtime.Application.Services
{
    /// <summary>
    /// This class represents a scoped service.
    /// </summary>
    public abstract class BotScopedService : IdentifiedDataItem, IBotScopedService
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        // Extensions ----------------------

        /// <summary>
        /// The scope of this instance.
        /// </summary>
        protected IBotScope _scope = null;

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        // Extensions ----------------------

        /// <summary>
        /// The application extension of this instance.
        /// </summary>
        public IBotScope Scope
        {
            get { return _scope; }
        }

        /// <summary>
        /// The data context of this instance.
        /// </summary>
        public IDataContext Context
        {
            get { return _scope?.Context; }
        }

        /// <summary>
        /// Script interpreter of this instance.
        /// </summary>
        public IScriptInterpreter Interpreter
        {
            get { return _scope?.Interpreter; }
        }

        /// <summary>
        /// Data source service of this instance.
        /// </summary>
        public IDataSourceDepot DataSourceDepot
        {
            get { return _scope?.DataSourceDepot; }
        }

        /// <summary>
        /// Connection manager of this instance.
        /// </summary>
        public IConnectionService ConnectionService
        {
            get { return _scope?.ConnectionService; }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ScopedService class.
        /// </summary>
        protected BotScopedService() : base("")
        {
        }

        /// <summary>
        /// Instantiates a new instance of the ScopedService class.
        /// </summary>
        /// <param name="scope">The application scope to consider.</param>
        protected BotScopedService(IBotScope scope) : base("")
        {
            _scope = scope;
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
            return Initialize<BotScope>();
        }

        /// <summary>
        /// Initializes information.
        /// </summary>
        /// <typeparam name="T">The bot scope to consider.</typeparam>
        /// <returns>Returns the log of the task.</returns>
        protected virtual ILog Initialize<T>() where T : IBotScope, new()
        {
            return new Log();
        }

        #endregion
    }
}