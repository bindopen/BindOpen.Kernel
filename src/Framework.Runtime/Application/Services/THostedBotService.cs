using System;
using BindOpen.Framework.Runtime.Application.Settings;
using BindOpen.Framework.Core.System.Diagnostics.Loggers;
using BindOpen.Framework.Runtime.Application.Bots;

namespace BindOpen.Framework.Runtime.Application.Services
{
    /// <summary>
    /// This class represents an bot.
    /// </summary>
    public abstract class THostedBotService<T> : TBotService<T>, IBoted
        where T : class, IBaseSettings, new()
    {
        /// <summary>
        /// The host of this instance.
        /// </summary>
        public IBot Bot { get; set; }

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the THostedAppService class.
        /// </summary>
        public THostedBotService() : base(null, default)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the THostedAppService class.
        /// </summary>
        public THostedBotService(
            IBot host,
            T settings = default,
            ILogger[] loggers = null) : base(host?.Scope, settings, loggers)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the THostedAppService class.
        /// </summary>
        public THostedBotService(
            IBot host,
            Func<IBot, T> settings = null,
            ILogger[] loggers = null) : base(host?.Scope, settings(host), loggers)
        {
        }

        #endregion
    }
}