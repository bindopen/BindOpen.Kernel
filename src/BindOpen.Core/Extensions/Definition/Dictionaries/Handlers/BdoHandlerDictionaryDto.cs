using System.Xml.Serialization;

namespace BindOpen.Extensions.Definition
{
    /// <summary>
    /// This class represents a handler index.
    /// </summary>
    [XmlType("BdoHandlerDictionary", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "handlers.dictionary", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class BdoHandlerDictionaryDto : TBdoExtensionDictionaryDto<BdoHandlerDefinitionDto>, IBdoHandlerDictionaryDto
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoHandlerDictionaryDto class.
        /// </summary>
        public BdoHandlerDictionaryDto()
        {
        }

        #endregion
    }
}
