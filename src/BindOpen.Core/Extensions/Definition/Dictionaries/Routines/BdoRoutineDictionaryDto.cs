using BindOpen.Extensions.Definition;
using System;
using System.Xml.Serialization;

namespace BindOpen.Extensions.Definition
{
    /// <summary>
    /// This class represents a DTO routine dictionary.
    /// </summary>
    [Serializable()]
    [XmlType("BdoRoutineDictionary", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "routines.dictionary", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
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
