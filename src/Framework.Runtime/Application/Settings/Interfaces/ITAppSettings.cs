using BindOpen.Framework.Runtime.Application.Configuration;

namespace BindOpen.Framework.Runtime.Application.Settings
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="Q"></typeparam>
    public interface ITAppSettings<Q> : ITSettings<Q>, IAppSettings
        where Q : class, IAppConfiguration, new()
    {
    }
}