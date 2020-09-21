using BindOpen.Application.Services;
using BindOpen.Extensions.Runtime;
using BindOpen.System.Diagnostics;

namespace BindOpen.Application.Scopes
{
    /// <summary>
    /// This class represents a connected service factory.
    /// </summary>
    public static class BdoServiceFactory
    {
        /// <summary>
        /// Creates a new service.
        /// </summary>
        /// <param name="scope"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>Returns the log of the operation.</returns>
        public static T CreateService<T>(
            this IBdoScope scope)
            where T : IBdoConnectedService, new()
        {
            var service = new T();
            service.WithScope(scope);

            return service;
        }

        /// <summary>
        /// Creates a new connected service.
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="connector"></param>
        /// <param name="log"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>Returns the log of the operation.</returns>
        public static T CreateConnected<T>(
            this IBdoScope scope,
            IBdoConnector connector,
            IBdoLog log = null)
            where T : IBdoConnectedService, new()
        {
            var service = scope.CreateService<T>();

            var subLog = new BdoLog();
            service.WithConnector(connector);
            subLog.AddEventsTo(log);


            if (subLog.HasErrorsOrExceptions())
            {
                return default;
            }

            return service;
        }
    }
}
