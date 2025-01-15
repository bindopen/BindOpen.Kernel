using System.Xml.Serialization;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This enumeration represents the possible kinds of meta data.
    /// </summary>
    [XmlType("BdoMetaDataKind", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    public enum BdoMetaDataKind
    {
        /// <summary>
        /// None.
        /// </summary>
        None = 0,

        /// <summary>
        /// Any.
        /// </summary>
        Any = Scalar | Node | Object | Scriptword,

        /// <summary>
        /// Scalar.
        /// </summary>
        Scalar = 0x01 << 0,

        /// <summary>
        /// Node.
        /// </summary>
        Node = 0x01 << 1,

        /// <summary>
        /// Object.
        /// </summary>
        Object = 0x01 << 2,

        /// <summary>
        /// Scriptword.
        /// </summary>
        Scriptword = 0x01 << 3
    }
}
