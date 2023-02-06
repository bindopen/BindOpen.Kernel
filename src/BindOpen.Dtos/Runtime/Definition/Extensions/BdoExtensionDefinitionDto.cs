using BindOpen.Data;
using BindOpen.Data.Items;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// This class represents the definition of a library.
    /// </summary>
    [XmlType("ExtensionDefinition", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot(ElementName = "extension", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class BdoExtensionDefinitionDto : DescribedDataItemDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Name of the group of this instance.
        /// </summary>
        [JsonPropertyName("groupName")]
        [XmlElement("groupName")]
        public string GroupName { get; set; }

        /// <summary>
        /// Name of the assembly where the library can be loaded.
        /// </summary>
        [JsonPropertyName("assembly")]
        [XmlElement("assembly")]
        public string AssemblyName { get; set; }

        /// <summary>
        /// Root name space of this intance.
        /// </summary>
        [JsonPropertyName("rootNamespace")]
        [XmlElement("rootNamespace")]
        public string RootNamespace { get; set; }

        // Files -------------------------------------

        /// <summary>
        /// File name of this instance.
        /// </summary>
        [JsonPropertyName("fileName")]
        [XmlElement("fileName")]
        public string FileName { get; set; }

        /// <summary>
        /// Names of the using assembly files of this instance.
        /// </summary>
        [JsonPropertyName("using")]
        [XmlArray("using")]
        [XmlArrayItem("add")]
        public List<string> UsingAssemblyFileNames { get; set; }

        // Dictionary full names -------------------------------------

        /// <summary>
        /// Dictionary full names of this instance.
        /// </summary>
        [JsonPropertyName("indexes")]
        [XmlElement("indexes")]
        public DictionaryDto ItemIndexFullNameDictionary { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of BdoExtensionDefinitionDto class.
        /// </summary>
        public BdoExtensionDefinitionDto()
        {
        }

        #endregion
    }
}
