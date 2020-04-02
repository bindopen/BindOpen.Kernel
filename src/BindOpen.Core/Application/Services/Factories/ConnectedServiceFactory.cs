using BindOpen.Extensions.Runtime;
using BindOpen.System.Diagnostics;

namespace BindOpen.Application.Services
{
    /// <summary>
    /// This class represents a connected service factory.
    /// </summary>
    public static class ConnectedServiceFactory
    {
        /// <summary>
        /// Creates a new connected service.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connector"></param>
        /// <param name="log"></param>
        /// <returns>Returns the log of the operation.</returns>
        public static T CreateConnectedService<T>(
            IBdoConnector connector,
            IBdoLog log = null)
            where T : IBdoConnectedService, new()
        {
            var service = new T();

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
