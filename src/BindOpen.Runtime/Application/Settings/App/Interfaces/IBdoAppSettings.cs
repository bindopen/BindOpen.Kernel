using BindOpen.Application.Configuration;

namespace BindOpen.Application.Settings
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoAppSettings : IBdoSettings
    {
        // -------------------------------------------------------
        // PROPERTIES
        // -------------------------------------------------------

        #region Properties

        /// <summary>
        /// The configuration of the instance.
        /// </summary>
        IBdoAppConfiguration AppConfiguration { get; }

        #endregion
    }
}