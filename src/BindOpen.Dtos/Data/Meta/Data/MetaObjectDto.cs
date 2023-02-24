using BindOpen.Data.Items;
using BindOpen.Extensions.Scripting;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a catalog element that is an element whose elements are carriers.
    /// </summary>
    [XmlType("MetaObject", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot(ElementName = "object", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class MetaObjectDto : MetaDataDto
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        // Sub elements

        /// <summary>
        /// The sub  mode of this instance.
        /// </summary>
        [JsonPropertyName("subDataSet")]
        [XmlElement("subDataSet")]
        public MetaSetDto SubDataSet { get; set; }

        /// <summary>
        /// The class full name of this instance.
        /// </summary>
        [JsonPropertyName("class")]
        [XmlAttribute("class")]
        [DefaultValue("")]
        public string ClassFullName { get; set; }

        /// <summary>
        /// The definition unique ID of this instance.
        /// </summary>
        [JsonPropertyName("definition")]
        [XmlAttribute("definition")]
        [DefaultValue("")]
        public string DefinitionUniqueName { get; set; }

        // --------------------------------------------------

        /// <summary>
        /// The value of this instance.
        /// </summary>
        [JsonPropertyName("item")]
        [XmlElement("item.datasource", Type = typeof(DatasourceDto))]
        [XmlElement("item.dictionary", Type = typeof(DictionaryDto))]
        [XmlElement("item.expression", Type = typeof(ExpressionDto))]
        [XmlElement("item.filter", Type = typeof(StringFilterDto))]
        public BdoItemDto Item { get; set; }

        /// <summary>
        /// The elements of this instance.
        /// </summary>
        [JsonPropertyName("items")]
        [XmlElement("set", Type = typeof(MetaSetDto))]
        [XmlElement("object", Type = typeof(MetaObjectDto))]
        [XmlElement("scalar", Type = typeof(MetaScalarDto))]
        [XmlElement("scriptword", Type = typeof(ScriptwordDto))]
        public List<MetaDataDto> MetaItems { get; set; }

        // Specification -----------------------

        /// <summary>
        /// The specification of this instance.
        /// </summary>
        [JsonPropertyName("specification")]
        [XmlElement("specification")]
        public ObjectSpecDto Specification { get; set; }

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
