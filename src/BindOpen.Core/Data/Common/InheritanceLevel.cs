using System.Xml.Serialization;

namespace BindOpen.Data.Common
{
    /// <summary>
    /// This enumeration represents the possible levels of inheritance.
    /// </summary>
    [XmlType("InheritanceLevel", Namespace = "https://storage.bindopen.org/pgrkhpym/docs/code/xsd/bindopen")]
    public enum InheritanceLevel
    {
        /// <summary>
        /// None.
        /// </summary>
        None,

        /// <summary>
        /// Heritable.
        /// </summary>
        Heritable,

        /// <summary>
        /// Inherited.
        /// </summary>
        Inherited
    }
}
