using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// This class represents a metrics definition.
    /// </summary>
    [XmlType("MetricsDefinition", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot(ElementName = "metrics.definition", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class BdoMetricsDefinitionDto : BdoExtensionItemDefinitionDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Item class of this instance.
        /// </summary>
        [JsonPropertyName("itemClass")]
        [XmlElement("itemClass")]
        public string ItemClass { get; set; }

        /// <summary>
        /// Unit code of this instance.
        /// </summary>
        [JsonPropertyName("unitCode")]
        [XmlElement("unitCode")]
        public string UnitCode { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoMetricsDefinitionDto class. 
        /// </summary>
        public BdoMetricsDefinitionDto()
        {
        }

        #endregion
    }
}