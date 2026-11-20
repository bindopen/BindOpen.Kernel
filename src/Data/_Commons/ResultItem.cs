using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data
{
    /// <summary>
    /// This interface defines a conditional object.
    /// </summary>
    public class ResultItem : IResultItem
    {
        // -------------------------------------------------------
        // PROPERTIES
        // -------------------------------------------------------

        #region Properties

        /// <summary>
        /// The key of this instance.
        /// </summary>
        [JsonPropertyName("key")]
        [XmlElement("key")]
        public string Key { get; set; }

        /// <summary>
        /// The status of this instance.
        /// </summary>
        [JsonPropertyName("status")]
        [XmlElement("status")]
        public ResourceStatus Status { get; set; }

        #endregion

        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the BdoResultItem class.
        /// </summary>
        public ResultItem()
        {
        }

        #endregion
    }
}