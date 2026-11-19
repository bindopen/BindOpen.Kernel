using System.Xml.Serialization;

namespace BindOpen.Scoping;

/// <summary>
/// Specifies the visual representation or viewer type for a library item.
/// </summary>
/// <remarks>
/// This enumeration is used to map extensions to their corresponding UI editors, 
/// property grids, or visualization templates.
/// </remarks>
[XmlType("BdoExtensionViewerKind", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
public enum BdoExtensionViewerKind
{
    /// <summary>
    /// Matches any viewer type.
    /// </summary>
    Any,

    /// <summary>
    /// No viewer type specified.
    /// </summary>
    None,

    /// <summary>
    /// A viewer specialized for connection strings and connector configurations.
    /// </summary>
    Connector,

    /// <summary>
    /// A viewer for library functions, showing input parameters and return types.
    /// </summary>
    Function,

    /// <summary>
    /// A viewer for script keywords and syntax documentation.
    /// </summary>
    Scriptword,

    /// <summary>
    /// A viewer for background tasks, often showing progress tracking and logs.
    /// </summary>
    Task,

    /// <summary>
    /// A viewer for database or API queries, potentially with a SQL/NoSQL editor.
    /// </summary>
    DataQuery,

    /// <summary>
    /// A viewer for data structures, classes, and schema definitions.
    /// </summary>
    DataClass,

    /// <summary>
    /// A viewer for formatting rules, serialization settings, and data layouts.
    /// </summary>
    FormatConfiguration
}