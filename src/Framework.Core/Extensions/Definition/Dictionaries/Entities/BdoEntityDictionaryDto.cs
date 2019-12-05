using BindOpen.Framework.Core.Extensions.Definition.Items;
using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Extensions.Definition.Dictionaries
{
    /// <summary>
    /// This class represents a DTO entity dictionary.
    /// </summary>
    [Serializable()]
    [XmlType("BdoEntityDictionary", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "entities.dictionary", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class BdoEntityDictionaryDto : TBdoExtensionDictionaryDto<BdoEntityDefinitionDto>, IBdoEntityDictionaryDto
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoEntityDictionaryDto class.
        /// </summary>
        public BdoEntityDictionaryDto()
        {
        }

        #endregion        
    }
}
