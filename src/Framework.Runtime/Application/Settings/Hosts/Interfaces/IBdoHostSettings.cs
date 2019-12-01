using BindOpen.Framework.Runtime.Application.Configuration;

namespace BindOpen.Framework.Runtime.Application.Settings.Hosts
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoHostSettings : IBdoSettings
    {
        // -------------------------------------------------------
        // PROPERTIES
        // -------------------------------------------------------

        #region Properties

        /// <summary>
        /// The configuration of the instance.
        /// </summary>
        IBdoHostConfiguration HostConfiguration { get;  }

        #endregion
    }
}