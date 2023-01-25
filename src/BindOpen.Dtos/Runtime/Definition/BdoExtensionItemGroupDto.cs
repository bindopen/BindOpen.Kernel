using BindOpen.Data;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// This class represents the group of BindOpen extension items.
    /// </summary>
    [XmlType("ExtensionItemGroup", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot(ElementName = "extension.item.group", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class BdoExtensionItemGroupDto : DescribedDataItemDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Sub groups of this instance.
        /// </summary>
        [JsonPropertyName("subGroups")]
        [XmlArray("subGroups")]
        [XmlArrayItem("subGroup")]
        public List<BdoExtensionItemGroupDto> SubGroups { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoExtensionItemGroupDto class.
        /// </summary>
        public BdoExtensionItemGroupDto()
        {
        }

        #endregion
    }
}