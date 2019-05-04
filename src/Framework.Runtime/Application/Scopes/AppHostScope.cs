using System;
using BindOpen.Framework.Runtime.Application.Services;

namespace BindOpen.Framework.Core.Application.Scopes
{
    /// <summary>
    /// This class represents a runtime application scope.
    /// </summary>
    public class AppHostScope : AppScope, IAppHostScope
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The connection service of this instance.
        /// </summary>
        public IConnectionService ConnectionService { get; set; } = null;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the RuntimeAppScope class.
        /// </summary>
        public AppHostScope() : this(AppDomain.CurrentDomain)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the RuntimeAppScope class.
        /// </summary>
        /// <param name="appDomain">The application domain to consider.</param>
        public AppHostScope(
            AppDomain appDomain,
            IConnectionService connectionService = null) : base(appDomain)
        {
            this.ConnectionService = connectionService ?? new ConnectionService(this);
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Sets the specified application domain.
        /// </summary>
        /// <param name="appDomain">The application domain to instance.</param>
        public override void Initialize(AppDomain appDomain = null)
        {
            base.Initialize(appDomain);
            this.ConnectionService = new ConnectionService(this);
        }

        #endregion
    }
}