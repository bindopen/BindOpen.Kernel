using System.Xml.Serialization;

namespace BindOpen.Data.Schema;

/// <summary>
/// This enumerates the possible schema rule modes.
/// </summary>
[XmlType("SchemaRuleKind", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
public enum BdoSchemaRuleKinds
{
    /// <summary>
    /// None.
    /// </summary>
    None = 0,

    /// <summary>
    /// Any.
    /// </summary>
    Any = Requirement | Constraint,

    /// <summary>
    /// Requirement.
    /// </summary>
    Requirement = 1,

    /// <summary>
    /// Constraint.
    /// </summary>
    Constraint = 2
};
