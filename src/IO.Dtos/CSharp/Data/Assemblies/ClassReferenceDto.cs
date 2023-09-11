﻿using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Kernel.Data.Assemblies
{
    /// <summary>
    /// This class represents the assembly reference DTO.
    /// </summary>
    [XmlType("ClassReference", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    [XmlRoot(ElementName = "class", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel", IsNullable = false)]
    public class ClassReferenceDto : AssemblyReferenceDto
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The library name of this instance.
        /// </summary>
        [JsonPropertyName("className")]
        [XmlElement("className")]
        public string ClassName { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ClassReferenceDto class.
        /// </summary>
        public ClassReferenceDto()
        {
        }

        #endregion
    }
}