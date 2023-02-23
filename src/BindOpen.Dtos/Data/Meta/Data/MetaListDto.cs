using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    [XmlType("MetaSet", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot(ElementName = "list", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class MetaSetDto : MetaDataDto, IIdentified
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The elements of this instance.
        /// </summary>
        [JsonPropertyName("items")]
        [XmlElement("list", Type = typeof(MetaSetDto))]
        [XmlElement("object", Type = typeof(MetaObjectDto))]
        [XmlElement("scalar", Type = typeof(MetaScalarDto))]
        public List<MetaDataDto> Items { get; set; }

        /// <summary>
        /// Indicates whether the entities property must be ignored.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public bool ElementsSpecified => Items?.Count > 0;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoElementSetDto class.
        /// </summary>
        public MetaSetDto()
        {
        }

        #endregion
    }
}

