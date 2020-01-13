using BindOpen.Framework.Extensions.Definition;
using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Extensions.Definition
{
    /// <summary>
    /// This class represents a DTO connector dictionary.
    /// </summary>
    [Serializable()]
    [XmlType("BdoConnectorDictionary", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "connectors.dictionary", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class BdoConnectorDictionaryDto : TBdoExtensionDictionaryDto<BdoConnectorDefinitionDto>, IBdoConnectorDictionaryDto
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoConnectorDictionaryDto class.
        /// </summary>
        public BdoConnectorDictionaryDto() : base()
        {
        }

        #endregion
    }
}
