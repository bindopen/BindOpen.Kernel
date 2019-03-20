using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Application.Configuration
{
    /// <summary>
    /// This class represents an usable configuration.
    /// </summary>
    [XmlType("UsableConfiguration", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot("usableConfiguration", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class UsableConfiguration : Configuration
    {
        // -------------------------------------------------------
        // VARIABLES
        // -------------------------------------------------------

        #region Variables

        #endregion

        // -------------------------------------------------------
        // PROPERTIES
        // -------------------------------------------------------

        #region Properties

        /// <summary>
        /// The using file paths of this instance.
        /// </summary>
        [XmlArray("usingFilePaths")]
        [XmlArrayItem("add")]
        public List<String> UsingFilePaths { get; set; } = new List<String>();

        /// <summary>
        /// Specification of the UsingFilePaths property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean UsingFilePathsSpecified
        {
            get
            {
                return UsingFilePaths != null && this.UsingFilePaths.Count > 0;
            }
        }

        /// <summary>
        /// The using configuration statement of this instance.
        /// </summary>
        [XmlIgnore()]
        public Configuration UsingConfiguration
        {
            get;
            set;
        }

        #endregion

        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the UsableConfiguration class.
        /// </summary>
        public UsableConfiguration()
            : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the UsableConfiguration class.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        public UsableConfiguration(IAppScope appScope)
            : base(appScope)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the UsableConfiguration class.
        /// </summary>
        /// <param name="filePath">The file path to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        public UsableConfiguration(string filePath, IAppScope appScope)
            : base(filePath, appScope)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the UsableConfiguration class.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="usingFilePaths">The paths of the using files to consider.</param>
        public UsableConfiguration(IAppScope appScope, params String[] usingFilePaths)
            : base(appScope)
        {
            this.UsingFilePaths = usingFilePaths?.ToList();
        }

        /// <summary>
        /// Instantiates a new instance of the UsableConfiguration class.
        /// </summary>
        /// <param name="filePath">The file path to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="usingFilePaths">The paths of the using files to consider.</param>
        public UsableConfiguration(string filePath, IAppScope appScope, params String[] usingFilePaths)
            : base(filePath, appScope)
        {
            this.UsingFilePaths = usingFilePaths?.ToList();
        }

        /// <summary>
        /// Instantiates a new instance of the UsableConfiguration class.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="items">The items to consider.</param>
        public UsableConfiguration(IAppScope appScope, params DataElement[] items) : base(appScope, items)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the UsableConfiguration class.
        /// </summary>
        /// <param name="filePath">The file path to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="items">The items to consider.</param>
        public UsableConfiguration(string filePath, IAppScope appScope, params DataElement[] items) : base(filePath, appScope, items)
        {
        }

        #endregion

        // -------------------------------------------------------------
        // LOADING / SAVING
        // -------------------------------------------------------------

        #region Loading_Saving

        /// <summary>
        /// Saves this instance.
        /// </summary>
        /// <param name="filePath">The file path to consider.</param>
        /// <param name="log">The output log.</param>
        /// <returns>True if this instance has been</returns>
        public override Boolean SaveXml(String filePath, Log log = null)
        {
            return (base.SaveXml(filePath, log));
        }

        /// <summary>
        /// Instantiates a new instance of UsableConfiguration class from a xml file.
        /// </summary>
        /// <typeparam name="T">The usable configuration class to consider.</typeparam>
        /// <param name="filePath">The file path to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="xmlSchemaSet">The XML schema set to consider for checking.</param>
        /// <param name="mustFileExist">Indicates whether the file must exist.</param>
        /// <returns>The Xml operation project defined in the Xml file.</returns>
        public new static T Load<T>(
            String filePath,
            Log log,
            IAppScope appScope,
            XmlSchemaSet xmlSchemaSet = null,
            Boolean mustFileExist = true) where T : UsableConfiguration, new()
        {
            T unionConfiguration = new T
            {
                AppScope = appScope
            };

            T topConfiguration = Configuration.Load<T>(filePath, log, appScope, xmlSchemaSet, mustFileExist) as T;
            if (topConfiguration!=null)
            {
                foreach (String usingFilePath in topConfiguration.UsingFilePaths)
                {
                    String completeUsingFilePath = (usingFilePath.Contains(":") ?
                        usingFilePath :
                        Path.GetDirectoryName(filePath).GetEndedString(@"\") + usingFilePath).ToPath();
                    if (DataItem.Load<T>(completeUsingFilePath, log, null, xmlSchemaSet, mustFileExist) is T usingConfiguration)
                        unionConfiguration.Update(usingConfiguration);
                }
            }

            unionConfiguration.CurrentFilePath = filePath;
            unionConfiguration.Update(topConfiguration);

            return unionConfiguration;
        }

        #endregion
    }
}
