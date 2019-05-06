using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Elements;

namespace BindOpen.Framework.Core.Application.Configuration
{
    /// <summary>
    /// This class represents an usable configuration.
    /// </summary>
    [XmlType("UsableConfiguration", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot("usableConfiguration", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class UsableConfiguration : Configuration, IUsableConfigurationDto
    {
        // -------------------------------------------------------
        // PROPERTIES
        // -------------------------------------------------------

        #region Properties

        /// <summary>
        /// The using file paths of this instance.
        /// </summary>
        [XmlArray("usingFilePaths")]
        [XmlArrayItem("add")]
        public List<string> UsingFilePaths { get; set; } = new List<string>();

        /// <summary>
        /// Specification of the UsingFilePaths property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool UsingFilePathsSpecified
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
        public IConfiguration UsingConfiguration
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
        public UsableConfiguration(string filePath)
            : base(filePath)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the UsableConfiguration class.
        /// </summary>
        /// <param name="filePath">The file path to consider.</param>
        /// <param name="items">The items to consider.</param>
        public UsableConfiguration(string filePath, params IDataElement[] items) : this(filePath, null, items)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the UsableConfiguration class.
        /// </summary>
        /// <param name="filePath">The file path to consider.</param>
        /// <param name="usingFilePaths">The paths of the using files to consider.</param>
        /// <param name="items">The items to consider.</param>
        public UsableConfiguration(string filePath, string[] usingFilePaths, params IDataElement[] items)
            : base(filePath, items)
        {
            this.UsingFilePaths = usingFilePaths?.ToList();
        }

        #endregion

        /// <summary>
        /// Adds the specified elements into the specified group.
        /// </summary>
        /// <param name="groupId">The ID of the group.</param>
        /// <param name="items">The items to add.</param>
        /// <returns>Returns this instance.</returns>
        public new UsableConfiguration AddGroup(string groupId, params IDataElement[] items) => base.AddGroup(groupId, items) as UsableConfiguration;
    }
}
