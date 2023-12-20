using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data.Assemblies
{
    /// <summary>
    /// This class represents an assembly reference DTO.
    /// </summary>
    [XmlType("AssemblyReference", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    [XmlRoot(ElementName = "assembly", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel", IsNullable = false)]
    public class AssemblyReferenceDto : IBdoDto
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The assembly name of this instance.
        /// </summary>
        [JsonPropertyName("assemblyName")]
        [XmlElement("assemblyName")]
        public string AssemblyName { get; set; }

        /// <summary>
        /// The assembly version of this instance.
        /// </summary>
        [JsonPropertyName("assemblyVersion")]
        [XmlElement("assemblyVersion")]
        public string AssemblyVersion { get; set; }

        /// <summary>
        /// The assembly file name of this instance.
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
        /// Instantiates a new instance of the AssemblyReferenceDto class.
        /// </summary>
        public AssemblyReferenceDto()
        {
        }

        #endregion
    }
}
