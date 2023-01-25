using BindOpen.Data.Meta;
using BindOpen.Runtime.Definition;
using BindOpen.Runtime.Dtos.Extensions;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Extensions.Processing
{
    /// <summary>
    /// This class represents a task configuration.
    /// </summary>
    [XmlType("TaskConfiguration", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot(ElementName = "task", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class BdoTaskConfigurationDto
        : TBdoExtensionTitledItemConfigurationDto<BdoTaskDefinitionDto>
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Input detail of this instance.
        /// </summary>
        [JsonPropertyName("inputs")]
        [XmlElement("inputs")]
        public MetaSetDto InputDetail { get; set; }


        /// <summary>
        /// Output detail of this instance.
        /// </summary>
        [JsonPropertyName("outputs")]
        [XmlElement("outputs")]
        public MetaSetDto OutputDetail { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoTaskConfigurationDto class.
        /// </summary>
        public BdoTaskConfigurationDto()
        {
        }

        #endregion
    }
}