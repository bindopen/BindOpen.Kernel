using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Runtime.Application.Settings;

namespace BindOpen.Framework.Runtime.Application.Options
{
    /// <summary>
    /// The interface defines the bot options.
    /// </summary>
    public interface ITBotOptions<T> : IBotOptions, IDataItem
        where T : class, IBotSettings, new()
    {
        /// <summary>
        /// The settings.
        /// </summary>
        new T Settings { get; set; }

        // Set -------------------------------------------

        /// <summary>
        /// Set the settings file.
        /// </summary>
        /// <param name="settingsFilePath">The path of the settings file.</param>
        /// <returns>Returns the bot option.</returns>
        ITBotOptions<T> SetSettingsFile(string settingsFilePath = null);

        /// <summary>
        /// Define the specified settings.
        /// </summary>
        /// <param name="specificationSet">The set of data element specifcations to consider.</param>
        /// <returns>Returns the bot option.</returns>
        ITBotOptions<T> DefineSettings(IDataElementSpecSet specificationSet = null);
    }
}