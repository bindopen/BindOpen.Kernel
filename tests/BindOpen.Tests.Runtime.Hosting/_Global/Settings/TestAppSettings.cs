﻿using BindOpen.Data.Meta;
using BindOpen.Runtime.Settings;
using System.Collections.Generic;

namespace BindOpen.Tests.Core.Settings
{
    /// <summary>
    /// This class represents a test application settings.
    /// </summary>
    public class TestAppSettings : BdoSettings
    {
        // -------------------------------------------------------
        // PROPERTIES
        // -------------------------------------------------------

        #region Properties

        /// <summary>
        /// The test folder path of this instance.
        /// </summary>
        [BdoData(Name = "test.folderPath")]
        public string TestFolderPath { get; set; }

        /// <summary>
        /// The URIs of this instance.
        /// </summary>
        [BdoData(Name = "test.uris")]
        public Dictionary<string, string> Uris { get; set; }

        #endregion

        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TestAppSettings class.
        /// </summary>
        public TestAppSettings() : base()
        {
        }

        #endregion
    }
}