using System;
using BindOpen.Framework.NetCore.Services;
using BindOpen.Framework.Runtime.Application.Configuration;
using BindOpen.Framework.Runtime.Application.Hosts;
using BindOpen.Framework.Runtime.Application.Options;
using BindOpen.Framework.Runtime.Application.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// This static class extends .Net core dependency injection namespace.
    /// </summary>
    public static class BdoAppServiceCollectionExtensions
    {
        // BindOpen host --------------------------

        /// <summary>
        /// Adds a BindOpen default application hosting serivce.
        /// </summary>
        /// <param name="services">The collection of services to populate.</param>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <returns></returns>
        public static IServiceCollection AddBindOpenDefaultHost(
            this IServiceCollection services,
            Action<ITAppHostOptions<AppConfiguration>> setupAction = null)
        {
            services.AddSingleton<IAppHost>(_ => AppHostFactory.CreateBindOpenDefaultHost(setupAction));

            return services;
        }

        /// <summary>
        /// Adds a BindOpen application hosting serivce.
        /// </summary>
        /// <param name="services">The collection of services to populate.</param>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <returns></returns>
        public static IServiceCollection AddBindOpenHost<Q>(
            this IServiceCollection services,
            Action<ITAppHostOptions<Q>> setupAction = null)
            where Q : class, IAppConfiguration, new()
        {
            services.AddSingleton<IAppHost>(_ => AppHostFactory.CreateBindOpenHost<Q>(setupAction));

            return services;
        }

        /// <summary>
        /// Adds a BindOpen application hosting serivce.
        /// </summary>
        /// <typeparam name="THost">The class of application host to consider.</typeparam>
        /// <param name="services">The collection of services to populate.</param>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <returns></returns>
        public static IServiceCollection AddBindOpenHost<THost, Q>(this IServiceCollection services, Action<ITAppHostOptions<Q>> setupAction = null)
            where THost : TAppHost<Q>, new()
            where Q : class, IAppConfiguration, new()
        {
            services.AddSingleton<IAppHost, THost>(_ => AppHostFactory.CreateBindOpenHost<THost, Q>(setupAction));

            return services;
        }

        // BindOpen services --------------------------

        /// <summary>
        /// Adds a BindOpen application service.
        /// </summary>
        /// <typeparam name="TAppService">The class of application service to consider.</typeparam>
        /// <param name="services">The collection of services to populate.</param>
        /// <returns></returns>
        public static IServiceCollection AddBindOpenService<TAppService, Q>(this IServiceCollection services)
            where TAppService : TAppService<Q>, ITAppHosted<Q>, new()
            where Q : IAppConfiguration, new()
        {
            services.AddHostedService<BdoHostedService<TAppService, Q>>();

            return services;
        }
    }
}