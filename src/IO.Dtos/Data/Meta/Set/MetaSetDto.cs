using BindOpen.System.Scoping.Script;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    [XmlType("MetaSet", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    [XmlRoot(ElementName = "set", Namespace = "https://storage.bindopen.org/xsd/bindopen", IsNullable = false)]
    public class MetaSetDto : MetaDataDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The elements of this instance.
        /// </summary>
        [JsonPropertyName("items")]
        [XmlElement("set", Type = typeof(MetaSetDto))]
        [XmlElement("object", Type = typeof(MetaObjectDto))]
        [XmlElement("scalar", Type = typeof(MetaScalarDto))]
        [XmlElement("scriptword", Type = typeof(ScriptwordDto))]
        public List<(string, MetaDataDto)> MetaItems { get; set; }

        /// <summary>
        /// Indicates whether the entities property must be ignored.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public bool MetaItemsSpecified => MetaItems?.Count > 0;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the MetaSetDto class.
        /// </summary>
        public MetaSetDto()
        {
        }

        #endregion
    }
}

