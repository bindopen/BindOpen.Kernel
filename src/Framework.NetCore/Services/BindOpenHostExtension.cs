using BindOpen.Framework.Core.Extensions;
using BindOpen.Framework.Runtime.Application.Options;

namespace BindOpen.Framework.Databases.MSSqlServer.Extensions
{
    /// <summary>
    /// This class extends BindOpenHost.
    /// </summary>
    public static class BindOpenHostExtension
    {
        /// <summary>
        /// Adds MS SqlServer extension to the specified options.
        /// </summary>
        /// <param name="options">The options to consider.</param>
        /// <returns>Returns the connection of this instance.</returns>
        public static IAppHostOptions AddMSSqlServerExtension(this IAppHostOptions options)
        {
            options?.SetExtensions(new AppExtensionFilter("BindOpen.Framework.Databases.MSSqlServer"));
            return options;
        }
    }
}
