using System.Xml.Serialization;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This enumeration represents the meta data kind.
    /// </summary>
    [XmlType("BdoMetaDataKind", Namespace = "https://xsd.bindopen.org")]
    public enum BdoMetaDataKind
    {
        /// <summary>
        /// None.
        /// </summary>
        None,

        /// <summary>
        /// Any.
        /// </summary>
        Any,

        /// <summary>
        /// Entity.
        /// </summary>
        Entity,

        /// <summary>
        /// Document.
        /// </summary>
        Document,

        /// <summary>
        /// Object.
        /// </summary>
        Object,

        /// <summary>
        /// Scalar.
        /// </summary>
        Scalar,

        /// <summary>
        /// Source.
        /// </summary>
        Source
    }
}
