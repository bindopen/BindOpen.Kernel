using System;
using BindOpen.Framework.Runtime.Application.Bots;
using BindOpen.Framework.Runtime.Application.Services;
using Microsoft.Extensions.Hosting;

namespace BindOpen.Framework.NetCore.Services
{
    /// <summary>
    /// This interface defines a BindOpen bot service that can be hosted in generic .Net core host.
    /// </summary>
    public interface ITBdoHostedService<T> : IHostedService, IDisposable
        where T : IBotService, IBoted, new()
    {
    }
}
