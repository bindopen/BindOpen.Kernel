using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Extensions.Definition.Scriptwords;

namespace BindOpen.Framework.Core.Extensions.Indexes.Scriptwords
{
    /// <summary>
    /// This class represents a DTO script word index.
    /// </summary>
    [Serializable()]
    [XmlType("ScriptwordIndexDto", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "scriptWords.index", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class ScriptwordIndexDto : TAppExtensionItemIndexDto<IScriptwordDefinitionDto>, IScriptwordIndexDto
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
    }
}
