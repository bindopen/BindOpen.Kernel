using System;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Application.Services.Data;
using BindOpen.Framework.Core.Data.Connections;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Runtime.Application.Services
{
    /// <summary>
    /// This class represents a data context service extension.
    /// </summary>
    public static class BdoDataServiceExtension
    {
        /// <summary>
        /// Executes the specified function.
        /// </summary>
        /// <typeparam name="Q"></typeparam>
        /// <param name="scope"></param>
        /// <param name="initializer"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IBdoLog Execute<Q>(
            this IBdoScope scope,
            Func<IBdoScope, IBdoLog, IBdoConnection> initializer,
            Action<Q, IBdoLog> action)
            where Q : IBdoDataService, new()
        {
            IBdoLog log = new BdoLog();

            using (IBdoConnection connection = initializer?.Invoke(scope, log))
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
