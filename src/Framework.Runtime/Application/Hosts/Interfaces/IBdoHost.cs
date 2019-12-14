using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Runtime.Application.Options;
using BindOpen.Framework.Runtime.Application.Security;
using BindOpen.Framework.Runtime.Application.Services;

namespace BindOpen.Framework.Runtime.Application.Hosts
{
    /// <summary>
    /// The interface defines the base bot.
    /// </summary>
    public interface IBdoHost : IBdoService
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
        /// The application settings.
        /// </summary>
        IBdoScope Scope { get; }

        // Settings ----------------------------------

        /// <summary>
        /// The set of user settings.
        /// </summary>
        IDataElementSet UserSettingsSet { get; set; }

        /// <summary>
        /// Gets the specified credential.
        /// </summary>
        /// <param name="name">The name of the credential to consider.</param>
        /// <returns>Returns the specified credential.</returns>
        IApplicationCredential GetCredential(string name);
    }
}