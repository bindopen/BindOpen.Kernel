using System;
using System.Xml.Serialization;

namespace BindOpen.Data.Schema;

/// <summary>
/// Specifies the various kinds of schema rules used to validate metadata.
/// This enum allows the engine to categorize rules into structural requirements 
/// or value-based constraints.
/// </summary>
[Flags]
[XmlType("SchemaRuleKind", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
public enum BdoSchemaRuleKinds
{
    /// <summary>
    /// No rule kind specified.
    /// </summary>
    None = 0,

    /// <summary>
    /// A rule defining structural necessity (e.g., Mandatory, Forbidden, Optional).
    /// </summary>
    Requirement = 1 << 0,

    /// <summary>
    /// A rule defining value limitations (e.g., Range, Format, Reference integrity).
    /// </summary>
    Constraint = 1 << 1,

    /// <summary>
    /// Represents any combination of schema rule kinds.
    /// </summary>
    Any = Requirement | Constraint
}