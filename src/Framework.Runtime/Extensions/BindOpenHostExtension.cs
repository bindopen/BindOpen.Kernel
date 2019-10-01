using BindOpen.Framework.Core.Extensions;
using BindOpen.Framework.Runtime.Application.Options;

namespace BindOpen.Framework.Runtime.Extensions.Connectors
{
    /// <summary>
    /// This class extends BindOpenHost.
    /// </summary>
    public static class BindOpenHostExtension
    {
        /// <summary>
        /// Gets the database connection of this instance.
        /// </summary>
        /// <returns>Returns the connection of this instance.</returns>
        public static void AddExtension_Messages(this IAppHostOptions options)
        {
            options?.SetExtensions(new AppExtensionFilter("BindOpen.Framework.Labs.Messages"));
        }

        /// <summary>
        /// Gets the database connection of this instance.
        /// </summary>
        /// <returns>Returns the connection of this instance.</returns>
        public static void AddExtension_DatabaseMSSqlServer(this IAppHostOptions options)
        {
            options?.SetExtensions(new AppExtensionFilter("BindOpen.Framework.Labs.Messages"));
        }

    }
}
