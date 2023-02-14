using BindOpen.Runtime.Dtos.Extensions;
using System.Xml.Serialization;

namespace BindOpen.Extensions.Modeling
{
    /// <summary>
    /// This class represents a carrier configuration DTO.
    /// </summary>
    [XmlType("EntityConfiguration", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "entity", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class EntityConfigurationDto : ExtensionItemConfigurationDto
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
