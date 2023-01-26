using BindOpen.Runtime.Definition;
using System.Xml.Serialization;

namespace BindOpen.Extensions.Modeling
{
    /// <summary>
    /// This class represents a entity configuration DTO.
    /// </summary>
    [XmlType("EntityConfiguration", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot(ElementName = "entity", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class BdoEntityConfigurationDto
        : TBdoExtensionItemConfigurationDto<BdoEntityDefinitionDto>
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoEntityConfigurationDto class.
        /// </summary>
        public BdoEntityConfigurationDto()
        {
        }

        #endregion
    }
}
