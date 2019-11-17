using BindOpen.Framework.Core.System.Diagnostics.Loggers;
using BindOpen.Framework.NetCore.Services;
using BindOpen.Framework.Runtime.Application.Bots;
using BindOpen.Framework.Runtime.Application.Options;
using BindOpen.Framework.Runtime.Application.Services;
using BindOpen.Framework.Runtime.Application.Settings;
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
        /// Adds a BindOpen default bot service.
        /// </summary>
        /// <param name="services">The collection of services to populate.</param>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <returns></returns>
        public static IServiceCollection AddBindOpenDefaultBot(
            this IServiceCollection services,
            Action<ITBotOptions<DefaultBotSettings>> setupAction = null)
        {
            services.AddSingleton<IBot>(_ => BotFactory.CreateBindOpenDefaultBot(setupAction));

            return services;
        }

        /// <summary>
        /// Adds a BindOpen default bot service.
        /// </summary>
        /// <param name="provider">The service provider to consider.</param>
        /// <returns></returns>
        public static IBot GetBindOpenDefaultBot(this IServiceProvider provider)
        {
            return provider?.GetService<IBot>();
        }

        /// <summary>
        /// Adds a BindOpen bot service.
        /// </summary>
        /// <param name="services">The collection of services to populate.</param>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <returns></returns>
        public static IServiceCollection AddBindOpenBot<T>(
            this IServiceCollection services,
            Action<ITBotOptions<T>> setupAction = null)
            where T : class, IBotSettings, new()
        {
            services.AddSingleton<IBot>(_ => BotFactory.CreateBindOpenBot<T>(setupAction));

            return services;
        }

        /// <summary>
        /// Adds a BindOpen bot service.
        /// </summary>
        /// <typeparam name="THost">The class of bot to consider.</typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="services">The collection of services to populate.</param>
        /// <param name="setupAction">The setup action to consider.</param>
        /// <returns></returns>
        public static IServiceCollection AddBindOpenBot<THost, T>(
            this IServiceCollection services,
            Action<ITBotOptions<T>> setupAction = null)
            where THost : TBot<T>, new()
            where T : class, IBotSettings, new()
        {
            services.AddSingleton<IBot, THost>(_ => BotFactory.CreateBindOpenBot<THost, T>(setupAction));

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
            Func<IBotSettings, IBaseSettings> funcSettings = null)
            where T : IBotService, IBoted, new()
        {
            services.AddSingleton<ITBotServiceOptions<T>>(_ => new TBotServiceOptions<T>(loggers, funcSettings));
            services.AddHostedService<TBdoHostedService<T>>();

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
            Func<IBotSettings, IBaseSettings> funcSettings = null)
            where T : IBotService, IBoted, new()
        {
            services.AddTransient<ITBotServiceOptions<T>>(_ => new TBotServiceOptions<T>(loggers, funcSettings));

            return services;
        }
    }
}