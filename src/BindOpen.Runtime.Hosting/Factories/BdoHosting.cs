using BindOpen.Logging;
using BindOpen.Runtime.Hosts;
using System;

namespace BindOpen.Runtime.Hosting
{
    /// <summary>
    /// This static class is a factory for BindOpen hosts.
    /// </summary>
    public static class BdoHosting
    {
        /// <summary>
        /// Adds a BindOpen host.
        /// </summary>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <returns></returns>
        public static BdoHost NewHost(
            IBdoLog log = null)
        {
            BdoHost host = NewHost(null, log);
            return host;
        }

        /// <summary>
        /// Adds a BindOpen host.
        /// </summary>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <returns></returns>
        public static BdoHost NewHost(
            Action<IBdoHostOptions> setupAction,
            IBdoLog log = null)
        {
            BdoHost host = new(log);
            host.Configure(setupAction);
            host.Start();
            return host;
        }
    }
}