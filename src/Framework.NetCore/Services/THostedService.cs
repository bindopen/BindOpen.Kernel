using BindOpen.Framework.Runtime.Application.Hosts;
using BindOpen.Framework.Runtime.Application.Options;
using BindOpen.Framework.Runtime.Application.Services;
using BindOpen.Framework.Runtime.Application.Settings;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BindOpen.Framework.NetCore.Services
{
    /// <summary>
    /// This service represents a BindOpen bot service that can be hosted in generic .Net core host.
    /// </summary>
    /// <typeparam name="Serv"></typeparam>
    /// <typeparam name="SServ"></typeparam>
    /// <typeparam name="SHost"></typeparam>
    public class THostedService<Serv, SServ, SHost> : ITHostedService<Serv, SServ, SHost>
        where Serv : ITBdoService<SServ>, IBdoHosted, new()
        where SServ : class, IBdoSettings, new()
        where SHost : IBdoAppSettings
    {
        private IBdoHost _host;
        private Serv _service;

        /// <summary>
        /// The BindOpen bot of this instance.
        /// </summary>
        public IBdoHost Host { get => _host; }

        /// <summary>
        /// Creates a new instance of the BdoHostedService class.
        /// </summary>
        public THostedService(
            IBdoHost host,
            ITBdoServiceOptions<SServ, SHost> options = null)
        {
            _host = host;

            _service = new Serv
            {
                Host = host,
                Settings = options?.FuncSettingsConverter.Invoke((SHost)host.HostOptions?.AppSettings),
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
            _service?.Start();

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
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // ------------------------------------------
        // IDISPOSABLE METHODS
        // ------------------------------------------

        #region IDisposable_Methods

        /// <summary>
        /// Disposes this instance. 
        /// </summary>
        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                _service.Dispose();
            }
        }

        #endregion

    }
}
