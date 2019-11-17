using BindOpen.Framework.Core.System.Diagnostics.Loggers;
using BindOpen.Framework.Runtime.Application.Services;
using BindOpen.Framework.Runtime.Application.Settings;
using System;

namespace BindOpen.Framework.Runtime.Application.Options
{
    /// <summary>
    /// This class represents bot service options.
    /// </summary>
    public class TBotServiceOptions<T> : ITBotServiceOptions<T>
        where T : IBotService, new()
    {
        // ------------------------------------------------------
        // PROPERTIES
        // ------------------------------------------------------

        #region Properties

        /// <summary>
        /// The loggers of this instance.
        /// </summary>
        public ILogger[] Loggers { get; }

        /// <summary>
        /// The settings function of this instance.
        /// </summary>
        public Func<IBotSettings, IBaseSettings> FuncSettings { get; }

        #endregion

        // ------------------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the Logger class.
        /// </summary>
        public TBotServiceOptions(
            ILogger[] loggers = null,
            Func<IBotSettings, IBaseSettings> funcSettings = null)
        {
            Loggers = loggers;
            FuncSettings = funcSettings;
        }

        #endregion
    }
}
