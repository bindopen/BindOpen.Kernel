using BindOpen.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Scoping
{
    /// <summary>
    /// This class represents the definition of a library.
    /// </summary>
    [XmlType("PackageDefinition", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    [XmlRoot(ElementName = "extension", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel", IsNullable = false)]
    public class PackageDefinitionDto : IBdoDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The expression of this instance.
        /// </summary>
        [Key]
        [Column("PackageDefinitionId")]
        [JsonPropertyName("id")]
        [XmlElement("id")]
        public string Identifier { get; set; }

        /// <summary>
        /// Description of this instance.
        /// </summary>
        [ForeignKey(nameof(DescriptionStringDictionaryId))]
        [JsonPropertyName("description")]
        [XmlElement("description")]
        public StringDictionaryDto Description { get; set; }

        /// <summary>
        /// The class name of this instance.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public string DescriptionStringDictionaryId { get; set; }

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
        [ForeignKey(nameof(ItemIndexFullNameDictionaryId))]
        [JsonPropertyName("indexes")]
        [XmlElement("indexes")]
        public StringDictionaryDto ItemIndexFullNameDictionary { get; set; }

        /// <summary>
        /// The class name of this instance.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public string ItemIndexFullNameDictionaryId { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of BdoPackageDefinitionDto class.
        /// </summary>
        public PackageDefinitionDto()
        {
        }

        #endregion
    }
}
