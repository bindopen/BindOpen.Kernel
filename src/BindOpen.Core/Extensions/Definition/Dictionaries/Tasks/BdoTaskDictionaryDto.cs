using System.Xml.Serialization;

namespace BindOpen.Extensions.Definition
{
    /// <summary>
    /// This class represents a DTO task dictionary.
    /// </summary>
    [XmlType("BdoTaskDictionary", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "tasks.dictionary", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class BdoTaskDictionaryDto : TBdoExtensionDictionaryDto<BdoTaskDefinitionDto>, IBdoTaskDictionaryDto
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoTaskDictionaryDto class.
        /// </summary>
        public BdoTaskDictionaryDto()
        {
        }

        #endregion
    }
}
