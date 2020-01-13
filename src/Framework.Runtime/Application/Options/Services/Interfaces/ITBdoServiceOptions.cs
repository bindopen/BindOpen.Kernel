using BindOpen.Framework.System.Diagnostics.Loggers;
using BindOpen.Framework.Application.Settings;
using System;
using System.Collections.Generic;

namespace BindOpen.Framework.Application.Options
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
        List<IBdoLogger> Loggers { get; }

        /// <summary>
        /// Functional settings.
        /// </summary>
        Func<SHost, SServ> FuncSettingsConverter { get; }
    }
}
