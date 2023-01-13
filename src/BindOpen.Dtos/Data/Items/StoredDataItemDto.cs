using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.MetaData.Items
{
    /// <summary>
    /// This class represents a stored data item.
    /// </summary>
    [XmlType("StoredDataItem", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot("storedDataItem", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
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
