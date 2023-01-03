using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This class represents a data key value.
    /// </summary>
    [XmlType("DataKeyValue", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "add.value", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class DataKeyValueDto
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// Key of this instance.
        /// </summary>
        [JsonPropertyName("key")]
        [XmlAttribute("key")]
        [DefaultValue("*")]
        public string Key { get; set; } = "*";

        /// <summary>
        /// Content of this instance.
        /// </summary>
        [JsonPropertyName("content")]
        [XmlText]
        public string Content { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of DataKeyValueDto class.
        /// </summary>
        public DataKeyValueDto()
        {
        }

        #endregion
    }
}
