using System;
using System.Xml.Serialization;

namespace BindOpen.Data.Common
{
    /// <summary>
    /// This enumeration represents the possible levels of inheritance.
    /// </summary>
    [XmlType("InheritanceLevels", Namespace = "https://docs.bindopen.org/xsd")]
    [Flags]
    public enum InheritanceLevels
    {
        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// None.
        /// </summary>
        None = 0x01 << 0,

        /// <summary>
        /// Heritable.
        /// </summary>
        Heritable = 0x01 << 1,

        /// <summary>
        /// Inherited.
        /// </summary>
        Inherited = 0x01 << 2,

        /// <summary>
        /// Any.
        /// </summary>
        Any = Heritable | Inherited
    }
}
