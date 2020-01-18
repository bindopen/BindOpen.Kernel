using BindOpen.Framework.Data.Connections;
using BindOpen.Framework.System.Diagnostics;
using System;

namespace BindOpen.Framework.Application.Services
{
    /// <summary>
    /// This class represents a connected service extension.
    /// </summary>
    public static class BdoConnectedServiceExtension
    {
        /// <summary>
        /// Executes the specified function.
        /// </summary>
        /// <typeparam name="Q"></typeparam>
        /// <param name="repository">The connected service to consider</param>
        /// <param name="initializer"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IBdoLog UsingConnection(
            this IBdoConnectedService repository,
            Action<IBdoConnection> action)
        {
            var log = new BdoLog();

            if (repository != null)
            {
                using (IBdoConnection connection = repository.Connector?.CreateConnection(log))
                {
                    if (!log.HasErrorsOrExceptions())
                    {
                        action?.Invoke(connection);
                    }
                }
            }

            return log;
        }
    }
}
