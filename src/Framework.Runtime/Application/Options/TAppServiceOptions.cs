using System;
using BindOpen.Framework.Core.Application.Settings;
using BindOpen.Framework.Core.System.Diagnostics.Loggers;
using BindOpen.Framework.Runtime.Application.Services;
using BindOpen.Framework.Runtime.Application.Settings;

namespace BindOpen.Framework.Runtime.Application.Options
{
    /// <summary>
    /// This class represents loggers.
    /// </summary>
    public class TAppServiceOptions<T> : ITAppServiceOptions<T>
        where T : IAppService, new()
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
        public Func<IAppSettings, IBaseSettings> FuncSettings { get; }

        #endregion

        // ------------------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the Logger class.
        /// </summary>
        public TAppServiceOptions(
            ILogger[] loggers = null,
            Func<IAppSettings, IBaseSettings> funcSettings = null)
        {
            Loggers = loggers;
            FuncSettings = funcSettings;
        }

        #endregion
    }
}
