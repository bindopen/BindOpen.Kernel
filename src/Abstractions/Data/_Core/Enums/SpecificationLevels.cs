using System;
using System.Xml.Serialization;

namespace BindOpen.Kernel.Abstractions.Data._Core.Enums
{
    /// <summary>
    /// This enumeration represents the possible levels of specification.
    /// </summary>
    [Flags]
    [XmlType("SpecificationLevels", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    public enum SpecificationLevels
    {
        /// <summary>
        /// None.
        /// </summary>
        None = 0,

        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined = 0x01 << 0,

        /// <summary>
        /// Definition.
        /// </summary>
        Definition = 0x01 << 1,

        /// <summary>
        /// Design.
        /// </summary>
        Design = 0x01 << 2,

        /// <summary>
        /// Configuration.
        /// </summary>
        Configuration = 0x01 << 3,

        /// <summary>
        /// Runtime.
        /// </summary>
        Runtime = 0x01 << 4,

        /// <summary>
        /// Any specification level.
        /// </summary>
        Any = Definition | Design | Configuration | Runtime
    }
}
