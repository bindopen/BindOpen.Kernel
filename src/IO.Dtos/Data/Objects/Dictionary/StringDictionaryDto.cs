using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a string dictionary item.
    /// </summary>
    /// <example>Titles, Descriptions.</example>
    /// <seealso cref="KeyValuePairDto"/>
    [XmlType("Dictionary", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    [XmlRoot(ElementName = "dictionary", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel", IsNullable = false)]
    public class StringDictionaryDto : IBdoDto
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The expression of this instance.
        /// </summary>
        [JsonPropertyName("id")]
        [XmlAttribute("id")]
        public string Identifier { get; set; }

        /// <summary>
        /// The values of this instance.
        /// </summary>
        [JsonPropertyName("values")]
        [XmlElement("add")]
        public List<KeyValuePairDto> Values { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the StringDictionaryDto class. 
        /// </summary>
        public StringDictionaryDto()
        {
        }

        #endregion
    }

}
