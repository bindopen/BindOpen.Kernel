using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Data.Common
{
    /// <summary>
    /// This enumeration represents the possible levels of inheritance.
    /// </summary>
    [Serializable()]
    [XmlType("InheritanceLevel", Namespace = "https://bindopen.org/xsd")]
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
