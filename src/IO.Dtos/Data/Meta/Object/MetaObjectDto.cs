using BindOpen.System.Scoping.Script;
using System.Collections.Generic;
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
    public class MetaObjectDto : MetaDataDto
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
        [XmlElement("item.datasource", Type = typeof(DatasourceDto))]
        [XmlElement("item.dictionary", Type = typeof(DictionaryDto))]
        [XmlElement("item.expression", Type = typeof(ExpressionDto))]
        [XmlElement("item.filter", Type = typeof(FilterDto))]
        public BdoItemDto Item { get; set; }

        /// <summary>
        /// The elements of this instance.
        /// </summary>
        [JsonPropertyName("items")]
        [XmlElement("set", Type = typeof(MetaSetDto))]
        [XmlElement("object", Type = typeof(MetaObjectDto))]
        [XmlElement("scalar", Type = typeof(MetaScalarDto))]
        [XmlElement("word", Type = typeof(ScriptwordDto))]
        public List<MetaDataDto> MetaItems { get; set; }

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
