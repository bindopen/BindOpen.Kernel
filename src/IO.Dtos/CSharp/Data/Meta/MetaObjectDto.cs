using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a catalog element that is an element whose elements are carriers.
    /// </summary>
    [XmlType("MetaObject", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    [XmlRoot(ElementName = "object", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel", IsNullable = false)]
    public class MetaObjectDto : MetaNodeDto
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        //// --------------------------------------------------

        [ForeignKey(nameof(Identifier))]
        [JsonIgnore]
        [XmlIgnore]
        public MetaDataDto MetaData { get; set; }

        ///// <summary>
        ///// The value of this instance.
        ///// </summary>
        //[ForeignKey(nameof(StringDictionaryItemId))]
        //[JsonPropertyName("item.dictionary")]
        //[XmlElement("item.dictionary", Type = typeof(StringDictionaryDto))]
        //public StringDictionaryDto StringDictionaryItem { get; set; }

        ///// <summary>
        ///// The class name of this instance.
        ///// </summary>
        //[JsonIgnore]
        //[XmlIgnore]
        //public string StringDictionaryItemId { get; set; }

        ///// <summary>
        ///// The value of this instance.
        ///// </summary>
        //[ForeignKey(nameof(ExpressionItemId))]
        //[JsonPropertyName("item.expression")]
        //[XmlElement("item.expression", Type = typeof(ExpressionDto))]
        //public StringDictionaryDto ExpressionItem { get; set; }

        ///// <summary>
        ///// The class name of this instance.
        ///// </summary>
        //[JsonIgnore]
        //[XmlIgnore]
        //public string ExpressionItemId { get; set; }

        ///// <summary>
        ///// The value of this instance.
        ///// </summary>
        //[ForeignKey(nameof(StringDictionaryItemId))]
        //[JsonPropertyName("item.dictionary")]
        //[XmlElement("item.dictionary", Type = typeof(StringDictionaryDto))]
        //public StringDictionaryDto StringDictionaryItem { get; set; }

        ///// <summary>
        ///// The class name of this instance.
        ///// </summary>
        //[JsonIgnore]
        //[XmlIgnore]
        //public string StringDictionaryItemId { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the MetaObjectDto class.
        /// </summary>
        public MetaObjectDto()
        {
        }

        #endregion
    }
}
