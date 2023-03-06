using BindOpen.Data.Helpers;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This class represents a data key value.
    /// </summary>
    [XmlType("KeyValuePair", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    [XmlRoot(ElementName = "add.value", Namespace = "https://storage.bindopen.org/xsd/bindopen", IsNullable = false)]
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
        public string Value { get; set; }

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
