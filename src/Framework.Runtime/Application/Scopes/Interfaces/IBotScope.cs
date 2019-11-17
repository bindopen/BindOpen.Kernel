using BindOpen.Framework.Runtime.Application.Services;

namespace BindOpen.Framework.Core.Application.Scopes
{
    /// <summary>
    /// This interface defines a bot scope.
    /// </summary>
    public interface IBotScope : IAppScope
    {
        /// <summary>
        /// The connection service.
        /// </summary>
        IConnectionService ConnectionService { get; set; }
    }
}