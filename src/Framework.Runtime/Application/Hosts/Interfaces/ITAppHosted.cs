using BindOpen.Framework.Runtime.Application.Configuration;

namespace BindOpen.Framework.Runtime.Application.Hosts
{
    /// <summary>
    /// The interface defines a BDO application hosted item.
    /// </summary>
    public interface ITAppHosted<Q>
        where Q : IAppConfiguration, new()
    {
        // Execution ---------------------------------

        /// <summary>
        /// The application host.
        /// </summary>
        ITAppHost<Q> Host { get; set; }
    }
}