using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Runtime.Application.Configuration;
using BindOpen.Framework.Runtime.Application.Hosts;
using BindOpen.Framework.Runtime.Application.Options;

namespace BindOpen.Framework.Runtime.Application.Services
{
    /// <summary>
    /// This class represents an application host.
    /// </summary>
    public class THostedAppService<Q> : TAppService<Q>, ITAppHosted<Q>
        where Q : IAppConfiguration, new()
    {
        /// <summary>
        /// The host of this instance.
        /// </summary>
        public ITAppHost<Q> Host { get; set; }

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the THostedAppService class.
        /// </summary>
        public THostedAppService() : this(null, null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the THostedAppService class.
        /// </summary>
        public THostedAppService(
            IAppHostScope appScope,
            ITAppHostOptions<Q> options) : base(appScope, options)
        {
        }

        #endregion
    }
}