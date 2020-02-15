using BindOpen.Application.Configuration;

namespace BindOpen.Application.Settings
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