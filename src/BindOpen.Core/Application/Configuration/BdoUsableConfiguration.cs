using BindOpen.Data.Elements;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BindOpen.Application.Configuration
{
    /// <summary>
    /// This class represents an usable configuration.
    /// </summary>
    [XmlType("UsableConfiguration", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot("usableConfiguration", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class BdoUsableConfiguration : BdoBaseConfiguration, IBdoUsableConfigurationDto
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
        /// The using configuration statement of this instance.
        /// </summary>
        [XmlIgnore()]
        public IBdoBaseConfiguration UsingConfiguration
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
        public BdoUsableConfiguration()
            : base()
        {
        }

        #endregion

        // -------------------------------------------------------------
        // MUTATORS
        // -------------------------------------------------------------

        #region Mutators

        /// <summary>
        /// Adds the specified elements into the specified group.
        /// </summary>
        /// <param name="groupId">The ID of the group.</param>
        /// <param name="items">The items to add.</param>
        /// <returns>Returns this instance.</returns>
        public new BdoUsableConfiguration AddGroup(string groupId, params IDataElement[] items)
            => base.AddGroup(groupId, items) as BdoUsableConfiguration;

        #endregion
    }
}
