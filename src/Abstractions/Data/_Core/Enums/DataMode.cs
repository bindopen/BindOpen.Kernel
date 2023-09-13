using System;
using System.Xml.Serialization;

namespace BindOpen.Kernel.Data
{
    /// <summary>
    /// This enumeration represents the data element itemization modes.
    /// </summary>
    [Flags]
    [XmlType("DataMode", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    public enum DataMode
    {
        /// <summary>
        /// None.
        /// </summary>
        None = 0,

        /// <summary>
        /// Any.
        /// </summary>
        Any = Value | Reference,

        /// <summary>
        /// Value.
        /// </summary>
        Value = 0x01 << 0,

        /// <summary>
        /// Reference.
        /// </summary>
        Reference = 0x01 << 1
    }
}
