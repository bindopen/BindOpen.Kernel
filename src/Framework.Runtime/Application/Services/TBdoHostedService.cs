using BindOpen.Framework.Core.System.Diagnostics.Loggers;
using BindOpen.Framework.Runtime.Application.Hosts;
using BindOpen.Framework.Runtime.Application.Settings;
using System;

namespace BindOpen.Framework.Runtime.Application.Services
{
    /// <summary>
    /// This class represents an bot.
    /// </summary>
    public abstract class TBdoHostedService<S> : TBdoService<S>, IBdoHosted
        where S : class, IBdoSettings, new()
    {
        /// <summary>
        /// The host of this instance.
        /// </summary>
        public IBdoHost Host { get; set; }

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TBdoHostedService class.
        /// </summary>
        protected TBdoHostedService() : base(null, default)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the THostedAppService class.
        /// </summary>
        protected TBdoHostedService(
            IBdoHost host,
            S settings = default,
            IBdoLogger[] loggers = null) : base(host?.Scope, settings, loggers)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the TBdoHostedService class.
        /// </summary>
        protected TBdoHostedService(
            IBdoHost host,
            Func<IBdoHost, S> settings = null,
            IBdoLogger[] loggers = null) : base(host?.Scope, settings(host), loggers)
        {
        }

        #endregion
    }
}