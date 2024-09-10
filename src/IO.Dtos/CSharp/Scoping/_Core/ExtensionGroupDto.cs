using BindOpen.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Scoping
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
        /// The expression of this instance.
        /// </summary>
        [Key]
        [Column("ExtensionGroupId")]
        [JsonPropertyName("id")]
        [XmlElement("id")]
        public string Identifier { get; set; }

        /// <summary>
        /// Description of this instance.
        /// </summary>
        [ForeignKey(nameof(DescriptionStringDictionaryId))]
        [JsonPropertyName("description")]
        [XmlElement("description")]
        public StringDictionaryDto Description { get; set; }

        /// <summary>
        /// The class name of this instance.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public string DescriptionStringDictionaryId { get; set; }

        /// <summary>
        /// Sub groups of this instance.
        /// </summary>
        [JsonPropertyName("subGroups")]
        [XmlArray("subGroups")]
        [XmlArrayItem("subGroup")]
        public List<ExtensionGroupDto> SubGroups { get; set; }

        [JsonIgnore()]
        [XmlIgnore()]
        public List<ExtensionGroupDto> Supers { get; set; }

        [NotMapped]
        [JsonIgnore()]
        [XmlIgnore()]
        public List<ExtensionDefinitionDto> Definitions { get; set; }

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