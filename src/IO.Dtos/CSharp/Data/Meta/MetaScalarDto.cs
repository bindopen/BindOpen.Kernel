using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a scalar element that is an element whose items are scalars.
    /// </summary>
    [XmlType("MetaScalar", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    [XmlRoot(ElementName = "scalar", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel", IsNullable = false)]
    public class MetaScalarDto : MetaDataDto
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The value of this instance.
        /// </summary>
        [NotMapped]
        [JsonPropertyName("item")]
        [XmlText()]
        public string Item { get; set; }

        /// <summary>
        /// The values of this instance.
        /// </summary>
        [JsonPropertyName("items")]
        [XmlArray("items")]
        [XmlArrayItem("add")]
        public List<string> Items { get; set; }

        /// <summary>
        /// Indicates whether the entities property must be ignored.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public bool ItemsSpecified => Items?.Count > 0;

        [ForeignKey(nameof(Identifier))]
        [JsonIgnore]
        [XmlIgnore]
        public MetaDataDto MetaData { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the MetaScalarDto class.
        /// </summary>
        public MetaScalarDto()
        {
        }

        #endregion
    }
}
