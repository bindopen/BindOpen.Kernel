using System;
using BindOpen.Framework.Core.System.Diagnostics.Loggers;
using BindOpen.Framework.NetCore.Services;
using BindOpen.Framework.Runtime.Application.Hosts;
using BindOpen.Framework.Runtime.Application.Options;
using BindOpen.Framework.Runtime.Application.Services;
using BindOpen.Framework.Runtime.Application.Settings;

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
            Action<ITAppHostOptions<DefaultAppSettings>> setupAction = null)
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
        public static IServiceCollection AddBindOpenHost<T>(
            this IServiceCollection services,
            Action<ITAppHostOptions<T>> setupAction = null)
            where T : class, IAppSettings, new()
        {
            services.AddSingleton<IAppHost>(_ => AppHostFactory.CreateBindOpenHost<T>(setupAction));

            return services;
        }

        /// <summary>
        /// Adds a BindOpen application hosting serivce.
        /// </summary>
        /// <typeparam name="THost">The class of application host to consider.</typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="services">The collection of services to populate.</param>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <returns></returns>
        public static IServiceCollection AddBindOpenHost<THost, T>(
            this IServiceCollection services,
            Action<ITAppHostOptions<T>> setupAction = null)
            where THost : TAppHost<T>, new()
            where T : class, IAppSettings, new()
        {
            services.AddSingleton<IAppHost, THost>(_ => AppHostFactory.CreateBindOpenHost<THost, T>(setupAction));

            return services;
        }

        // BindOpen services --------------------------

        /// <summary>
        /// Adds a BindOpen service.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services">The collection of services to populate.</param>
        /// <param name="loggers"></param>
        /// <param name="funcSettings"></param>
        /// <returns></returns>
        public static IServiceCollection AddBindOpenService<T>(
            this IServiceCollection services,
            ILogger[] loggers = null,
            Func<IAppSettings, IBaseSettings> funcSettings =null)
            where T : IAppService, IAppHosted, new()
        {
            services.AddSingleton<ITAppServiceOptions<T>>(_ => new TAppServiceOptions<T>(loggers, funcSettings));
            services.AddHostedService<TBdoHostedService<T> >();

            return services;
        }

        /// <summary>
        /// Adds a BindOpen transient service.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="loggers"></param>
        /// <param name="funcSettings"></param>
        /// <returns></returns>
        public static IServiceCollection AddBindOpenTransientService<T>(
            this IServiceCollection services,
            ILogger[] loggers = null,
            Func<IAppSettings, IBaseSettings> funcSettings = null)
            where T : IAppService, IAppHosted, new()
        {
            services.AddTransient<ITAppServiceOptions<T>>(_ => new TAppServiceOptions<T>(loggers, funcSettings));

            return services;
        }
    }
}