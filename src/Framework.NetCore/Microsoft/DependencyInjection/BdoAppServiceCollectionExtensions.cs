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
        /// <summary>
        /// Adds a BindOpen default application hosting serivce.
        /// </summary>
        /// <param name="services">The collection of services to populate.</param>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <returns></returns>
        public static IServiceCollection AddBdoDefaultAppHosting(
            this IServiceCollection services,
            Action<ITAppHostOptions<AppConfiguration>> setupAction = null)
        {
            return services.AddBdoAppHosting<TAppHost<AppConfiguration>, AppConfiguration>(setupAction);
        }

        /// <summary>
        /// Adds a BindOpen application hosting serivce.
        /// </summary>
        /// <param name="services">The collection of services to populate.</param>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <returns></returns>
        public static IServiceCollection AddBdoAppHosting<Q>(
            this IServiceCollection services,
            Action<ITAppHostOptions<Q>> setupAction = null)
            where Q : class, IAppConfiguration, new()
        {
            return services.AddBdoAppHosting<TAppHost<Q>, Q>(setupAction);
        }

        /// <summary>
        /// Adds a BindOpen application hosting serivce.
        /// </summary>
        /// <typeparam name="THost">The class of application host to consider.</typeparam>
        /// <param name="services">The collection of services to populate.</param>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <returns></returns>
        public static IServiceCollection AddBdoAppHosting<THost, Q>(this IServiceCollection services, Action<ITAppHostOptions<Q>> setupAction = null)
            where THost : TAppHost<Q>
            where Q : class, IAppConfiguration, new()
        {
            services.AddSingleton<ITAppHost<Q>, THost>(sp => {
                THost host = new TAppHost<Q>() as THost;
                host.Configure(setupAction);
                host.Start();
                return host;
            });

            return services;
        }

        /// <summary>
        /// Adds a BindOpen application service.
        /// </summary>
        /// <typeparam name="TAppService">The class of application service to consider.</typeparam>
        /// <param name="services">The collection of services to populate.</param>
        /// <returns></returns>
        public static IServiceCollection AddBdoAppService<TAppService, Q>(this IServiceCollection services)
            where TAppService : TAppService<Q>, ITAppHosted<Q>, new()
            where Q : IAppConfiguration, new()
        {
            services.AddHostedService<BdoHostedService<TAppService, Q>>();

            return services;
        }
    }
}