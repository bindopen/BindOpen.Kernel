using BindOpen.Framework.Core.System.Diagnostics.Loggers;
using BindOpen.Framework.Runtime.Application.Services;
using BindOpen.Framework.Runtime.Application.Settings;
using System;

namespace BindOpen.Framework.Runtime.Application.Options
{
    /// <summary>
    /// This interface represents a logger.
    /// </summary>
    /// <remarks>The output format is YAML.</remarks>
    public interface ITBotServiceOptions<T>
        where T : IBotService, new()
    {
        /// <summary>
        /// Loggers.
        /// </summary>
        ILogger[] Loggers { get; }


        /// <summary>
        /// Functional settings.
        /// </summary>
        Func<IBotSettings, IBaseSettings> FuncSettings { get; }
    }
}
