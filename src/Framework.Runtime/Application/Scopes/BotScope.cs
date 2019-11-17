using BindOpen.Framework.Runtime.Application.Services;
using System;

namespace BindOpen.Framework.Core.Application.Scopes
{
    /// <summary>
    /// This class represents a bot scope.
    /// </summary>
    public class BotScope : AppScope, IBotScope
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
        public BotScope() : this(AppDomain.CurrentDomain)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the RuntimeAppScope class.
        /// </summary>
        /// <param name="appDomain">The application domain to consider.</param>
        public BotScope(
            AppDomain appDomain) : base(appDomain)
        {
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