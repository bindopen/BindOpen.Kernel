﻿using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Runtime.Application.Settings;

namespace BindOpen.Framework.Runtime.Application.Options
{
    /// <summary>
    /// This class represents an application host options.
    /// </summary>
    public class TAppHostOptions<T> : AppHostOptions, ITAppHostOptions<T>
        where T : class, IAppSettings, new()
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The settings.
        /// </summary>
        public new T Settings
        {
            get => base.Settings as T;
            set { base.Settings = value; }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the AppHostOptions class.
        /// </summary>
        public TAppHostOptions() : base()
        {
        }

        #endregion

        // ------------------------------------------
        // SETTERS
        // ------------------------------------------

        #region Usage

        /// <summary>
        /// Sets the specified settings file.
        /// </summary>
        /// <param name="settingsFilePath">The path of the settings file to consider.</param>
        /// <returns>Returns this instance.</returns>
        public ITAppHostOptions<T> SetSettingsFile(string settingsFilePath = null)
        {
            this.DefineSettings();

            if (settingsFilePath!=null)
            {
                if (settingsFilePath.StartsWith(@"\") || settingsFilePath.EndsWith(@"\") || settingsFilePath.EndsWith(@"\.."))
                {
                    settingsFilePath = settingsFilePath.GetEndedString(@"\") + __DefaultSettingsFileName;
                }

                this._settingsFilePath = settingsFilePath.ToPath();
            }

            return this;
        }

        /// <summary>
        /// Defines the specified settings.
        /// </summary>
        /// <param name="specificationSet">The set of data element specifcations to consider.</param>
        /// <returns>Returns this instance.</returns>
        public ITAppHostOptions<T> DefineSettings(IDataElementSpecSet specificationSet = null)
        {
            this.Settings = new T();
            this.SettingsSpecificationSet = specificationSet ?? new DataElementSpecSet();

            return this;
        }

        #endregion
    }
}