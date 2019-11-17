using System;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Processing;
using BindOpen.Framework.Runtime.Application.Options;
using BindOpen.Framework.Runtime.Application.Security;
using BindOpen.Framework.Runtime.Application.Services;
using BindOpen.Framework.Runtime.Application.Settings;

namespace BindOpen.Framework.Runtime.Application.Bots
{
    /// <summary>
    /// The interface defines the bot.
    /// </summary>
    public interface ITBot<T> : ITBotService<T>, IBot
        where T : class, IBotSettings, new()
    {
        // Execution ---------------------------------

        /// <summary>
        /// The options.
        /// </summary>
        new ITBotOptions<T> Options { get; set; }

        /// <summary>
        /// Configures the bot.
        /// </summary>
        /// <param name="setupOptions">The action to setup the bot.</param>
        /// <returns>Returns the bot.</returns>
        ITBot<T> Configure(Action<ITBotOptions<T>> setupOptions);

        // Process ----------------------------------

        /// <summary>
        /// Starts the process.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the bot to consider.</returns>
        new ITBot<T> Start(ILog log = null);

        /// <summary>
        /// Ends the process specifying the status.
        /// </summary>
        /// <param name="executionStatus">The execution status to apply.</param>
        /// <returns>Returns the bot to consider.</returns>
        new ITBot<T> End(ProcessExecutionStatus executionStatus = ProcessExecutionStatus.Stopped);

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

        /// <summary>
        /// Saves settings.
        /// </summary>
        void SaveSettings();
    }
}