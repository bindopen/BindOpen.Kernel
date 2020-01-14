using BindOpen.Framework.Data.Connections;
using BindOpen.Framework.System.Diagnostics;
using System;

namespace BindOpen.Framework.Application.Repositories
{
    /// <summary>
    /// This class represents a repository extension.
    /// </summary>
    public static class BdoDbRepositoryExtension
    {
        /// <summary>
        /// Executes the specified function.
        /// </summary>
        /// <typeparam name="Q"></typeparam>
        /// <param name="repository">The repository to consider</param>
        /// <param name="initializer"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IBdoLog UsingConnection(
            this IBdoDbRepository repository,
            Action<IBdoDbConnection> action)
        {
            var log = new BdoLog();

            if (repository != null)
            {
                using (IBdoDbConnection connection = repository.Connector?.CreateConnection(log))
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
