using BindOpen.Framework.Data.Services;
using BindOpen.Framework.NetCore.Services;
using BindOpen.Framework.Runtime.Application.Hosts;
using BindOpen.Framework.Runtime.Application.Options;
using BindOpen.Framework.Runtime.Application.Services;
using BindOpen.Framework.Runtime.Application.Settings;
using BindOpen.Framework.System.Diagnostics.Loggers;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// This static class extends .Net core dependency injection namespace.
    /// </summary>
    public static class BdoAppServiceCollectionExtensions
    {
        // BindOpen host --------------------------

        /// <summary>
        /// Adds a BindOpen default host.
        /// </summary>
        /// <param name="services">The collection of services to populate.</param>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <returns></returns>
        public static IServiceCollection AddBindOpenDefaultHost(
            this IServiceCollection services,
            Action<ITBdoHostOptions<BdoDefaultAppSettings>> setupAction = null)
        {
            services.AddSingleton<IBdoHost>(_ => BdoHostFactory.CreateBindOpenDefaultHost(setupAction));

            return services;
        }

        /// <summary>
        /// Adds a BindOpen default host.
        /// </summary>
        /// <param name="provider">The service provider to consider.</param>
        /// <returns></returns>
        public static IBdoHost GetBindOpenDefaulHost(this IServiceProvider provider)
        {
            return provider?.GetService<IBdoHost>();
        }

        /// <summary>
        /// Adds a BindOpen host.
        /// </summary>
        /// <param name="services">The collection of services to populate.</param>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <returns></returns>
        public static IServiceCollection AddBindOpenHost<S>(
            this IServiceCollection services,
            Action<ITBdoHostOptions<S>> setupAction = null)
            where S : class, IBdoAppSettings, new()
        {
            services.AddSingleton<IBdoHost>(_ => BdoHostFactory.CreateBindOpenHost<S>(setupAction));

            return services;
        }

        /// <summary>
        /// Adds a BindOpen host.
        /// </summary>
        /// <typeparam name="SHost">The class of bot to consider.</typeparam>
        /// <typeparam name="S"></typeparam>
        /// <param name="services">The collection of services to populate.</param>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <returns></returns>
        public static IServiceCollection AddBindOpenHost<SHost, S>(
            this IServiceCollection services,
            Action<ITBdoHostOptions<S>> setupAction = null)
            where SHost : TBdoHost<S>, new()
            where S : class, IBdoAppSettings, new()
        {
            services.AddSingleton<IBdoHost, SHost>(_ => BdoHostFactory.CreateBindOpenHost<SHost, S>(setupAction));

            return services;
        }

        // BindOpen services --------------------------

        /// <summary>
        /// Adds a BindOpen service.
        /// </summary>
        /// <typeparam name="Serv"></typeparam>
        /// <typeparam name="SServ"></typeparam>
        /// <typeparam name="SHost"></typeparam>
        /// <param name="services">The collection of services to populate.</param>
        /// <param name="loggers"></param>
        /// <param name="funcSettingsConverter"></param>
        /// <returns></returns>
        public static IServiceCollection AddBindOpenService<Serv, SServ, SHost>(
            this IServiceCollection services,
            IBdoLogger[] loggers = null,
            Func<SHost, SServ> funcSettingsConverter = null)
            where Serv : ITBdoService<SServ>, IBdoHosted, new()
            where SServ : class, IBdoSettings, new()
            where SHost : IBdoAppSettings
        {
            services.AddSingleton<TBdoServiceOptions<SServ, SHost>>(_ => new TBdoServiceOptions<SServ, SHost>(loggers, funcSettingsConverter));
            services.AddHostedService<THostedService<Serv, SServ, SHost>>();

            return services;
        }

        // BindOpen data services --------------------------

        /// <summary>
        /// Adds a singleton data service.
        /// </summary>
        /// <typeparam name="ITBdoDataService">The interface of BindOpen data service to consider.</typeparam>
        /// <typeparam name="TBdoDataService">The BindOpen data service class to consider.</typeparam>
        /// <param name="services">The collection of services to populate.</param>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <returns>Returns the updated service collection.</returns>
        public static IServiceCollection AddSingletonDataService<ITBdoDataService, TBdoDataService>(
            this IServiceCollection services,
            Action<TBdoDataService> setupAction = null)
            where ITBdoDataService : class, IBdoDataService
            where TBdoDataService : class, ITBdoDataService, new()
        {
            services.AddSingleton<ITBdoDataService, TBdoDataService>(p =>
            {
                var service = p.GetService<ITBdoDataService>() as TBdoDataService;
                setupAction?.Invoke(service);
                return service;
            });

            return services;
        }

        /// <summary>
        /// Adds a scoped data service.
        /// </summary>
        /// <typeparam name="ITBdoDataService">The interface of BindOpen data service to consider.</typeparam>
        /// <typeparam name="TBdoDataService">The BindOpen data service class to consider.</typeparam>
        /// <param name="services">The collection of services to populate.</param>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <returns>Returns the updated service collection.</returns>
        public static IServiceCollection AddScopedDataService<ITBdoDataService, TBdoDataService>(
            this IServiceCollection services,
            Action<TBdoDataService> setupAction = null)
            where ITBdoDataService : class, IBdoDataService
            where TBdoDataService : class, ITBdoDataService, new()
        {
            services.AddScoped<ITBdoDataService, TBdoDataService>(p =>
            {
                var service = p.GetService<ITBdoDataService>() as TBdoDataService;
                setupAction?.Invoke(service);
                return service;
            });

            return services;
        }

        /// <summary>
        /// Adds a transient data service.
        /// </summary>
        /// <typeparam name="ITBdoDataService">The interface of BindOpen data service to consider.</typeparam>
        /// <typeparam name="TBdoDataService">The BindOpen data service class to consider.</typeparam>
        /// <param name="services">The collection of services to populate.</param>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <returns>Returns the updated service collection.</returns>
        public static IServiceCollection AddTransientDataService<ITBdoDataService, TBdoDataService>(
            this IServiceCollection services,
            Action<TBdoDataService> setupAction = null)
            where ITBdoDataService : class, IBdoDataService
            where TBdoDataService : class, ITBdoDataService, new()
        {
            services.AddTransient<ITBdoDataService, TBdoDataService>(p =>
            {
                var service = p.GetService<ITBdoDataService>() as TBdoDataService;
                setupAction?.Invoke(service);
                return service;
            });

            return services;
        }
    }
}