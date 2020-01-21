using BindOpen.Framework.Data.Common;
using System.Xml.Serialization;

namespace BindOpen.Framework.Data.Items
{
    /// <summary>
    /// This class represents a DTO item result.
    /// </summary>
    [XmlType("ItemResultDto", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot("itemResultDto", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class ItemResultDto
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
        /// Initializes a new instance of the ItemResultDto class.
        /// </summary>
        public ItemResultDto() : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ItemResultDto class.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="status">The status to consider.</param>
        public ItemResultDto(string key, ResourceStatus status = ResourceStatus.None)
        {
            Key = key;
            Status = status;
        }
    }
}