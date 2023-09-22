using BindOpen.Kernel.Data;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Kernel.Scoping
{
    /// <summary>
    /// This class represents the group of BindOpen extension items.
    /// </summary>
    [XmlType("ExtensionGroup", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    [XmlRoot(ElementName = "extension.item.group", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel", IsNullable = false)]
    public class ExtensionGroupDto : IBdoDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Description of this instance.
        /// </summary>
        [JsonPropertyName("description")]
        [XmlElement("description")]
        public StringDictionaryDto Description { get; set; }

        /// <summary>
        /// Sub groups of this instance.
        /// </summary>
        [JsonPropertyName("subGroups")]
        [XmlArray("subGroups")]
        [XmlArrayItem("subGroup")]
        public List<ExtensionGroupDto> SubGroups { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoExtensionGroupDto class.
        /// </summary>
        public ExtensionGroupDto()
        {
        }

        #endregion
    }
}