using System;
using BindOpen.Framework.Core.System.Diagnostics.Loggers;
using BindOpen.Framework.Runtime.Application.Services;
using BindOpen.Framework.Runtime.Application.Settings;

namespace BindOpen.Framework.Runtime.Application.Options
{
    /// <summary>
    /// This interface represents a logger.
    /// </summary>
    /// <remarks>The output format is YAML.</remarks>
    public interface ITAppServiceOptions<T>
        where T : IAppService, new()
    {
        /// <summary>
        /// Loggers.
        /// </summary>
        ILogger[] Loggers { get; }


        /// <summary>
        /// Functional settings.
        /// </summary>
        Func<IAppSettings, IBaseSettings> FuncSettings { get; }
    }
}
