using System;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Application.Services
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
        /// <param name="function"></param>
        /// <returns></returns>
        public static Log Execute<Q>(this Q service, Action<Q> function)
            where Q : IDataService
        {
            Log log = new Log();

            try
            {
                if (service == null)
                {
                    log.AddError(
                        "Service missing",
                        description: "Could not execute action in context as the connection is missing");
                }
                else if (service.Connection == null)
                {
                    log.AddError(
                        "Connection missing",
                        description: "Could not execute action in context as the connection is missing");
                }
                else
                {
                    function?.Invoke(service);
                }
            }
            catch (Exception ex)
            {
                log.AddException(ex.ToString());
            }

            return log;
        }
    }
}
