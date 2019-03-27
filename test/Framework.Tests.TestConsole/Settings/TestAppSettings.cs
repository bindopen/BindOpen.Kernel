using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Runtime.Application.Settings;

namespace BindOpen.TestConsole.Settings
{
    /// <summary>
    /// This class represents a test application settings.
    /// </summary>
    [XmlType("TestAppSettings", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot("app.settings", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class TestAppSettings : AppSettings
    {
        // -------------------------------------------------------
        // PROPERTIES
        // -------------------------------------------------------

        #region Properties

        /// <summary>
        /// The resources folder path of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name= "test.folderPath")]
        public String TestFolderPath
        {
            get { return this.Get<String>(); }
            set { this.Set(value); }
        }

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

        /// <summary>
        /// Instantiates a new instance of the TestAppSettings class.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        public TestAppSettings(IAppScope appScope)
            : base(appScope)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the TestAppSettings class.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="usingFilePaths">The paths of the using files to consider.</param>
        public TestAppSettings(IAppScope appScope, params String[] usingFilePaths)
            : base(appScope, usingFilePaths)
        {
        }

        #endregion
    }
}
