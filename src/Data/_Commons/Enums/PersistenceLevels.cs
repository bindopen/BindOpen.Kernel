using System;
using System.Xml.Serialization;

namespace BindOpen.Data;

/// <summary>
/// This enumerates the possible persistence levels.
/// </summary>
[XmlType("PersistenceLevels", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
[Flags]
public enum PersistenceLevels
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
    /// The information remains alive.
    /// </summary>
    Singleton = 0x01 << 1,

    /// <summary>
    /// The information remains alive until the session ends.
    /// </summary>
    Scoped = 0x01 << 2,

    /// <summary>
    /// The information remains alive until the request ends.
    /// </summary>
    Transient = 0x01 << 3,

    /// <summary>
    /// Any the persistence level.
    /// </summary>
    Any = Singleton | Scoped | Transient
}
