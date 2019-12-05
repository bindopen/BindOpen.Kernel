using BindOpen.Framework.Core.Extensions.Definition.Items;
using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Extensions.Definition.Dictionaries
{
    /// <summary>
    /// This class represents a handler index.
    /// </summary>
    [Serializable()]
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
