using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a BindOpen item DTO.
    /// </summary>
    [XmlType("item", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    [XmlInclude(typeof(StringDictionaryDto))]
    [XmlInclude(typeof(ExpressionDto))]
    [XmlInclude(typeof(MergerDto))]
    [XmlInclude(typeof(ReferenceDto))]

    [JsonPolymorphic(TypeDiscriminatorPropertyName = "itemKind")]
    [JsonDerivedType(typeof(StringDictionaryDto), "Dictionary")]
    [JsonDerivedType(typeof(ExpressionDto), "Expression")]
    [JsonDerivedType(typeof(MergerDto), "Merger")]
    [JsonDerivedType(typeof(ReferenceDto), "Reference")]

    public class BdoItemDto : IBdoDto
    {
        /// <summary>
        /// The expression of this instance.
        /// </summary>
        [Key]
        [Column("ItemId")]
        [JsonPropertyName("id")]
        [XmlElement("id")]
        public string Identifier { get; set; }

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the BdoItemDto class.
        /// </summary>
        public BdoItemDto()
        {
        }

        #endregion
    }
}
