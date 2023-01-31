using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using BindOpen.Data;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This class represents a data key value.
    /// </summary>
    [XmlType("KeyValuePair", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot(ElementName = "add.value", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class KeyValuePairDto
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
        [DefaultValue(StringHelper.__Star)]
        public string Key { get; set; } = StringHelper.__Star;

        /// <summary>
        /// Content of this instance.
        /// </summary>
        [JsonPropertyName("value")]
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
        public KeyValuePairDto()
        {
        }

        #endregion
    }
}
