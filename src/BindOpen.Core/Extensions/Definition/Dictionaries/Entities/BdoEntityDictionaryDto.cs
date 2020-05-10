using System.Xml.Serialization;

namespace BindOpen.Extensions.Definition
{
    /// <summary>
    /// This class represents a DTO entity dictionary.
    /// </summary>
    [XmlType("BdoEntityDictionary", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "entities.dictionary", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class BdoEntityDictionaryDto : TBdoExtensionDictionaryDto<BdoEntityDefinitionDto>, IBdoEntityDictionaryDto
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoEntityDictionaryDto class.
        /// </summary>
        public BdoEntityDictionaryDto()
        {
        }

        #endregion        
    }
}
