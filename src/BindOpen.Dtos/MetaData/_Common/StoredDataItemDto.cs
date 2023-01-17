using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.MetaData
{
    /// <summary>
    /// This class represents a stored data item.
    /// </summary>
    [XmlType("StoredDataItem", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot("storedDataItem", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class StoredDataItemDto : IDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Creation date of this instance.
        /// </summary>
        [JsonPropertyName("creationDate")]
        [XmlElement("creationDate")]
        [DefaultValue("")]
        public string CreationDate { get; set; }

        /// <summary>
        /// Last modification date of this instance.
        /// </summary>
        [JsonPropertyName("lastModificationDate")]
        [XmlElement("lastModificationDate")]
        [DefaultValue("")]
        public string LastModificationDate { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the StoredDataItemDto class.
        /// </summary>
        public StoredDataItemDto()
        {
        }

        #endregion
    }
}
