using BindOpen.Data.Connections;
using BindOpen.System.Diagnostics;
using System;

namespace BindOpen.Application.Services
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
        /// <param name="action">The action using the created connection and the current log to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns></returns>
        public static void UsingConnection(
            this IBdoConnectedService repository,
            Action<IBdoConnection> action)
            => repository.UsingConnection((c, l) => action?.Invoke(c), null);

        /// <summary>
        /// Executes the specified function.
        /// </summary>
        /// <typeparam name="Q"></typeparam>
        /// <param name="repository">The connected service to consider</param>
        /// <param name="action">The action using the created connection and the current log to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns></returns>
        public static void UsingConnection(
            this IBdoConnectedService repository,
            Action<IBdoConnection, IBdoLog> action,
            IBdoLog log = null)
        {
            log = log ?? new BdoLog();

            if (repository != null)
            {
                using (IBdoConnection connection = repository.Connector?.CreateConnection(log))
                {
                    if (!log.HasErrorsOrExceptions() && connection != null)
                    {
                        action?.Invoke(connection, log);
                    }
                }
            }
        }
    }
}
