using System;
using System.Xml.Serialization;

namespace BindOpen.Data.Meta;

/// <summary>
/// Specifies the various kinds of metadata supported by the BindOpen kernel.
/// This enum uses a bitmask (Flags) to allow for easy filtering and grouping.
/// </summary>
[Flags]
[XmlType("BdoMetaDataKind", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
public enum BdoMetaDataKind
{
    /// <summary>
    /// No metadata kind specified.
    /// </summary>
    None = 0,

    /// <summary>
    /// Represents a scalar metadata (a single atomic value).
    /// </summary>
    Scalar = 1 << 0,

    /// <summary>
    /// Represents a set metadata (formerly known as a Node). 
    /// It contains a collection of other metadata.
    /// </summary>
    Set = 1 << 1,

    /// <summary>
    /// Represents an object metadata. It encapsulates a complex typed object.
    /// </summary>
    Object = 1 << 2,

    /// <summary>
    /// Represents a script word metadata. It defines a functional instruction or expression.
    /// </summary>
    Scriptword = 1 << 3,

    /// <summary>
    /// Represents any combination of the supported metadata kinds.
    /// </summary>
    Any = Scalar | Set | Object | Scriptword
}