using System;
using System.Xml.Serialization;

namespace BindOpen.Data;

/// <summary>
/// This enumerates the possible data value types.
/// </summary>
[Flags]
[XmlType("DataValueTypes", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
public enum DataValueTypes
{
    /// <summary>
    /// None.
    /// </summary>
    None = 0,

    /// <summary>
    /// Any.
    /// </summary>
    Any = 1,

    /// <summary>
    /// Text.
    /// </summary>
    MetaData = 2,

    /// <summary>
    /// Script word.
    /// </summary>
    Scriptword = 3,

    /// <summary>
    /// Script word.
    /// </summary>
    Reference = 4,

    /// <summary>
    /// Boolean.
    /// </summary>
    Boolean = 5,

    /// <summary>
    /// Binary.
    /// </summary>
    Binary = 6,

    /// <summary>
    /// Connector.
    /// </summary>
    Connector = 7,

    /// <summary>
    /// Date.
    /// </summary>
    Date = 8,

    /// <summary>
    /// Document.
    /// </summary>
    Document = 9,

    /// <summary>
    /// Data entity.
    /// </summary>
    Entity = 10,

    /// <summary>
    /// Integer.
    /// </summary>
    Integer = 11,

    /// <summary>
    /// Long.
    /// </summary>
    Long = 12,

    /// <summary>
    /// Ultra long.
    /// </summary>
    ULong = 13,

    /// <summary>
    /// Number value.
    /// </summary>
    Number = 14,

    /// <summary>
    /// Object.
    /// </summary>
    Object = 15,

    /// <summary>
    /// Text.
    /// </summary>
    Task = 16,

    /// <summary>
    /// Text.
    /// </summary>
    Text = 17,

    /// <summary>
    /// Time.
    /// </summary>
    Time = 18,

    /// <summary>
    /// Null
    /// </summary>
    Null = 19
}
