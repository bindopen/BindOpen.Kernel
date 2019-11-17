using System;
using BindOpen.Framework.Core.Application.Services.Data;
using BindOpen.Framework.Core.Data.Connections;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Runtime.Application.Services
{
    /// <summary>
    /// This class represents a data context service extension.
    /// </summary>
    public static class DataServiceExtension
    {
        /// <summary>
        /// Executes the specified function.
        /// </summary>
        /// <typeparam name="Q"></typeparam>
        /// <param name="service"></param>
        /// <param name="initializer"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static ILog Execute<Q>(
            this IBotService service,
            Func<IBotService, ILog, IConnection> initializer,
            Action<Q, ILog> action)
            where Q : IDataService, new()
        {
            ILog log = new Log();

            using (IConnection connection = initializer?.Invoke(service, log))
            {
                if (!log.HasErrorsOrExceptions())
                {
                    using (Q dataService = new Q())
                    {
                        dataService.SetConnection(connection);

                        if (!log.HasErrorsOrExceptions())
                        {
                            action?.Invoke(dataService, log);
                        }
                    }
                }                
            }

            return log;
        }
    }
}
