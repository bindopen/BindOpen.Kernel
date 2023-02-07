﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data.Configuration
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    [XmlType("ConfigurationSet", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot(ElementName = "config.set", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class ConfigurationSetDto : IDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The elements of this instance.
        /// </summary>
        [JsonPropertyName("items")]
        [XmlElement("config")]
        public List<ConfigurationDto> Items { get; set; }

        /// <summary>
        /// Indicates whether the entities property must be ignored.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public bool ItemsSpecified => Items?.Count > 0;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ConfigurationSetDto class.
        /// </summary>
        public ConfigurationSetDto()
        {
        }

        #endregion
    }
}

