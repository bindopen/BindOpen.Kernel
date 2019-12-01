using BindOpen.Framework.Runtime.Application.Options.Hosts;
using BindOpen.Framework.Runtime.Application.Settings.Hosts;
using System;

namespace BindOpen.Framework.Runtime.Application.Hosts
{
    /// <summary>
    /// This static class is a factory for BindOpen hosts.
    /// </summary>
    public static class BdoHostFactory
    {
        // Factories --------------------------

        /// <summary>
        /// Adds a BindOpen host with default settings.
        /// </summary>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <returns></returns>
        public static ITBdoHost<BdoDefaultHostSettings> CreateBindOpenDefaultHost(
            Action<ITBdoHostOptions<BdoDefaultHostSettings>> setupAction = null)
            => CreateBindOpenHost<TBdoHost<BdoDefaultHostSettings>, BdoDefaultHostSettings>(setupAction);

        /// <summary>
        /// Adds a BindOpen host.
        /// </summary>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <returns></returns>
        public static ITBdoHost<S> CreateBindOpenHost<S>(Action<ITBdoHostOptions<S>> setupAction = null)
            where S : class, IBdoHostSettings, new()
            => CreateBindOpenHost<TBdoHost<S>, S>(setupAction);

        /// <summary>
        /// Adds a BindOpen host.
        /// </summary>
        /// <typeparam name="THost">The class of host to consider.</typeparam>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <typeparam name="S"></typeparam>
        /// <returns></returns>
        public static THost CreateBindOpenHost<THost, S>(
            Action<ITBdoHostOptions<S>> setupAction = null)
            where THost : class, ITBdoHost<S>, new()
            where S : class, IBdoHostSettings, new()
        {
            THost host = new THost();
            host.Configure(setupAction);
            host.Start();
            return host;
        }
    }
}