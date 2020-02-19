using BindOpen.Data.Connections;
using BindOpen.System.Diagnostics;
using System;

namespace BindOpen.Application.Services
{
    /// <summary>
    /// This class represents a database service extension.
    /// </summary>
    public static class BdoDbServiceExtension
    {
        /// <summary>
        /// Executes the specified function.
        /// </summary>
        /// <typeparam name="Q"></typeparam>
        /// <param name="repository">The connected service to consider</param>
        /// <param name="action">The action using the created connection and the current log to consider.</param>
        /// <returns></returns>
        public static void UsingDbConnection(
            this IBdoDbService repository,
            Action<IBdoDbConnection> action)
            => repository.UsingDbConnection((c, l) => action?.Invoke(c), null);

        /// <summary>
        /// Executes the specified function.
        /// </summary>
        /// <typeparam name="Q"></typeparam>
        /// <param name="repository">The connected service to consider</param>
        /// <param name="action">The action using the created connection and the current log to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns></returns>
        public static void UsingDbConnection(
            this IBdoDbService repository,
            Action<IBdoDbConnection, IBdoLog> action,
            IBdoLog log)
        {
            log = log ?? new BdoLog();

            if (repository != null)
            {
                using (IBdoDbConnection connection = repository.Connector?.CreateConnection(log))
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
