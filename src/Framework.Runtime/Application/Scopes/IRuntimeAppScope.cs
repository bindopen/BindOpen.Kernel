using BindOpen.Framework.Runtime.Application.Services;

namespace BindOpen.Framework.Core.Application.Scopes
{
    public interface IRuntimeAppScope : IAppScope
    {
        IConnectionService ConnectionService { get; set; }
    }
}