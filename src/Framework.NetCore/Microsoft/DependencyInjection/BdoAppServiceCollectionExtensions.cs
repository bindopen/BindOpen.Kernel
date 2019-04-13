using System;
using BindOpen.Framework.NetCore.Services;
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
        public static IServiceCollection AddBdoAppHosting(this IServiceCollection services, Action<IAppHostOptions> setupAction = null)
        {
            return services.AddBdoAppHosting<BdoAppHost>(setupAction);
        }

        /// <summary>
        /// Adds a BindOpen application hosting serivce.
        /// </summary>
        /// <typeparam name="THost">The class of application host to consider.</typeparam>
        /// <param name="services">The collection of services to populate.</param>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <returns></returns>
        public static IServiceCollection AddBdoAppHosting<THost>(this IServiceCollection services, Action<IAppHostOptions> setupAction = null)
        where THost : BdoAppHost
        {
            services.AddSingleton<IBdoAppHost, THost>();

            var sp = services.BuildServiceProvider();
            THost host = sp.GetService<IBdoAppHost>() as THost;
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
        public static IServiceCollection AddBdoAppService<TAppService>(this IServiceCollection services)
            where TAppService : BdoAppService, IBdoAppHosted, new()
        {
            services.AddHostedService<BdoHostedService<TAppService>>();

            return services;
        }
    }
}