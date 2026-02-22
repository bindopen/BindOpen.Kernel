using System;
using System.Xml.Serialization;

namespace BindOpen.Scoping;

/// <summary>
/// Enumerates the primary categories of library items within the BindOpen framework.
/// </summary>
/// <remarks>
/// This enumeration supports bitwise combinations, allowing for flexible filtering 
/// and discovery of different extension types.
/// </remarks>
[Flags]
[XmlType("BdoExtensionKind", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
public enum BdoExtensionKinds
{
    /// <summary>
    /// No specific extension kind.
    /// </summary>
    None = 0x0,

    /// <summary>
    /// Represents a connection to an external resource (e.g., Database, API, File System).
    /// </summary>
    Connector = 0x1 << 0,

    /// <summary>
    /// Represents a structured data object or business entity.
    /// </summary>
    Entity = 0x1 << 1,

    /// <summary>
    /// Represents a discrete script function or logic operation.
    /// </summary>
    Function = 0x1 << 2,

    /// <summary>
    /// Represents an executable unit of work or a background process.
    /// </summary>
    Task = 0x1 << 3,

    /// <summary>
    /// Represents a keyword or functional element used within BindOpen scripts.
    /// </summary>
    Scriptword = 0x1 << 4,

    /// <summary>
    /// Represents a combination of all standard extension kinds.
    /// </summary>
    Any = Connector | Entity | Function | Task | Scriptword
}