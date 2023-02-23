using BindOpen.Runtime.Dtos.Extensions;
using System.Xml.Serialization;

namespace BindOpen.Extensions.Modeling
{
    /// <summary>
    /// This class represents a carrier configuration DTO.
    /// </summary>
    [XmlType("EntityConfiguration", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot(ElementName = "entity", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class EntityConfigurationDto : ExtensionConfigurationDto
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoCarrierConfigurationDto class.
        /// </summary>
        public EntityConfigurationDto()
        {
        }

        #endregion
    }
}
