using BindOpen.Framework.Core.System.Diagnostics.Loggers;
using BindOpen.Framework.Runtime.Application.Settings;
using BindOpen.Framework.Runtime.Application.Settings.Hosts;
using System;

namespace BindOpen.Framework.Runtime.Application.Options.Services
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
        /// Loggers.
        /// </summary>
        IBdoLogger[] Loggers { get; }

        /// <summary>
        /// Functional settings.
        /// </summary>
        Func<SHost, SServ> FuncSettingsConverter { get; }
    }
}
