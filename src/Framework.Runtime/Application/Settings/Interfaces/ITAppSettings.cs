using BindOpen.Framework.Core.Application.Settings;
using BindOpen.Framework.Runtime.Application.Configuration;

namespace BindOpen.Framework.Runtime.Application.Settings
{
    public interface ITAppSettings<Q> : ITSettings<Q>, IAppSettings
        where Q : class, IAppConfiguration, new()
    {
    }
}