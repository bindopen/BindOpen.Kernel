using BindOpen.Framework.System.Diagnostics.Loggers;
using BindOpen.Framework.Application.Settings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Framework.Application.Options
{
    /// <summary>
    /// This class represents a service options provider.
    /// </summary>
    /// <remarks>The genericity was added to insure depency injection.</remarks>
    public class TBdoServiceOptions<SServ, SHost> : ITBdoServiceOptions<SServ, SHost>
        where SServ : class, IBdoSettings, new()
        where SHost : IBdoAppSettings
    {
        // ------------------------------------------------------
        // PROPERTIES
        // ------------------------------------------------------

        #region Properties

        /// <summary>
        /// The loggers of this instance.
        /// </summary>
        public List<IBdoLogger> Loggers { get; }

        /// <summary>
        /// The settings function of this instance.
        /// </summary>
        public Func<SHost, SServ> FuncSettingsConverter { get; }

        #endregion

        // ------------------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TBdoHostServiceOptions class.
        /// </summary>
        /// <param name="loggers">The loggers to consider.</param>
        /// <param name="funcSettingsConverter">The settings converter function to consider.</param>
        public TBdoServiceOptions(
            IBdoLogger[] loggers = null,
            Func<SHost, SServ> funcSettingsConverter = null)
        {
            Loggers = loggers?.ToList();
            FuncSettingsConverter = funcSettingsConverter;
        }

        #endregion
    }
}
