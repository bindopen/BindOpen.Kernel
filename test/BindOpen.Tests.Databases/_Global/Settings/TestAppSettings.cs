using BindOpen.Extensions.Runtime;
using BindOpen.Application.Settings;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BindOpen.Tests.Databases.Settings
{
    /// <summary>
    /// This class represents a test application settings.
    /// </summary>
    public class TestAppSettings : BdoDefaultAppSettings
    {
        // -------------------------------------------------------
        // PROPERTIES
        // -------------------------------------------------------

        #region Properties

        /// <summary>
        /// The test folder path of this instance.
        /// </summary>
        [DetailProperty(Name= "test.folderPath")]
        public string TestFolderPath { get; set; }

        /// <summary>
        /// The URIs of this instance.
        /// </summary>
        [DetailProperty(Name = "test.uris")]
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
