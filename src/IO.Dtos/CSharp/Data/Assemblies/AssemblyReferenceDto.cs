using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Kernel.Data.Assemblies
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
        /// The library name of this instance.
        /// </summary>
        [JsonPropertyName("assemblyName")]
        [XmlElement("assemblyName")]
        public string AssemblyName { get; set; }

        /// <summary>
        /// The library version of this instance.
        /// </summary>
        [JsonPropertyName("assemblyVersion")]
        [XmlElement("assemblyVersion")]
        public string AssemblyVersion { get; set; }

        /// <summary>
        /// The file name of this instance.
        /// </summary>
        [JsonPropertyName("assemblyFileName")]
        [XmlElement("assemblyFileName")]
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
