using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Common;

namespace BindOpen.Framework.Core.Data.Items.Dto
{
    /// <summary>
    /// This class represents the ID item result.
    /// </summary>
    [Serializable()]
    [XmlType("IdItemResult", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot("idItemResult", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class IdItemResult
    {
        [XmlElement("id")]
        public string Id { get; set; }

        [XmlElement("status")]
        public ResourceStatus Status { get; set; }

        public IdItemResult(string id, ResourceStatus status)
        {
            Id = id;
            Status = status;
        }
    }
}