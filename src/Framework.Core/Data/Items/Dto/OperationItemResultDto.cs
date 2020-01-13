using BindOpen.Framework.Data.Common;
using System.Xml.Serialization;

namespace BindOpen.Framework.Data.Items
{
    /// <summary>
    /// This class represents a DTO operation item result.
    /// </summary>
    [XmlType("OperationItemResultDto", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot("operationItemResultDto", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class OperationItemResultDto
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
        /// Initializes a new instance of the OperationItemResultDto class.
        /// </summary>
        public OperationItemResultDto() : this(null, ResourceStatus.None)
        {
        }

        /// <summary>
        /// Initializes a new instance of the OperationItemResultDto class.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="status">The status to consider.</param>
        public OperationItemResultDto(string key, ResourceStatus status)
        {
            Key = key;
            Status = status;
        }
    }
}