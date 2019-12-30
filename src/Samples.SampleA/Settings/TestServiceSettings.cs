using System.Xml.Serialization;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Runtime.Application.Settings;

namespace BindOpen.Framework.Samples.SampleA.Settings
{
    /// <summary>
    /// This class represents a test service settings.
    /// </summary>
    public class TestServiceSettings : BdoDefaultSettings
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

        #endregion

        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TestServiceSettings class.
        /// </summary>
        public TestServiceSettings()
            : base()
        {
        }

        #endregion
    }
}
