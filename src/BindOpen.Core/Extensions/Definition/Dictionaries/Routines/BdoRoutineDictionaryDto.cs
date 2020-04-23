using System.Xml.Serialization;

namespace BindOpen.Extensions.Definition
{
    /// <summary>
    /// This class represents a DTO routine dictionary.
    /// </summary>
    [XmlType("BdoRoutineDictionary", Namespace = "https://storage.bindopen.org/pgrkhpym/docs/code/xsd/bindopen")]
    [XmlRoot(ElementName = "routines.dictionary", Namespace = "https://storage.bindopen.org/pgrkhpym/docs/code/xsd/bindopen", IsNullable = false)]
    public class BdoRoutineDictionaryDto : TBdoExtensionDictionaryDto<BdoRoutineDefinitionDto>, IBdoRoutineDictionaryDto
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoRoutineDictionary class.
        /// </summary>
        public BdoRoutineDictionaryDto()
        {
        }

        #endregion        
    }
}
