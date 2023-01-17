using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace BindOpen.MetaData.Elements
{
    /// <summary>
    /// This class represents a object element that is an element whose items are entities.
    /// </summary>
    [XmlType("MetaObject", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot(ElementName = "object", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class MetaObjectDto : MetaElementDto
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
        [JsonPropertyName("specs")]
        [XmlArray("specs")]
        [XmlArrayItem("spec")]
        public List<MetaObjectSpecDto> Specs { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ObjectElementDto class.
        /// </summary>
        public MetaObjectDto()
        {
        }

        #endregion
    }
}
