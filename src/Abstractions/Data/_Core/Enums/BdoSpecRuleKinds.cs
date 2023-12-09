using System.Xml.Serialization;

namespace BindOpen.Kernel.Data
{
    /// <summary>
    /// This enumeration lists the possible specification rule modes.
    /// </summary>
    [XmlType("SpecRuleKind", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    public enum BdoSpecRuleKinds
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

}
