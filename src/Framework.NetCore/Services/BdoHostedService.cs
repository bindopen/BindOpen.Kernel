﻿using System.Threading;
using System.Threading.Tasks;
using BindOpen.Framework.Runtime.Application.Configuration;
using BindOpen.Framework.Runtime.Application.Hosts;
using BindOpen.Framework.Runtime.Application.Services;

namespace BindOpen.Framework.NetCore.Services
{
    /// <summary>
    /// This service represents a BindOpen application service that can be hosted in generic .Net core host.
    /// </summary>
    /// <typeparam name="T">The class of the BindOpen application service to consider.</typeparam>
    public class BdoHostedService<T, Q> : IBdoHostedService
        where T : TBdoAppService<Q>, ITBdoAppHosted<Q>, new()
        where Q : BdoAppConfiguration, new()
    {
        private ITBdoAppHost<Q> _host;
        private T _bdoService;

        /// <summary>
        /// The BindOpen application host of this instance.
        /// </summary>
        public ITBdoAppHost<Q> Host { get => _host; }

        /// <summary>
        /// Creates a new instance of the BdoHostedService class.
        /// </summary>
        public BdoHostedService(ITBdoAppHost<Q> host)
        {
            _host = host;
            _bdoService = new T
            {
                Host = Host
            };
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _bdoService?.Start(_host?.Log);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Ends this instance.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _bdoService?.End();

            return Task.CompletedTask;
        }

        /// <summary>
        /// Disposes this instance.
        /// </summary>
        public void Dispose()
        {
            _bdoService.Dispose();
        }
    }
}