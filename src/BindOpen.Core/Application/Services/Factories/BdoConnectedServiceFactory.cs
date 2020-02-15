using BindOpen.Application.Scopes;
using BindOpen.Extensions.Runtime;
using BindOpen.System.Diagnostics;
using System;

namespace BindOpen.Application.Services
{
    /// <summary>
    /// This class represents a connected service factory.
    /// </summary>
    public static class BdoConnectedServiceFactory
    {
        /// <summary>
        /// Creates a new connected service.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="scope"></param>
        /// <param name="initializer"></param>
        /// <param name="log"></param>
        /// <returns>Returns the log of the operation.</returns>
        public static T Create<T>(
            this IBdoScope scope,
            Func<IBdoScope, IBdoLog, IBdoConnector> initializer,
            IBdoLog log = null)
            where T : IBdoConnectedService, new()
        {
            var repo = new T();

            var subLog = new BdoLog();
            repo.WithConnector(initializer?.Invoke(scope, subLog));
            subLog.AddEventsTo(log);


            if (subLog.HasErrorsOrExceptions())
            {
                return default;
            }

            return repo;
        }
    }
}
