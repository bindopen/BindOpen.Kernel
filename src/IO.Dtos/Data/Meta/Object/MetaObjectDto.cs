using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This class represents a catalog element that is an element whose elements are carriers.
    /// </summary>
    [XmlType("MetaObject", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    [XmlRoot(ElementName = "object", Namespace = "https://storage.bindopen.org/xsd/bindopen", IsNullable = false)]
    public class MetaObjectDto : MetaCompositeDto
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        // --------------------------------------------------

        /// <summary>
        /// The value of this instance.
        /// </summary>
        [JsonPropertyName("item")]
        [XmlElement("item.dictionary", Type = typeof(DictionaryDto))]
        [XmlElement("item.expression", Type = typeof(ExpressionDto))]
        [XmlElement("item.merger", Type = typeof(MergerDto))]
        public BdoItemDto Item { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the MetaObjectDto class.
        /// </summary>
        public MetaObjectDto()
        {
        }

        #endregion
    }
}
