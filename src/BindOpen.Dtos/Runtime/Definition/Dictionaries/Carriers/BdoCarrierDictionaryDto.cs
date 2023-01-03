using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// This class represents a DTO carrier dictionary.
    /// </summary>
    [XmlType("CarrierDictionary", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "carriers.dictionary", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class BdoCarrierDictionaryDto : 
        TBdoExtensionDictionaryDto<BdoCarrierDefinitionDto>
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
        public List<BdoCarrierDefinitionDto> Definitions { get; set; }

        #endregion

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
