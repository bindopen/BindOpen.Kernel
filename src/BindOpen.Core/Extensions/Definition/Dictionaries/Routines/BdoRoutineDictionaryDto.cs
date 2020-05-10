using System.Xml.Serialization;

namespace BindOpen.Extensions.Definition
{
    /// <summary>
    /// This class represents a DTO routine dictionary.
    /// </summary>
    [XmlType("BdoRoutineDictionary", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "routines.dictionary", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
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
