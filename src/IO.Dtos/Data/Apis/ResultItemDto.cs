using BindOpen.System.Data;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.System.Data.Apis
{
    /// <summary>
    /// This class represents a DTO item result.
    /// </summary>
    [XmlType("ResultItemDto", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    [XmlRoot("resultItemDto", Namespace = "https://storage.bindopen.org/xsd/bindopen", IsNullable = false)]
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
        public ResultItemDto() : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ResultItemDto class.
        /// </summary>
        /// <param key="key">The key to consider.</param>
        /// <param key="status">The status to consider.</param>
        public ResultItemDto(string key, ResourceStatus status = ResourceStatus.None)
        {
            Key = key;
            Status = status;
        }

        #endregion
    }
}