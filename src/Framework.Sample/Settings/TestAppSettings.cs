using System.Collections.Generic;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Runtime.Application.Settings;

namespace BindOpen.Framework.Sample.Settings
{
    /// <summary>
    /// This class represents a test application settings.
    /// </summary>
    public class TestAppSettings : DefaultAppSettings
    {
        // -------------------------------------------------------
        // PROPERTIES
        // -------------------------------------------------------

        #region Properties

        /// <summary>
        /// The test folder path of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name= "test.folderPath")]
        public string TestFolderPath { get; set; }

        /// <summary>
        /// The URIs of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "test.uris")]
        public Dictionary<string, object> Uris { get; set; }

        #endregion

        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TestAppSettings class.
        /// </summary>
        public TestAppSettings()
            : base()
        {
        }

        #endregion
    }
}
