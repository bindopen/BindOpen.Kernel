﻿using BindOpen.System.Logging;
using BindOpen.System.Hosting;
using BindOpen.System.Hosting.Hosts;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// This static class extends .Net core dependency injection namespace.
    /// </summary>
    public static partial class BindOpenServiceCollectionExtensions
    {
        // BindOpen host --------------------------

        /// <summary>
        /// Adds a BindOpen default service.
        /// </summary>
        /// <param key="services">The set of services to populate.</param>
        /// <param key="setupAction">The setup action to consider.</param>
        /// <returns></returns>
        public static IServiceCollection AddBindOpen(
            this IServiceCollection services,
            Action<IBdoHostOptions> setupAction = null,
            IBdoLog log = null)
        {
            var host = BdoHosting.NewHost(setupAction, log);
            services.AddSingleton<IBdoHost>(_ => host);

            return services;
        }
    }
}