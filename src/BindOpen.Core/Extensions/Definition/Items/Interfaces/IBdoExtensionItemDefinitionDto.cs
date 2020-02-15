using BindOpen.Data.Items;
using System.Xml.Serialization;

namespace BindOpen.Extensions.Definition
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoExtensionItemDefinitionDto : IIndexedDataItem
    {
        /// <summary>
        /// 
        /// </summary>
        [XmlElement("imageUri")]
        string ImageUri { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("isEditable")]
        bool IsEditable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("isIndexed")]
        bool IsIndexed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore()]
        string LibraryId { get; set; }
    }
}