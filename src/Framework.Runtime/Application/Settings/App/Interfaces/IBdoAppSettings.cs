using BindOpen.Framework.Application.Configuration;

namespace BindOpen.Framework.Application.Settings
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