using BindOpen.Runtime.Dtos.Extensions;
using BindOpen.Runtime.Definition;
using System.Xml.Serialization;

namespace BindOpen.Extensions.Modeling
{
    /// <summary>
    /// This class represents a carrier configuration DTO.
    /// </summary>
    [XmlType("CarrierConfiguration", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot(ElementName = "carrier", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class BdoCarrierConfigurationDto
        : TBdoExtensionTitledItemConfigurationDto<BdoCarrierDefinitionDto>
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoCarrierConfigurationDto class.
        /// </summary>
        public BdoCarrierConfigurationDto()
        {
        }

        #endregion
    }
}
