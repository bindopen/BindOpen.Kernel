using BindOpen.Framework.Runtime.Application.Configuration;

namespace BindOpen.Framework.Runtime.Application.Settings
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