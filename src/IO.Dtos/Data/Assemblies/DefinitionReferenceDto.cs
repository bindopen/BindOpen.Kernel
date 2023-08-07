using BindOpen.System.Scoping;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.System.Data.Assemblies
{
    /// <summary>
    /// This class represents the assembly reference DTO.
    /// </summary>
    [XmlType("DefinitionReference", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    [XmlRoot(ElementName = "definition", Namespace = "https://storage.bindopen.org/xsd/bindopen", IsNullable = false)]
    public struct DefinitionReferenceDto
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        [JsonPropertyName("kind")]
        [XmlAttribute("kind")]
        [DefaultValue(BdoExtensionKind.Any)]
        public BdoExtensionKind DefinitionExtensionKind { get; set; }

        /// <summary>
        /// Creation date of this instance.
        /// </summary>
        [JsonPropertyName("uniqueName")]
        [XmlText()]
        public string DefinitionUniqueName { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DefinitionReferenceDto class.
        /// </summary>
        public DefinitionReferenceDto()
        {
        }

        #endregion
    }
}
