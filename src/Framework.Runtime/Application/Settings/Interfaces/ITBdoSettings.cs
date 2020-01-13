using BindOpen.Framework.Application.Configuration;

namespace BindOpen.Framework.Application.Settings
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="Q"></typeparam>
    public interface ITBdoSettings<Q> : IBdoSettings
        where Q : IBdoBaseConfiguration, new()
    {
        /// <summary>
        /// 
        /// </summary>
        Q Configuration { get; }
    }
}