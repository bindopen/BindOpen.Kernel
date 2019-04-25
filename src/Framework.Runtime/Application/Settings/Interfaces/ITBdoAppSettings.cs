using BindOpen.Framework.Core.Application.Configuration;
using BindOpen.Framework.Runtime.Application.Configuration;

namespace BindOpen.Framework.Runtime.Application.Settings
{
    public interface ITBdoAppSettings<Q> : ITSettings<Q>, IBaseBdoAppSettings
        where Q : BdoAppConfiguration, new()
    {
    }
}