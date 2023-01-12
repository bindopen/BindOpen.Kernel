using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// This class represents a DTO metrics dico.
    /// </summary>
    [XmlType("MetricsDictionary", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "metrics.dico", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class BdoMetricsDictionaryDto
        : TBdoExtensionDictionaryDto<BdoMetricsDefinitionDto>
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Definitions of this instance.
        /// </summary>
        [JsonPropertyName("definitions")]
        [XmlArray("definitions")]
        [XmlArrayItem("add.definition")]
        public List<BdoMetricsDefinitionDto> Definitions { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoMetricsDictionaryDto class.
        /// </summary>
        public BdoMetricsDictionaryDto()
        {
        }

        #endregion
    }
}
