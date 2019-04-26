using BindOpen.Framework.Core.Application.Configuration;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Runtime.Application.Configuration;

namespace BindOpen.Framework.Runtime.Application.Settings
{
    public interface ITBdoAppSettings<Q> : ITSettings<Q>, IBaseBdoAppSettings
        where Q : BdoAppConfiguration, new()
    {
        void SetAppScope(IRuntimeAppScope appScope);
    }
}