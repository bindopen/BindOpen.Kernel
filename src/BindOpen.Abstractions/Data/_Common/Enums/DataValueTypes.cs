using System;
using System.Xml.Serialization;

namespace BindOpen.Data
{
    /// <summary>
    /// This enumeration represents the possible data value types.
    /// </summary>
    [Flags]
    [XmlType("DataValueTypes", Namespace = "https://xsd.bindopen.org")]
    public enum DataValueTypes
    {
        /// <summary>
        /// None.
        /// </summary>
        None = 0,

        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined = 0x01 << 0,

        /// <summary>
        /// Boolean.
        /// </summary>
        Boolean = 0x01 << 1,

        /// <summary>
        /// Byte array.
        /// </summary>
        ByteArray = 0x01 << 2,

        /// <summary>
        /// Connector.
        /// </summary>
        Connector = 0x01 << 3,

        /// <summary>
        /// Date.
        /// </summary>
        Date = 0x01 << 4,

        /// <summary>
        /// Document.
        /// </summary>
        Document = 0x01 << 5,

        /// <summary>
        /// Data entity.
        /// </summary>
        Entity = 0x01 << 6,

        /// <summary>
        /// Integer.
        /// </summary>
        Integer = 0x01 << 7,

        /// <summary>
        /// Long.
        /// </summary>
        Long = 0x01 << 8,

        /// <summary>
        /// Ultra long.
        /// </summary>
        ULong = 0x01 << 9,

        /// <summary>
        /// Number value.
        /// </summary>
        Number = 0x01 << 10,

        /// <summary>
        /// Object.
        /// </summary>
        Object = 0x01 << 11,

        /// <summary>
        /// Text.
        /// </summary>
        Task = 0x01 << 12,

        /// <summary>
        /// Text.
        /// </summary>
        Text = 0x01 << 13,

        /// <summary>
        /// Time.
        /// </summary>
        Time = 0x01 << 14,

        /// <summary>
        /// Text.
        /// </summary>
        MetaData = 0x01 << 15,

        /// <summary>
        /// Any data value type.
        /// </summary>
        Any = Boolean | Entity | Entity | Date | Document | Integer | Long
            | ULong | ByteArray | Number | Object | Text | Time
    }
}
