using System;
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
        public RuntimeAppScope() : this(AppDomain.CurrentDomain)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the RuntimeAppScope class.
        /// </summary>
        /// <param name="appDomain">The application domain to consider.</param>
        public RuntimeAppScope(AppDomain appDomain) : base(appDomain)
        {
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
        public override void Initialize(AppDomain appDomain = null)
        {
            base.Initialize(appDomain);
            this.ConnectionService = new ConnectionService(this);
        }

        #endregion
    }
}