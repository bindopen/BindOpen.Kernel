using BindOpen.Data.Elements;
using BindOpen.Runtime.Definition;
using System.Xml.Serialization;
using System.Text.Json.Serialization;
using BindOpen.Runtime.Dtos.MetaData.Elements.Configuration;
using BindOpen.Runtime.Dtos.Extensions;

namespace BindOpen.Extensions.Processing
{
    /// <summary>
    /// This class represents a task configuration.
    /// </summary>
    [XmlType("TaskConfiguration", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "task", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
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
        public BdoElementSetDto InputDetail { get; set; }


        /// <summary>
        /// Output detail of this instance.
        /// </summary>
        [JsonPropertyName("outputs")]
        [XmlElement("outputs")]
        public BdoElementSetDto OutputDetail { get; set; }

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