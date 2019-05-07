using System;
using BindOpen.Framework.Runtime.Application.Configuration;
using BindOpen.Framework.Runtime.Application.Options;

namespace BindOpen.Framework.Runtime.Application.Hosts
{
    /// <summary>
    /// This static class proposes application host creators.
    /// </summary>
    public static class AppHostFactory
    {
        // Factories --------------------------

        /// <summary>
        /// Adds a BindOpen default application hosting serivce.
        /// </summary>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <returns></returns>
        public static TAppHost<AppConfiguration> CreateBindOpenDefaultHost(
            Action<ITAppHostOptions<AppConfiguration>> setupAction = null)
        {
            return CreateBindOpenHost<TAppHost<AppConfiguration>, AppConfiguration>(setupAction);
        }

        /// <summary>
        /// Adds a BindOpen application hosting serivce.
        /// </summary>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <returns></returns>
        public static TAppHost<Q> CreateBindOpenHost<Q>(
            Action<ITAppHostOptions<Q>> setupAction = null)
            where Q : class, IAppConfiguration, new()
        {
            return CreateBindOpenHost<TAppHost<Q>, Q>(setupAction);
        }

        /// <summary>
        /// Adds a BindOpen application hosting serivce.
        /// </summary>
        /// <typeparam name="THost">The class of application host to consider.</typeparam>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <returns></returns>
        public static THost CreateBindOpenHost<THost, Q>(
            Action<ITAppHostOptions<Q>> setupAction = null)
            where THost : TAppHost<Q>
            where Q : class, IAppConfiguration, new()
        {
            THost host = new TAppHost<Q>() as THost;
            host.Configure(setupAction);
            host.Start();
            return host;
        }
   }
}