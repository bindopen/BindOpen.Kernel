﻿using BindOpen.Data.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This class represents a data source.
    /// </summary>
    [XmlType("Datasource", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot(ElementName = "datasource", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class DatasourceDto : IdentifiedNamedDataItemDto
    {
        // -----------------------------------------------
        // PROPERTIES
        // ----------------------------------------------

        #region Properties

        /// <summary>
        /// The config items for this instance.
        /// </summary>
        [JsonPropertyName("config")]
        [XmlArray("config")]
        [XmlArrayItem("add")]
        public List<BdoConfigurationDto> Configurations { get; set; }

        /// <summary>
        /// Kind of the data module of this instance. 
        /// </summary>
        [JsonPropertyName("kind")]
        [XmlAttribute("kind")]
        [DefaultValue(DatasourceKind.Any)]
        public DatasourceKind Kind { get; set; } = DatasourceKind.Any;

        /// <summary>
        /// The instance name of this instance.
        /// </summary>
        [JsonPropertyName("instanceName")]
        [XmlAttribute("instanceName")]
        public string InstanceName { get; set; }

        /// <summary>
        /// Indicates whether this instance is default.
        /// </summary>
        [JsonPropertyName("isDefault")]
        [XmlAttribute("isDefault")]
        [DefaultValue(false)]
        public bool IsDefault { get; set; } = false;

        /// <summary>
        /// The module name of this instance.
        /// </summary>
        [JsonPropertyName("moduleName")]
        [XmlAttribute("moduleName")]
        public string ModuleName { get; set; }

        #endregion

        // -----------------------------------------------
        // CONSTRUCTORS
        // -----------------------------------------------

        #region Constructors

        /// <summary>
        /// This instantiates a new instance of the DatasourceDto class.
        /// </summary>
        public DatasourceDto()
        {
        }

        #endregion
    }
}