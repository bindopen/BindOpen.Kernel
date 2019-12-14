using BindOpen.Framework.Runtime.Application.Configuration;

namespace BindOpen.Framework.Runtime.Application.Settings.Hosts
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
        IBdoAppConfiguration HostConfiguration { get; }

        #endregion
    }
}