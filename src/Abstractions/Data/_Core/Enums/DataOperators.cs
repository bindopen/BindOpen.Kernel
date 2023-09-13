using System;
using System.Xml.Serialization;

namespace BindOpen.Kernel.Data
{
    /// <summary>
    /// This enumeration represents the possible data operators.
    /// </summary>
    [Flags]
    [XmlType("DataOperators", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    public enum DataOperators
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
        /// Exists.
        /// </summary>
        Exists = 0x1 << 1,

        /// <summary>
        /// Contains.
        /// </summary>
        Contains = 0x1 << 2,

        /// <summary>
        /// Has.
        /// </summary>
        Has = 0x1 << 3,

        /// <summary>
        /// In.
        /// </summary>
        In = 0x1 << 4,

        /// <summary>
        /// Equal.
        /// </summary>
        EqualTo = 0x1 << 5,

        /// <summary>
        /// Different.
        /// </summary>
        DifferentFrom = 0x1 << 6,

        /// <summary>
        /// Lesser.
        /// </summary>
        LesserThan = 0x1 << 7,

        /// <summary>
        /// Lesser or equal.
        /// </summary>
        LesserThanOrEqualTo = LesserThan | EqualTo,

        /// <summary>
        /// Greater.
        /// </summary>
        GreaterThan = 0x1 << 8,

        /// <summary>
        /// Greater or equal.
        /// </summary>
        GreaterThanOrEqualTo = GreaterThan | EqualTo,

        /// <summary>
        /// Any operator.
        /// </summary>
        Any = Exists | Contains | Has | In | EqualTo | DifferentFrom | LesserThan | LesserThanOrEqualTo | GreaterThan | GreaterThanOrEqualTo
    }
}
