using BindOpen.Scoping.Script;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a meta set DTO.
    /// </summary>
    [XmlType("MetaSet", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    [XmlRoot("set", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel", IsNullable = false)]
    public class MetaSetDto : MetaDataDto, IBdoDto
    {
        // -------------------------------------------------------
        // PROPERTIES
        // -------------------------------------------------------

        #region Properties

        /// <summary>
        /// The elements of this instance.
        /// </summary>
        [JsonPropertyName("items")]
        [XmlElement("node", Type = typeof(MetaSetDto))]
        [XmlElement("object", Type = typeof(MetaObjectDto))]
        [XmlElement("scalar", Type = typeof(MetaScalarDto))]
        [XmlElement("word", Type = typeof(ScriptwordDto))]
        public List<MetaDataDto> MetaItems { get; set; }

        /// <summary>
        /// Indicates whether the entities property must be ignored.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public bool MetaItemsSpecified => MetaItems?.Count > 0;

        #endregion

        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the MetaSetDto class.
        /// </summary>
        public MetaSetDto() : base()
        {
        }

        #endregion
    }
}
