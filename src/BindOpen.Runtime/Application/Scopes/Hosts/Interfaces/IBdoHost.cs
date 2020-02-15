using BindOpen.Application.Security;
using BindOpen.Application.Services;

namespace BindOpen.Application.Scopes
{
    /// <summary>
    /// The interface defines the base bot.
    /// </summary>
    public interface IBdoHost : IBdoService, IBdoScope
    {
        /// <summary>
        /// Get the specified known path.
        /// </summary>
        /// <param name="pathKind">The kind of known path.</param>
        /// <returns>Returns the specified path.</returns>
        string GetKnownPath(BdoHostPathKind pathKind);

        /// <summary>
        /// The options.
        /// </summary>
        IBdoHostOptions HostOptions { get; }

        /// <summary>
        /// The scope to consider.
        /// </summary>
        IBdoScope Scope { get; }

        // Settings ----------------------------------

        /// <summary>
        /// Gets the specified credential.
        /// </summary>
        /// <param name="name">The name of the credential to consider.</param>
        /// <returns>Returns the specified credential.</returns>
        IApplicationCredential GetCredential(string name);
    }
}