using BindOpen.Data;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Kernel
{
    /// <summary>
    /// This interface represents a result item DTO.
    /// </summary>
    public class ResultItemDto
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
        /// Initializes a new instance of the ResultItemDto class.
        /// </summary>
        public ResultItemDto()
        {
        }

        #endregion
    }
}