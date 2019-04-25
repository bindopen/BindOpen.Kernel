using BindOpen.Framework.Runtime.Application.Hosts.Options;

namespace BindOpen.Framework.Runtime.Application.Hosts
{
    /// <summary>
    /// The interface defines the application host.
    /// </summary>
    public interface IBaseBdoAppHost
    {
        /// <summary>
        /// Get the specified known path.
        /// </summary>
        /// <param name="pathKind">The kind of known path.</param>
        /// <returns>Returns the specified path.</returns>
        string GetKnownPath(ApplicationPathKind pathKind);

        /// <summary>
        /// The base options.
        /// </summary>
        IBaseBdoAppHostOptions BaseOptions { get; }
    }
}