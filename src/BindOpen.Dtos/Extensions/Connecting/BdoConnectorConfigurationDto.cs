using BindOpen.Runtime.Definition;
using System.Xml.Serialization;

namespace BindOpen.Extensions.Connecting
{
    /// <summary>
    /// This class represents a connector configuration DTO.
    /// </summary>
    [XmlType("ConnectorConfiguration", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot(ElementName = "connector", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class BdoConnectorConfigurationDto
        : TBdoExtensionItemConfigurationDto<BdoConnectorDefinitionDto>
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoConnectorConfigurationDto class.
        /// </summary>
        public BdoConnectorConfigurationDto()
        {
        }

        #endregion
    }
}
