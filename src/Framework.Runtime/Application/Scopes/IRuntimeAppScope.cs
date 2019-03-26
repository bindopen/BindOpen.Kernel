using BindOpen.Framework.Runtime.Application.Services;

namespace BindOpen.Framework.Core.Application.Scopes
{
    /// <summary>
    /// This interface defines a runtime application scope.
    /// </summary>
    public interface IRuntimeAppScope : IAppScope
    {
        /// <summary>
        /// The connection service.
        /// </summary>
        IConnectionService ConnectionService { get; set; }
    }
}