using BindOpen.Script;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a data exp that can contain a literal and script texts.
    /// </summary>
    [XmlType("Reference", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    [XmlRoot(ElementName = "reference", Namespace = "https://storage.bindopen.org/xsd/bindopen", IsNullable = false)]
    public class ReferenceDto : ExpressionDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The script word of this instance.
        /// </summary>
        [JsonPropertyName("word")]
        [XmlElement("word")]
        public ScriptwordDto Word { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of DataReferenceDto class.
        /// </summary>
        public ReferenceDto()
        {
        }

        #endregion
    }
}