using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.System.Data
{
    /// <summary>
    /// This class represents a catalog element that is an element whose elements are carriers.
    /// </summary>
    [XmlType("item", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    [XmlInclude(typeof(DatasourceDto))]
    [XmlInclude(typeof(DictionaryDto))]
    [XmlInclude(typeof(ExpressionDto))]
    [XmlInclude(typeof(FilterDto))]

    [JsonDerivedType(typeof(DatasourceDto), "source")]
    [JsonDerivedType(typeof(DictionaryDto), "dictonary")]
    [JsonDerivedType(typeof(ExpressionDto), "expression")]
    [JsonDerivedType(typeof(FilterDto), "string.filter")]

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
