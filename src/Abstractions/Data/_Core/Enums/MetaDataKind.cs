using System;
using System.Xml.Serialization;

namespace BindOpen.Data
{
    /// <summary>
    /// This enumeration represents the meta data kinds.
    /// </summary>
    [Flags]
    [XmlType("MetaDataKind", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    public enum MetaDataKind
    {
        /// <summary>
        /// None.
        /// </summary>
        None = 0,

        /// <summary>
        /// Any.
        /// </summary>
        Any = Scalar | Node,

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
        Object = 0x01 << 1,

        /// <summary>
        /// Scriptword.
        /// </summary>
        Scriptword = 0x01 << 1
    }
}
