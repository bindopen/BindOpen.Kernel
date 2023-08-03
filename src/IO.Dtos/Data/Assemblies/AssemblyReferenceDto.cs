using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.System.Data.Assemblies
{
    /// <summary>
    /// This class represents the assembly reference DTO.
    /// </summary>
    [XmlType("AssemblyReference", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    [XmlRoot(ElementName = "assembly", Namespace = "https://storage.bindopen.org/xsd/bindopen", IsNullable = false)]
    public class AssemblyReferenceDto : IBdoDto
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The definition of this instance.
        /// </summary>
        [JsonPropertyName("definition")]
        [XmlElement("definition")]
        public DefinitionReferenceDto Definition { get; set; }

        /// <summary>
        /// The library name of this instance.
        /// </summary>
        [JsonPropertyName("assemblyName")]
        [XmlAttribute("assemblyName")]
        public string AssemblyName { get; set; }

        /// <summary>
        /// The library version of this instance.
        /// </summary>
        [JsonPropertyName("assemblyVersion")]
        [XmlAttribute("assemblyVersion")]
        public string AssemblyVersion { get; set; }

        /// <summary>
        /// The file name of this instance.
        /// </summary>
        [JsonPropertyName("assemblyFileName")]
        [XmlAttribute("assemblyFileName")]
        public string AssemblyFileName { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoAssemblyReferenceDto class.
        /// </summary>
        public AssemblyReferenceDto()
        {
        }

        #endregion
    }
}
