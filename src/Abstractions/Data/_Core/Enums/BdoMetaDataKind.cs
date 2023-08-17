using System.Xml.Serialization;

namespace BindOpen.System.Data
{
    /// <summary>
    /// This enumeration represents the meta data kind.
    /// </summary>
    [XmlType("BdoMetaDataKind", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
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
        /// Object.
        /// </summary>
        Object,

        /// <summary>
        /// Scalar.
        /// </summary>
        Scalar,

        /// <summary>
        /// Collection.
        /// </summary>
        Collection,

        /// <summary>
        /// Set.
        /// </summary>
        Set
    }
}
