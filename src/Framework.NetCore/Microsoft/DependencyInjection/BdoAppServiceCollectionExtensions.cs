using System;
using BindOpen.Framework.NetCore.Services;
using BindOpen.Framework.Runtime.Application.Configuration;
using BindOpen.Framework.Runtime.Application.Hosts;
using BindOpen.Framework.Runtime.Application.Hosts.Options;
using BindOpen.Framework.Runtime.Application.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// This static class extends .Net core dependency injection namespace.
    /// </summary>
    public static class BdoAppServiceCollectionExtensions
    {
        /// <summary>
        /// Adds a BindOpen application hosting serivce.
        /// </summary>
        /// <param name="services">The collection of services to populate.</param>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <returns></returns>
        public static IServiceCollection AddBdoAppHosting<Q>(
            this IServiceCollection services,
            Action<ITBdoAppHostOptions<Q>> setupAction = null)
            where Q : BdoAppConfiguration, new()
        {
            return services.AddBdoAppHosting<TBdoAppHost<Q>, Q>(setupAction);
        }

        /// <summary>
        /// Adds a BindOpen application hosting serivce.
        /// </summary>
        /// <typeparam name="THost">The class of application host to consider.</typeparam>
        /// <param name="services">The collection of services to populate.</param>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <returns></returns>
        public static IServiceCollection AddBdoAppHosting<THost, Q>(this IServiceCollection services, Action<ITBdoAppHostOptions<Q>> setupAction = null)
            where THost : TBdoAppHost<Q>
            where Q : BdoAppConfiguration, new()
        {
            services.AddSingleton<ITBdoAppHost<Q>, THost>();

            var sp = services.BuildServiceProvider();
            THost host = sp.GetService<ITBdoAppHost<Q>>() as THost;
            host.Configure(setupAction);
            host.Start();

            return services;
        }

        /// <summary>
        /// Adds a BindOpen application service.
        /// </summary>
        /// <typeparam name="TAppService">The class of application service to consider.</typeparam>
        /// <param name="services">The collection of services to populate.</param>
        /// <returns></returns>
        public static IServiceCollection AddBdoAppService<TAppService, Q>(this IServiceCollection services)
            where TAppService : TBdoAppService<Q>, ITBdoAppHosted<Q>, new()
            where Q : BdoAppConfiguration, new()
        {
            services.AddHostedService<BdoHostedService<TAppService, Q>>();

            return services;
        }
    }
}