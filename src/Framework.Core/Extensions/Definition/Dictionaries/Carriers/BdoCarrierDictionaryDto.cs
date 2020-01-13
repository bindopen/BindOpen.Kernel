using BindOpen.Framework.Extensions.Definition;
using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Extensions.Definition
{
    /// <summary>
    /// This class represents a DTO carrier dictionary.
    /// </summary>
    [Serializable()]
    [XmlType("BdoCarrierDictionary", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "carriers.dictionary", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
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
