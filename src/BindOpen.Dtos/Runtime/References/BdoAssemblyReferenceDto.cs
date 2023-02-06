using BindOpen.Data;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Runtime.References
{
    /// <summary>
    /// This class represents the assembly reference DTO.
    /// </summary>
    [XmlType("AssemblyReference", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot(ElementName = "assembly.reference", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class BdoAssemblyReferenceDto : IDto
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The library name of this instance.
        /// </summary>
        [JsonPropertyName("name")]
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// The library version of this instance.
        /// </summary>
        [JsonPropertyName("version")]
        [XmlAttribute("version")]
        public string Version { get; set; }

        /// <summary>
        /// The file name of this instance.
        /// </summary>
        [JsonPropertyName("fileName")]
        [XmlAttribute("fileName")]
        public string FileName { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoAssemblyReferenceDto class.
        /// </summary>
        public BdoAssemblyReferenceDto()
        {
        }

        #endregion
    }
}
