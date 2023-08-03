using BindOpen.System.Data.Meta;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.System.Data
{
    /// <summary>
    /// This class represents a data source.
    /// </summary>
    [XmlType("Datasource", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    [XmlRoot(ElementName = "datasource", Namespace = "https://storage.bindopen.org/xsd/bindopen", IsNullable = false)]
    public class DatasourceDto : BdoItemDto
    {
        // -----------------------------------------------
        // PROPERTIES
        // ----------------------------------------------

        #region Properties

        /// <summary>
        /// Identifier of this instance.
        /// </summary>
        [JsonPropertyName("id")]
        [XmlAttribute("id")]
        [DefaultValue(null)]
        public string Id { get; set; }

        /// <summary>
        /// Name of this instance.
        /// </summary>
        [JsonPropertyName("name")]
        [XmlAttribute("name")]
        [DefaultValue(null)]
        public string Name { get; set; }

        /// <summary>
        /// The config items for this instance.
        /// </summary>
        [JsonPropertyName("config")]
        [XmlElement("config")]
        public List<ConfigurationDto> Configurations { get; set; }

        /// <summary>
        /// Indicates whether the entities property must be ignored.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public bool ConfigurationsSpecified => Configurations?.Count > 0;

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
