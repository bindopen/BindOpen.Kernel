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

    [JsonDerivedType(typeof(StringDictionaryDto), "dictonary")]
    [JsonDerivedType(typeof(ExpressionDto), "expression")]
    [JsonDerivedType(typeof(MergerDto), "merger")]
    [JsonDerivedType(typeof(ReferenceDto), "reference")]

    public class BdoItemDto : IBdoDto
    {
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
