using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This class represents a object element that is an element whose items are entities.
    /// </summary>
    [XmlType("ObjectElement", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "object", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class ObjectElementDto : BdoElementDto
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

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
        public string DefinitionUniqueId { get; set; }

        // --------------------------------------------------

        /// <summary>
        /// Objects of this instance.
        /// </summary>
        [JsonPropertyName("items")]
        [XmlArray("items")]
        [XmlArrayItem("add")]
        public List<BdoElementSetDto> Objects { get; set; }

        // --------------------------------------------------

        /// <summary>
        /// Object element specification of this instance.
        /// </summary>
        [JsonPropertyName("specification")]
        [XmlElement("specification")]
        public ObjectElementSpecDto Specification { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ObjectElementDto class.
        /// </summary>
        public ObjectElementDto()
        {
        }

        #endregion
    }
}
