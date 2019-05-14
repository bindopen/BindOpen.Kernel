using System;
using BindOpen.Framework.Runtime.Application.Settings;
using BindOpen.Framework.Core.System.Diagnostics.Loggers;
using BindOpen.Framework.Runtime.Application.Hosts;

namespace BindOpen.Framework.Runtime.Application.Services
{
    /// <summary>
    /// This class represents an application host.
    /// </summary>
    public abstract class THostedAppService<T> : TAppService<T>, IAppHosted
        where T : class, IBaseSettings, new()
    {
        /// <summary>
        /// The host of this instance.
        /// </summary>
        public IAppHost Host { get; set; }

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the THostedAppService class.
        /// </summary>
        public THostedAppService() : base(null, default)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the THostedAppService class.
        /// </summary>
        public THostedAppService(
            IAppHost host,
            T settings = default,
            ILogger[] loggers = null) : base(host?.Scope, settings, loggers)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the THostedAppService class.
        /// </summary>
        public THostedAppService(
            IAppHost host,
            Func<IAppHost, T> settings = null,
            ILogger[] loggers = null) : base(host?.Scope, settings(host), loggers)
        {
        }

        #endregion
    }
}