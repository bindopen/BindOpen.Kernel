using BindOpen.Framework.Runtime.Application.Hosts;
using BindOpen.Framework.Runtime.Application.Services;
using BindOpen.Framework.Runtime.Application.Settings;
using BindOpen.Framework.Runtime.Application.Settings.Hosts;
using Microsoft.Extensions.Hosting;
using System;

namespace BindOpen.Framework.NetCore.Services
{
    /// <summary>
    /// This interface defines a BindOpen bot service that can be hosted in generic .Net core host.
    /// </summary>
    public interface ITHostedService<Serv, SServ, SHost> : IHostedService, IDisposable
        where Serv : ITBdoService<SServ>, IBdoHosted, new()
        where SServ : class, IBdoSettings, new()
        where SHost : IBdoHostSettings
    {
        /// <summary>
        /// 
        /// </summary>
        IBdoHost Host { get; }
    }
}
