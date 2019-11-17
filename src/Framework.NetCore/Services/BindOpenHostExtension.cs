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
        /// Adds Runtime extension to the specified options.
        /// </summary>
        /// <param name="options">The options to consider.</param>
        /// <returns>Returns the connection of this instance.</returns>
        public static void AddRuntimeExtension(this IBotOptions options)
        {
            options?.AddExtensions(new AppExtensionFilter("BindOpen.Framework.Runtime"));
        }

        /// <summary>
        /// Adds MS SqlServer extension to the specified options.
        /// </summary>
        /// <param name="options">The options to consider.</param>
        /// <returns>Returns the connection of this instance.</returns>
        public static IBotOptions AddMSSqlServerExtension(this IBotOptions options)
        {
            options?.AddExtensions(
                new AppExtensionFilter("BindOpen.Framework.Databases"),
                new AppExtensionFilter("BindOpen.Framework.Databases.MSSqlServer"));
            return options;
        }

        /// <summary>
        /// Adds PostgreSql extension to the specified options.
        /// </summary>
        /// <param name="options">The options to consider.</param>
        /// <returns>Returns the connection of this instance.</returns>
        public static IBotOptions AddPostgreSqlExtension(this IBotOptions options)
        {
            options?.AddExtensions(
                new AppExtensionFilter("BindOpen.Framework.Databases"),
                new AppExtensionFilter("BindOpen.Framework.Databases.PostgreSql"));
            return options;
        }
    }
}
