using System;
using BindOpen.Framework.Core.Application.Settings;
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
        ILogger[] Loggers { get; }

        Func<IAppSettings, IBaseSettings> FuncSettings { get; }
    }
}
