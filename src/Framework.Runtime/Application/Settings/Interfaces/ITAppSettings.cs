using BindOpen.Framework.Core.Application.Configuration;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Runtime.Application.Configuration;

namespace BindOpen.Framework.Runtime.Application.Settings
{
    public interface ITAppSettings<Q> : ITSettings<Q>, IBaseAppSettings
        where Q : IAppConfiguration, new()
    {
        void SetAppScope(IAppHostScope appScope);
    }
}