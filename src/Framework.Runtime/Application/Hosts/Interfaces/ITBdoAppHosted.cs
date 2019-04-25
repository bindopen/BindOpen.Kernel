using BindOpen.Framework.Runtime.Application.Configuration;

namespace BindOpen.Framework.Runtime.Application.Hosts
{
    /// <summary>
    /// The interface defines a BDO application hosted item.
    /// </summary>
    public interface ITBdoAppHosted<Q>
        where Q : BdoAppConfiguration, new()
    {
        // Execution ---------------------------------

        /// <summary>
        /// The application host.
        /// </summary>
        ITBdoAppHost<Q> Host { get; set; }
    }
}