using System.Xml.Serialization;

namespace BindOpen.Data.Common
{
    /// <summary>
    /// This class represents a DTO item result.
    /// </summary>
    [XmlType("ResultItemDto", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot("resultItemDto", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class ResultItemDto
    {
        /// <summary>
        /// The key of this instance.
        /// </summary>
        [XmlElement("key")]
        public string Key { get; set; }

        /// <summary>
        /// The status of this instance.
        /// </summary>
        [XmlElement("status")]
        public ResourceStatus Status { get; set; }

        /// <summary>
        /// Initializes a new instance of the ResultItemDto class.
        /// </summary>
        public ResultItemDto() : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ResultItemDto class.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="status">The status to consider.</param>
        public ResultItemDto(string key, ResourceStatus status = ResourceStatus.None)
        {
            Key = key;
            Status = status;
        }
    }
}