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
        public static IServiceCollection AddBindOpenDefault(
            this IServiceCollection services,
            Action<ITAppHostOptions<AppConfiguration>> setupAction = null)
        {
            return services.AddBindOpen<TAppHost<AppConfiguration>, AppConfiguration>(setupAction);
        }

        /// <summary>
        /// Adds a BindOpen application hosting serivce.
        /// </summary>
        /// <param name="services">The collection of services to populate.</param>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <returns></returns>
        public static IServiceCollection AddBindOpen<Q>(
            this IServiceCollection services,
            Action<ITAppHostOptions<Q>> setupAction = null)
            where Q : class, IAppConfiguration, new()
        {
            return services.AddBindOpen<TAppHost<Q>, Q>(setupAction);
        }

        /// <summary>
        /// Adds a BindOpen application hosting serivce.
        /// </summary>
        /// <typeparam name="THost">The class of application host to consider.</typeparam>
        /// <param name="services">The collection of services to populate.</param>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <returns></returns>
        public static IServiceCollection AddBindOpen<THost, Q>(this IServiceCollection services, Action<ITAppHostOptions<Q>> setupAction = null)
            where THost : TAppHost<Q>
            where Q : class, IAppConfiguration, new()
        {
            services.AddSingleton<ITAppHost<Q>, THost>(_ => AppHostFactory.CreateBindOpen<THost, Q>(setupAction));

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