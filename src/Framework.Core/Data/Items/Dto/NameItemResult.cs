using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Common;

namespace BindOpen.Framework.Core.Data.Items
{
    /// <summary>
    /// This class represents the named item result.
    /// </summary>
    [Serializable()]
    [XmlType("NameItemResult", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot("nameItemResult", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class NameItemResult
    {
        /// <summary>
        /// The name of this instance.
        /// </summary>
        [XmlElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// The status of this instance.
        /// </summary>
        [XmlElement("status")]
        public ResourceStatus Status { get; set; }

        /// <summary>
        /// Initializes a new instance of the NameItemResult class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="status">The status to consider.</param>
        public NameItemResult(string name, ResourceStatus status)
        {
            Name = name;
            Status = status;
        }
    }
}