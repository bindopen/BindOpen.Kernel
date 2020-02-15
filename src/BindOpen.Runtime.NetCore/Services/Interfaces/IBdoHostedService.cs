using Microsoft.Extensions.Hosting;
using System;

namespace BindOpen.Application.Services
{
    /// <summary>
    /// This interface defines a BindOpen bot service that can be hosted in generic .Net core host.
    /// </summary>
    public interface IBdoHostedService : IHostedService, IDisposable, IBdoHosted
    {
    }
}
