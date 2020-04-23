using System.Xml.Serialization;

namespace BindOpen.Extensions.Definition
{
    /// <summary>
    /// This class represents a DTO task dictionary.
    /// </summary>
    [XmlType("BdoTaskDictionary", Namespace = "https://storage.bindopen.org/pgrkhpym/docs/code/xsd/bindopen")]
    [XmlRoot(ElementName = "tasks.dictionary", Namespace = "https://storage.bindopen.org/pgrkhpym/docs/code/xsd/bindopen", IsNullable = false)]
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
