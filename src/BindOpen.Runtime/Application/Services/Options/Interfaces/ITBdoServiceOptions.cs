using BindOpen.Application.Settings;
using Microsoft.Extensions.Logging;
using System;

namespace BindOpen.Application.Services
{
    /// <summary>
    /// This interface represents a service options provider.
    /// </summary>
    /// <remarks>The genericity was added to insure depency injection.</remarks>
    public interface ITBdoServiceOptions<SServ, SHost>
        where SServ : IBdoSettings
        where SHost : IBdoAppSettings
    {
        /// <summary>
        /// Logger.
        /// </summary>
        ILogger Logger { get; }

        /// <summary>
        /// Functional settings.
        /// </summary>
        Func<SHost, SServ> FuncSettingsConverter { get; }
    }
}
