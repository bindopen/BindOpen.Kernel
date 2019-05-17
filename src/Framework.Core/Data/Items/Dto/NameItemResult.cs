using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Common;

namespace BindOpen.Framework.Core.Data.Items.Dto
{
    /// <summary>
    /// This class represents the named item result.
    /// </summary>
    [Serializable()]
    [XmlType("NameItemResult", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot("nameItemResult", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class NameItemResult
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("status")]
        public ResourceStatus Status { get; set; }

        public NameItemResult(string name, ResourceStatus status)
        {
            Name = name;
            Status = status;
        }
    }
}