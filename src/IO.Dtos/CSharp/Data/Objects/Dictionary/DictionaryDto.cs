using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Kernel.Data
{
    /// <summary>
    /// This class represents a dico data item.
    /// </summary>
    /// <example>Titles, Descriptions.</example>
    /// <seealso cref="KeyValuePairDto"/>
    [XmlType("Dictionary", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    [XmlRoot(ElementName = "dictionary", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel", IsNullable = false)]
    public class DictionaryDto : BdoItemDto, IIdentified
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The ID of this instance.
        /// </summary>
        [JsonPropertyName("id")]
        [XmlElement("id")]
        public string Id { get; set; }

        /// <summary>
        /// Values of this instance.
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
        /// Instantiates a new instance of the DictionaryDto class. 
        /// </summary>
        public DictionaryDto()
        {
        }

        #endregion
    }

}
