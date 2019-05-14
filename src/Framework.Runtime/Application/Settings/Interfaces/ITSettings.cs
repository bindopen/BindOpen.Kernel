using BindOpen.Framework.Core.Application.Configuration;

namespace BindOpen.Framework.Runtime.Application.Settings
{
    public interface ITSettings<Q> : IBaseSettings
        where Q : class, IBaseConfiguration, new()
    {
        new Q Configuration { get; }
    }
}