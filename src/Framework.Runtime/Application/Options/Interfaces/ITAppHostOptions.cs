using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Runtime.Application.Settings;

namespace BindOpen.Framework.Runtime.Application.Options
{
    /// <summary>
    /// The interface defines the application host options.
    /// </summary>
    public interface ITAppHostOptions<T> : IAppHostOptions, IDataItem
        where T : class, IAppSettings, new()
    {
        /// <summary>
        /// The settings.
        /// </summary>
        new T Settings { get; set; }

        // Set -------------------------------------------

        /// <summary>
        /// Set the settings file.
        /// </summary>
        /// <typeparam name="T">The application settings class to consider.</typeparam>
        /// <param name="settingsFilePath">The path of the settings file.</param>
        /// <returns>Returns the application host option.</returns>
        ITAppHostOptions<T> SetSettingsFile(string settingsFilePath = null);

        ///// <summary>
        ///// Define the specified settings.
        ///// </summary>
        ///// <param name="specificationSet">The set of data element specifcations to consider.</param>
        ///// <returns>Returns the application host option.</returns>
        //ITAppHostOptions<Q> DefineUserSettings(IDataElementSpecSet specificationSet = null);

        /// <summary>
        /// Define the specified settings.
        /// </summary>
        /// <typeparam name="T">The application settings class to consider.</typeparam>
        /// <param name="specificationSet">The set of data element specifcations to consider.</param>
        /// <returns>Returns the application host option.</returns>
        ITAppHostOptions<T> DefineSettings(IDataElementSpecSet specificationSet = null);
    }
}