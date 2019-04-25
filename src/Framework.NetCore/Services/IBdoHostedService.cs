using System;
using Microsoft.Extensions.Hosting;

namespace BindOpen.Framework.NetCore.Services
{
    /// <summary>
    /// This interface defines a BindOpen application service that can be hosted in generic .Net core host.
    /// </summary>
    public interface IBdoHostedService : IHostedService, IDisposable
    {
    }
}
