using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Extensions.Items.Scriptwords.Definition;
using BindOpen.Framework.Core.Extensions.Items.Scriptwords.Definition.Dto;

namespace BindOpen.Framework.Core.Extensions.Indexes.Scriptwords
{
    /// <summary>
    /// This class represents a DTO script word index.
    /// </summary>
    [Serializable()]
    [XmlType("ScriptwordIndexDto", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "scriptWords.index", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class ScriptwordIndexDto : TAppExtensionItemIndexDto<ScriptwordDefinitionDto>, IScriptwordIndexDto
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
        /// Instantiates a new instance of the ScriptwordIndex class.
        /// </summary>
        public ScriptwordIndexDto()
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
        public IScriptwordDefinitionDto GetDefinition(
            string definitionName,
            string methodName,
            IScriptwordDefinitionDto parent = null)
        {
            List<ScriptwordDefinitionDto> definitionDtos = parent == null ? Definitions : parent.Children;

            foreach (ScriptwordDefinitionDto childDefinitionDto in definitionDtos)
            {
                if ((!string.IsNullOrEmpty(definitionName) && childDefinitionDto.Name.KeyEquals(definitionName))
                    || (string.IsNullOrEmpty(definitionName) && childDefinitionDto.RuntimeFunctionName.KeyEquals(methodName)))
                {
                    return childDefinitionDto;
                }
                else
                {
                    IScriptwordDefinitionDto definitionDto;
                    if ((definitionDto = GetDefinition(definitionName, methodName, childDefinitionDto))!=null)
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
