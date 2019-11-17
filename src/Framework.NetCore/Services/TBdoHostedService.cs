using BindOpen.Framework.Runtime.Application.Bots;
using BindOpen.Framework.Runtime.Application.Options;
using BindOpen.Framework.Runtime.Application.Services;
using System.Threading;
using System.Threading.Tasks;

namespace BindOpen.Framework.NetCore.Services
{
    /// <summary>
    /// This service represents a BindOpen bot service that can be hosted in generic .Net core host.
    /// </summary>
    /// <typeparam name="T">The class of the BindOpen bot service to consider.</typeparam>
    public class TBdoHostedService<T> : ITBdoHostedService<T>
        where T : IBotService, IBoted, new()
    {
        private IBot _host;
        private T _service;

        /// <summary>
        /// The BindOpen bot of this instance.
        /// </summary>
        public IBot Host { get => _host; }

        /// <summary>
        /// Creates a new instance of the BdoHostedService class.
        /// </summary>
        public TBdoHostedService(
            IBot host,
            ITBotServiceOptions<T> options)
        {
            _host = host;

            _service = new T
            {
                Bot = host,
                Settings = options?.FuncSettings?.Invoke(host.Settings),
                Loggers = options?.Loggers
            };
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _service?.Start(_host?.Log);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Ends this instance.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _service?.End();

            return Task.CompletedTask;
        }

        /// <summary>
        /// Disposes this instance.
        /// </summary>
        public void Dispose()
        {
            _service.Dispose();
        }
    }
}
