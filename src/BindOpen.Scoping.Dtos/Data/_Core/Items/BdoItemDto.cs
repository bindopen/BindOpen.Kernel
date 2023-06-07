using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Scoping.Data
{
    /// <summary>
    /// This class represents a catalog element that is an element whose elements are carriers.
    /// </summary>
    [XmlType("BdoItem", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    [XmlInclude(typeof(DatasourceDto))]
    [XmlInclude(typeof(DictionaryDto))]
    [XmlInclude(typeof(ExpressionDto))]
    [XmlInclude(typeof(StringSetDto))]

    [JsonDerivedType(typeof(DatasourceDto), "source")]
    [JsonDerivedType(typeof(DictionaryDto), "dictonary")]
    [JsonDerivedType(typeof(ExpressionDto), "expression")]
    [JsonDerivedType(typeof(StringSetDto), "string.filter")]

    public class BdoItemDto : IDto
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
