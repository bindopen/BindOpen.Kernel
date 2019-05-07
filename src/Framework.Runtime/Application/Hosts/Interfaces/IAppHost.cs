using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Runtime.Application.Options;
using BindOpen.Framework.Runtime.Application.Services;

namespace BindOpen.Framework.Runtime.Application.Hosts
{
    /// <summary>
    /// The interface defines the base application host.
    /// </summary>
    public interface IAppHost : IAppService
    {
        /// <summary>
        /// The application settings.
        /// </summary>
        new IAppHostScope Scope { get; }

        /// <summary>
        /// Get the specified known path.
        /// </summary>
        /// <param name="pathKind">The kind of known path.</param>
        /// <returns>Returns the specified path.</returns>
        string GetKnownPath(ApplicationPathKind pathKind);

        /// <summary>
        /// The base options.
        /// </summary>
        IBaseAppHostOptions BaseOptions { get; }
    }
}