using BindOpen.Data.Helpers;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a key value pair DTO.
    /// </summary>
    [XmlType("KeyValuePair", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    [XmlRoot(ElementName = "add.value", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel", IsNullable = false)]
    public class KeyValuePairDto
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The key of this instance.
        /// </summary>
        [JsonPropertyName("key")]
        [XmlAttribute("key")]
        [DefaultValue(StringHelper.__Star)]
        public string Key { get; set; } = StringHelper.__Star;

        /// <summary>
        /// The value of this instance.
        /// </summary>
        [JsonPropertyName("value")]
        [XmlText]
        public string Value { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of KeyValuePairDto class.
        /// </summary>
        public KeyValuePairDto()
        {
        }

        #endregion
    }
}
