using System;
using BindOpen.Framework.Runtime.Application.Hosts;
using BindOpen.Framework.Runtime.Application.Services;

namespace BindOpen.Framework.Core.Application.Scopes
{
    /// <summary>
    /// This class represents a runtime application scope.
    /// </summary>
    public class RuntimeAppScope : AppScope, IRuntimeAppScope
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
        public RuntimeAppScope()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the RuntimeAppScope class.
        /// </summary>
        /// <param name="appDomain">The application domain to consider.</param>
        public RuntimeAppScope(AppDomain appDomain) : base(appDomain)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the RuntimeAppScope class.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        public RuntimeAppScope(IRuntimeAppScope appScope) : base(appScope)
        {
            if (appScope != null)
                this.ConnectionService = new ConnectionService(this);
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
        public override void SetAppDomain(AppDomain appDomain)
        {
            base.SetAppDomain(appDomain);
            if (appDomain != null)
                this.ConnectionService = new ConnectionService(this);
        }

        #endregion
    }
}