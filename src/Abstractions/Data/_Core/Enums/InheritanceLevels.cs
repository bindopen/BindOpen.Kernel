using System;
using System.Xml.Serialization;

namespace BindOpen.Data;

/// <summary>
/// This enumerates the possible levels of inheritance.
/// </summary>
[XmlType("InheritanceLevels", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
[Flags]
public enum InheritanceLevels
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
    /// Heritable.
    /// </summary>
    Heritable = 0x01 << 1,

    /// <summary>
    /// Inherited.
    /// </summary>
    Inherited = 0x01 << 2,

    /// <summary>
    /// Any inherintance level.
    /// </summary>
    Any = Heritable | Inherited
}
