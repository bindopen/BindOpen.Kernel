using System;
using BindOpen.Framework.Runtime.Application.Options;
using BindOpen.Framework.Runtime.Application.Settings;

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
        public static TAppHost<DefaultAppSettings> CreateBindOpenDefaultHost(
            Action<ITAppHostOptions<DefaultAppSettings>> setupAction = null)
        {
            return CreateBindOpenHost<TAppHost<DefaultAppSettings>, DefaultAppSettings>(setupAction);
        }

        /// <summary>
        /// Adds a BindOpen application hosting serivce.
        /// </summary>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <returns></returns>
        public static TAppHost<T> CreateBindOpenHost<T>(
            Action<ITAppHostOptions<T>> setupAction = null)
            where T : class, IAppSettings, new()
        {
            return CreateBindOpenHost<TAppHost<T>, T>(setupAction);
        }

        /// <summary>
        /// Adds a BindOpen application hosting serivce.
        /// </summary>
        /// <typeparam name="THost">The class of application host to consider.</typeparam>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static THost CreateBindOpenHost<THost, T>(
            Action<ITAppHostOptions<T>> setupAction = null)
            where THost : TAppHost<T>, new()
            where T : class, IAppSettings, new()
        {
            THost host = new THost();
            host.Configure(setupAction);
            host.Start();
            return host;
        }
   }
}