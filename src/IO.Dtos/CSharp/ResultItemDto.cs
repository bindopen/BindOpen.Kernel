using System.Text.Json.Serialization;
using System.Xml.Serialization;
using BindOpen.Kernel.Abstractions.Data._Core.Enums;

namespace BindOpen.Kernel
{
    /// <summary>
    /// This interface represents a conditional object.
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
        /// Initializes a new instance of the BdoResultItemDto class.
        /// </summary>
        public ResultItemDto()
        {
        }

        #endregion
    }
}