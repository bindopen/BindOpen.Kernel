using BindOpen.Logging;
using BindOpen.Runtime.Hosting;
using BindOpen.Runtime.Hosts;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// This static class extends .Net core dependency injection namespace.
    /// </summary>
    public static partial class BindOpenServiceCollectionExtensions_Hosts
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
            Action<IBdoHostOptions> setupAction = null,
            IBdoLog log = null)
        {
            var host = BdoHosting.NewHost(setupAction, log);
            services.AddSingleton<IBdoHost>(_ => host);

            return services;
        }
    }
}