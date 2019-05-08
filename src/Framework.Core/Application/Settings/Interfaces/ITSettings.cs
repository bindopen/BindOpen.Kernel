using BindOpen.Framework.Core.Application.Configuration;

namespace BindOpen.Framework.Core.Application.Settings
{
    public interface ITSettings<Q> : IBaseSettings
        where Q : class, IBaseConfiguration, new()
    {
        new Q Configuration { get; }
    }
}