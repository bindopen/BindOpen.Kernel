using BindOpen.Framework.Application.Options;
using BindOpen.Framework.Application.Scopes;
using BindOpen.Framework.Application.Services;
using BindOpen.Framework.Application.Settings;
using BindOpen.Framework.NetCore.Services;
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
        /// Adds a BindOpen default service.
        /// </summary>
        /// <param name="services">The collection of services to populate.</param>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <returns></returns>
        public static IServiceCollection AddBindOpen(
            this IServiceCollection services,
            Action<ITBdoHostOptions<BdoDefaultAppSettings>> setupAction = null)
        {
            services.AddSingleton<IBdoHost>(_ => BdoHostFactory.CreateBindOpenDefaultHost(setupAction));

            return services;
        }

        /// <summary>
        /// Adds a BindOpen host.
        /// </summary>
        /// <param name="provider">The service provider to consider.</param>
        /// <returns></returns>
        public static IBdoHost GetBindOpenHost(this IServiceProvider provider)
        {
            return provider?.GetService<IBdoHost>();
        }

        /// <summary>
        /// Adds a BindOpen service.
        /// </summary>
        /// <param name="services">The collection of services to populate.</param>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <returns></returns>
        public static IServiceCollection AddBindOpen<S>(
            this IServiceCollection services,
            Action<ITBdoHostOptions<S>> setupAction = null)
            where S : class, IBdoAppSettings, new()
        {
            services.AddSingleton<IBdoHost>(_ => BdoHostFactory.CreateBindOpenHost<S>(setupAction));

            return services;
        }

        /// <summary>
        /// Adds a BindOpen service.
        /// </summary>
        /// <typeparam name="SHost">The class of bot to consider.</typeparam>
        /// <typeparam name="S"></typeparam>
        /// <param name="services">The collection of services to populate.</param>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <returns></returns>
        public static IServiceCollection AddBindOpen<SHost, S>(
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

        // BindOpen connected services --------------------------

        /// <summary>
        /// Adds a singleton connected service.
        /// </summary>
        /// <typeparam name="TService">The interface of BindOpen connected service to consider.</typeparam>
        /// <typeparam name="TImplementation">The service implementation to consider.</typeparam>
        /// <param name="services">The collection of services to populate.</param>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <returns>Returns the updated service collection.</returns>
        public static IServiceCollection AddSingletonConnectedService<TService, TImplementation>(
            this IServiceCollection services,
            Func<IBdoHost, TImplementation> setupAction = null)
            where TService : class, IBdoConnectedService
            where TImplementation : class, TService
        {
            services.AddSingleton<TService, TImplementation>(p =>
            {
                var host = p.GetService<IBdoHost>();
                var repo = setupAction?.Invoke(host);

                return repo;
            });

            return services;
        }

        /// <summary>
        /// Adds a scoped connected service.
        /// </summary>
        /// <typeparam name="TService">The interface of BindOpen connected service to consider.</typeparam>
        /// <typeparam name="TImplementation">The service implementation to consider.</typeparam>
        /// <param name="services">The collection of services to populate.</param>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <returns>Returns the updated service collection.</returns>
        public static IServiceCollection AddScopedConnectedService<TService, TImplementation>(
            this IServiceCollection services,
            Func<IBdoHost, TImplementation> setupAction = null)
            where TService : class, IBdoConnectedService
            where TImplementation : class, TService
        {
            services.AddScoped<TService, TImplementation>(p =>
            {
                var host = p.GetService<IBdoHost>();
                var repo = setupAction?.Invoke(host);

                return repo;
            });

            return services;
        }

        /// <summary>
        /// Adds a transient connected service.
        /// </summary>
        /// <typeparam name="TService">The interface of BindOpen connected service to consider.</typeparam>
        /// <typeparam name="TImplementation">The service implementation to consider.</typeparam>
        /// <param name="services">The collection of services to populate.</param>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <returns>Returns the updated service collection.</returns>
        public static IServiceCollection AddTransientConnectedService<TService, TImplementation>(
            this IServiceCollection services,
            Func<IBdoHost, TImplementation> setupAction = null)
            where TService : class, IBdoConnectedService
            where TImplementation : class, TService
        {
            services.AddTransient<TService, TImplementation>(p =>
            {
                var host = p.GetService<IBdoHost>();
                var repo = setupAction?.Invoke(host);

                return repo;
            });

            return services;
        }
    }
}