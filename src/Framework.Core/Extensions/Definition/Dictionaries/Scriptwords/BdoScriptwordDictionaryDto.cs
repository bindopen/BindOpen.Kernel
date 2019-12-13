using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Extensions.Definition.Items;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Extensions.Definition.Dictionaries
{
    /// <summary>
    /// This class represents a DTO script word dictionary.
    /// </summary>
    [XmlType("BdoScriptwordDictionaryDto", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "scriptWords.dictionary", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class BdoScriptwordDictionaryDto : TBdoExtensionDictionaryDto<BdoScriptwordDefinitionDto>, IBdoScriptwordDictionaryDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The definition class of this instance.
        /// </summary>
        [XmlElement("definitionClass")]
        public string DefinitionClass { get; set; } = "";

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoScriptwordDictionary class.
        /// </summary>
        public BdoScriptwordDictionaryDto()
        {
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Gets the specified definition.
        /// </summary>
        /// <param name="definitionName">The defintion name to consider.</param>
        /// <param name="methodName">The name of the method to consider.</param>
        /// <param name="parent">The parent to consider.</param>
        /// <returns></returns>
        public IBdoScriptwordDefinitionDto GetDefinition(
            string definitionName,
            string methodName,
            IBdoScriptwordDefinitionDto parent = null)
        {
            List<BdoScriptwordDefinitionDto> definitionDtos = parent == null ? Definitions : parent.Children;

            foreach (BdoScriptwordDefinitionDto childDefinitionDto in definitionDtos)
            {
                if ((!string.IsNullOrEmpty(definitionName) && childDefinitionDto.Name.KeyEquals(definitionName))
                    || (string.IsNullOrEmpty(definitionName) && childDefinitionDto.RuntimeFunctionName.KeyEquals(methodName)))
                {
                    return childDefinitionDto;
                }
                else
                {
                    IBdoScriptwordDefinitionDto definitionDto;
                    if ((definitionDto = GetDefinition(definitionName, methodName, childDefinitionDto)) != null)
                    {
                        return definitionDto;
                    }
                }
            }

            return null;
        }

        #endregion
    }
}
