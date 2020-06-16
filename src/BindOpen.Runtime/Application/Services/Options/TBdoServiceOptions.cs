using BindOpen.Application.Settings;
using Microsoft.Extensions.Logging;
using System;

namespace BindOpen.Application.Services
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
        /// The logger of this instance.
        /// </summary>
        public ILogger Logger { get; }

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
        /// <param name="logger">The logger to consider.</param>
        /// <param name="funcSettingsConverter">The settings converter function to consider.</param>
        public TBdoServiceOptions(
            ILogger logger = null,
            Func<SHost, SServ> funcSettingsConverter = null)
        {
            Logger = logger;
            FuncSettingsConverter = funcSettingsConverter;
        }

        #endregion
    }
}
