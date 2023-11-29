using System.Xml.Serialization;

namespace BindOpen.Kernel.Data
{
    /// <summary>
    /// This enumeration lists the possible criticalities.
    /// </summary>
    [XmlType("ConstraintMode", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    public enum BdoConstraintModes
    {
        /// <summary>
        /// None.
        /// </summary>
        None = 0,

        Any = Requirement | Rule,

        /// <summary>
        /// Element must be.
        /// </summary>
        Requirement = 1,

        /// <summary>
        /// Element must not be.
        /// </summary>
        Rule = 2
    };

}
