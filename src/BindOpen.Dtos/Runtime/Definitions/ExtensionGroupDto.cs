﻿using BindOpen.Data;
using BindOpen.Data.Items;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Runtime.Definitions
{
    /// <summary>
    /// This class represents the group of BindOpen extension items.
    /// </summary>
    [XmlType("ExtensionGroup", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot(ElementName = "extension.item.group", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class ExtensionGroupDto : IDto
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
        public DictionaryDto Description { get; set; }

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