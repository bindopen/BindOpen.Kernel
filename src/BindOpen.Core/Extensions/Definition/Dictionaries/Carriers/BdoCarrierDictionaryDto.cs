using System.Xml.Serialization;

namespace BindOpen.Extensions.Definition
{
    /// <summary>
    /// This class represents a DTO carrier dictionary.
    /// </summary>
    [XmlType("BdoCarrierDictionary", Namespace = "https://storage.bindopen.org/pgrkhpym/docs/code/xsd/bindopen")]
    [XmlRoot(ElementName = "carriers.dictionary", Namespace = "https://storage.bindopen.org/pgrkhpym/docs/code/xsd/bindopen", IsNullable = false)]
    public class BdoCarrierDictionaryDto : TBdoExtensionDictionaryDto<BdoCarrierDefinitionDto>, IBdoCarrierDictionaryDto
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoCarrierDictionaryDto class.
        /// </summary>
        public BdoCarrierDictionaryDto()
        {
        }

        #endregion
    }
}
