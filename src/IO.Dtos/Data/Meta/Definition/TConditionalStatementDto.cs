using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This class represents a data key value.
    /// </summary>
    //[XmlType("TConditionalStatementDto", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    [XmlRoot(ElementName = "conditional.statement", Namespace = "https://storage.bindopen.org/xsd/bindopen", IsNullable = false)]
    public class TConditionalStatementDto<T>
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The ID of this instance.
        /// </summary>
        [JsonPropertyName("id")]
        [XmlElement("id")]
        public string Id { get; set; }

        /// <summary>
        /// Values of this instance.
        /// </summary>
        [JsonPropertyName("items")]
        [XmlElement("add")]
        public List<TConditionalStatementPairDto<T>> Items { get; set; }


        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of TConditionalStatementDto class.
        /// </summary>
        public TConditionalStatementDto()
        {
        }

        #endregion
    }
}
