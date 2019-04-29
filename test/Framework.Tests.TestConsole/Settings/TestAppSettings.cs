using System.Xml.Serialization;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Runtime.Application.Configuration;
using BindOpen.Framework.Runtime.Application.Settings;

namespace BindOpen.TestConsole.Settings
{
    /// <summary>
    /// This class represents a test application settings.
    /// </summary>
    public class TestAppSettings : TAppSettings<AppConfiguration>
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
